using System;
using System.Collections.Generic;
using System.Globalization;

namespace CNC_Assist
{
    /// <summary>
    /// Данный клас применятся лишь для хранения текста G-кода, и набора точек для визуализации траектории
    /// </summary>
    public static class GkodeWorker
    {
        /// <summary>
        /// Текст G-кода
        /// </summary>
        public static List<string> Gkode = new List<string>();

        public static List<PointCNC> Point3D = new List<PointCNC>();


        private static List<string> ParseStringToListString(string value)
        {
            List<string> returnValue = new List<string>();

            string tmpString = value.Trim().ToUpper();

            //все что после скобки отбросим, дальше не будем анализировать
            int i = tmpString.IndexOf(@"(", StringComparison.Ordinal);
            if (i != -1) return returnValue;

            //все что после точки с запятой отбросим, дальше не будем анализировать
            i = tmpString.IndexOf(@";", StringComparison.Ordinal);
            if (i != -1) return returnValue;

            //все что после точки с запятой отбросим, дальше не будем анализировать
            i = tmpString.IndexOf(@"%", StringComparison.Ordinal);
            if (i != -1) return returnValue;

            //все что после двух косых отбросим, дальше не будем анализировать
            i = tmpString.IndexOf(@"//", StringComparison.Ordinal);
            if (i != -1) return returnValue;

            // ещё раз обрежем
            tmpString = tmpString.Trim();

            if (tmpString.Length < 2) return returnValue;

            // распарсим строку на отдельные строки с параметрами
            int inx = 0;

            bool collectCommand = false;

            foreach (char symb in tmpString)
            {
                if (symb > 0x40 && symb < 0x5B)  //символы от A до Z
                {
                    if (collectCommand)
                    {
                        inx++;
                    }

                    collectCommand = true;
                    returnValue.Add("");
                }

                if (collectCommand && symb != ' ') returnValue[inx] += symb.ToString();
            }

            return returnValue;
        }

        public static void Parsing()
        {
            Point3D.Clear();

            //получим символ разделения дробной и целой части.
            string symbSeparatorDec = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            char csourse = '.';
            char cdestination = ',';

            if (symbSeparatorDec == ".")
            {
                csourse = ',';
                cdestination = '.';
            }

            bool AbsolutlePosParsing = true;

            PointCNC LastPoint = new PointCNC(ControllerPlanetCNC.Info.AxesXPositionMm, ControllerPlanetCNC.Info.AxesYPositionMm, ControllerPlanetCNC.Info.AxesZPositionMm, ControllerPlanetCNC.Info.AxesAPositionMm);

            int numRow = 0;

            foreach (string str in Gkode)
            {
                List<string> lcmd = ParseStringToListString(str);

                if (lcmd.Count == 0) continue;


                //необходимо для получения следующей команды, т.к. команда G04 P500 будет раздельно
                int index = 0;
                foreach (string code in lcmd)
                {
                    if (code == "M3" || code == "M03")
                    {
                        LastPoint.InstrumentOn = true;
                    }

                    if (code == "M5" || code == "M05")
                    {
                        LastPoint.InstrumentOn = false;
                    }

                    if (code == "G0" || code == "G00") //холостое движение
                    {
                        LastPoint.workSpeed = false;
                    }

                    if (code == "G1" || code == "G01") //рабочее движение
                    {
                        LastPoint.workSpeed = true;
                    }

                    if (code.Substring(0, 1) == "X") //координата
                    {
                        string svalue = code.Substring(1).Replace(csourse, cdestination); ;
                        decimal pos = 0;
                        decimal.TryParse(svalue, out pos);

                        if (AbsolutlePosParsing) LastPoint.X = pos;
                        else LastPoint.X += pos;
                    }


                    if (code.Substring(0, 1) == "Y") //координата
                    {
                        string svalue = code.Substring(1).Replace(csourse, cdestination); ;
                        decimal pos = 0;
                        decimal.TryParse(svalue, out pos);

                        if (AbsolutlePosParsing) LastPoint.Y = pos;
                        else LastPoint.Y += pos;
                    }


                    if (code.Substring(0, 1) == "Z") //координата
                    {
                        string svalue = code.Substring(1).Replace(csourse, cdestination); ;
                        decimal pos = 0;
                        decimal.TryParse(svalue, out pos);

                        if (AbsolutlePosParsing) LastPoint.Z = pos;
                        else LastPoint.Z += pos;
                    }


                    if (code.Substring(0, 1) == "A") //координата
                    {
                        string svalue = code.Substring(1);
                        decimal pos = 0;
                        decimal.TryParse(svalue, out pos);

                        if (AbsolutlePosParsing) LastPoint.A = pos;
                        else LastPoint.A += pos;
                    }

                    if (code == "G90")
                    {
                        AbsolutlePosParsing = true; //применяем абсолютные координаты
                    }

                    if (code == "G91")
                    {
                        AbsolutlePosParsing = false;//применяем относительные координаты
                    }
 
                    index++;
                }//foreach (string CODE in lcmd)

                LastPoint.numRow = numRow;

                Point3D.Add(LastPoint);

                numRow++;
            }
        }
   
    
    
    
    }
}
