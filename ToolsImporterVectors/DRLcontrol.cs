using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ToolsImporterVectors
{
    public partial class DRLcontrol : UserControl
    {

        public string FileNAME = "";

        public DRLcontrol()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Функция анализирует переданную строку, и вычисляет номер последнего символа, который является числом, символ точка и запятая так-же считаются частью числа
        /// </summary>
        /// <param name="_str">Входящая строка для анализа</param>
        /// <returns></returns>
        private int LastDitsSymbol(string _str)
        {
            int position = -1;
            bool isStart = false;

            for (int i = 0; i <_str.Length;i++)
            {
                char symb = _str[i];

                if (symb == '0' || symb == '1' || symb == '2' || symb == '3' || symb == '4' || symb == '5' ||
                    symb == '6' || symb == '7' || symb == '8' || symb == '9' || symb == '.' || symb == ',')
                {
                    position = i;
                    isStart = true;
                    continue;
                }

                if (isStart) break; //закончились цифры
            }
            return position;
        }




        private void readFromFile_Click(object sender, EventArgs e)
        {
            if (FileNAME.Trim().Length == 0) return;

            List<DRLPoint> _point = null;   // Список точек сверловки, для конкретного инструмента

            StreamReader fs = new StreamReader(FileNAME);
            string s = fs.ReadLine();

            int SelectedInstrumentNumber = 0; // № последнего активного инструмента
            DRLTOOLS toolsNow = null;

            int numRow = 1;
            while (s != null)
            {
                // пропустим ненужные данные
                if (s.Trim().Substring(0, 1) == "%")
                {
                    s = fs.ReadLine();
                    numRow++;
                    continue;
                } 
                // пропустим ненужные данные
                if (s.Trim().Substring(0, 1) == ";")
                {
                    s = fs.ReadLine();
                    numRow++;
                    continue;
                }

                s = s.Trim();

                //описание инструмента, или выбор текущего инструмента, после которого последуют точки
                if (s.Substring(0, 1) == "T") 
                {
                    // 1) Попробуем распарсить число
                    int tmpEndDigits = LastDitsSymbol(s);

                    if (tmpEndDigits == -1)
                    {
                        MessageBox.Show(@"Ошибка парсинга номера инструмента в строке № " + numRow.ToString());
                        break;
                    }

                    int tmpNumTool = 0;
                    string tmpSnum = s.Substring(1,tmpEndDigits); //узнаем где завершился номер инструмента, его длина меняется от 1 до 2-х символов

                    if (!int.TryParse(tmpSnum, out tmpNumTool))
                    {
                        MessageBox.Show(@"Ошибка преобразования номера инструмента в строке № " + numRow.ToString());
                        break;
                    }

                    SelectedInstrumentNumber = tmpNumTool;  //распарсенный номер инструмента устанавливается в качестве текущего

                    
                    //получим контекст инструмена из списка _tools
                    toolsNow = ArrayData.DRL_tools.Find(p => p.numTool == SelectedInstrumentNumber);

                    if (toolsNow == null)
                    {
                        // данного инструмента в списке пока нет, поэтому добавим
                        toolsNow = new DRLTOOLS(SelectedInstrumentNumber,0,new List<DRLPoint>());
                        ArrayData.DRL_tools.Add(toolsNow);
                    }

                    //теперь из строки удалим номер инструмента
                    s = s.Substring(tmpEndDigits + 1);
                    // и будем дальше анализировать данные
                    if (s != "" && s.Substring(0, 1) == "C")
                    {
                        tmpEndDigits = LastDitsSymbol(s);

                        if (tmpEndDigits == -1)
                        {
                            MessageBox.Show(@"Ошибка парсинга размера инструмента в строке № " + numRow.ToString());
                            break;
                        }

                        s = s.Replace('.', ','); //TODO: добавить локализацию

                        string tmpDiametr = s.Substring(1, tmpEndDigits);

                        decimal tmpdiametr = 0;

                        if (!decimal.TryParse(tmpDiametr, out tmpdiametr))
                        {
                            MessageBox.Show(@"Ошибка преобразования размера инструмента в строке № " + numRow.ToString());
                            break;
                        }

                        toolsNow.DiametrTools = tmpdiametr;
                    }

                    s = fs.ReadLine();
                    numRow++;
                    continue;
                }//if (s.Substring(0, 1) == "T") 


                if (s.Substring(0, 1) == "X")
                {
                    int tmpX = 0;
                    int tmpY = 0;

                    // 1) Попробуем распарсить число
                    int tmpEndDigits = LastDitsSymbol(s);

                    if (tmpEndDigits == -1)
                    {
                        MessageBox.Show(@"Ошибка парсинга координаты X в строке № " + numRow.ToString());
                        break;
                    }

                    string tmpSnum = s.Substring(1, tmpEndDigits); 

                    if (!int.TryParse(tmpSnum, out tmpX))
                    {
                        MessageBox.Show(@"Ошибка преобразования координаты X в строке № " + numRow.ToString());
                        break;
                    }

                    //теперь из строки удалим координату X
                    s = s.Substring(tmpEndDigits + 1);

                    if (s.Substring(0, 1) == "Y")
                    {
                        // 1) Попробуем распарсить число
                        tmpEndDigits = LastDitsSymbol(s);

                        if (tmpEndDigits == -1)
                        {
                            MessageBox.Show(@"Ошибка парсинга координаты Y в строке № " + numRow.ToString());
                            break;
                        }

                        tmpSnum = s.Substring(1, tmpEndDigits); 

                        if (!int.TryParse(tmpSnum, out tmpY))
                        {
                            MessageBox.Show(@"Ошибка преобразования координаты Y в строке № " + numRow.ToString());
                            break;
                        }

                        //теперь из строки удалим координату X
                        s = s.Substring(tmpEndDigits + 1);
                    }

                    decimal increment = 1;

                    for (int i = 0; i < numericUpDown3.Value; i++)
                    {
                        increment = increment * 10;
                    }

                    decimal posX = tmpX / increment;
                    decimal posY = tmpY / increment;

                    // А теперь точку добавим в список
                    toolsNow.points.Add(new DRLPoint(posX, posY));

                }

                s = fs.ReadLine();
                numRow++;
            }

            fs = null;

            labelNumTools.Text = ArrayData.DRL_tools.Count.ToString();

            int countPoint = 0;

            foreach (DRLTOOLS drltools in ArrayData.DRL_tools)
            {
                countPoint += drltools.points.Count;
            }

            labelNumDrills.Text = countPoint.ToString();

            TreeDrill.Nodes.Clear();

            foreach (DRLTOOLS drltools in ArrayData.DRL_tools)
            {
                TreeNode trc = new TreeNode("Сверло № " + drltools.numTool.ToString() + " диаметр: " + drltools.DiametrTools.ToString());

                foreach (DRLPoint drlPoint in drltools.points)
                {
                    trc.Nodes.Add("X: " + drlPoint.X.ToString() + " Y: " + drlPoint.Y.ToString());
                }
                TreeDrill.Nodes.Add(trc);
            }
        }


    }


}
