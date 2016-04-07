using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ClipperLib;
//using Tao.FreeGlut;
//using Tao.OpenGl;


namespace ToolsImporterVectors
{
    using Polygon = List<IntPoint>;
    using Polygons = List<List<IntPoint>>;



    public partial class MainForm : Form
    {
       

        private DRLcontrol _DRLForm;
        private PLTcontrol _PLTForm;


        public MainForm()
        {
            InitializeComponent();
            // подключение обработчика, колесика мышки
            MouseWheel += this_MouseWheel;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        } 
        
        //событие от колёсика мышки
        void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
             // zoom.Value += (decimal)(0.1);
            }
            else
            {
              //  zoom.Value -= (decimal)(0.1);
            }
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            //dCenterX = (double)numericUpDownX.Value;
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
           // dCenterY = (double)numericUpDownY.Value;
        }

        //сдвиг к нулю
        private void button2_Click(object sender, EventArgs e)
        {
            //float Xmin = 99999;
            //float Ymin = 99999;

            //foreach (DataCollections lpoints in data)
            //{
            //    foreach (Point fPoint in lpoints.Points)
            //    {
            //        if (fPoint.X < Xmin) Xmin = fPoint.X;
            //        if (fPoint.Y < Ymin) Ymin = fPoint.Y;
            //    }


            //    foreach (Point fPoint in lpoints.Points)
            //    {
            //        fPoint.X += -Xmin;
            //        fPoint.Y += -Ymin;
            //    }
            //}
        }






        public enum typeFileLoad { None,PLT,DRL,GBR}
        private typeFileLoad TypeFile = typeFileLoad.None;

        //// value = 1 для спринта
        //// value = 2 остальное
        //private void Read_PLT(int value)
        //{
        //    List<Point> points = new List<Point>();

        //    data.Clear();


        //    this.Text = @"анализ файла";

        //    int index = 0;
        //    int indexList = -1;

        //    StreamReader fs = new StreamReader(tbFile.Text);
        //    string s = fs.ReadLine();


        //    bool needCollectData = false;

        //    points = new List<Point>();

        //    while (s != null)
        //    {
        //        this.Text = @"анализ файла - " + index.ToString();


        //        #region Sprint-Layout 5
        //        if (value == 1)
        //        {
        //            //начальная точка
        //            if (s.Trim().Substring(0, 3) == "PU;")
        //            {
        //                needCollectData = true;
        //                if (points.Count > 2)
        //                {
        //                    //последней точкой будет первая
        //                    points.Add(points[0]);

        //                    data.Add(new DataCollections(points));

        //                }
        //                points = new List<Point>();
        //            }

        //            //завершение отрезка
        //            if (s.Trim().Substring(0, 3) == "PD;")
        //            {
        //                needCollectData = false;

        //                if (points.Count > 2)
        //                {
        //                    //последней точкой будет первая
        //                    points.Add(points[0]);

        //                    data.Add(new DataCollections(points));
                            
        //                }
        //                points = new List<Point>();
        //            }

        //            //продолжение
        //            if (s.Trim().Substring(0, 2) == "PA" && s.Trim().Length > 3)
        //            {
        //                int pos1 = s.IndexOf('A');
        //                int pos2 = s.IndexOf(',');
        //                int pos3 = s.IndexOf(';');

        //                float posX = float.Parse(s.Substring(pos1 + 1, pos2 - pos1 - 1));
        //                float posY = float.Parse(s.Substring(pos2 + 1, pos3 - pos2 - 1));

        //                // Пересчет в милиметры
        //                posX = posX / 40;
        //                posY = posY / 40;

        //                points.Add(new Point(posX, posY));




        //            }





        //        }
        //        #endregion

        //        #region из других прорамм
        //        if (value == 2)
        //        {
        //            //начальная точка
        //            if (s.Trim().Substring(0, 2) == "PU" && s.Trim().Length > 3)
        //            {
        //                int pos1 = s.IndexOf('U');
        //                int pos2 = s.IndexOf(' ');
        //                int pos3 = s.IndexOf(';');

        //                float posX = float.Parse(s.Substring(pos1 + 1, pos2 - pos1 - 1));
        //                float posY = float.Parse(s.Substring(pos2 + 1, pos3 - pos2 - 1));

        //                // Пересчет в милиметры
        //                posX = posX / 40;
        //                posY = posY / 40;

        //                if (indexList == -1)
        //                {
        //                    //первый раз
        //                    indexList++;
        //                }
        //                else
        //                {
        //                    indexList++;
        //                    //checkedListBox1.Items.Add("линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек");
        //                    //trc.Text = "линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек";

        //                    data.Add(new DataCollections(points));
        //                    points = new List<Point>();

        //                  //  treeView1.Nodes.Add(trc);
        //                  //  trc = new TreeNode("");
        //                }
        //                points.Add(new Point(posX, posY));
        //            }



        //            //продолжение
        //            if (s.Trim().Substring(0, 2) == "PD" && s.Trim().Length > 3)
        //            {
        //                int pos1 = s.IndexOf('D');
        //                int pos2 = s.IndexOf(' ');
        //                int pos3 = s.IndexOf(';');

        //                float posX = float.Parse(s.Substring(pos1 + 1, pos2 - pos1 - 1));
        //                float posY = float.Parse(s.Substring(pos2 + 1, pos3 - pos2 - 1));

        //                // Пересчет в милиметры
        //                posX = posX / 40;
        //                posY = posY / 40;

        //                points.Add(new Point(posX, posY));
        //             //   trc.Nodes.Add("Точка - X: " + posX.ToString() + "  Y: " + posY.ToString());

        //            }

        //        }



        //        #endregion

        //        s = fs.ReadLine();
        //        index++;
        //    }

        //    indexList++;
        //    //checkedListBox1.Items.Add("линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек");
        //    //trc.Text = "линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек";
        //    data.Add(new DataCollections(points));
        //    points = new List<Point>();

        //    points.Clear();

        //    //treeView1.Nodes.Add(trc);
        //    //trc = new TreeNode("");


        //    this.Text = @"загружено!!!!!!!!";
        //    fs = null;



        //}

        //private void Read_DRL()
        //{
        //    data.Clear();

        //    List<Point> points = null;

        //    StreamReader fs = new StreamReader(tbFile.Text);
        //    string s = fs.ReadLine();

        //    bool isDataDrill = false; //определение того какие сейчас данные, всё что до строки с % параметры инструментов, после - дырки для сверлений

        //    DataCollections dc = null;

        //    while (s != null)
        //    {
        //        if (s.Trim().Substring(0, 1) == "%") isDataDrill = true;

        //        if (!isDataDrill && s.Trim().Substring(0, 1) == "T") //описание инструмента
        //        {
        //            // На данном этапе в список добавляем интрумент, без точек сверловки
        //            int numInstrument = int.Parse(s.Trim().Substring(1, 2));

        //            int pos1 = s.IndexOf('C');
        //            float diametr = float.Parse(s.Substring(pos1 + 1).Replace('.', ','));

        //            data.Add(new DataCollections(new List<Point>(), new Instrument(numInstrument, diametr)));
        //        }

        //        if (isDataDrill && s.Trim().Substring(0, 1) == "T")
        //        {
        //            //начало сверловки данным инструментом
        //            int numInstrument = int.Parse(s.Trim().Substring(1, 2));

        //            foreach (DataCollections VARIABLE in data)
        //            {
        //                if (VARIABLE.intrument.Numer == numInstrument) dc = VARIABLE;
        //            }
        //        }

        //        if (isDataDrill && s.Trim().Substring(0, 1) == "X")
        //        {
        //            int pos1 = s.IndexOf('X');
        //            int pos2 = s.IndexOf('Y');

        //            //numericUpDown3

        //            //decimal posX = decimal.Parse(s.Substring(pos1 + 1, (int)numericUpDowndrlDigits.Value));
        //            //decimal posY = decimal.Parse(s.Substring(pos2 + 1, (int)numericUpDowndrlDigits.Value));

        //            //float increment = 1;

        //            ////for (int i = 0; i < numericUpDown3.Value; i++)
        //            ////{
        //            ////    increment = increment*10;
        //            ////}

        //            //posX = posX / increment;
        //            //posY = posY / increment;



        //         //   dc.Points.Add(new Point(posX, posY));
        //        }
        //        s = fs.ReadLine();
        //    }

        //    fs = null;

        //    treeView1.Nodes.Clear();

        //    foreach (DataCollections VARIABLE in data)
        //    {
        //        TreeNode trc = new TreeNode("Сверловка - " + VARIABLE.intrument.Diametr.ToString());

        //        foreach (Point VARIABLE2 in VARIABLE.Points)
        //        {
        //            trc.Nodes.Add("Точка - X: " + VARIABLE2.X.ToString() + "  Y: " + VARIABLE2.Y.ToString());
        //        }
        //        treeView1.Nodes.Add(trc);
        //    }

        //    //TreeNode trc = new TreeNode("");
        //    //this.Text = @"анализ файла";

        //    //int index = 0;
        //    //int indexList = -1;



        //}



        void Swap(ref int p1, ref int p2)
        {
            int p3 = p1;
            int p4 = p2;
            p1 = p4;
            p2 = p3;
        }

        /// <summary>
        /// Заполнение в матрицы окружностью
        /// </summary>
        /// <param name="arrayPoint">Массив в котором делать</param>
        /// <param name="x0">центр круга по оси Х</param>
        /// <param name="y0">центр круга по оси Y</param>
        /// <param name="radius">радиус круга</param>
        /// <param name="setvalue">какое значение записывать в матрицу, если необходимо нарисовать точку окружности</param>
        /// <param name="needFill">необходимость заполнить внутренность круга</param>
        void BresenhamCircle(ref byte[,] arrayPoint,  int x0, int y0, int radius,byte setvalue = 4, bool needFill = false)
        {
            int tmpradius = radius;

            while (tmpradius > 0)
            {

                int x = tmpradius;
                //int x = radius;
                int y = 0;
                int radiusError = 1 - x;
                while (x >= y)
                {
                    arrayPoint[x + x0, y + y0] = setvalue;
                    arrayPoint[y + x0, x + y0] = setvalue;
                    arrayPoint[-x + x0, y + y0] = setvalue;
                    arrayPoint[-y + x0, x + y0] = setvalue;
                    arrayPoint[-x + x0, -y + y0] = setvalue;
                    arrayPoint[-y + x0, -x + y0] = setvalue;
                    arrayPoint[x + x0, -y + y0] = setvalue;
                    arrayPoint[y + x0, -x + y0] = setvalue;
                    y++;
                    if (radiusError < 0)
                    {
                        radiusError += 2 * y + 1;
                    }
                    else
                    {
                        x--;
                        radiusError += 2 * (y - x + 1);
                    }
                }

                tmpradius--;
           }
        }

        void BresenhamLine(ref byte[,] arrayPoint, int x0, int y0, int x1, int y1, typeSpline _Splane)
        {
            //матрицу сплайна
            byte[,] spArray = new byte[1, 1];
            spArray[0, 0] = 1; //просто обычная точка
            
            int sizeMatrixX = 1;
            int sizeMatrixY = 1;

            int sizeMatrixdX = 1;
            int sizeMatrixdY = 1;
            
            //но если это круг
            if (_Splane.aperture == Apertures.C_circle)
            {
                sizeMatrixX = (int)(_Splane.size1 * 100);
                sizeMatrixY = sizeMatrixX;

                if (checkBox2.Checked)
                {
                    sizeMatrixX = 1;
                    sizeMatrixY = 1;

                }


                spArray = new byte[sizeMatrixX + 1, sizeMatrixY + 1];

                if (checkBox2.Checked)
                {
                    BresenhamCircle(ref spArray, sizeMatrixX / 2, sizeMatrixY / 2, sizeMatrixX / 2, 1, false); 
                }
                else
                {
                    BresenhamCircle(ref spArray, sizeMatrixX / 2, sizeMatrixY / 2, sizeMatrixX / 2, 1, true);
                }

                
            }

            //TODO: отладка с выводом в txt формат

            //string debugstr = "";
            //for (int dyy = 0; dyy < sizeMatrixY; dyy++)
            //{
            //    for (int dxx = 0; dxx < sizeMatrixX; dxx++)
            //    {
            //        debugstr += spArray[dxx, dyy].ToString();
            //    }
            //    debugstr += "\n";
            //}
            



            //if (x0 == x1 && y0 == y1) return;

            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
            // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию для красоты
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                int possX = (steep ? y : x);
                int possY = (steep ? x : y);
                arrayPoint[possX, possY] = 2; //TODO: это нужно УБРАТЬ!!! Не забываем вернуть координаты на место

                //а тут нужно наложить матрицу на массив данных
                for (int xxx = 0; xxx < sizeMatrixX; xxx++)
                {
                    for (int yyy = 0; yyy < sizeMatrixY; yyy++)
                    {
                        if (spArray[xxx, yyy] != 0)
                        {
                            int pointX = possX + xxx - (sizeMatrixX / 2);
                            int pointY = possY + yyy - (sizeMatrixY / 2);

                            arrayPoint[pointX, pointY] = 2; // Не забываем вернуть координаты на место
                            //arrayPoint[possX + xxx - (sizeMatrixdX/2), possY + yyy - (sizeMatrixdY/2)] = 2; // Не забываем вернуть координаты на место
                        }
                    }
                }
                


                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }


        private Polygons subjects = new Polygons();
        private Polygons clips = new Polygons();
        private Polygons solution = new Polygons();
        private Bitmap mybitmap;

        /// <summary>
        /// Анализ данных gerber
        /// </summary>
        private void Read_GBR()
        {
            // Шаг 1: извлечем в удобном виде все данные
            
            // ВАЖНО!!! для упрощения ряда задач, координаты будут иметь тип INT, в котором последние 3 числа будут относиться к дробной части.
            // и 1мм будет иметь значение 1000, а 4.56 мм = 4560
            
            #region Загрузка данных из файла
            
            
            GerberData grb = new GerberData();

            int numberSplineNow = -1;

            int X = 0;
            int Y = 0;


            string sType_old = "";

            StreamReader fs = new StreamReader(tbFile.Text);
            string s = fs.ReadLine().Trim();
            while (s != null)
            {
                s = s.Trim();

                if (s == "")
                {
                    s = fs.ReadLine();
                    continue; // пропутим пустую строку
                }

                if (s == "*")
                {
                    s = fs.ReadLine();
                    continue; // пропутим пустую строку
                }
                if (s.Length == 1)
                {
                    s = fs.ReadLine();
                    continue; // пропутим пустую строку
                }

                // Извлечем тип единицы измерения
                if (s.Length > 3 && s.Substring(0, 3) == "%MO")
                {
                    grb.UnitsType = s.Substring(3, 2);
                }

                //извлечем параметры сплайна
                if (s.Length > 3 && s.Substring(0, 3) == "%AD")
                {
                    int numb = int.Parse(s.Substring(4, 2));
                    string letterAperture = s.Substring(6, 1);

                    //т.к. сплайны бывают разные, то и метод парсинга разный

                    if (letterAperture == "C") //если окружность
                    {
                        int sstart = s.IndexOf(",");
                        int sEnd = s.IndexOf("*");
                        string sValue = s.Substring(sstart + 1, sEnd - sstart - 1).Replace('.', ','); //TODO: добавить корректировку символа разделителя

                        decimal DsizeRound = decimal.Parse(sValue);

                        //теперь значение приведем в НАШ формат представления чисел
                        int sizeRound = (int)(DsizeRound * 1000);

                        grb.typeSplines.Add(new typeSpline(numb, Apertures.C_circle, sizeRound));
                    }

                    if (letterAperture == "R") //если прямоугольник
                    {
                        int sstart1 = s.IndexOf(",");
                        int sstart2 = s.IndexOf("X");
                        int sEnd = s.IndexOf("*");

                        string sValueX = s.Substring(sstart1 + 1, sstart2 - sstart1 - 1).Replace('.', ','); //TODO: добавить корректировку символа разделителя
                        string sValueY = s.Substring(sstart2 + 1, sEnd - sstart2 - 1).Replace('.', ',');    //TODO: добавить корректировку символа разделителя

                        decimal DsizeХ = decimal.Parse(sValueX);
                        decimal DsizeY = decimal.Parse(sValueY);

                        //теперь значение приведем в НАШ формат представления чисел
                        int sizeХ = (int)(DsizeХ * 1000);
                        int sizeY = (int)(DsizeY * 1000);

                        grb.typeSplines.Add(new typeSpline(numb, Apertures.R_rectangle, sizeХ, sizeY));
                    }

                    if (letterAperture == "O") //если овал
                    {
                        int sstart1 = s.IndexOf(",");
                        int sstart2 = s.IndexOf("X");
                        int sEnd = s.IndexOf("*");

                        string sValueX = s.Substring(sstart1 + 1, sstart2 - sstart1 - 1).Replace('.', ','); //TODO: добавить корректировку символа разделителя
                        string sValueY = s.Substring(sstart2 + 1, sEnd - sstart2 - 1).Replace('.', ',');    //TODO: добавить корректировку символа разделителя

                        decimal DsizeХ = decimal.Parse(sValueX);
                        decimal DsizeY = decimal.Parse(sValueY);

                        //теперь значение приведем в НАШ формат представления чисел
                        int sizeХ = (int)(DsizeХ * 1000);
                        int sizeY = (int)(DsizeY * 1000);

                        grb.typeSplines.Add(new typeSpline(numb, Apertures.O_obround, sizeХ, sizeY));
                    }

                }

                //извлечение номера сплайна, которым будет рисовать
                if (s.Substring(0, 1) == "D")
                {
                    int posSYMBOL = s.IndexOf("*");
                    numberSplineNow = int.Parse(s.Substring(1, posSYMBOL-1));
                }

                //извлечение движения
                if (s.Substring(0, 1) == "X" || s.Substring(0, 1) == "Y")
                {
                    int posX = s.IndexOf("X");
                    int posY = s.IndexOf("Y");
                    int posD = s.IndexOf("D"); // "D" - может и не быть

                    //по звездочке определять окончание
                    int posS = s.IndexOf("*");

                    string sType = "";

                    if (posD == -1)
                    {
                        sType = sType_old;
                        posD = posS;
                    }
                    else
                    {
                        sType = s.Substring(posD, posS - posD);

                        sType_old = sType;
                    }



                    if (posX != -1)
                    {
                        if (posY != -1) X = int.Parse(s.Substring(posX + 1, posY - posX - 1));
                        else X = int.Parse(s.Substring(posX + 1, posD - posX - 1));
                    }

                    if (posY != -1)
                    {
                        Y = int.Parse(s.Substring(posY + 1, posD-posY-1));
                    }


                    //теперь значение приведем в НАШ формат представления чисел, где дробная часть это 3 последних числа в значении типа INT

                    //TODO: тут реализовать
                    int xdig = int.Parse("1".PadRight(grb.countPdigX+1, '0'));
                    int ydig = int.Parse("1".PadRight(grb.countPdigY+1, '0'));


                    int iposX = (X* 1000) / xdig ;
                    int iposY = (Y* 1000 ) / ydig;

                    grb.points.Add(new grbPoint(iposX, iposY, sType, numberSplineNow));
                }

                // Тут информация о способе лисования, и параметрах указывающих как форматировать значения X,Y для поличения координат в мм.
                if (s.Length > 4 && s.Trim().ToUpper().Substring(0, 5) == @"%FSLA")
                {
                    //параметры разбора чисел
                    int pos1 = s.IndexOf('X');
                    int pos2 = s.IndexOf('Y');
                    //int pos3 = s.IndexOf('*');

                    grb.countDigitsX = int.Parse(s.Substring(pos1 + 1, 1));
                    grb.countPdigX = int.Parse(s.Substring(pos1 + 2, 1));
                    grb.countDigitsY = int.Parse(s.Substring(pos2 + 1, 1));
                    grb.countPdigY = int.Parse(s.Substring(pos2 + 2, 1));
                }


                //поищем звездочку далее, т.к. бывают ситуации когда в одной строке сразу несколько данных, разделенных звездочкой

                int posSymbDeliv = s.IndexOf('*');

                if (posSymbDeliv < s.Length)
                {
                    s = s.Substring(posSymbDeliv+1);
                }
                else
                {
                    s = fs.ReadLine();
                }                
            }
            fs = null;

            #endregion

            // Шаг 2: сконвертируем в области наборы точек

            subjects.Clear();
            clips.Clear();

            IntPoint oldPoint = new IntPoint();
            IntPoint newPoint = new IntPoint();

            foreach (grbPoint VARIABLE in grb.points)
            {

                //int xi = VARIABLE.X;
                //int yi = VARIABLE.Y;

                //int xdig = int.Parse("1".PadRight(grb.countPdigX, '0'));
                //int ydig = int.Parse("1".PadRight(grb.countPdigY, '0'));

                //double xd = ((double)xi) / xdig;
                //double yd = ((double)yi) / ydig;

                //double xd = ((double)xi) ;
                //double yd = ((double)yi) ;





                //newPoint = new DPoint(xd + (double)numericUpDownXX.Value, yd + (double)numericUpDownYY.Value);
                newPoint = new IntPoint(VARIABLE.X + (double)(numericUpDownXX.Value*100), VARIABLE.Y + (double)(numericUpDownYY.Value*100));


                if (VARIABLE.typePoint == "D2" || VARIABLE.typePoint == "D02")
                {
                    oldPoint = newPoint;
                    continue;
                }

                if (VARIABLE.typePoint == "D1" || VARIABLE.typePoint == "D01")
                {
                    //тут создадим массив точек, но в начале получим характеристики слайна (объекта которым засвечивается траектория)
                    typeSpline splaynNow = new typeSpline(1, Apertures.C_circle, 1, 1); //по умолчанию просто точка

                    foreach (typeSpline spl in grb.typeSplines)
                    {
                        if (spl.number == VARIABLE.numberSplane)
                        {
                            splaynNow = spl;
                            break;
                        }
                    }

                    //применяем круглый сплайн
                    if (splaynNow.aperture == Apertures.C_circle)
                    {
                        int sized = splaynNow.size1/2;


                        // добавим 3 области точек, 1-я и 2-я окружности в районе начала и окончания, и 3-я это прямоугольник проводника
                        

                        Polygon subj = new Polygon(1);
                        Polygon clip = new Polygon(1);
                        for (int i = 0; i < 360; i++)
                        {
                            DPoint tmppoint = getPosPointFromAngle(newPoint, (double)sized, (double)i);
                            subj.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                            clip.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                        }
                        subjects.Add(subj);
                        clips.Add(clip);


                        Polygon subj1 = new Polygon(1);
                        Polygon clip1 = new Polygon(1);
                        for (int i = 0; i < 360; i++)
                        {
                            DPoint tmppoint = getPosPointFromAngle(oldPoint, (double)sized, (double)i);
                            subj1.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                            clip1.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                        }
                        subjects.Add(subj1);
                        clips.Add(clip1);

                        Polygon subj2 = new Polygon(1);
                        Polygon clip2 = new Polygon(1);


                        

                        double angle = getAngle(oldPoint, newPoint);
                        

                        if (angle >= 0)
                        {

                            angle = 90 - angle;


                        }
                        else
                        {
                            angle =  (-angle) +90 ;
                            

                        }


                        DPoint n1 = getPosPointFromAngle(newPoint, sized, angle + 90);
                        DPoint n2 = getPosPointFromAngle(newPoint, sized, angle -90);
                        DPoint n3 = getPosPointFromAngle(oldPoint, sized, angle + 90);
                        DPoint n4 = getPosPointFromAngle(oldPoint, sized, angle -90);



                        subj2.Add(new IntPoint(n1.X, n1.Y));
                        subj2.Add(new IntPoint(n3.X, n3.Y));  
                        subj2.Add(new IntPoint(n4.X, n4.Y));  
                        subj2.Add(new IntPoint(n2.X, n2.Y));


                        clip2.Add(new IntPoint(n1.X, n1.Y));
                        clip2.Add(new IntPoint(n3.X, n3.Y));
                        clip2.Add(new IntPoint(n4.X, n4.Y));
                        clip2.Add(new IntPoint(n2.X, n2.Y));


                        subjects.Add(subj2);
                        clips.Add(clip2);

                    }


                    if (splaynNow.aperture == Apertures.R_rectangle)
                    {
                        int sized1 = splaynNow.size1 / 2;
                        int sized2 = splaynNow.size2 / 2;


                        DPoint n1 = new DPoint(newPoint.X - (sized1/2), newPoint.Y - (sized2/2));
                        DPoint n2 = new DPoint(newPoint.X - (sized1 / 2), newPoint.Y + (sized2 / 2));
                        DPoint n3 = new DPoint(newPoint.X + (sized1 / 2), newPoint.Y - (sized2 / 2));
                        DPoint n4 = new DPoint(newPoint.X + (sized1 / 2), newPoint.Y + (sized2 / 2));


                        Polygon subj = new Polygon(1);
                        Polygon clip = new Polygon(1);

                        subj.Add(new IntPoint(n1.X, n1.Y));
                        subj.Add(new IntPoint(n2.X, n2.Y));
                        subj.Add(new IntPoint(n3.X, n3.Y));
                        subj.Add(new IntPoint(n4.X, n4.Y));

                        clip.Add(new IntPoint(n1.X, n1.Y));
                        clip.Add(new IntPoint(n2.X, n2.Y));
                        clip.Add(new IntPoint(n3.X, n3.Y));
                        clip.Add(new IntPoint(n4.X, n4.Y));


                        subjects.Add(subj);
                        clips.Add(clip);

                        //for (int ix = -sized1; ix < sized1; ix++)
                        //{
                        //    for (int iy = -sized2; iy < sized2; iy++)
                        //    {
                        //        arrayPoint[newX + ix, newY + iy] = 1;
                        //    }
                        //}
                    }







                    oldPoint = newPoint;
                    continue;

                }





                if (VARIABLE.typePoint == "D3" || VARIABLE.typePoint == "D03")
                {
                    //continue;
                    //тут создадим массив точек, но в начале получим характеристики слайна (объекта которым засвечивается траектория)
                    typeSpline splaynNow = new typeSpline(1, Apertures.C_circle, 1, 1); //по умолчанию просто точка

                    foreach (typeSpline spl in grb.typeSplines)
                    {
                        if (spl.number == VARIABLE.numberSplane)
                        {
                            splaynNow = spl;
                            break;
                        }
                    }



                    if (splaynNow.aperture == Apertures.C_circle)
                    {
                        int sized = splaynNow.size1/2;


                        sized = sized;

                        Polygon subj = new Polygon(1);
                        Polygon clip = new Polygon(1);
                        for (int i = 0; i < 360; i++)
                        {
                            DPoint tmppoint = getPosPointFromAngle(newPoint, (double)sized, (double)i);
                            subj.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                            clip.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                        }
                        subjects.Add(subj);
                        clips.Add(clip);


                        Polygon subj1 = new Polygon(1);
                        Polygon clip1 = new Polygon(1);
                        for (int i = 0; i < 360; i++)
                        {
                            DPoint tmppoint = getPosPointFromAngle(oldPoint, (double)sized, (double)i);
                            subj1.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                            clip1.Add(new IntPoint(tmppoint.X, tmppoint.Y));
                        }
                        subjects.Add(subj1);
                        clips.Add(clip1);




                    }

                        if (splaynNow.aperture == Apertures.R_rectangle)
                        {
                            int sized1 = splaynNow.size1 / 2;
                            int sized2 = splaynNow.size2 / 2;


                            DPoint n1 = new DPoint(newPoint.X - (sized1 / 2), newPoint.Y - (sized2 / 2));
                            DPoint n4 = new DPoint(newPoint.X - (sized1 / 2), newPoint.Y + (sized2 / 2));
                            DPoint n2 = new DPoint(newPoint.X + (sized1 / 2), newPoint.Y - (sized2 / 2));
                            DPoint n3 = new DPoint(newPoint.X + (sized1 / 2), newPoint.Y + (sized2 / 2));


                            Polygon subj = new Polygon(1);
                            Polygon clip = new Polygon(1);

                            subj.Add(new IntPoint(n1.X, n1.Y));
                            subj.Add(new IntPoint(n2.X, n2.Y));
                            subj.Add(new IntPoint(n3.X, n3.Y));
                            subj.Add(new IntPoint(n4.X, n4.Y));

                            clip.Add(new IntPoint(n1.X, n1.Y));
                            clip.Add(new IntPoint(n2.X, n2.Y));
                            clip.Add(new IntPoint(n3.X, n3.Y));
                            clip.Add(new IntPoint(n4.X, n4.Y));


                            subjects.Add(subj);
                            clips.Add(clip);



                        }

                //        if (splaynNow.aperture == Apertures.O_obround)
                //        {
                //            //TODO: для овала....
                //            //овал это линия из круглых сплейнов

                //            int sized1 = (int)(splaynNow.size1 * 100);
                //            int sized2 = (int)(splaynNow.size2 * 100);

                //            if (sized1 > sized2)
                //            {
                //                //овал горизонтальный
                //                int oX1 = newX - (sized1 / 2) + (sized2 / 2);
                //                int oY1 = newY;
                //                int oX2 = newX + (sized1 / 2) - (sized2 / 2);
                //                int oY2 = newY;

                //                typeSpline tps;

                //                if (checkBox2.Checked)
                //                {
                //                    tps = new typeSpline(0, Apertures.C_circle, 1);

                //                }
                //                else
                //                {
                //                    tps = new typeSpline(0, Apertures.C_circle, sized2 / 100);
                //                }



                //                BresenhamLine(ref arrayPoint, oX1, oY1, oX2, oY2, tps);
                //            }
                //            else
                //            {
                //                //овал вертикальный
                //                int oX1 = newX;
                //                int oY1 = newY - (sized2 / 2) + (sized1 / 2);
                //                int oX2 = newX;
                //                int oY2 = newY + (sized2 / 2) - (sized1 / 2);
                //                typeSpline tps;
                //                if (checkBox2.Checked)
                //                {
                //                    tps = new typeSpline(0, Apertures.C_circle, 1);

                //                }
                //                else
                //                {
                //                    tps = new typeSpline(0, Apertures.C_circle, sized1 / 100);
                //                }


                //                BresenhamLine(ref arrayPoint, oX1, oY1, oX2, oY2, tps);
                //            }
                //        }

                }
 





            //    BresenhamLine(ref arrayPoint, oldX, oldY, newX, newY, splaynNow);







                //newX = VARIABLE.X;
                //newY = VARIABLE.Y;








                //DoublePoint p1 = new DoublePoint(0D, 0D);
                //DoublePoint p2 = new DoublePoint(10D, 10D);

                //double angle = getAngle(p1, p2);

                //DoublePoint n1 = getPosPointFromAngle(p1, 2D, angle + 90);
                //DoublePoint n2 = getPosPointFromAngle(p1, 2D, angle + 180);


            }













            // ---- СТАРЫЙ КОД ----------------
          #region oldcode





          // Вычислим границы данных, и уменьшим размерчик
            //grb.CalculateGatePoints(10);

            this.Text = "Наполнение массива";

            //byte[,] arrayPoint = new byte[grb.X_max + 1, grb.Y_max + 1];
            
            //int newX = 0;
            //int newY = 0;

            //int oldX = 0;
            //int oldY = 0;

            //typeSpline splaynNow = new typeSpline(1,Apertures.C_circle,1,1); //по умолчанию просто точка

            //foreach (grbPoint VARIABLE in grb.points)
            {
            //    //дополнительно получим характеристики сплайна
            //    foreach (typeSpline spl in grb.typeSplines)
            //    {
            //        if (spl.number == VARIABLE.numberSplane)
            //        {
            //            splaynNow = spl;
            //            break;
            //        }
            //    }

            //    newX = VARIABLE.X;
            //    newY = VARIABLE.Y;

            //    if (VARIABLE.typePoint == "D1")
            //    {
            //        BresenhamLine(ref arrayPoint, oldX, oldY, newX, newY, splaynNow);

            //        oldX = newX;
            //        oldY = newY;

            //    }

            //    if (VARIABLE.typePoint == "D2")
            //    {
            //        oldX = newX;
            //        oldY = newY;
            //    }

            //    if (VARIABLE.typePoint == "D3")
            //    {
            //        if (splaynNow.aperture == Apertures.C_circle)
            //        {
            //            int sized = (int)(splaynNow.size1 * 100);

            //            if (checkBox2.Checked)
            //            {
            //                BresenhamCircle(ref arrayPoint, newX, newY, sized / 2, 1, false);

            //            }
            //            else
            //            {
            //                BresenhamCircle(ref arrayPoint, newX, newY, sized / 2, 1, true); 
            //            }

                        
            //        }

            //        if (splaynNow.aperture == Apertures.R_rectangle)
            //        {
            //            int sized1 = (int)(splaynNow.size1 * 100) / 2;
            //            int sized2 = (int)(splaynNow.size2 * 100) / 2;

            //            for (int ix = -sized1; ix < sized1; ix++)
            //            {
            //                for (int iy = -sized2; iy < sized2; iy++)
            //                {
            //                    arrayPoint[newX + ix, newY + iy] = 1;
            //                }
            //            }
            //        }

            //        if (splaynNow.aperture == Apertures.O_obround)
            //        {
            //            //TODO: для овала....
            //            //овал это линия из круглых сплейнов

            //            int sized1 = (int)(splaynNow.size1 * 100);
            //            int sized2 = (int)(splaynNow.size2 * 100);

            //            if (sized1 > sized2)
            //            {
            //                //овал горизонтальный
            //                int oX1 = newX - (sized1 / 2) + (sized2 / 2);
            //                int oY1 = newY;
            //                int oX2 = newX + (sized1 / 2) - (sized2 / 2);
            //                int oY2 = newY;

            //                typeSpline tps;

            //                if (checkBox2.Checked)
            //                {
            //                    tps = new typeSpline(0, Apertures.C_circle, 1);

            //                }
            //                else
            //                {
            //                    tps = new typeSpline(0, Apertures.C_circle, sized2 / 100);
            //                }


                            
            //                BresenhamLine(ref arrayPoint, oX1, oY1, oX2, oY2, tps);
            //            }
            //            else
            //            {
            //                //овал вертикальный
            //                int oX1 = newX;
            //                int oY1 = newY - (sized2 / 2) + (sized1 / 2);
            //                int oX2 = newX;
            //                int oY2 = newY + (sized2 / 2) - (sized1 / 2);
            //                typeSpline tps;
            //                if (checkBox2.Checked)
            //                {
            //                    tps = new typeSpline(0, Apertures.C_circle, 1);

            //                }
            //                else
            //                {
            //                    tps = new typeSpline(0, Apertures.C_circle, sized1 / 100);
            //                }

                             
            //                BresenhamLine(ref arrayPoint, oX1, oY1, oX2, oY2, tps);
            //            }
            //        }

            //    }
            }

            this.Text = "Помещение в BMP";

            //////Bitmap bmp = new Bitmap(grb.X_max + 1, grb.Y_max + 1,PixelFormat.Format16bppArgb1555);

            //////for (int x = 0; x < grb.X_max; x++)
            //////{
            //////    for (int y = 0; y < grb.Y_max; y++)
            //////    {
            //////        if (arrayPoint[x, y] != 0)
            //////        {
            //////            bmp.SetPixel(x,y,Color.Black);
            //////        }
            //////    }
            //////}

            //////bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //////bmp.Save(@"f:\sample.png",ImageFormat.Png);

            this.Text = "Готово!";

            //arrayPoint = null;

            //System.Diagnostics.Process proc = System.Diagnostics.Process.Start(@"mspaint.exe", @"d:\sample.bmp"); //Запускаем блокнот
            //proc.WaitForExit();//и ждем, когда он завершит свою работу


          #endregion






           // Cursor.Current = Cursors.WaitCursor;
          //  try
          //  {






            #region new




            /*

        //вот тут уже имеем перечень векторов

        using (Graphics newgraphic = Graphics.FromImage(mybitmap))
        using (GraphicsPath path = new GraphicsPath())
        {
          newgraphic.SmoothingMode = SmoothingMode.AntiAlias;
          newgraphic.Clear(Color.White);
          if (rbNonZero.Checked)
            path.FillMode = FillMode.Winding;

          //draw subjects ...
          foreach (Polygon pg in subjects)
          {
            PointF[] pts = PolygonToPointFArray(pg, scale);
            path.AddPolygon(pts);
            pts = null;
          }
          using (Pen myPen = new Pen(Color.FromArgb(196, 0xC3, 0xC9, 0xCF), (float)0.6))
          using (SolidBrush myBrush = new SolidBrush(Color.FromArgb(127, 0xDD, 0xDD, 0xF0)))
          {
            newgraphic.FillPath(myBrush, path);
            newgraphic.DrawPath(myPen, path);
            path.Reset();

            //draw clips ...
            if (rbNonZero.Checked)
              path.FillMode = FillMode.Winding;
            foreach (Polygon pg in clips)
            {
              PointF[] pts = PolygonToPointFArray(pg, scale);
              path.AddPolygon(pts);
              pts = null;
            }
            myPen.Color = Color.FromArgb(196, 0xF9, 0xBE, 0xA6);
            myBrush.Color = Color.FromArgb(127, 0xFF, 0xE0, 0xE0);
            newgraphic.FillPath(myBrush, path);
            newgraphic.DrawPath(myPen, path);

            //do the clipping ...
            if ((clips.Count > 0 || subjects.Count > 0) && !rbNone.Checked)
            {
              Polygons solution2 = new Polygons();
              Clipper c = new Clipper();
              c.AddPaths(subjects, PolyType.ptSubject, true);
              c.AddPaths(clips, PolyType.ptClip, true);
              solution.Clear();
#if UsePolyTree
              bool succeeded = c.Execute(GetClipType(), solutionTree, GetPolyFillType(), GetPolyFillType());
              //nb: we aren't doing anything useful here with solutionTree except to show
              //that it works. Convert PolyTree back to Polygons structure ...
              Clipper.PolyTreeToPolygons(solutionTree, solution);
#else
              bool succeeded = c.Execute(GetClipType(), solution, GetPolyFillType(), GetPolyFillType());
#endif
              if (succeeded)
              {
                //SaveToFile("solution", solution);
                myBrush.Color = Color.Black;
                path.Reset();

                //It really shouldn't matter what FillMode is used for solution
                //polygons because none of the solution polygons overlap. 
                //However, FillMode.Winding will show any orientation errors where 
                //holes will be stroked (outlined) correctly but filled incorrectly  ...
                path.FillMode = FillMode.Winding;

                //or for something fancy ...

                if (nudOffset.Value != 0)
                {
                  ClipperOffset co = new ClipperOffset();
                  co.AddPaths(solution, JoinType.jtRound, EndType.etClosedPolygon);
                  co.Execute(ref solution2, (double)nudOffset.Value * scale);
                }
                else
                  solution2 = new Polygons(solution);

                foreach (Polygon pg in solution2)
                {
                  PointF[] pts = PolygonToPointFArray(pg, scale);
                  if (pts.Count() > 2)
                    path.AddPolygon(pts);
                  pts = null;
                }
                myBrush.Color = Color.FromArgb(127, 0x66, 0xEF, 0x7F);
                myPen.Color = Color.FromArgb(255, 0, 0x33, 0);
                myPen.Width = 1.0f;
                newgraphic.FillPath(myBrush, path);
                newgraphic.DrawPath(myPen, path);

                //now do some fancy testing ...
                using (Font f = new Font("Arial", 8))
                using (SolidBrush b = new SolidBrush(Color.Navy))
                {
                  double subj_area = 0, clip_area = 0, int_area = 0, union_area = 0;
                  c.Clear();
                  c.AddPaths(subjects, PolyType.ptSubject, true);
                  c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                  foreach (Polygon pg in solution2)
                    subj_area += Clipper.Area(pg);
                  c.Clear();
                  c.AddPaths(clips, PolyType.ptClip, true);
                  c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                  foreach (Polygon pg in solution2)
                    clip_area += Clipper.Area(pg);
                  c.AddPaths(subjects, PolyType.ptSubject, true);
                  c.Execute(ClipType.ctIntersection, solution2, GetPolyFillType(), GetPolyFillType());
                  foreach (Polygon pg in solution2)
                    int_area += Clipper.Area(pg);
                  c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                  foreach (Polygon pg in solution2)
                    union_area += Clipper.Area(pg);

                  using (StringFormat lftStringFormat = new StringFormat())
                  using (StringFormat rtStringFormat = new StringFormat())
                  {
                    lftStringFormat.Alignment = StringAlignment.Near;
                    lftStringFormat.LineAlignment = StringAlignment.Near;
                    rtStringFormat.Alignment = StringAlignment.Far;
                    rtStringFormat.LineAlignment = StringAlignment.Near;
                    Rectangle rec = new Rectangle(pictureBox1.ClientSize.Width - 108,
                                     pictureBox1.ClientSize.Height - 116, 104, 106);
                    newgraphic.FillRectangle(new SolidBrush(Color.FromArgb(196, Color.WhiteSmoke)), rec);
                    newgraphic.DrawRectangle(myPen, rec);
                    rec.Inflate(new Size(-2, 0));
                    newgraphic.DrawString("Areas", f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 14));
                    newgraphic.DrawString("subj: ", f, b, rec, lftStringFormat);
                    newgraphic.DrawString((subj_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 12));
                    newgraphic.DrawString("clip: ", f, b, rec, lftStringFormat);
                    newgraphic.DrawString((clip_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 12));
                    newgraphic.DrawString("intersect: ", f, b, rec, lftStringFormat);
                    newgraphic.DrawString((int_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 12));
                    newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 10));
                    newgraphic.DrawString("s + c - i: ", f, b, rec, lftStringFormat);
                    newgraphic.DrawString(((subj_area + clip_area - int_area) / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 10));
                    newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 10));
                    newgraphic.DrawString("union: ", f, b, rec, lftStringFormat);
                    newgraphic.DrawString((union_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                    rec.Offset(new Point(0, 10));
                    newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                  }
                }
              } //end if succeeded
            } //end if something to clip
            pictureBox1.Image = mybitmap;
          }
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }

             * 
             * 
             * 
            */


            #endregion




        }




        //private void Read_File()
        //{
        //    //if (comboBoxTypeFile.Text == @"PLT (Sprint-layout)")
        //    //{
        //    //    //Read_PLT(1);
        //    //    //TypeFile = typeFileLoad.PLT;
        //    //}

        //    //if (comboBoxTypeFile.Text == @"PLT")
        //    //{
        //    //    //Read_PLT(2);
        //    //    //TypeFile = typeFileLoad.PLT;
        //    //}


        //    //if (comboBoxTypeFile.Text.Substring(0, 3) == "DRL")
        //    //{
        //    //    Read_DRL();
        //    //    TypeFile = typeFileLoad.DRL;
        //    //}

        //    //if (comboBoxTypeFile.Text.Substring(0, 3) == "GBR")
        //    //{
        //    //    Read_GBR();
        //    //    TypeFile = typeFileLoad.GBR;
        //    //}

        //    ////перезаполним дерево данных
        //    //treeView1.Nodes.Clear();
        //    //int numberCurve = 1;

        //    //foreach (DataCollections VARIABLE1 in data)
        //    //{
        //    //    if (VARIABLE1.Points.Count == 0) continue;

        //    //    TreeNode trc = new TreeNode("Кривая: " + numberCurve.ToString());
        //    //    foreach (Point VARIABLE2 in VARIABLE1.Points)
        //    //    {
        //    //        trc.Nodes.Add("Точка - X: " + VARIABLE2.X.ToString() + "  Y: " + VARIABLE2.Y.ToString());
                    
        //    //    }

        //    //    treeView1.Nodes.Add(trc);
                
        //    //}


        //   // 
        //   // trc.Nodes.Add("Точка - X: " + posX.ToString() + "  Y: " + posY.ToString());

        //    //int pos1 = s.IndexOf('U');
        //    //int pos2 = s.IndexOf(' ');
        //    //int pos3 = s.IndexOf(';');

        //    //float posX = float.Parse(s.Substring(pos1 + 1, pos2 - pos1 - 1));
        //    //float posY = float.Parse(s.Substring(pos2 + 1, pos3 - pos2 - 1));

        //    //// Пересчет в милиметры
        //    //posX = posX / 40;
        //    //posY = posY / 40;

        //    //if (indexList == -1)
        //    //{
        //    //    //первый раз
        //    //    indexList++;
        //    //}
        //    //else
        //    //{
        //    //    indexList++;
        //    //    //checkedListBox1.Items.Add("линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек");
        //    //    trc.Text = "линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек";

        //    //    data.Add(new DataCollections(points));
        //    //    points = new List<Point>();

        //    //    treeView1.Nodes.Add(trc);
        //    //    trc = new TreeNode("");
        //    //}
        //    //points.Add(new Point(posX, posY));


        //}

        private void btSelectFile_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = tbFile.Text;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = ofd.FileName;

                if (typeNow == TypeFileImport.DRL)
                {
                    _DRLForm.FileNAME = ofd.FileName;
                }

                if (typeNow == TypeFileImport.PLT)
                {
                    _PLTForm.FileNAME = ofd.FileName;
                }
            }

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            //Read_File();
        }

        private void genGKode_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            // сформируем из сверловки данные
            foreach (DRLTOOLS drlt in ArrayData.DRL_tools) //цикл по линиям
            {
                textBox1.AppendText("M6 T" + drlt.numTool.ToString() + " D" + drlt.DiametrTools.ToString() + Environment.NewLine);

                foreach (DRLPoint pnt in drlt.points)
                {
                    textBox1.AppendText("g0 x" + pnt.X + " y" + pnt.Y + " z10" + Environment.NewLine);  //позиционирование над точкой
                    textBox1.AppendText("g1 z0" + Environment.NewLine);//сверление
                    textBox1.AppendText("g0 z10" + Environment.NewLine); //поднятие инструмента
                }
            }
            


            // контур из PLT
            foreach (SegmentCollections seg in ArrayData.segments) //цикл по линиям
            {
                bool firstpoint = true;

                foreach (PointF fPoint in seg.Points)
                {
                    if (firstpoint)
                    {
                        firstpoint = false;

                        textBox1.AppendText("g0 x" + (fPoint.X * (float)numericUpDown3.Value) + " y" + (fPoint.Y * (float)numericUpDown3.Value) + " z10" + Environment.NewLine);
                        textBox1.AppendText("g1 z0" + Environment.NewLine);
                    }
                    else
                    {
                        textBox1.AppendText("g1 x" + (fPoint.X * (float)numericUpDown3.Value) + " y" + (fPoint.Y * (float)numericUpDown3.Value) + " z0" + Environment.NewLine);
                    }
                }
                textBox1.AppendText("g0 z10" + Environment.NewLine);
            }
            textBox1.AppendText("g0 z10" + Environment.NewLine);
          




        }


        #region Форматы импортируемых данных

        private enum TypeFileImport
        {
            DRL, PLT, GRB, DXF, none
        }

        /*
            PLT1 - Sprint-layout
            PLT2 - CorelDraw
            DRL  - сверловка
            GBR  - gerber
            DXF  - AutoCad файлы
         */

        private TypeFileImport typeNow = TypeFileImport.none;

        #endregion




        private void comboBoxTypeFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPageMain.Controls.Clear();
            tbFile.Text = "";

            if (comboBoxTypeFile.Text.Substring(0, 3) == "PLT") //Sprint-layout
            {
                if (typeNow == TypeFileImport.PLT) return; // нет смысла в дальнейшем, т.к. данный формат уже ранее выбран

                typeNow = TypeFileImport.PLT;

                _PLTForm = new PLTcontrol();

                _PLTForm.Dock = DockStyle.Fill;

                tabPageMain.Controls.Add(_PLTForm);

                return; // дальше код не пускаем

            }


            if (comboBoxTypeFile.Text.Substring(0, 3) == "DRL")
            {
                if (typeNow == TypeFileImport.DRL) return; // нет смысла в дальнейшем, т.к. данный формат уже ранее выбран

                typeNow = TypeFileImport.DRL;


                //tbFile.Text = @"C:\Users\root\Desktop\PRJ54";


                _DRLForm = new DRLcontrol();

                //_DRLForm.Width = tabPageMain.Width;
                //_DRLForm.Height = tabPageMain.Height;
                _DRLForm.Dock = DockStyle.Fill;

                tabPageMain.Controls.Add(_DRLForm);

                return; // дальше код не пускаем

            }

            if (comboBoxTypeFile.Text.Substring(0, 3) == "GBR")
            {
                if (typeNow == TypeFileImport.GRB) return; // нет смысла в дальнейшем, т.к. данный формат уже ранее выбран

                typeNow = TypeFileImport.GRB;

                //tbFile.Text = @"Z:\Bottom.gbr";
                //tbFile.Text = @"C:\Users\root\Desktop\Mk1.gbl";
                //tbFile.Text = @"C:\Users\SelenuR\Desktop\Mk1.gbl";
                
                //tbFile.Text = @"C:\Users\root\Desktop\sample.gbr";

                return; // дальше код не пускаем
                
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void buttonFitDataToScreen_Click(object sender, EventArgs e)
        {
            //dCenterX = 0;
           // dCenterY = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //// TODO: SAMPLE 
            //DPoint p0 = new DPoint(0D, 0D);
            //DPoint p1 = new DPoint(10D, 10D);
            //DPoint p2 = new DPoint(10D, -10D);
            //DPoint p3 = new DPoint(-10D, 10D);
            //DPoint p4 = new DPoint(-10D, -10D);

            //double angle1 = getAngle(p0, p1);
            //double angle2 = getAngle(p0, p2);
            //double angle3 = getAngle(p0, p3);
            //double angle4 = getAngle(p0, p4);

            //DPoint n1 = getPosPointFromAngle(p0, 10D, angle + 90);
            //DPoint n2 = getPosPointFromAngle(p0, 10D, angle + 180);


            //listBox1.Items.Clear();

            //for (int i = 0; i < 360; i++)
            //{
            //    DPoint n1 = getPosPointFromAngle(new DPoint(), 10D, i);

            //    listBox1.Items.Add(i.ToString() + @" - " + n1.X.ToString() + @" ; " + n1.Y.ToString());

            //}



        }

        /// <summary>
        /// Функция возвращает угол, между отрезком и осью Х, в качестве ключевой точки используется точка А
        /// </summary>
        /// <param name="A">Начальная точка отрезка</param>
        /// <param name="B">Конечная точка</param>
        /// <returns>Угол</returns>
        private double getAngle(IntPoint A, IntPoint B)
        {
            // Проверка, что координаты точек не совпадают.
            if (A.X == B.X && A.Y == B.Y) return 0;

            double radians = Math.Atan2(B.Y - A.Y, B.X - A.X); // Получаем угол.
            //получаем угол
            return radians * 180 / Math.PI;
        }


        private DPoint getPosPointFromAngle(IntPoint stPoint, double radius, double angle)
        {
            double a = angle * System.Math.PI / 180.0;

            double X = radius*Math.Sin(a);
            double Y = radius*Math.Cos(a);

            return new DPoint(X + stPoint.X, Y + stPoint.Y);
        }

        private void nudOffset_ValueChanged(object sender, EventArgs e)
        {
            DrawBitmap(true);
        }

        static private PointF[] PolygonToPointFArray(Polygon pg, float scale)
        {
            PointF[] result = new PointF[pg.Count];
            for (int i = 0; i < pg.Count; ++i)
            {
                result[i].X = (float)pg[i].X / scale;
                result[i].Y = (float)pg[i].Y / scale;
            }
            return result;
        }

        ClipType GetClipType()
        {

            //return ClipType.ctIntersection;
            return ClipType.ctUnion;
            //return ClipType.ctDifference;
            //return ClipType.ctXor;
        }


        PolyFillType GetPolyFillType()
        {
            return PolyFillType.pftNonZero;
            //return PolyFillType.pftEvenOdd;
        }


        private float scale = 100;

        private void DrawBitmap(bool justClip = false)
        {

            mybitmap = new Bitmap(pictureBox1.ClientRectangle.Width,pictureBox1.ClientRectangle.Height,PixelFormat.Format32bppArgb);

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                //вот тут имеем перечень векторов

                using (Graphics newgraphic = Graphics.FromImage(mybitmap))
                using (GraphicsPath path = new GraphicsPath())
                {
                    newgraphic.SmoothingMode = SmoothingMode.AntiAlias;
                    newgraphic.Clear(Color.White);
                    //if (rbNonZero.Checked)
                        path.FillMode = FillMode.Winding;

                    //draw subjects ...
                    foreach (Polygon pg in subjects)
                    {
                        PointF[] pts = PolygonToPointFArray(pg, scale);
                        path.AddPolygon(pts);
                        pts = null;
                    }
                    using (Pen myPen = new Pen(Color.FromArgb(196, 0xC3, 0xC9, 0xCF), (float)0.6))
                    using (SolidBrush myBrush = new SolidBrush(Color.FromArgb(127, 0xDD, 0xDD, 0xF0)))
                    {
                        newgraphic.FillPath(myBrush, path);
                        newgraphic.DrawPath(myPen, path);
                        path.Reset();

                        //draw clips ...
                        //if (rbNonZero.Checked)
                            path.FillMode = FillMode.Winding;

                        foreach (Polygon pg in clips)
                        {
                            PointF[] pts = PolygonToPointFArray(pg, scale);
                            path.AddPolygon(pts);
                            pts = null;
                        }
                        myPen.Color = Color.FromArgb(196, 0xF9, 0xBE, 0xA6);
                        myBrush.Color = Color.FromArgb(127, 0xFF, 0xE0, 0xE0);
                        newgraphic.FillPath(myBrush, path);
                        newgraphic.DrawPath(myPen, path);
                        
                        //do the clipping ...
                        if ((clips.Count > 0 || subjects.Count > 0) && true)
                        {
                            Polygons solution2 = new Polygons();
                            Clipper c = new Clipper();
                            c.AddPaths(subjects, PolyType.ptSubject, true);
                            c.AddPaths(clips, PolyType.ptClip, true);
                            solution.Clear();
#if UsePolyTree
              bool succeeded = c.Execute(GetClipType(), solutionTree, GetPolyFillType(), GetPolyFillType());
              //nb: we aren't doing anything useful here with solutionTree except to show
              //that it works. Convert PolyTree back to Polygons structure ...
              Clipper.PolyTreeToPolygons(solutionTree, solution);
#else
                            bool succeeded = c.Execute(GetClipType(), solution, GetPolyFillType(), GetPolyFillType());
#endif
                            if (succeeded)
                            {
                                //SaveToFile("solution", solution);
                                myBrush.Color = Color.Black;
                                path.Reset();

                                //It really shouldn't matter what FillMode is used for solution
                                //polygons because none of the solution polygons overlap. 
                                //However, FillMode.Winding will show any orientation errors where 
                                //holes will be stroked (outlined) correctly but filled incorrectly  ...
                                path.FillMode = FillMode.Winding;

                                //or for something fancy ...

                                if (nudOffset.Value != 0)
                                {
                                    ClipperOffset co = new ClipperOffset();
                                    co.AddPaths(solution, JoinType.jtRound, EndType.etClosedPolygon);
                                    co.Execute(ref solution2, (double)nudOffset.Value * scale);
                                }
                                else
                                    solution2 = new Polygons(solution);

                                foreach (Polygon pg in solution2)
                                {
                                    PointF[] pts = PolygonToPointFArray(pg, scale);
                                    //if (pts.Count() > 2)
                                        path.AddPolygon(pts);
                                    pts = null;
                                }
                                myBrush.Color = Color.FromArgb(127, 0x66, 0xEF, 0x7F);
                                myPen.Color = Color.FromArgb(255, 0, 0x33, 0);
                                myPen.Width = 1.0f;
                                newgraphic.FillPath(myBrush, path);
                                newgraphic.DrawPath(myPen, path);

                                //now do some fancy testing ...
                                using (Font f = new Font("Arial", 8))
                                using (SolidBrush b = new SolidBrush(Color.Navy))
                                {
                                    double subj_area = 0, clip_area = 0, int_area = 0, union_area = 0;
                                    c.Clear();
                                    c.AddPaths(subjects, PolyType.ptSubject, true);
                                    c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                                    foreach (Polygon pg in solution2)
                                        subj_area += Clipper.Area(pg);
                                    c.Clear();
                                    c.AddPaths(clips, PolyType.ptClip, true);
                                    c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                                    foreach (Polygon pg in solution2)
                                        clip_area += Clipper.Area(pg);
                                    c.AddPaths(subjects, PolyType.ptSubject, true);
                                    c.Execute(ClipType.ctIntersection, solution2, GetPolyFillType(), GetPolyFillType());
                                    foreach (Polygon pg in solution2)
                                        int_area += Clipper.Area(pg);
                                    c.Execute(ClipType.ctUnion, solution2, GetPolyFillType(), GetPolyFillType());
                                    foreach (Polygon pg in solution2)
                                        union_area += Clipper.Area(pg);

                                    using (StringFormat lftStringFormat = new StringFormat())
                                    using (StringFormat rtStringFormat = new StringFormat())
                                    {
                                        lftStringFormat.Alignment = StringAlignment.Near;
                                        lftStringFormat.LineAlignment = StringAlignment.Near;
                                        rtStringFormat.Alignment = StringAlignment.Far;
                                        rtStringFormat.LineAlignment = StringAlignment.Near;
                                        Rectangle rec = new Rectangle(pictureBox1.ClientSize.Width - 108,
                                                         pictureBox1.ClientSize.Height - 116, 104, 106);
                                        newgraphic.FillRectangle(new SolidBrush(Color.FromArgb(196, Color.WhiteSmoke)), rec);
                                        newgraphic.DrawRectangle(myPen, rec);
                                        rec.Inflate(new Size(-2, 0));
                                        newgraphic.DrawString("Areas", f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 14));
                                        newgraphic.DrawString("subj: ", f, b, rec, lftStringFormat);
                                        newgraphic.DrawString((subj_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 12));
                                        newgraphic.DrawString("clip: ", f, b, rec, lftStringFormat);
                                        newgraphic.DrawString((clip_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 12));
                                        newgraphic.DrawString("intersect: ", f, b, rec, lftStringFormat);
                                        newgraphic.DrawString((int_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 12));
                                        newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 10));
                                        newgraphic.DrawString("s + c - i: ", f, b, rec, lftStringFormat);
                                        newgraphic.DrawString(((subj_area + clip_area - int_area) / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 10));
                                        newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 10));
                                        newgraphic.DrawString("union: ", f, b, rec, lftStringFormat);
                                        newgraphic.DrawString((union_area / 100000).ToString("0,0"), f, b, rec, rtStringFormat);
                                        rec.Offset(new System.Drawing.Point(0, 10));
                                        newgraphic.DrawString("---------", f, b, rec, rtStringFormat);
                                    }
                                }
                            } //end if succeeded
                        } //end if something to clip


                        
                        pictureBox1.Image = mybitmap;
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawBitmap(true);
        }

        private void numericUpDownZoom_ValueChanged(object sender, EventArgs e)
        {
            scale = (float)numericUpDownZoom.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Read_File();
            Read_GBR();
        }



        private void button7_Click_1(object sender, EventArgs e)
        {
            previewBox1.DRAW();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // тут проверим
        }

        private void button9_Click(object sender, EventArgs e)
        {
            About abfrm = new About();
            abfrm.ShowDialog();
        }


        //---------------------------------------------------------------------


    }

    //Для хранения дополнительной информации
    public class Property
    {
        public string property;
        public string description;

        public Property(string _pr,string _des)
        {
            property = _pr;
            description = _des;
        }
    }



    /// <summary>
    /// Класс для хранения информации из gerber файла
    /// </summary>
    public class GerberData
    {
        /// <summary>
        /// Тип единицы измерения, мм или дюймы, "ММ" - так обычно милиметры выглядят
        /// </summary>
        public string UnitsType = "";

        /// <summary>
        /// Список сплайнов
        /// </summary>
        public List<typeSpline> typeSplines = new List<typeSpline>();

        /// <summary>
        /// Точки из файла (данные)
        /// </summary>
        public List<grbPoint> points = new List<grbPoint>();

        //длина всего числа
        public int countDigitsX = 1;
        //длина всего числа
        public int countDigitsY = 1;
        //длина дробной части
        public int countPdigX = 0;
        //длина дробной части
        public int countPdigY = 0;


        public int X_min = 100000;
        public int X_max = -100000;

        public int Y_min = 100000;
        public int Y_max = -100000;
        

        ///// <summary>
        ///// Вычисление размерности необходимого массива, для анализа
        ///// </summary>
        ///// <param name="accuracy">Коэфициент уменьшения размеров данных</param>
        //public void CalculateGatePoints(int _accuracy)
        //{
        //    // немного уменьшим значения
        //    foreach (grbPoint VARIABLE in points)
        //    {
        //        VARIABLE.X = VARIABLE.X / _accuracy;
        //        VARIABLE.Y = VARIABLE.Y / _accuracy;
        //    }

        //    foreach (grbPoint VARIABLE in points)
        //    {
        //        if (VARIABLE.X > X_max) X_max = VARIABLE.X;

        //        if (VARIABLE.X < X_min) X_min = VARIABLE.X;

        //        if (VARIABLE.Y > Y_max) Y_max = VARIABLE.Y;

        //        if (VARIABLE.Y < Y_min) Y_min = VARIABLE.Y;
        //    }

        //    // Немного расширим границу
        //    X_max += 500;
        //    Y_max += 500;

        //}

    }


    public enum Apertures
    {
        C_circle,
        R_rectangle,
        O_obround,
        P_polygon
    }

    public class typeSpline
    {
        public int number;
        public Apertures aperture; 
        public int size1;
        public int size2;

        public typeSpline(int _number, Apertures _aperture, int _size1 = 0, int _size2 = 0)
        {
            number = _number;
            aperture = _aperture;
            size1 = _size1;
            size2 = _size2;
        }
    }

    //для работы с гербером
    public class grbPoint
    {
        public int X;
        public int Y;
        public string typePoint; // D1 - видимое движение D2 - невидимое движение D3 - точка
        public int numberSplane;

        public grbPoint(int _x, int _y, string _typePoint, int _numberSplane)
        {
            X = _x;
            Y = _y;
            typePoint = _typePoint;
            numberSplane = _numberSplane;
        }
    }

    //класс описания точки
    public class DPoint
    {
        public double X;
        public double Y;

        public DPoint(double _x = 0D, double _y = 0D)
        {
            X = _x;
            Y = _y;            
        }
    }

    ////класс описания инструмента
    //public class Instrument
    //{
    //    public int Numer;
    //    public float Diametr;

    //    public Instrument(int _number, float _diametr)
    //    {
    //        Numer = _number;
    //        Diametr = _diametr;
    //    }
    //}

    //возможные типы данных
    public enum typeCollections
    {
        Points,
        Instruments,
        Property,
    };

    //////Набор однотипных данных
    ////public class DataCollections
    ////{
    ////    public typeCollections TypeData;

    ////    public List<Point> Points;
    ////    //public Instrument intrument;

    ////    //public Property property;
 





    ////    /// <summary>
    ////    /// Конструктор набора точек
    ////    /// </summary>
    ////    /// <param name="_Points">Список точек</param>
    ////    public DataCollections(List<Point> _Points, Instrument _intrument = null)
    ////    {
    ////        TypeData = typeCollections.Points;
    ////        Points = _Points;
    ////        intrument = _intrument;
    ////    }





    ////}
}