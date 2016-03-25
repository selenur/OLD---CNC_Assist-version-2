using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using CNC_Assist.PlanetCNC;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace CNC_Assist
{
    /// <summary>
    /// Класс работы с контроллером
    /// </summary>
    public static class Controller
    {
        #region Переменные

        /// <summary>
        /// Наличие установленной связи между контроллером и компьтером
        /// </summary>
        private static bool _isAvailableController;

        /// <summary>
        /// Наличие установленной связи между контроллером и программой
        /// </summary>
        private static bool _isConnectedController;

        /// <summary>
        /// Параметр за которым следит поток поиска контроллера, т.к. при закрытии основной программы, данный поток нужно завершить заранее. 
        /// TRUE - нужно завершить поток
        /// </summary>
        private static bool _exitprogramm;

        /// <summary>
        /// Для монопольного доступа записи на диск, файла логов
        /// </summary>
        private static readonly object Locker;

        /// <summary>
        /// Массив данных для посылки в контроллер, при выполнении программы из G-кодов
        /// </summary>
        private static readonly List<byte[]> DataForSend;

        /// <summary>
        /// Массив данных для посылки в контроллер, при ручном управлении
        /// </summary>
        private static readonly List<byte[]> ManualDataForSend;


        /// <summary>
        /// Массив данных для посылки в контроллер, С ВЫСШИМ ПРИОРИТЕТОМ!!!
        /// </summary>
        private static readonly List<byte[]> ImportantDataForSend;

        /// <summary>
        /// Локер для Массива данных
        /// </summary>
        static readonly object LockDataForSend = new object();

        /// <summary>
        /// Информация о параметрах контроллера
        /// </summary>
        public static volatile DeviceInfo Info = new DeviceInfo();


        /// <summary>
        /// Информация о применении смещения
        /// </summary>
        public static volatile CorrectionPos CorrectionPos = new CorrectionPos();

        /// <summary>
        /// Для контроля доступности контроллера, значение равно TRUE если шпиндель движется, в режиме простоя FALSE
        /// </summary>
        private static volatile bool _controllerIsLock;

        /// <summary>
        /// Статус выполнения обычных данных
        /// </summary>
        private static volatile ETaskStatus _taskStatus;



        #endregion

        // ИНИЦИАЛИЗАЦИЯ
        static Controller()
        {
            _isAvailableController = false;
            _isConnectedController = false;
            _exitprogramm = false;
            ImportantDataForSend = new List<byte[]>();
            DataForSend = new List<byte[]>();
            ManualDataForSend = new List<byte[]>();
            _controllerIsLock = false;
            _taskStatus = ETaskStatus.Off;

            Locker = new object();

            //запустим поток, который будет периодически проверять наличие подключенного контроллера к компьютеру
            new Thread(ThreadController).Start();
        }

        #region Свойства

        /// <summary>
        /// Свойство - показывающее о подключенном контроллере к компьютеру
        /// </summary>
        /// <value>булево - Доступен ли для использования</value>
        public static bool IsAvailableController
        {
            get { return _isAvailableController; }
        }

        /// <summary>
        /// Свойство - показывающее о подключении программы к контроллеру
        /// </summary>
        public static bool IsConnectedToController
        {
            get { return _isConnectedController; }
        }

        /// <summary>
        /// Проверка наличия связи, и незанятости контроллера
        /// </summary>
        /// <returns>булево, возможно ли посылать контроллеру задачи</returns>
        public static bool TestAllowActions
        {
            get {

            if (!IsConnectedToController) return false;

            if (_controllerIsLock) return false;

            return true;

            }

        }

        /// <summary>
        /// Свойство получения статуса
        /// </summary>
        public static ETaskStatus TaskStatus
        {
            get { return _taskStatus; }
        }

        #endregion

        #region События от контроллера

        // для посылки главному потоку сообщений, о статусе работы, отладочных сообщений
        public delegate void DeviceEventNewMessage(object sender, DeviceEventArgsMessage e); 
        public static event DeviceEventNewMessage Message;
        private static void AddMessage(string stringMessage)
        {
            if (Message != null) Message(null, new DeviceEventArgsMessage(stringMessage));

            lock (Locker)
            {
                if (GlobalSetting.AppSetting.debugLevel == 1)
                {
                    //добавим запись в файл

                    string fileDebug = string.Format("{0}\\debugMessage.log", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    File.AppendAllText(fileDebug, DateTime.Now + @" - " + stringMessage + Environment.NewLine, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// Событие при успешном подключении к контроллеру
        /// </summary>
        public static event DeviceEventConnect WasConnected;   
        public delegate void DeviceEventConnect(object sender);                              // уведомление об установки связи

        /// <summary>
        /// Событие при отключении от контроллера, или разрыве связи с контроллером
        /// </summary>
        public static event DeviceEventDisconnect WasDisconnected; 
        public delegate void DeviceEventDisconnect(object sender, DeviceEventArgsMessage e); // уведомление об обрыве/прекращении связи

        /// <summary>
        /// Получены новые данные от контроллера
        /// </summary>
        public static event DeviceEventNewData NewDataFromController;
        public delegate void DeviceEventNewData(object sender);                              // уведомление что получены новые данные контроллером

        #endregion
        
        #region Команды подключения/отключения от контроллера

        /// <summary>
        /// Установка связи с контроллером
        /// </summary>
        public static void Connect()
        {
            if (IsConnectedToController)
            {
                AddMessage("Команда ПОДКЛЮЧЕНИЕ: Попытка установить соединение, когда оно уже установлено, действие отменено!");
                return;
            }

            AddMessage("Команда ПОДКЛЮЧЕНИЕ: активация");

            //инициализация запуска
            _isConnectedController = true;

            //вызовем событие что подключились
            if (WasConnected != null) WasConnected(null);

        }

        /// <summary>
        /// Отключение от контроллера
        /// </summary>
        public static void Disconnect()
        {
            AddMessage("Команда ОТКЛЮЧЕНИЕ: программы от контроллера!");
           
            _isConnectedController = false;
        }

        /// <summary>
        /// Переподключение к контроллеру
        /// </summary>
        public static void Reconnect()
        {
            AddMessage("Команда переподключения программы к контроллеру");
            Disconnect();
            Connect();
        }

        /// <summary>
        /// Активация завершения, перед завершением работы программы
        /// </summary>
        public static void PreparingForExit()
        {
            _exitprogramm = true;
        }


        #endregion
        
        #region Потоки работы с контроллером

        // ПОТОК  - работы с контроллером
        private static void ThreadController()
        {
            AddMessage(@" <-- Запуск потока работы с контроллером");

            // Для отслеживания, предыдущего значения
            bool lastStatusConnected = false;

            //vid 2121 pid 2130 в десятичной системе будет как 8481 и 8496 соответственно
            UsbDeviceFinder myUsbFinder = new UsbDeviceFinder(8481, 8496);

            UsbDevice tmpUsbDevice = null;

            UsbEndpointReader usbReader = null;
            UsbEndpointWriter usbWriter = null;

            // Для отслеживания изменений в полученных данных от контроллера 
            byte[] oldInfoFromController = new byte[64];

            while (!_exitprogramm)
            {

                // уснем, что-бы работа выполнялась 1000 раз в секунду
                Thread.Sleep(1);

                #region Проверка наличия связи

                // 1) проверим наличие физического подключения контроллера
                if (tmpUsbDevice == null)
                {

                    // Попытаемся установить связь
                    tmpUsbDevice = UsbDevice.OpenUsbDevice(myUsbFinder);

                    var wholeUsbDevice = tmpUsbDevice as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null))
                    {
                        // This is a "whole" USB device. Before it can be used, 
                        // the desired configuration and interface must be selected.

                        // Select config #1
                        wholeUsbDevice.SetConfiguration(1);

                        // Claim interface #0.
                        wholeUsbDevice.ClaimInterface(0);

                        // open read endpoint 1.
                        usbReader = tmpUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
                        usbWriter = tmpUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);

                        _isAvailableController = true;

                    }
                    else
                    {
                        //AddMessage(@" <-- Ошибка установки связи с контроллером!");

                        //запустим событие о разрыве связи
                        //if (WasDisconnected != null) WasDisconnected(null, new DeviceEventArgsMessage("Ошибка"));
                        
                        _isConnectedController = false;
                        _isAvailableController = false;
                        //tmpUsbDevice = null;
                    }
                }  //if (tmpUsbDevice == null) //попытка установки связи


                if (lastStatusConnected != _isAvailableController)
                {
                    lastStatusConnected = _isAvailableController;

                    if (_isAvailableController)
                    {
                        AddMessage(@" <-- СОЕДИНЕНИЕ: Контроллер подключен к компьютеру!");
                        if (WasConnected != null) WasConnected(null);
                        
                    }
                    else
                    {
                        AddMessage(@" <-- СОЕДИНЕНИЕ: Контроллер отключен от компьютера!");
                        if (WasDisconnected != null) WasDisconnected(null, new DeviceEventArgsMessage("Ошибка"));


                    }
                }



                if (tmpUsbDevice == null || !_isAvailableController)
                {
                    _isConnectedController = false;
                    _isAvailableController = false;

                    continue;
                }


                #endregion

                #region Получение данных от контроллера

                //1) Проверим не прислал ли нам контроллер каких либо данных
                byte[] readBuffer = new byte[64];
                int bytesRead;

                ErrorCode ec;
                try
                {
                    ec = usbReader.Read(readBuffer, 2000, out bytesRead);
                }
                catch (Exception)
                {
                    AddMessage(@" <-- СОЕДИНЕНИЕ: Контроллер отключен от компьютера!");
                    if (WasDisconnected != null) WasDisconnected(null, new DeviceEventArgsMessage("Ошибка"));
                    _isConnectedController = false;
                    _isAvailableController = false;
                    tmpUsbDevice = null;
                    continue;
                }
                
                
                if (ec != ErrorCode.None)
                {
                    _isConnectedController = false;
                    AddMessage(" <-- Ошибка получения данных с контроллера, связь разорвана!");
                    if (WasDisconnected != null) WasDisconnected(null, new DeviceEventArgsMessage(@" <-- Ошибка получения данных с контроллера, связь разорвана!"));
                    _isConnectedController = false;
                    _isAvailableController = false;
                    tmpUsbDevice = null;
                    continue;
                }

                if (bytesRead > 0 && readBuffer[0] == 0x01 && !CompareArray(oldInfoFromController, readBuffer))
                {
                    Info.RawData = readBuffer;

                    ParseInfo(readBuffer);



                    //if (GlobalSetting.AppSetting.debugLevel > 1)
                    //{
                    //    //добавим запись в файл

                    //    string fileDebug = string.Format("{0}\\debugIN.log",
                    //        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    //    string ss = GetTextFromBinary(readBuffer);
                    //    File.AppendAllText(fileDebug, ss, Encoding.UTF8);
                    //}

                    oldInfoFromController = readBuffer;

                    if (NewDataFromController != null) 
                    {
                        NewDataFromController(null); //событие о получении новых данных
                    }
                }

                //Если значение не равно нулю, значит контроллер выполняет задание
                if (Info.Byte6 == 0)
                {
                    // тут простаивает
                    _controllerIsLock = false;
                }
                else
                {
                    // Если активированна пауза, то позволим управлять в ручном режиме
                    _controllerIsLock = _taskStatus != ETaskStatus.Pause;
                }

                #endregion

                #region Посылка данных в контроллер

                // получим монопольный доступ
                lock (LockDataForSend)
                {
                    // В начале отправим все высокоприоритетные данные
                    while (ImportantDataForSend.Count > 0)
                    {
                        try
                        {
                            byte[] data = ImportantDataForSend[ImportantDataForSend.Count - 1];

                            int bytesWritten;
                            usbWriter.Write(data, 200, out bytesWritten);

                            ImportantDataForSend.Remove(data);
                        }
                        catch (Exception e)
                        {
                            AddMessage(@" <-- ОШИБКА посылки данных в контроллер: " + e);
                        }
                    }

                    //потом ручные команды
                    while (ManualDataForSend.Count > 0)
                    {
                        try
                        {
                            byte[] data = ManualDataForSend[ManualDataForSend.Count - 1];

                            int bytesWritten;
                            usbWriter.Write(data, 200, out bytesWritten);

                            ManualDataForSend.Remove(data);
                        }
                        catch (Exception e)
                        {
                            AddMessage(@" <-- ОШИБКА посылки данных в контроллер: " + e);
                        }
                    }

                    // а потом обычные данные
                    if (DataForSend.Count > 0 && _taskStatus == ETaskStatus.Work)
                    {

                        ////while ((int)INFO.FreebuffSize > GlobalSetting.ControllerSetting.MinBuffSize)
                        ////{
                        ////    int bytesWritten = 64;

                        ////    try
                        ////    {
                        ////        byte[] data = dataForSend[dataForSend.Count - 1];

                        ////        _ec = _usbWriter.Write(data, 200, out bytesWritten);

                        ////        //AddMessage(@" <-- посылка комманды");

                        ////        dataForSend.Remove(data);
                        ////    }
                        ////    catch (Exception e)
                        ////    {
                        ////        AddMessage(@" <-- ОШИБКА посылки данных в контроллер: " + e.ToString());
                        ////    }
                        ////}

                        if (Info.FreebuffSize > GlobalSetting.ControllerSetting.MinBuffSize)
                        {
                            try
                            {
                                byte[] data = DataForSend[DataForSend.Count - 1];

                                int bytesWritten;
                                usbWriter.Write(data, 200, out bytesWritten);

                                DataForSend.Remove(data);
                            }
                            catch (Exception e)
                            {
                                AddMessage(@" <-- ОШИБКА посылки данных в контроллер: " + e);
                            }
                        }
                    }

                    if (DataForSend.Count == 0) _taskStatus = ETaskStatus.Off;
                }

                #endregion

            }
            AddMessage(@" <-- Завершение потока работы с контроллером");

            UsbDevice.Exit();
        }

        

        public static void TASK_SendStartData()
        {
            AddBinaryDataToTask(BinaryData.pack_9E(0x05));
            AddBinaryDataToTask(BinaryData.pack_BF(GlobalSetting.ControllerSetting.AxleX.MaxSpeed, GlobalSetting.ControllerSetting.AxleY.MaxSpeed, GlobalSetting.ControllerSetting.AxleZ.MaxSpeed, GlobalSetting.ControllerSetting.AxleA.MaxSpeed));
            AddBinaryDataToTask(BinaryData.pack_C0());
            //посылка настроек
            AddBinaryDataToTask(BinaryData.pack_D3());
            AddBinaryDataToTask(BinaryData.pack_AB());
            AddBinaryDataToTask(BinaryData.pack_9F(GlobalSetting.ControllerSetting.allowMotorUse, GlobalSetting.ControllerSetting.useSensorTools, GlobalSetting.ControllerSetting.AxleX.CountPulse, GlobalSetting.ControllerSetting.AxleY.CountPulse, GlobalSetting.ControllerSetting.AxleZ.CountPulse, GlobalSetting.ControllerSetting.AxleA.CountPulse));
            AddBinaryDataToTask(BinaryData.pack_A0(GlobalSetting.ControllerSetting.AxleX.Acceleration, GlobalSetting.ControllerSetting.AxleY.Acceleration, GlobalSetting.ControllerSetting.AxleZ.Acceleration, GlobalSetting.ControllerSetting.AxleA.Acceleration, GlobalSetting.ControllerSetting.AxleX.reversAxle, GlobalSetting.ControllerSetting.AxleY.reversAxle, GlobalSetting.ControllerSetting.AxleZ.reversAxle, GlobalSetting.ControllerSetting.AxleA.reversAxle, GlobalSetting.ControllerSetting.AxleX.reversSignal, GlobalSetting.ControllerSetting.AxleY.reversSignal, GlobalSetting.ControllerSetting.AxleZ.reversSignal, GlobalSetting.ControllerSetting.AxleA.reversSignal));
            AddBinaryDataToTask(BinaryData.pack_A1(GlobalSetting.ControllerSetting.UseLimitSwichXmin, GlobalSetting.ControllerSetting.UseLimitSwichXmax, GlobalSetting.ControllerSetting.UseLimitSwichYmin, GlobalSetting.ControllerSetting.UseLimitSwichYmax, GlobalSetting.ControllerSetting.UseLimitSwichZmin, GlobalSetting.ControllerSetting.UseLimitSwichZmax, false, false));
            AddBinaryDataToTask(BinaryData.pack_BF(GlobalSetting.ControllerSetting.AxleX.MaxSpeed, GlobalSetting.ControllerSetting.AxleY.MaxSpeed, GlobalSetting.ControllerSetting.AxleZ.MaxSpeed, GlobalSetting.ControllerSetting.AxleA.MaxSpeed));
            AddBinaryDataToTask(BinaryData.pack_B5());
            AddBinaryDataToTask(BinaryData.pack_B6());
            AddBinaryDataToTask(BinaryData.pack_C2());
            AddBinaryDataToTask(BinaryData.pack_9D());
            AddBinaryDataToTask(BinaryData.pack_9E(0x01));            
        }


        public static void TASK_SendStopData()
        {
            AddBinaryDataToTask(BinaryData.pack_FF());
            AddBinaryDataToTask(BinaryData.pack_9D());
            AddBinaryDataToTask(BinaryData.pack_9E(0x02));
            AddBinaryDataToTask(BinaryData.pack_FF());
            AddBinaryDataToTask(BinaryData.pack_FF());
            AddBinaryDataToTask(BinaryData.pack_FF());
            AddBinaryDataToTask(BinaryData.pack_FF());
            AddBinaryDataToTask(BinaryData.pack_FF());           

        }

        public static void TASK_START()
        {
            _taskStatus = ETaskStatus.Work;
        }

        public static void TASK_STOP()
        {
            TASK_CLEAR();
            TASK_SendStopData();
        }

        public static void TASK_PAUSE()
        {

            if (_taskStatus == ETaskStatus.Work) //нужно остановить
            {
                _taskStatus = ETaskStatus.Pause;
            } 
            else if (_taskStatus == ETaskStatus.Pause) //нужно запустить
            {
                _taskStatus = ETaskStatus.Work;
            }

        }

        public static void TASK_CLEAR()
        {
            DataForSend.Clear();
        }



        //// ведения логов
        //private static string GetTextFromBinary(byte[] _bytes)
        //{
        //    string returnValue = @"[" + DateTime.Now.ToString("O") + "] ";

        //    //и добавим байты в виде строк

        //    foreach (byte VARIABLE in _bytes)
        //    {
        //        string sByte = VARIABLE.ToString("X2");

        //        returnValue += sByte + " ";

        //    }

        //    returnValue += Environment.NewLine;

        //    return returnValue;
        //}

        /// <summary>
        /// Парсит полученные данные с контроллера 
        /// </summary>
        /// <param name="readBuffer"></param>
        private static void ParseInfo(IList<byte> readBuffer)
        {

            if (GlobalSetting.DISSABLE_CHECK != true)
            {
                
                // получим значение скорости движения
                int ttm = (int)(((readBuffer[22] * 65536) + (readBuffer[21] * 256) + (readBuffer[20])) / 2.1);

                if (ttm > 5000) return;

            }


            //TODO: иногда в МК2 бывает глюк, поэтому защитимся от него, костылем
            //if (readBuffer[10] == 0x58 && readBuffer[11] == 0x02 && readBuffer[22] == 0x20 && readBuffer[23] == 0x02) return;

            Info.FreebuffSize = readBuffer[1];

            Info.ShpindelMoveSpeed = 0;

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK1)
            {
                Info.ShpindelMoveSpeed = (int)(((readBuffer[22] * 65536) + (readBuffer[21] * 256) + (readBuffer[20])) / 2.1);
            }

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK2)
            {
                Info.ShpindelMoveSpeed = (int)(((readBuffer[22] * 65536) + (readBuffer[21] * 256) + (readBuffer[20])) / 1.341);
            }



            Info.ShpindelMoveSpeedSumm = (Info.ShpindelMoveSpeedSumm + Info.ShpindelMoveSpeed)/2;

            Info.ShpindelStopped = Info.ShpindelMoveSpeedSumm == 0;

            Info.Byte6 = readBuffer[6];

            Info.AxesXPositionPulse = (readBuffer[27] * 16777216) + (readBuffer[26] * 65536) + (readBuffer[25] * 256) + (readBuffer[24]);
            Info.AxesYPositionPulse = (readBuffer[31] * 16777216) + (readBuffer[30] * 65536) + (readBuffer[29] * 256) + (readBuffer[28]);
            Info.AxesZPositionPulse = (readBuffer[35] * 16777216) + (readBuffer[34] * 65536) + (readBuffer[33] * 256) + (readBuffer[32]);
            Info.AxesAPositionPulse = (readBuffer[39] * 16777216) + (readBuffer[38] * 65536) + (readBuffer[37] * 256) + (readBuffer[36]);

            Info.AxesXLimitMax = (readBuffer[15] & (1 << 0)) != 0;
            Info.AxesXLimitMin = (readBuffer[15] & (1 << 1)) != 0;
            Info.AxesYLimitMax = (readBuffer[15] & (1 << 2)) != 0;
            Info.AxesYLimitMin = (readBuffer[15] & (1 << 3)) != 0;
            Info.AxesZLimitMax = (readBuffer[15] & (1 << 4)) != 0;
            Info.AxesZLimitMin = (readBuffer[15] & (1 << 5)) != 0;

            Info.NuberCompleatedInstruction = readBuffer[9] * 16777216 + (readBuffer[8] * 65536) + (readBuffer[7] * 256) + (readBuffer[6]);


            SuperByte bb = new SuperByte(readBuffer[19]);

            Info.ShpindelEnable = bb.Bit0;

            SuperByte bb2 = new SuperByte(readBuffer[14]);
            Info.Estop = bb2.Bit7;
        }

        /// <summary>
        /// Функция сравнения двух массивов (возврат: Истина - одинаковые, Ложь - различаются)
        /// </summary>
        /// <param name="arr1">первый массив</param>
        /// <param name="arr2">второй массив</param>
        /// <returns>Истина - одинаковые массивы, Ложь - различающиеся</returns>
        private static bool CompareArray(byte[] arr1, byte[] arr2)
        {
            if (arr1 == null || arr2 == null) return false;

            //проверяем 64 байта
            bool value = true;

            for (int i = 0; i < 64; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    value = false;
                    break;
                }
            }
            return value;
        }

        #endregion

        #region Послания данных в контроллер


   

        /// <summary>
        /// Посылка в контроллер двоичных данных
        /// </summary>
        /// <param name="data">массив даных</param>
        /// <param name="outOfTurn">Необходимость послать данные вне очереди</param>
        /// <param name="insertFirst">В обычной очереди добавить запись в начало очереди для выполнения типа LIFO, по умолчанию FIFO применяется</param>
        public static void AddBinaryDataToTask(byte[] data, bool outOfTurn = false, bool insertFirst = false)
        {
            if (outOfTurn) 
            {
                //В неочереди отправим сообщение

           //     lock (lockDataForSend)
           //     {
                    //Добавим приоритетные данные
                    ImportantDataForSend.Insert(0, data);
           //     }
            }
            else
            {
                // добавление обычные данных в очередь
             //   lock (lockDataForSend)
             //   {
                    if (insertFirst)
                    {
                        DataForSend.Add(data);
                    }
                    else
                    {
                        // добавляем запись в начало, т.к. выполнение производится от последней записи к первой
                        DataForSend.Insert(0,data);
                    }
                    
              //  }
            }
        }

        private static void AddBinaryManualTask(byte[] data)
        {
            ManualDataForSend.Add(data);
        }



        /// <summary>
        /// Посылка аварийной остановки
        /// </summary>
        public static void EnergyStop()
        {
            //Controller.TASK_STOP();
            TASK_CLEAR();
            AddBinaryDataToTask(BinaryData.pack_AA(), true);
            AddBinaryDataToTask(BinaryData.pack_AA(), true);
            AddBinaryDataToTask(BinaryData.pack_AA(), true);
        }

        /// <summary>
        /// Запуск движения без остановки
        /// </summary>
        /// <param name="x">Ось Х (доступные значения "+" "0" "-")</param>
        /// <param name="y">Ось Y (доступные значения "+" "0" "-")</param>
        /// <param name="z">Ось Z (доступные значения "+" "0" "-")</param>
        /// <param name="speed"></param>
        public static void StartManualMove(string x, string y, string z, int speed)
        {
            if (!TestAllowActions) return;

            SuperByte axesDirection = new SuperByte(0x00);
            //поставим нужные биты
            if (x == "-") axesDirection.SetBit(0, true);
            if (x == "+") axesDirection.SetBit(1, true);
            if (y == "-") axesDirection.SetBit(2, true);
            if (y == "+") axesDirection.SetBit(3, true);
            if (z == "-") axesDirection.SetBit(4, true);
            if (z == "+") axesDirection.SetBit(5, true);

            AddBinaryManualTask(BinaryData.pack_BE(axesDirection.ValueByte, speed,x,y,z));
        }

        public static void StopManualMove()
        {
            byte[] buff = BinaryData.pack_BE(0x00, 0);
            
            buff[22] = 0x01;//TODO: разобраться для чего, этот байт

            AddBinaryManualTask(buff);
        }

        /// <summary>
        /// Установка в контроллер, нового положения по осям
        /// </summary>
        /// <param name="x">Положение в импульсах</param>
        /// <param name="y">Положение в импульсах</param>
        /// <param name="z">Положение в импульсах</param>
        /// <param name="a">Положение в импульсах</param>
        private static void DeviceNewPosition(int x, int y, int z, int a)
        {
            if (!TestAllowActions) return;

            AddBinaryDataToTask(BinaryData.pack_C8(x, y, z,a),true);
        }

        /// <summary>
        /// Установка в контроллер, нового положения по осям в ммилиметрах
        /// </summary>
        /// <param name="x">в миллиметрах</param>
        /// <param name="y">в миллиметрах</param>
        /// <param name="z">в миллиметрах</param>
        /// <param name="a">в миллиметрах</param>
        // ReSharper disable once UnusedMember.Global
        public static void DeviceNewPosition(decimal x, decimal y, decimal z, decimal a)
        {
            if (!TestAllowActions) return;

            AddBinaryDataToTask(BinaryData.pack_C8(Info.CalcPosPulse("X", x), Info.CalcPosPulse("Y", y), Info.CalcPosPulse("Z", z), Info.CalcPosPulse("A", a)),true);
        }




        public static void ResetToZeroAxes(string nameAxes)
        {
            switch (nameAxes)
            {
                case "X":
                    DeviceNewPosition(0, Info.AxesYPositionPulse, Info.AxesZPositionPulse, Info.AxesAPositionPulse);
                    break;

                case "Y":
                    DeviceNewPosition(Info.AxesXPositionPulse, 0, Info.AxesZPositionPulse, Info.AxesAPositionPulse);
                    break;

                case "Z":
                    DeviceNewPosition(Info.AxesXPositionPulse, Info.AxesYPositionPulse, 0, Info.AxesAPositionPulse);
                    break;

                case "A":
                    DeviceNewPosition(Info.AxesXPositionPulse, Info.AxesYPositionPulse, Info.AxesZPositionPulse, 0);
                    break;
            }
        }

 


        #endregion
    
    
    



        #region разбор


        /// <summary>
        /// Выполнение G-кода
        /// </summary>
        /// <param name="command">строка с G-кодом</param>
        public static void ExecuteCommand(string command)
        {

            DataRow dataRowNow = new DataRow(0, command);

            DataLoader.FillStructure(PlanetCNC_Controller.LastStatus, ref dataRowNow);

            if (dataRowNow.Machine.SpindelON != PlanetCNC_Controller.LastStatus.Machine.SpindelON || dataRowNow.Machine.SpeedSpindel != PlanetCNC_Controller.LastStatus.Machine.SpeedSpindel)
            {
                AddBinaryManualTask(BinaryData.pack_B5(dataRowNow.Machine.SpindelON, 2, BinaryData.TypeSignal.Hz, dataRowNow.Machine.SpeedSpindel));
            }


            if (dataRowNow.POS.X != PlanetCNC_Controller.LastStatus.POS.X || dataRowNow.POS.Y != PlanetCNC_Controller.LastStatus.POS.Y || dataRowNow.POS.Z != PlanetCNC_Controller.LastStatus.POS.Z || dataRowNow.POS.Z != PlanetCNC_Controller.LastStatus.POS.Z)
            {
                AddBinaryManualTask(BinaryData.pack_CA(Info.CalcPosPulse("X", dataRowNow.POS.X),
                                                                Info.CalcPosPulse("Y", dataRowNow.POS.Y),
                                                                Info.CalcPosPulse("Z", dataRowNow.POS.Z),
                                                                Info.CalcPosPulse("A", dataRowNow.POS.A),
                                                                dataRowNow.Machine.SpeedMaсhine,
                                                                dataRowNow.numberRow));
            }
        }


        #endregion

    
    
    }


    /// <summary>
    /// Варианты статусов выполнения задания
    /// </summary>
    public enum ETaskStatus
    {
        Off = 0,
        Work = 1,
        Pause = 2
    };

    public class DeviceInfo
    {
        /// <summary>
        /// Сырые данные от контроллера
        /// </summary>
        public byte[] RawData = new byte[64];

        /// <summary>
        /// Размер доступного буфера в контроллере
        /// </summary>
        public byte FreebuffSize;
        /// <summary>
        /// Номер выполненной инструкции
        /// </summary>
        public int NuberCompleatedInstruction;

        /// <summary>
        /// Текущее положение в импульсах
        /// </summary>
        public int AxesXPositionPulse;
        /// <summary>
        /// Текущее положение в импульсах
        /// </summary>
        public int AxesYPositionPulse;
        /// <summary>
        /// Текущее положение в импульсах
        /// </summary>
        public int AxesZPositionPulse;        
        /// <summary>
        /// Текущее положение в импульсах
        /// </summary>
        public int AxesAPositionPulse;

        //public static int AxesX_PulsePerMm = 400;
        //public static int AxesY_PulsePerMm = 400;
        //public static int AxesZ_PulsePerMm = 400;

        //срабатывание сенсора
        public bool AxesXLimitMax;
        public bool AxesXLimitMin;
        public bool AxesYLimitMax;
        public bool AxesYLimitMin;
        public bool AxesZLimitMax;
        public bool AxesZLimitMin;



        public int ShpindelMoveSpeed;
        public bool ShpindelEnable;
        public bool Estop;

        /// <summary>
        /// Данный параметр вычисляется усредненнием значений скорости, из выборки последних 10 значенией
        /// Если это значение равно 0 то "shpindel_STOPPED = true" иначе значение равно "ложь"
        /// </summary>
        private bool _shpindelStopped = true;
        public int ShpindelMoveSpeedSumm;


        public byte Byte6;

        




        public decimal AxesX_PositionMM
        {
            get
            {
                return (decimal)Controller.Info.AxesXPositionPulse / GlobalSetting.ControllerSetting.AxleX.CountPulse;
            }
        }

        public decimal AxesY_PositionMM
        {
            get
            {
                return (decimal)Controller.Info.AxesYPositionPulse / GlobalSetting.ControllerSetting.AxleY.CountPulse;
            }
        }

        public decimal AxesZ_PositionMM
        {
            get
            {
                return (decimal)Controller.Info.AxesZPositionPulse / GlobalSetting.ControllerSetting.AxleZ.CountPulse;
            }
        }

        public decimal AxesA_PositionMM
        {
            get
            {
                return (decimal)Controller.Info.AxesAPositionPulse / GlobalSetting.ControllerSetting.AxleA.CountPulse;
            }
        }

        /// <summary>
        /// Данный параметр вычисляется усредненнием значений скорости, из выборки последних 10 значенией
        /// Если это значение равно 0 то "shpindel_STOPPED = true" иначе значение равно "ложь"
        /// </summary>
        public bool ShpindelStopped
        {
            get { return _shpindelStopped; }
            set { _shpindelStopped = value; }
        }

        /// <summary>
        /// Вычисление положения в импульсах, при указании оси, и положения в миллиметрах
        /// </summary>
        /// <param name="axes">имя оси X,Y,Z</param>
        /// <param name="posMm">положение в мм</param>
        /// <returns>Количество импульсов</returns>
        public int CalcPosPulse(string axes, decimal posMm)
        {
            if (axes == "X") return (int)(posMm * GlobalSetting.ControllerSetting.AxleX.CountPulse);
            if (axes == "Y") return (int)(posMm * GlobalSetting.ControllerSetting.AxleY.CountPulse);
            if (axes == "Z") return (int)(posMm * GlobalSetting.ControllerSetting.AxleZ.CountPulse);
            if (axes == "A") return (int)(posMm * GlobalSetting.ControllerSetting.AxleA.CountPulse);
            return 0;
        }
    }




    /// <summary>
    /// Аргументы для события
    /// </summary>
    public class DeviceEventArgsMessage
    {
        public string Message { get; private set; }

        public DeviceEventArgsMessage(string str)
        {
            Message = str;
            
        }
    }

    /////// <summary>
    /////// Статусы работы с устройством
    /////// </summary>
    ////public enum EStatusDevice { Connect = 0, Disconnect };






    /// <summary>
    /// Класс для получиния бинарных данных
    /// </summary>
    public static class BinaryData
    {
        /// <summary>
        /// Данная команда пока непонятна....
        /// </summary>
        /// <param name="byte05"></param>
        /// <returns></returns>
        public static byte[] pack_C0(byte byte05)
        {
            byte[] buf = new byte[64];

            buf[0] = 0xC0;
            buf[5] = byte05;

            return buf;
        }



        public static byte[] pack_C2()
        {
            byte[] buf = new byte[64];

            buf[0] = 0xC2;
            buf[4] = 0x80;
            buf[5] = 0x03;

            return buf;
        }


        public enum TypeSignal
        {
            None,
            Hz,
            Rc
        };

        /// <summary>
        /// Управление работой шпинделя
        /// </summary>
        /// <param name="shpindelOn">Вкл/Выключен</param>
        /// <param name="numShimChanel">номер канала 1,2, или 3</param>
        /// <param name="ts">Тип сигнала</param>
        /// <param name="speedShim">Значение определяющее форму сигнала</param>
        /// <returns></returns>
        public static byte[] pack_B5(bool shpindelOn = false, int numShimChanel = 0, TypeSignal ts = TypeSignal.None, int speedShim = 0)
        {

            int tmpSpeed = speedShim;

            //что-бы не привысить максимум, после которого контроллер вырубается с ошибкой...
            if (tmpSpeed > 65000) tmpSpeed = 65000;


            byte[] buf = new byte[64];

            buf[0] = 0xB5;
            buf[4] = 0x80;


            if (shpindelOn)
            {
                buf[5] = 0x02;
            }
            else
            {
                buf[5] = 0x01;
            }

            buf[6] = 0x01; //х.з.

            switch (numShimChanel)
            {
                case 2:
                    {
                        buf[8] = 0x02;
                        break;
                    }
                case 3:
                    {
                        buf[8] = 0x03;
                        break;
                    }
                default:
                    {
                        buf[8] = 0x00; //доступен только 2 и 3 канал, остальные не подходят....
                        break;
                    }
            }


            switch (ts)
            {
                case TypeSignal.Hz:
                    {
                        buf[9] = 0x01;
                        break;
                    }

                case TypeSignal.Rc:
                    {
                        buf[9] = 0x02;
                        break;
                    }
                default:
                    {
                        buf[9] = 0x00;
                        break;
                    }
            }




            int itmp = tmpSpeed;
            buf[10] = (byte)(itmp);
            buf[11] = (byte)(itmp >> 8);
            buf[12] = (byte)(itmp >> 16);


            //buf[10] = 0xFF;
            //buf[11] = 0xFF;
            //buf[12] = 0x04;

            //зафиксируем
            PlanetCNC_Controller.LastStatus.Machine.SpindelON = shpindelOn;

            return buf;
        }



        public static byte[] pack_B6(bool chanel2On = false,bool chanel3On = false)
        {
            byte[] buf = new byte[64];

            buf[0] = 0xB6;
            buf[4] = 0x80;

            if (chanel2On)
            {
                buf[5] = 0x02;
            }
            else
            {
                buf[5] = 0x01;
            }

            if (chanel3On)
            {
                buf[7] = 0x02;
            }
            else
            {
                buf[7] = 0x01;
            }
            //зафиксируем
            PlanetCNC_Controller.LastStatus.Machine.Chanel2ON = chanel2On;
            PlanetCNC_Controller.LastStatus.Machine.Chanel3ON = chanel3On;

            return buf;
        }


        public static byte[] pack_A0(int accelX, int accelY, int accelZ, int accelA, bool reversAxeX, bool reversAxeY, bool reversAxeZ, bool reversAxeA, bool reversSignalX, bool reversSignalY, bool reversSignalZ, bool reversSignalA)
        {
            // A0 00 00 00 80 12 05 F5 01 00 05 F5 01 00 05 F5
            // 01 00 8D C4 02 00 00 00 00 00 00 00 00 00 00 00 
            // 00 00 00 00 00 00 00 00 00 00 B0 04 00 00 08 00 
            // 00 00 00 00 00 00 00 00 00 FD 01 00 00 00 00 00



            byte[] buf = new byte[64];
            buf[0] = 0xA0;
            buf[4] = 0x80;

            buf[5] = 0x12;

            accelX = (2565 / accelX) * 1000;
            accelY = (2565 / accelY) * 1000;
            accelZ = (2565 / accelZ) * 1000;
            accelA = (2565 / accelA) * 1000;


            //ускорение X
            buf[6] = (byte)(accelX);
            buf[7] = (byte)(accelX >> 8);
            buf[8] = (byte)(accelX >> 16);
            buf[9] = (byte)(accelX >> 24);
            //ускорение Y
            buf[10] = (byte)(accelY);
            buf[11] = (byte)(accelY >> 8);
            buf[12] = (byte)(accelY >> 16);
            buf[13] = (byte)(accelY >> 24);
            //ускорение Z 
            buf[14] = (byte)(accelZ);
            buf[15] = (byte)(accelZ >> 8);
            buf[16] = (byte)(accelZ >> 16);
            buf[17] = (byte)(accelZ >> 24);
            //ускорение A 
            buf[18] = (byte)(accelA);
            buf[19] = (byte)(accelA >> 8);
            buf[20] = (byte)(accelA >> 16);
            buf[21] = (byte)(accelA >> 24);





            buf[42] = 0xb0;
            buf[43] = 0x04;

            buf[46] = 0x08;

            buf[57] = 0xfd;
            buf[58] = 0x01;

            //реверс осей
            buf[59] = (new SuperByte(false, false, false, false, reversAxeA, reversAxeZ, reversAxeY, reversAxeX)).ValueByte;
            //реверс step сигнала
            buf[60] = (new SuperByte(false, false, false, false, reversSignalA, reversSignalZ, reversSignalY, reversSignalX)).ValueByte;
            

            return buf;
        }


        /// <summary>
        /// Установка применения лимитов/концевиков
        /// </summary>
        /// <param name="xmax">необходимость использования</param>
        /// <param name="xmin">необходимость использования</param>
        /// <param name="ymax">необходимость использования</param>
        /// <param name="ymin">необходимость использования</param>
        /// <param name="zmax">необходимость использования</param>
        /// <param name="zmin">необходимость использования</param>
        /// <param name="amax">необходимость использования</param>
        /// <param name="amin">необходимость использования</param>
        /// <returns></returns>
        public static byte[] pack_A1(bool xmax, bool xmin, bool ymax, bool ymin, bool zmax, bool zmin, bool amax, bool amin)
        {
            // A1 00 00 00 80 00 00 00 00 00 00 00 00 00 00 00
            // 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
            // 00 00 00 00 00 00 00 00 00 00 1F 00 00 00 00 00
            // FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

            byte[] buf = new byte[64];
            buf[0] = 0xA1;
            buf[4] = 0x80;

            buf[42] = (new SuperByte(amax, amin, zmax, zmin, ymax, ymin, xmax, xmin)).ValueByte;

            buf[48] = 0xFF;
            return buf;
        }


        /// <summary>
        /// Аварийная остановка
        /// </summary>
        /// <returns></returns>
        public static byte[] pack_AA()
        {
            byte[] buf = new byte[64];
            buf[0] = 0xAA;
            buf[4] = 0x80;
            return buf;
        }

        public static byte[] pack_AB()
        {
            byte[] buf = new byte[64];
            buf[0] = 0xAB;
            buf[4] = 0x80;
            return buf;
        }

        /// <summary>
        /// Установка в контроллер новых координат, без движения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static byte[] pack_C8(int x, int y, int z, int a)
        {
            int newPosX = x;
            int newPosY = y;
            int newPosZ = z;
            int newPosA = a;

            byte[] buf = new byte[64];
            buf[0] = 0xC8;
            //сколько импульсов сделать
            buf[6] = (byte)(newPosX);
            buf[7] = (byte)(newPosX >> 8);
            buf[8] = (byte)(newPosX >> 16);
            buf[9] = (byte)(newPosX >> 24);
            //сколько импульсов сделать
            buf[10] = (byte)(newPosY);
            buf[11] = (byte)(newPosY >> 8);
            buf[12] = (byte)(newPosY >> 16);
            buf[13] = (byte)(newPosY >> 24);
            //сколько импульсов сделать
            buf[14] = (byte)(newPosZ);
            buf[15] = (byte)(newPosZ >> 8);
            buf[16] = (byte)(newPosZ >> 16);
            buf[17] = (byte)(newPosZ >> 24);       
            //сколько импульсов сделать
            buf[18] = (byte)(newPosA);
            buf[19] = (byte)(newPosA >> 8);
            buf[20] = (byte)(newPosA >> 16);
            buf[21] = (byte)(newPosA >> 24);

            return buf;
        }










        /// <summary>
        /// Проверка длины инструмента, или прощупывание
        /// </summary>
        /// <returns></returns>
        public static byte[] pack_D2(int speed, decimal returnDistance)
        {
            byte[] buf = new byte[64];

            buf[0] = 0xD2;


            int inewSpd = 0;

            if (speed != 0)
            {
                double dnewSpd = (1800 / (double)speed) * 1000;
                inewSpd = (int)dnewSpd;
            }
            //скорость
            buf[43] = (byte)(inewSpd);
            buf[44] = (byte)(inewSpd >> 8);
            buf[45] = (byte)(inewSpd >> 16);


            //х.з.
            buf[46] = 0x10;

            // 
            int inewReturn = (int)(returnDistance * GlobalSetting.ControllerSetting.AxleZ.CountPulse);

            //растояние возврата
            buf[50] = (byte)(inewReturn);
            buf[51] = (byte)(inewReturn >> 8);
            buf[52] = (byte)(inewReturn >> 16);
            
            //х.з.
            buf[55] = 0x12;
            buf[56] = 0x7A;

            return buf;
        }



        public static byte[] pack_D3()
        {
            byte[] buf = new byte[64];

            buf[0] = 0xD3;
            buf[5] = 0x01;

            return buf;
        }

        /// <summary>
        /// Запуск движения без остановки (и остановка)
        /// </summary>
        /// <param name="direction">Направление по осям в байте</param>
        /// <param name="speed">Скорость движения</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static byte[] pack_BE(byte direction, int speed, string x = "_", string y = "_", string z = "_", string a = "_")
        {
            //TODO: переделать определения с направлениями движения

            byte[] buf = new byte[64];

            buf[0] = 0xBE;
            buf[4] = 0x80;
            buf[6] = direction;

            int inewSpd = 0;

            if (speed != 0)
            {
                double dnewSpd = (1800 / (double)speed) * 1000;
                inewSpd = (int)dnewSpd;
            }

            //скорость
            buf[10] = (byte)(inewSpd);
            buf[11] = (byte)(inewSpd >> 8);
            buf[12] = (byte)(inewSpd >> 16);

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK2)
            {
                //TODO: Для МК2 немного иные посылки данных

                if (speed != 0)
                {
                    double dnewSpd = (9000 / (double)speed) * 1000;
                    inewSpd = (int)dnewSpd;
                }

                //скорость
                buf[10] = (byte)(inewSpd);
                buf[11] = (byte)(inewSpd >> 8);
                buf[12] = (byte)(inewSpd >> 16);

                if (speed == 0)
                {
                    buf[14] = 0x00;
                    buf[18] = 0x01;
                    buf[22] = 0x01;

                    //x
                    buf[26] = 0x00;
                    buf[27] = 0x00;
                    buf[28] = 0x00;
                    buf[29] = 0x00;

                    //y
                    buf[30] = 0x00;
                    buf[31] = 0x00;
                    buf[32] = 0x00;
                    buf[33] = 0x00;

                    //z
                    buf[34] = 0x00;
                    buf[35] = 0x00;
                    buf[36] = 0x00;
                    buf[37] = 0x00;

                    //a
                    buf[38] = 0x00;
                    buf[39] = 0x00;
                    buf[40] = 0x00;
                    buf[41] = 0x00;


                }
                else
                {
                    buf[14] = 0xC8; //TODO: WTF?? 
                    buf[18] = 0x14; //TODO: WTF??
                    buf[22] = 0x14; //TODO: WTF??




                    if (x == "+")
                    {
                        buf[26] = 0x40;
                        buf[27] = 0x0D;
                        buf[28] = 0x03;
                        buf[29] = 0x00;
                    }

                    if (x == "-")
                    {
                        buf[26] = 0xC0;
                        buf[27] = 0xF2;
                        buf[28] = 0xFC;
                        buf[29] = 0xFF;
                    }

                    if (y == "+")
                    {
                        buf[30] = 0x40;
                        buf[31] = 0x0D;
                        buf[32] = 0x03;
                        buf[33] = 0x00;
                    }

                    if (y == "-")
                    {
                        buf[30] = 0xC0;
                        buf[31] = 0xF2;
                        buf[32] = 0xFC;
                        buf[33] = 0xFF;
                    }

                    if (z == "+")
                    {
                        buf[34] = 0x40;
                        buf[35] = 0x0D;
                        buf[36] = 0x03;
                        buf[37] = 0x00;
                    }

                    if (z == "-")
                    {
                        buf[34] = 0xC0;
                        buf[35] = 0xF2;
                        buf[36] = 0xFC;
                        buf[37] = 0xFF;
                    }

                    if (a == "+")
                    {
                        buf[38] = 0x40;
                        buf[39] = 0x0D;
                        buf[40] = 0x03;
                        buf[41] = 0x00;
                    }

                    if (a == "-")
                    {
                        buf[38] = 0xC0;
                        buf[39] = 0xF2;
                        buf[40] = 0xFC;
                        buf[41] = 0xFF;
                    }



                }
            }

            


            return buf;
        }


        public static byte[] pack_9E(byte byte4 = 0x00, byte byte5 = 0x00)
        {
            byte[] buf = new byte[64];

            buf[0] = 0x9E;
            buf[4] = byte4;
            buf[5] = byte5;

            return buf;
        }






        public static byte[] pack_9F(bool allowMotorUse, bool useSensorTools, int countPulseX, int countPulseY, int countPulseZ, int countPulseA)
        {
            // 9F 00 00 00 80 B3 90 01 00 00 90 01 00 00 90 01 
            // 00 00 C8 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

            byte[] buf = new byte[64];

            buf[0] = 0x9F;
            buf[4] = 0x80;

            buf[5] = (new SuperByte(true, false, true, true, false, false, useSensorTools, allowMotorUse)).ValueByte;

            buf[6] = (byte)(countPulseX);
            buf[7] = (byte)(countPulseX >> 8);
            buf[8] = (byte)(countPulseX >> 16);
            buf[9] = (byte)(countPulseX >> 24);

            buf[10] = (byte)(countPulseY);
            buf[11] = (byte)(countPulseY >> 8);
            buf[12] = (byte)(countPulseY >> 16);
            buf[13] = (byte)(countPulseY >> 24);

            buf[14] = (byte)(countPulseZ);
            buf[15] = (byte)(countPulseZ >> 8);
            buf[16] = (byte)(countPulseZ >> 16);
            buf[17] = (byte)(countPulseZ >> 24);

            buf[18] = (byte)(countPulseA);
            buf[19] = (byte)(countPulseA >> 8);
            buf[20] = (byte)(countPulseA >> 16);
            buf[21] = (byte)(countPulseA >> 24);

            return buf;
        }


        /// <summary>
        /// Установка ограничения максимальной скорости, по осям
        /// </summary>
        /// <param name="speedLimitX">Максимальная скорость по оси X</param>
        /// <param name="speedLimitY">Максимальная скорость по оси Y</param>
        /// <param name="speedLimitZ">Максимальная скорость по оси Z</param>
        /// <param name="speedLimitA"></param>
        /// <returns></returns>
        public static byte[] pack_BF(int speedLimitX, int speedLimitY, int speedLimitZ, int speedLimitA)
        {
            // BF 00 00 00 80 00 00 27 23 00 00 27 23 00 00 27
            // 23 00 00 4F 46 00 00 00 00 00 00 00 00 00 00 00 
            // 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

            //в программе максимальная скорость = 0, при этом значение передается 00-00-23-27

            byte[] buf = new byte[64];

            buf[0] = 0xBF;

            buf[4] = 0x80;

            double koef = 4500;

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK1)
            {
                buf[4] = 0x80; //TODO: непонятный байт
                koef = 3600;
            }

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK2)
            {
                buf[4] = 0x00; //TODO: непонятный байт
                koef = 4500;
            }


            double dnewSpdX = (koef / speedLimitX) * 1000;
            int inewSpdX = (int)dnewSpdX;

            double dnewSpdY = (koef / speedLimitY) * 1000;
            int inewSpdY = (int)dnewSpdY;

            double dnewSpdZ = (koef / speedLimitZ) * 1000;
            int inewSpdZ = (int)dnewSpdZ;

            double dnewSpdA = (koef / speedLimitA) * 1000;
            int inewSpdA = (int)dnewSpdA;

            buf[07] = (byte)(inewSpdX);
            buf[08] = (byte)(inewSpdX >> 8);
            buf[09] = (byte)(inewSpdX >> 16);
            buf[10] = (byte)(inewSpdX >> 24);


            buf[11] = (byte)(inewSpdY);
            buf[12] = (byte)(inewSpdY >> 8);
            buf[13] = (byte)(inewSpdY >> 16);
            buf[14] = (byte)(inewSpdY >> 24);

            buf[15] = (byte)(inewSpdZ);
            buf[16] = (byte)(inewSpdZ >> 8);
            buf[17] = (byte)(inewSpdZ >> 16);
            buf[18] = (byte)(inewSpdZ >> 24);

            buf[19] = (byte)(inewSpdA);
            buf[20] = (byte)(inewSpdA >> 8);
            buf[21] = (byte)(inewSpdA >> 16);
            buf[22] = (byte)(inewSpdA >> 24);

            return buf;
        }

        /// <summary>
        /// НЕИЗВЕСТНАЯ КОМАНДА
        /// </summary>
        /// <returns></returns>
        public static byte[] pack_C0()
        {
            byte[] buf = new byte[64];

            buf[0] = 0xC0;

            return buf;
        }

        /// <summary>
        /// Движение в указанную точку
        /// </summary>
        /// <param name="posX">положение X в импульсах</param>
        /// <param name="posY">положение Y в импульсах</param>
        /// <param name="posZ">положение Z в импульсах</param>
        /// <param name="posA"></param>
        /// <param name="speed">скорость мм/минуту</param>
        /// <param name="numberInstruction">Номер данной инструкции</param>
        /// <param name="withoutpause"></param>
        /// <returns>набор данных для посылки</returns>
        public static byte[] pack_CA(int posX, int posY, int posZ, int posA, int speed, int numberInstruction = 0, bool withoutpause = false)
        {
            int newPosX = posX;
            int newPosY = posY;
            int newPosZ = posZ;
            int newPosA = posA;
            int newInst = numberInstruction;





            ////корректировка положения
            //if (Controller.CorrectionPos.useCorrection)
            //{
            //    newPosX += Controller.INFO.CalcPosPulse("X", Controller.CorrectionPos.deltaX);
            //    newPosY += Controller.INFO.CalcPosPulse("Y", Controller.CorrectionPos.deltaY);
            //    newPosZ += Controller.INFO.CalcPosPulse("Z", Controller.CorrectionPos.deltaZ);
            //    newPosA += Controller.INFO.CalcPosPulse("A", Controller.CorrectionPos.deltaA);
            //}







            byte[] buf = new byte[64];

            buf[0] = 0xCA;
            //запись номера инструкции
            buf[1] = (byte)(newInst);
            buf[2] = (byte)(newInst >> 8);
            buf[3] = (byte)(newInst >> 16);
            buf[4] = (byte)(newInst >> 24);

            // Зная угол между 2-мя отрезками, вычислим необходимую паузу перехода, от отрезка к отрезку
            //int deltaAngle = 180 - AngleVectors;

            //buf[5] = 0x01;

            //if (deltaAngle > 45) buf[5] = 0x39;


            //if (deltaAngle <= 25) 
            //buf[5] = 0x03;
            //buf[5] = (byte)_valuePause;


           // if (Distance >0 && Distance < 5) buf[5] = 0x03;

            //if (deltaAngle < 15) buf[5] = 0x10;


            //if (deltaAngle < 10) buf[5] = 0x02;


            //if (deltaAngle < 3) buf[5] = 0x01;



            //buf[5] = (byte)deltaAngle;

            //if (buf[5] == 0x00) buf[5] = 0x01;

            //if (buf[5] > 0x39) buf[5] = 0x39;

            //TODO: старый алгоритм для удаления
            //// 0х01 нет паузы, 0х39 есть пауза (при маленькой паузе, и большой скорости происходит срыв...)

            if (withoutpause)
            {
                buf[5] = 0x01;
            }
            else
            {
                buf[5] = 0x39;
            }

            //сколько импульсов сделать
            buf[6] = (byte)(newPosX);
            buf[7] = (byte)(newPosX >> 8);
            buf[8] = (byte)(newPosX >> 16);
            buf[9] = (byte)(newPosX >> 24);

            //сколько импульсов сделать
            buf[10] = (byte)(newPosY);
            buf[11] = (byte)(newPosY >> 8);
            buf[12] = (byte)(newPosY >> 16);
            buf[13] = (byte)(newPosY >> 24);

            //сколько импульсов сделать
            buf[14] = (byte)(newPosZ);
            buf[15] = (byte)(newPosZ >> 8);
            buf[16] = (byte)(newPosZ >> 16);
            buf[17] = (byte)(newPosZ >> 24);

            //сколько импульсов сделать
            buf[18] = (byte)(newPosA);
            buf[19] = (byte)(newPosA >> 8);
            buf[20] = (byte)(newPosA >> 16);
            buf[21] = (byte)(newPosA >> 24);



            double koef = 4500;

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK1)
            {
                buf[4] = 0x80; //TODO: непонятный байт
                //koef = 3600;
                koef = 2000;
            }

            if (GlobalSetting.AppSetting.Controller == ControllerModel.PlanetCNC_MK2)
            {
                buf[4] = 0x00; //TODO: непонятный байт
                koef = 4500;
            }


            //TODO: если дистанция = 0 то используем скорость....

            //TODO: Учесть длину траектории, если она короткая то нельзя ставить большую скорость, т.к. легко можно сорвать вращение 
            //int SpeedToSend = _speed;

            //int SpeedToSend = 2328; 
            ////старый код
            //if (_speed != 0)
            //{
            //    SpeedToSend = _speed;
            //}

            //TODO: добавить ограничение скорости от дистанции!!!
            //при большой дистанции используем скорость заданную G-кодом
            //if (Distance > 50) SpeedToSend = _speed;
            //else
            //{
            //    //иначе замедлим скорость
            //    /* растояние 50мм скорость = 500мм/минуту
            //     * растояние 30мм скорость = 300мм/минуту
            //     * растояние 10мм скорость = 100мм/минуту
            //     * и т.д....
            //     */
            //    //SpeedToSend = Distance * 10;
            //    SpeedToSend = 500;
            //    // не всегда имеем дистанцию 
            //    //if (Distance == 0) SpeedToSend = 200;
            //}

            //////if (Distance < 10) SpeedToSend = 400;

            //////if (_speed == 0) SpeedToSend = 200;



            int iSpeed = (int)(koef / speed) * 1000;
            //скорость ось х
            buf[43] = (byte)(iSpeed);
            buf[44] = (byte)(iSpeed >> 8);
            buf[45] = (byte)(iSpeed >> 16);
            
            buf[54] = 0x40;  //TODO: непонятный байт

            return buf;
        }

        /// <summary>
        /// Завершение выполнения всех операций
        /// </summary>
        /// <returns></returns>
        public static byte[] pack_FF()
        {
            byte[] buf = new byte[64];

            buf[0] = 0xFF;

            return buf;
        }

        /// <summary>
        /// НЕИЗВЕСТНАЯ КОМАНДА
        /// </summary>
        /// <returns></returns>
        public static byte[] pack_9D()
        {
            byte[] buf = new byte[64];

            buf[0] = 0x9D;

            return buf;
        }

        //public static byte[] GetPack07()
        //{
        //    byte[] buf = new byte[64];

        //    buf[0] = 0x9E;
        //    buf[5] = 0x02;

        //    return buf;
        //}





    }

    #region НАБОР ДАННЫХ







    //public class matrixYline
    //{
    //    public decimal Y = 0;       // координата в мм
    //   // public List<matrixPoint> X = new List<matrixPoint>(); //набор координат по оси X, и свойства используется ли эта точка
    //}

    //public class matrixPoint
    //{
    //    public decimal X = 0;       // координата в мм
    //    public decimal Z = 0;       // координата в мм
    //    public bool Used = false;       // используется ли эта точка

    //    public matrixPoint(decimal _X, decimal _Z, bool _Used)
    //    {
    //        X = _X;
    //        Z = _Z;
    //        Used = _Used;
    //    }
    //}






    #endregion

    ////public class decPoint
    ////{

    ////    public decimal X;       // координата в мм
    ////    public decimal Y;       // координата в мм
    ////    public decimal Z;       // координата в мм
    ////    public decimal A;       // координата в мм

    ////    public decPoint(decimal _x, decimal _y, decimal _z, decimal _a)
    ////    {
    ////        X = _x;
    ////        Y = _y;
    ////        Z = _z;
    ////        A = _a;
    ////    }
    ////}

    ////public class dobPoint
    ////{

    ////    public double X;       // координата в мм
    ////    public double Y;       // координата в мм
    ////    public double Z;       // координата в мм
    ////    public double A;       // координата в мм

    ////    public dobPoint(double _x, double _y, double _z, double _a)
    ////    {
    ////        X = _x;
    ////        Y = _y;
    ////        Z = _z;
    ////        A = _a;
    ////    }
    ////}




    /// <summary>
    /// Хласс для хранения данных о смещении координат, пропорций и прочего
    /// </summary>
    public class CorrectionPos
    {
        /// <summary>
        /// Статус применения корректировки
        /// </summary>
        public bool UseCorrection;
        /// <summary>
        /// Смещение по X
        /// </summary>
        public decimal DeltaX;
        /// <summary>
        /// Смещение по Y
        /// </summary>
        public decimal DeltaY;
        /// <summary>
        /// Смещение по Z
        /// </summary>
        public decimal DeltaZ;
        /// <summary>
        /// Смещение по A
        /// </summary>
        public decimal DeltaA;

        /// <summary>
        /// Применение матрицы сканирования поверхности
        /// </summary>
        public bool UseMatrix;

        public CorrectionPos()
        {
            UseCorrection = false;
            DeltaX = 0;
            DeltaY = 0;
            DeltaZ = 0;
            DeltaA = 0;
            UseMatrix = false;
        }





    }


}


