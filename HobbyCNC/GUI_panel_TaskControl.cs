using System;
using System.Drawing;
using System.Windows.Forms;
using CNC_Assist.Properties;

namespace CNC_Assist
{
    public partial class GuiPanelTaskControl : UserControl
    {
        /// <summary>
        /// Для сравнения с датой загрузки из файла G-кода, и последующего перезаполнения
        /// </summary>
        private DateTime _dateGetLastNewDataCode;

        /// <summary>
        /// текущая выполняемая строка
        /// </summary>
        private static int _nowPos;   
        /// <summary>
        /// конечная строка данных для выполнения
        /// </summary>
        private static int _endPos; 

        public GuiPanelTaskControl()
        {
            InitializeComponent();

            _nowPos = 0;
            _endPos = 0;
        }

        // выделение мышкой одной или нескольких строк g-кода
        private void listGkodeCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataLoader.SelectedRowStart = listGkodeCommand.SelectedIndex;
            DataLoader.SelectedRowStop  = listGkodeCommand.SelectedIndex + listGkodeCommand.SelectedItems.Count;
        }

        // флаг применять свою скорость движения
        private void checkBoxManualSpeed_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBoxManualSpeed.Checked;
            numericUpDown2.Enabled = checkBoxManualSpeed.Checked;
        }

        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            label3.Text = @"Выделенно с " + DataLoader.SelectedRowStart + @" по " + DataLoader.SelectedRowStop;
            textBoxNumberLine.Text = @"Выполнена: " + ControllerPlanetCNC.Info.NuberCompleatedInstruction;

            // проверим не обновились ли данные
            if (_dateGetLastNewDataCode != DataLoader.dateGetNewDataCode && DataLoader.status == DataLoader.eDataSetStatus.none)
            {
                int idx=0;
                listGkodeCommand.Items.Clear();

                listGkodeCommand.BeginUpdate();
                foreach (string val in GkodeWorker.Gkode)
                {
                    //listGkodeCommand.Items.Add(@"№" + (idx++) + " " + val);
                    listGkodeCommand.Items.Add(val);
                }
                listGkodeCommand.EndUpdate();
                _dateGetLastNewDataCode = DataLoader.dateGetNewDataCode;
            }

            if (ControllerPlanetCNC.StatusTThread == EnumStatusThread.Off)
            {
                labelStatusTask.Image = Resources.ball_red;
                labelStatusTask.Text = @"Нет связи";
                buttonStartTask.Enabled = false;
                buttonPauseTask.Enabled = false;
                btStopTask.Enabled = false;
            }

            if (ControllerPlanetCNC.StatusTThread == EnumStatusThread.Wait)
            {
                labelStatusTask.Image = Resources.ball_red;
                labelStatusTask.Text = @"Нет задания";
                buttonStartTask.Enabled = true;
                buttonPauseTask.Enabled = false;
                btStopTask.Enabled = false;
            }

            if (ControllerPlanetCNC.StatusTThread == EnumStatusThread.Work)
            {
                labelStatusTask.Image = Resources.ball_green;
                labelStatusTask.Text = @"ВЫПОЛНЕНИЕ";
                buttonStartTask.Enabled = false;
                buttonPauseTask.Enabled = true;
                btStopTask.Enabled = true;
            }

            if (ControllerPlanetCNC.StatusTThread == EnumStatusThread.Pause)
            {
                labelStatusTask.Image = Resources.ball_yellow;
                labelStatusTask.Text = @"ПАУЗА";
                buttonStartTask.Enabled = false;
                buttonPauseTask.Enabled = true;
                btStopTask.Enabled = true;
            }

            if (!ControllerPlanetCNC.IsConnectedToController)
            {
                groupBoxManualSpeedGkode.Enabled = false;
                groupBoxWorking.Enabled = false;
                return;
            }

            groupBoxManualSpeedGkode.Enabled = true;
            groupBoxWorking.Enabled = true;
        }

        private void buttonStartTask_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.TASK_CLEAR();

            ControllerPlanetCNC.TASK_SendStartData();

            // Определимся с границами
            _nowPos = listGkodeCommand.SelectedIndex;

            if (listGkodeCommand.SelectedItems.Count == 1)
            {
                _endPos = listGkodeCommand.Items.Count;
            }
            else
            {
                _endPos = listGkodeCommand.SelectedIndex + listGkodeCommand.SelectedItems.Count;
            }
            
            if (_nowPos >= _endPos)
            {
                return;
            }

            bool needContinue = true;

            while (needContinue)
            {
                //DataRow dataRowNow = DataLoader.DataRows[_nowPos];

                ControllerPlanetCNC.TASK_AddCommand(listGkodeCommand.Items[_nowPos].ToString(), _nowPos);

                _nowPos++;

                //TODO: так-же добавить прерывание, в случае смены инструмента/паузы
                
                needContinue = (_nowPos < _endPos);
            }

            // данные переданы для выполнения, запустим....

            ControllerPlanetCNC.TASK_SendStopData();

            ControllerPlanetCNC.TASK_START();
        }

        private void buttonPauseTask_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.TASK_PAUSE();
        }

        private void btStopTask_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.TASK_STOP();
        }


        private void listGkodeCommand_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Перерисовываем фон всех элементов ListBox.  
            e.DrawBackground();

            // Создаем объект Brush.  
            Brush myBrush = Brushes.Black;

            // Определяем номер текущего элемента  
            //switch (e.Index)
            //{
            //    case 0:
            //        myBrush = Brushes.Red;
            //        break;
            //    case 1:
            //        myBrush = Brushes.Green;
            //        break;
            //    case 2:
            //        myBrush = Brushes.Blue;
            //        break;
            //    default: myBrush = Brushes.Yellow;
            //        break;
            //}

            //Если необходимо, закрашиваем фон   
            //активного элемента в новый цвет  
            //e.Graphics.FillRectangle(myBrush, e.Bounds);  


            try
            {
                // Перерисовываем текст текущего элемента  
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                //выделение другим цветом в той-же строке
                //e.Graphics.DrawString(" 123", e.Font, Brushes.Blue, e.Bounds, StringFormat.GenericDefault);

                // Если ListBox в фокусе, рисуем прямоугольник   
                //вокруг активного элемента.  
                e.DrawFocusRectangle();
            }
            catch (Exception)
            {
                
              //  throw;
            }

            

        }
    }
}