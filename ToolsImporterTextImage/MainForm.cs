using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace CNCImporterGkode
{
    public partial class MainForm : Form
    {

        private readonly SelectFont _selFont = new SelectFont();
        private readonly SelectImage _selImage = new SelectImage();

        private List<List<PointF>> _lines;

        private float _minX = 99999;
        private float _maxX;
        private float _minY = 99999;
        private float _maxY;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _selFont.IsChange += EventFromUc;
            _selImage.IsChange += EventFromUc;

            panel1.Controls.Add(_selFont);

            RefreshStep1();
        }

        private void EventFromUc(object sender, EventArgs e)
        {
            RefreshStep1();
        }

        // Установка доступности тех или иных элементов на страницах
        void RefreshEnableTab()
        {
            bool enableStep2;

            if (radioButtonTypeSourcePicture.Checked)
            {
                enableStep2 = true;
            }
            else
            {
                enableStep2 = !_selFont.UseVectorFont;                   
            }

            groupBoxFilter1.Enabled = enableStep2;
            groupBoxFilter2.Enabled = enableStep2;
            groupBoxFilter3.Enabled = enableStep2;
            groupBoxFilter4.Enabled = enableStep2;
            groupBoxFilter5.Enabled = enableStep2;
        }

        private void radioButtonTypeSourceText_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(_selFont);
            RefreshEnableTab();
        }

        private void radioButtonTypeSourcePicture_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(_selImage);
            RefreshEnableTab();
        }

        void RefreshStep1()
        {
           ShowImageWindowFromStep(1);

           RefreshEnableTab();
        }

        private void RefreshStep2(int _i)
        {
            ShowImageWindowFromStep(_i);
        }

        private void numericUpDownKoefPalitra_ValueChanged(object sender, EventArgs e)
        {
            RefreshStep2(2);
        }

        private void btPreviewGrey_Click(object sender, EventArgs e)
        {
            RefreshStep2(2);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            RefreshStep2(3);
        }

        private void checkBoxFlipX_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStep2(3);
        }

        private void checkBoxFlipY_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStep2(3);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RefreshStep2(4);
        }

        private void numericUpDownRotate_ValueChanged(object sender, EventArgs e)
        {
            RefreshStep2(4);
        }

        private void checkBoxUseFilter1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStep2(5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshStep2(5);
        }

        private void checkBoxUseFilter2_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStep2(6);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RefreshStep2(6);
        }

        private void RefreshTree()
        {
            //заполним дерево
            treeView1.Nodes.Clear();
            textBoxGkod.Text = "";
            _minX = 99999;
            _maxX = 0;
            _minY = 99999;
            _maxY = 0;

            foreach (List<PointF> line in _lines)
            {
                TreeNode tn = treeView1.Nodes.Add("Точек: " + line.Count);

                foreach (PointF point in line)
                {
                    tn.Nodes.Add(@"x: " + point.X + @" y: " + point.Y);

                    if (_minX > point.X) _minX = point.X;

                    if (_maxX < point.X) _maxX = point.X;

                    if (_minY > point.Y) _minY = point.Y;

                    if (_maxY < point.Y) _maxY = point.Y;
                }
            }
            Calculate();
        }

        //вычисление отрезков
        private void buttonCalculateVectors_Click(object sender, EventArgs e)
        {
            Bitmap tmp = GetImageFromStep(6);
            _lines = GetVectorFromImage(tmp);


            RefreshTree();
            //pictureBoxPreview.Image = GetTraectory();
        }




        #region Функции обработки изображения


        /// <summary>
        /// Функция обходит контур по часовой стрелке
        /// </summary>
        /// <param name="image">Рисунок для анализа</param>
        /// <returns>Список точек</returns>
        private List<List<PointF>> GetVectorFromImage(Bitmap image)
        {
            // для сбора информации о точках у отрезка
            List<PointF> ListPoints = new List<PointF>();
            // для сбора информации об отрезках
            List<List<PointF>> Lines = new List<List<PointF>>();

            Bitmap tmp = GetImageFromStep(6);


            for (int pY = 1; pY < tmp.Height - 1; pY++)
            {
                for (int pX = 1; pX < tmp.Width - 1; pX++)
                {
                    Color compareColor = tmp.GetPixel(pX, pY);

                    int tmpPalitra = ((int)compareColor.R + (int)compareColor.G + (int)compareColor.B) / 3;

                    if (tmpPalitra != 0) continue; // очень светлая точка, такое нас не интересует

                    //начинаем обход

                    //временные координаты, для обхода
                    int tx = pX;
                    int ty = pY;

                    bool bypass = true; //для определения того что занимаемся обходом по точкам

                    int direction = 1; //начальное направление вправо, по часовой стрелке.

                    bool firstPoint = true;
                    float firstX = 0;
                    float firstY = 0;

                    //начинаем обход траектории
                    while (bypass)
                    {
                        //1) добавляем текущую точку
                        //   points.Add(new mPoint(tx, ty));
                        ListPoints.Add(new PointF(tx,ty));

                        if (firstPoint)
                        {
                            firstPoint = false;
                            firstX = tx;
                            firstY = ty;
                            //и стираем текущую черную точку
                            tmp.SetPixel(tx, ty, Color.FromArgb(255, 0, 255, 255));
                        }
                        else
                        {
                            //и стираем текущую черную точку
                            tmp.SetPixel(tx, ty, Color.FromArgb(255, 255, 255, 255));                            
                        }



                        bool tst = CheckBlackPoint(ref tmp, tx, ty);
                        if (tst == false)
                        {
                            bypass = false;
                            continue;
                        }

                        //2) определяем направление с которого просканируем окружающую остановку
                        direction += 5;

                        //типа "переполнение"
                        if (direction > 8) direction -= 8;

                        //3) по очереди по всем направлениям проверим наличие черной точки
                        int test = 7;

                        while (test > 0)
                        {

                            int oX = tx;
                            int oY = ty;

                            switch (direction)
                            {
                                case 1:
                                    oX++;
                                    break;
                                case 2:
                                    oX++; oY++;
                                    break;
                                case 3:
                                    oY++;
                                    break;
                                case 4:
                                    oX--; oY++;
                                    break;
                                case 5:
                                    oX--;
                                    break;
                                case 6:
                                    oX--; oY--;
                                    break;
                                case 7:
                                    oY--;
                                    break;
                                case 8:
                                    oX++; oY--;
                                    break;
                                default:
                                    break;
                            }

                            //если цвет в нужном направлении черный хорошо, иначе продолжим дальше
                            Color testColor = tmp.GetPixel(oX, oY);
                            int testPalitra = ((int)testColor.R + (int)testColor.G + (int)testColor.B) / 3;

                            if (testPalitra == 0)
                            {
                                //наша точка
                                test = 0;
                                tx = oX;
                                ty = oY;

                                continue;
                            }

                            if (direction < 8) direction++;
                            else direction = 1;

                            test--;
                        }//while (test>0)

                    }//while (bypass)


                    // В связи с тем что мы закрашиваем пройденные точки, то неоходимо проверить, последняя точка замыкается с начальной, или нет
                    PointF pp = ListPoints[ListPoints.Count - 1];
                    float distance = (firstX - pp.X) + (firstY - pp.Y);
                    if (distance < 2 && distance > -2) ListPoints.Add(new PointF(firstX,firstY));


                    List<PointF> TMPpoints = new List<PointF>();

                    ////добавим начальную точку в любом случае
                    TMPpoints.Add(ListPoints[0]);


                    for (int i = 1; i < ListPoints.Count - 1; i++)
                    {
                        float angl = GetAngleFrom3Points(ListPoints[i - 1], ListPoints[i], ListPoints[i + 1]);
                        float deltaAngle = (float)numericUpDownAngle.Value;

                        bool bAddPoint = true;

                        // проверка угла
                        if ((angl >= (180 - deltaAngle) && angl <= (180 + deltaAngle))) bAddPoint = false;

                        // проверка длины
                        //int lenghAB = 1;
                        if (numericUpDownMunimumLenght.Value > 1 && TMPpoints.Count > 0)
                        {
                            float xa = (TMPpoints[TMPpoints.Count - 1].X - ListPoints[i].X);
                            float ya = (TMPpoints[TMPpoints.Count - 1].Y - ListPoints[i].Y);
                            double dlenght = Math.Sqrt(xa * xa + ya * ya);
                            
                            if (dlenght < (double)numericUpDownMunimumLenght.Value) bAddPoint = false;
                        }

                        if (bAddPoint) TMPpoints.Add(ListPoints[i]);

                    }

                    ////и конечную точку в любом случае
                    TMPpoints.Add(ListPoints[ListPoints.Count - 1]);

                    bool needAddOtrezok = true;

                    if (TMPpoints.Count == 2)
                    {
                        if (TMPpoints[0].X == TMPpoints[1].X &&
                            TMPpoints[0].Y == TMPpoints[1].Y)
                        {
                            needAddOtrezok = false;
                        }
                    }


                    //обход по контуру завершен
                    //treeView1.Nodes.Add(tn);
                   if (needAddOtrezok) Lines.Add(TMPpoints);


                    ////очистим
                   ListPoints = new List<PointF>();

                }//for (int pX = 1;
            }//for (int pY = 1;

            //TODO: удаляем отрезки у которых всего 2 точки, и они одинаковые

            //for (int findPos = otrezki.Count; findPos > 0; findPos--)
            //{
            //    if (otrezki[findPos].points.Count == 2)
            //    {
            //        if (otrezki[findPos].points[0].X == otrezki[findPos].points[1].X &&
            //            otrezki[findPos].points[0].Y == otrezki[findPos].points[1].Y)
            //        {
            //            otrezki[findPos].
            //        }
            //    }
            //}








            //tmp = null;


            return Lines;
        }


        /// <summary>
        /// Функция возвращает массив отрезков, которые и составляют рисунок шрифта
        /// </summary>
        /// <param name="text">Текст кторый нужно преобразовать в вектора</param>
        /// <param name="fontName">Имя шрифта</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="extFileFont">Имя внешненего файла (если используется не системный шрифт)</param>
        /// <returns>Набор отрезков</returns>
        private List<List<PointF>> GetVectorFromText(string text, string fontName, float fontSize, string extFileFont = "")
        {
            PointF[] pts = null; //список точек
            byte[] ptsType = null; //информация о начале/окончании отрезка

            using (GraphicsPath path = new GraphicsPath())
            {
                if (extFileFont != "")
                {
                    PrivateFontCollection customFontFromFile;
                    customFontFromFile = new PrivateFontCollection();
                    customFontFromFile.AddFontFile(extFileFont);
                    if (customFontFromFile.Families.Length > 0)
                    {
                        Font font = new Font(customFontFromFile.Families[0], (int)fontSize);
                        path.AddString(text, font.FontFamily, (int)FontStyle.Regular, fontSize, new PointF(0f, 0f), StringFormat.GenericDefault);
                    }
                    else
                    {
                        MessageBox.Show(@"Ошибка загрузки шрифта из файла!!!");
                    }
                }
                else
                {
                    path.AddString(text, new FontFamily(fontName), (int)FontStyle.Regular, fontSize, new PointF(0f, 0f), StringFormat.GenericDefault);
                }

                path.Flatten();

                if (path.PointCount == 0)//нет отрезков
                {
                    return new List<List<PointF>>();
                }

                pts = path.PathPoints;
                ptsType = path.PathTypes;
            }
            
            //для сбора информации о точках у отрезка
            List<PointF> ListPoints = new List<PointF>();
            // для сбора информации об отрезках
            List<List<PointF>> Lines = new List<List<PointF>>();

            int index = 0;
            foreach (PointF value in pts)
            {
                byte ptypePoint = ptsType[index]; //тип точки: 0-точка является началом отрезка, 1-точка является продолжением отрезка, 129-161 и прочее окончанием отрезка, причем необходимо добавлять линию соединяющую начальную точку и конечную

                // это первая точка
                if (ptypePoint == 0)
                {
                    ListPoints.Add(value);
                }

                //а это продолжение
                if (ptypePoint == 1)
                {
                    ListPoints.Add(value);
                }

                //окончание
                if (ptypePoint == 129)
                {
                    ListPoints.Add(value);
                    ListPoints.Add(ListPoints[0]); //иногда не нужна
                    Lines.Add(ListPoints);
                    ListPoints = new List<PointF>();
                }

                if (ptypePoint == 161)
                {
                    ListPoints.Add(value);
                    //ListPoints.Add(ListPoints[0]);
                    Lines.Add(ListPoints);
                    ListPoints = new List<PointF>();
                }

                index++;
            }

            return Lines;

        }

        // Создание рисунка из текста
        private Bitmap CreateBitmapFromText(string text, string fontName, float fontSize, string extFileFont = "")
        {
            //string imageText = textString.Text;

            if (text.Trim().Length == 0) text = " ";

            Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format24bppRgb);

            int width = 0;
            int height = 0;

            // Создаем объект Font для "рисования" им текста.
            Font font = new Font(fontName, (int)fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            if (extFileFont != "")
            {
                PrivateFontCollection customFontFromFile;
                customFontFromFile = new PrivateFontCollection();
                customFontFromFile.AddFontFile(extFileFont);

                if (customFontFromFile.Families.Length > 0)
                {
                    font = new Font(customFontFromFile.Families[0], (int)fontSize);
                }
                else
                {
                    MessageBox.Show(@"Ошибка загрузки шрифта из файла!!!");
                }
            }

            // Создаем объект Graphics для вычисления высоты и ширины текста.
            Graphics graphics = Graphics.FromImage(bitmap);

            // Определение размеров изображения.
            width = (int)graphics.MeasureString(text, font).Width;
            height = (int)graphics.MeasureString(text, font).Height;

            // Пересоздаем объект Bitmap с откорректированными размерами под текст и шрифт.
            bitmap = new Bitmap(bitmap, new Size(width, height));

            // Пересоздаем объект Graphics
            graphics = Graphics.FromImage(bitmap);

            // Задаем цвет фона.
            graphics.Clear(Color.White);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            // Пишем (рисуем) текст
            graphics.DrawString(text, font, new SolidBrush(Color.Black), 0, 0);

            graphics.Flush();

            return (bitmap);
        }




        private Bitmap PreviewLines(List<List<PointF>> linesList)
        {


            return GetTraectory();

            //получим границы векторов
            //float maxX = 0;
            //float maxY = 0;
            //float minX = 9999;
            //float minY = 9999;

            //foreach (List<PointF> line in linesList)
            //{
            //    foreach (PointF point in line)
            //    {
            //        if (point.X > maxX) maxX = point.X;
            //        if (point.Y > maxY) maxY = point.Y;
            //        if (point.X < minX) minX = point.X;
            //        if (point.Y < minY) minY = point.Y;                    
            //    }
            //}

            //labelTextSize.Text = @"Размер текста: " + (maxX - minX) + " x " + (maxY - minY) +" единиц.";



            ////сформируем чистый рисунок, в котором всё и нарисуем
            //Bitmap bitmap = new Bitmap((int)maxX + ((int)textSize.Value / 10), (int)maxY + ((int)textSize.Value / 10), PixelFormat.Format24bppRgb);
            //// Создаем объект Graphics для вычисления высоты и ширины текста.
            //Graphics graphics = Graphics.FromImage(bitmap);
            //// Задаем цвет фона.
            //graphics.Clear(Color.White);
            //// Задаем параметры анти-алиасинга
            //graphics.SmoothingMode = SmoothingMode.None;
            //graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

            //Pen cPen = Pens.BlueViolet;
            //int indx = 0;

            //foreach (List<PointF> line in linesList)
            //{
            //    int oldX = 0;
            //    int oldY = 0;
            //    bool firstRecord = true;

            //    //PointF[] tmp = new PointF[line.Count];
            //    //int i = 0;
            //    foreach (PointF point in line)
            //    {
            //      //  tmp[i++] = point;

            //        if (firstRecord)
            //        {
            //            firstRecord = false;
            //            oldX = (int)point.X;
            //            oldY = (int)point.Y;
            //            continue;
            //        }


            //        graphics.DrawLine(cPen, oldX, oldY, point.X, point.Y);

            //        oldX = (int)point.X;
            //        oldY = (int)point.Y;


            //    }
            //    indx++;

            //    //graphics.DrawCurve(cPen, tmp);
            //}

            //graphics.Flush();

            //pictureBoxPreview.Image = bitmap;

            //if (radioButton_Zoom.Checked)
            //{
            //    pictureBoxPreview.Width = panel1.ClientSize.Width - 2;
            //    pictureBoxPreview.Height = panel1.ClientSize.Height - 26;
            //    panel1.AutoScrollMinSize = new Size(0, 0);
            //    panel1.AutoScroll = false;
            //}
            //else
            //{
            //    pictureBoxPreview.Width = pictureBoxPreview.Image.Width;
            //    pictureBoxPreview.Height = pictureBoxPreview.Image.Height;

            //    panel1.AutoScrollMinSize = new Size(pictureBoxPreview.Width, pictureBoxPreview.Height);
            //    panel1.AutoScroll = true;
            //}
        }




        private Bitmap ConvertImageToBlackWhileColor(Bitmap bmp)
        {
            // Задаём формат Пикселя, с которым будем работать
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            //проверим что входящее изображение имеет нужный формат:
            if (bmp.PixelFormat != pxf)
            {
                //преобразуем в нужный нам формат
                Bitmap clone = new Bitmap(bmp.Width, bmp.Height, pxf);
                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                bmp = clone;
            }

            // Получаем данные картинки.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //Блокируем набор данных изображения в памяти
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Получаем адрес первой линии.
            IntPtr ptr = bmpData.Scan0;

            // Задаём массив из Byte и помещаем в него надор данных.
            //int numBytes = bmp.Width * bmp.Height * 3; 
            //На 3 умножаем - поскольку RGB цвет кодируется 3-мя байтами
            //Либо используем вместо Width - Stride
            int numBytes = bmpData.Stride * bmp.Height;
            //int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            int koef = (int)numericUpDownKoefPalitra.Value;

            // Копируем значения в массив.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int fy = 0; fy < bmpData.Height; fy++)
            {
                for (int fx = 0; fx < bmpData.Width; fx++)
                {
                    int positionRam = (fy * bmpData.Stride) + (fx * 3);

                    int value = rgbValues[positionRam] + rgbValues[positionRam + 1] + rgbValues[positionRam + 2];

                    value = value / 3;

                    //byte color_b = 0;

                    if (value > koef)
                    {
                        rgbValues[positionRam] = 255;
                        rgbValues[positionRam + 1] = 255;
                        rgbValues[positionRam + 2] = 255;
                    }
                    else
                    {
                        rgbValues[positionRam] = 0;
                        rgbValues[positionRam + 1] = 0;
                        rgbValues[positionRam + 2] = 0;
                    }

                }
            }

            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Разблокируем набор данных изображения в памяти.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        private Bitmap FlipBitmap(Bitmap bmp)
        {
            Bitmap tmp = bmp;

            RotateFlipType value = RotateFlipType.RotateNoneFlipNone;
 
            if (checkBoxFlipX.Checked && checkBoxFlipY.Checked) value = RotateFlipType.RotateNoneFlipXY;
            if (!checkBoxFlipX.Checked && checkBoxFlipY.Checked) value = RotateFlipType.RotateNoneFlipY;
            if (checkBoxFlipX.Checked && !checkBoxFlipY.Checked) value = RotateFlipType.RotateNoneFlipX;

            tmp.RotateFlip(value);
            return tmp;
        }

        private Bitmap RotateBitmap(Bitmap bmp)
        {
            Bitmap tmp = bmp;


            tmp = RotatePic(tmp, (float)numericUpDownRotate.Value, true);
            //tmp = RotateImageByAngle(tmp, (float)numericUpDownRotate.Value);
            return tmp;
        }

        private Bitmap InvertBlackWhileColor(Bitmap bmp)
        {
            if (!checkBoxUseFilter1.Checked) return bmp;

            // Задаём формат Пикселя, с которым будем работать
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            //проверим что входящее изображение имеет нужный формат:
            if (bmp.PixelFormat != pxf)
            {
                //преобразуем в нужный нам формат
                Bitmap clone = new Bitmap(bmp.Width, bmp.Height, pxf);
                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                bmp = clone;
            }

            // Получаем данные картинки.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //Блокируем набор данных изображения в памяти
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Получаем адрес первой линии.
            IntPtr ptr = bmpData.Scan0;

            // Задаём массив из Byte и помещаем в него надор данных.
            //int numBytes = bmp.Width * bmp.Height * 3; 
            //На 3 умножаем - поскольку RGB цвет кодируется 3-мя байтами
            //Либо используем вместо Width - Stride
            int numBytes = bmpData.Stride * bmp.Height;
            //int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            int koef = 128;

            // Копируем значения в массив.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int fy = 0; fy < bmpData.Height; fy++)
            {
                for (int fx = 0; fx < bmpData.Width; fx++)
                {
                    int positionRam = (fy * bmpData.Stride) + (fx * 3);

                    int value = rgbValues[positionRam] + rgbValues[positionRam + 1] + rgbValues[positionRam + 2];

                    value = value / 3;

                    //byte color_b = 0;

                    if (value > koef)
                    {
                        rgbValues[positionRam] = 0;
                        rgbValues[positionRam + 1] = 0;
                        rgbValues[positionRam + 2] = 0;
                    }
                    else
                    {
                        rgbValues[positionRam] = 255;
                        rgbValues[positionRam + 1] = 255;
                        rgbValues[positionRam + 2] = 255;
                    }

                }
            }

            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Разблокируем набор данных изображения в памяти.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        // удаление закрашенных областей
        private Bitmap BitmapDeleteContent(Bitmap tmp)
        {
            if (!checkBoxUseFilter2.Checked) return tmp;

            toolStripProgressBar1.Maximum = tmp.Height;

            Bitmap bitmap = new Bitmap(tmp.Width, tmp.Height, PixelFormat.Format32bppArgb);
            for (int pY = 0; pY < tmp.Height; pY++)
            {
                toolStripProgressBar1.Value = pY;
                for (int pX = 0; pX < tmp.Width; pX++)
                {
                    Color compareColor = tmp.GetPixel(pX, pY);

                    int tmpPalitra = ((int)compareColor.R + (int)compareColor.G + (int)compareColor.B)/3;

                    if (tmpPalitra < (int)numericUpDownKoefPalitra.Value)
                    {
                        bitmap.SetPixel(pX, pY, Color.FromArgb(255, 0, 0, 0));
                    }
                    else
                    {
                        bitmap.SetPixel(pX, pY, Color.FromArgb(255, 255, 255, 255));
                    }
                }
            }

            for (int pY = 0; pY < bitmap.Height; pY++)
            {
                for (int pX = 0; pX < bitmap.Width; pX++)
                {
                    if (pX == 0 || pX == bitmap.Width - 1)
                    {
                        bitmap.SetPixel(pX, pY, Color.FromArgb(255, 255, 255, 255)); //TODO: граничные элементы пока пропускаем, но потом переделать!!!
                        continue;
                    }

                    if (pY == 0 || pY == tmp.Height - 1)
                    {
                        bitmap.SetPixel(pX, pY, Color.FromArgb(255, 255, 255, 255)); //TODO: граничные элементы пока пропускаем, но потом переделать!!!
                        continue;
                    }

                    Color pColor = bitmap.GetPixel(pX, pY);

                    if (pColor == Color.White) continue; //если это белая точка нет необходимости осматриваться


                    //а тут осмотримся:
                    Color pColorUp = bitmap.GetPixel(pX, pY - 1);
                    Color pColorDown = bitmap.GetPixel(pX, pY + 1);
                    Color pColorLeft = bitmap.GetPixel(pX - 1, pY);
                    Color pColorRight = bitmap.GetPixel(pX + 1, pY);

                    Color compareColor = Color.FromArgb(255, 255, 255, 255);

                    if (pColorUp != compareColor && pColorDown != compareColor && pColorLeft != compareColor && pColorRight != compareColor)
                    {
                        //tmp.SetPixel(pX, pY, Color.FromArgb(245, 245, 245, 245));
                        bitmap.SetPixel(pX, pY, Color.FromArgb(245, 245, 245, 245));
                    }
                }
            }

            return bitmap;
        }

        private void ShowImageWindowFromStep(int showstep)
        {
            Bitmap tmp = GetImageFromStep(showstep);
            pictureBoxPreview.Image = tmp;
        }


        private Bitmap RotatePic(Bitmap bmpBU, float w, bool keepWholeImg)
        {
            //keepWholeImg - если истина, то изображение ужимается, при повороте, что-бы вписаться в рамки изображения
            Bitmap bmp = null;
            Graphics g = null;
            try
            {
                //Modus
                if (!keepWholeImg)
                {
                    bmp = new Bitmap(bmpBU.Width, bmpBU.Height,PixelFormat.Format24bppRgb);

                    g = Graphics.FromImage(bmp);
                    float hw = bmp.Width / 2f;
                    float hh = bmp.Height / 2f;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    //translate center
                    g.TranslateTransform(hw, hh);
                    //rotate
                    g.RotateTransform(w);
                    //re-translate
                    g.TranslateTransform(-hw, -hh);
                    g.DrawImage(bmpBU, 0, 0);
                    g.Dispose();
                }
                else
                {
                    //get the new size and create the blank bitmap
                    float rad = (float)(w / 180.0 * Math.PI);
                    double fW = Math.Abs((Math.Cos(rad) * bmpBU.Width)) + Math.Abs((Math.Sin(rad) * bmpBU.Height));
                    double fH = Math.Abs((Math.Sin(rad) * bmpBU.Width)) + Math.Abs((Math.Cos(rad) * bmpBU.Height));

                    bmp = new Bitmap((int)Math.Ceiling(fW), (int)Math.Ceiling(fH));

                    g = Graphics.FromImage(bmp);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    float hw = bmp.Width / 2f;
                    float hh = bmp.Height / 2f;

                    System.Drawing.Drawing2D.Matrix m = g.Transform;

                    //here we do not need to translate, we rotate at the specified point
                    m.RotateAt(w, new PointF((float)(bmp.Width / 2), (float)(bmp.Height / 2)), System.Drawing.Drawing2D.MatrixOrder.Append);

                    g.Transform = m;

                    //draw the rotated image
                    g.DrawImage(bmpBU, new PointF((float)((bmp.Width - bmpBU.Width) / 2), (float)((bmp.Height - bmpBU.Height) / 2)));
                    g.Dispose();
                }
            }
            catch
            {
                if ((bmp != null))
                {
                    bmp.Dispose();
                    bmp = null;
                }

                if ((g != null))
                {
                    g.Dispose();
                }

               // MessageBox.Show("Fehler.");
            }

            return bmp;
        }

        /// <summary>
        /// Возвращает изображение полученное на определенном шаге обработки
        /// 1 - Изображение введенного текста, или выбраного файла изображения
        /// 2 - Изображение сконвертированное в черно-белое
        /// 3 - Зеркалирование
        /// 4 - вращение
        /// 5 - Инвертирование цветов
        /// 6 - Удаление содержимого черного цвета
        /// </summary>
        /// <param name="NumStep"></param>
        /// <returns></returns>
        private Bitmap GetImageFromStep(int NumStep)
        {
            Bitmap bitmap = null;

            // ШАГ 1

            if (radioButtonTypeSourceText.Checked)
            {
                if (_selFont.UseVectorFont)
                {
                    if (_selFont.UseSystemFont)
                    {
                        //используем системный шрифт
                        _lines = GetVectorFromText(_selFont.textString.Text, _selFont.comboBoxFont.Text, (float)_selFont.textSize.Value);
                    }
                    else
                    {
                        //используем внешний файл шрифта
                        _lines = GetVectorFromText(_selFont.textString.Text, _selFont.comboBoxFont.Text, (float)_selFont.textSize.Value, _selFont.nameFontFile.Text);
                    }
                    bitmap = PreviewLines(_lines);
                }
                else
                {
                    bitmap = CreateBitmapFromText(_selFont.textString.Text, _selFont.comboBoxFont.Text, (float)_selFont.textSize.Value, _selFont.nameFontFile.Text);
                }
            }
            else
            {
                if (_selImage.textBoxFileName.Text == "") return null;
                bitmap = new Bitmap(_selImage.textBoxFileName.Text);
                Bitmap newbitmap = new Bitmap(bitmap, bitmap.Width + 2, bitmap.Height + 2);
                Graphics g = Graphics.FromImage(newbitmap);
                g.TranslateTransform(1, 1);
                bitmap = newbitmap;
            }
            
            if (NumStep == 1) return bitmap;

            // ШАГ 2
            bitmap = ConvertImageToBlackWhileColor(bitmap);

            if (NumStep == 2) return bitmap;

            bitmap = FlipBitmap(bitmap);

            if (NumStep == 3) return bitmap;

            bitmap = RotateBitmap(bitmap);

            if (NumStep == 4) return bitmap;

            bitmap = InvertBlackWhileColor(bitmap);

            if (NumStep == 5) return bitmap;

            bitmap = BitmapDeleteContent(bitmap);

            if (NumStep == 6) return bitmap;


            return bitmap;
        }

        #endregion













        
        private void Calculate()
        {
            labelInfoX.Text = @"значения X: от " + _minX.ToString() + @" до " + _maxX.ToString();
            labelInfoY.Text = @"значения Y: от " + _minY.ToString() + @" до " + _maxY.ToString();
            label9.Text = @"Итоговый размер: " + (_maxX - _minX) + @" x " + (_maxY - _minY);

            changeIsUser = false;
            numericUpDownCalcX.Value = (decimal)(_maxX - _minX);
            numericUpDownCalcY.Value = (decimal)(_maxY - _minY);
            changeIsUser = true;
        }

        private bool changeIsUser = true;

        private void numericUpDownCalcX_ValueChanged(object sender, EventArgs e)
        {
            if (!changeIsUser) return;

            if (checkBoxCalcXY.Checked)
            {
                decimal delta = (decimal)(_maxX - _minX) / (decimal)(_maxY - _minY);

                changeIsUser = false;
                numericUpDownCalcY.Value = numericUpDownCalcX.Value / (decimal)delta;
                changeIsUser = true;
            }
            


        }

        private void numericUpDownCalcY_ValueChanged(object sender, EventArgs e)
        {
            if (!changeIsUser) return;

            if (checkBoxCalcXY.Checked)
            {
                double delta = (_maxX - _minX) / (_maxY - _minY);
                changeIsUser = false;
                numericUpDownCalcX.Value = numericUpDownCalcX.Value * (decimal)delta;
                changeIsUser = true;
            }
      
        }

        /// <summary>
        /// Функция для вычисления угла, в точке №2
        /// </summary>
        /// <param name="point1">первая точка</param>
        /// <param name="point2">вторая точка</param>
        /// <param name="point3">третья точка</param>
        /// <returns></returns>
        private float GetAngleFrom3Points(PointF point1, PointF point2, PointF point3)
        {
            //сместим точки так, что точка №2 окажется с координатами (0;0)
            float xa = (point1.X - point2.X);
            float ya = (point1.Y - point2.Y);
            float xb = (point3.X - point2.X);
            float yb = (point3.Y - point2.Y);

            double angle = Math.Acos((xa * xb + ya * yb) / (Math.Sqrt(xa * xa + ya * ya) * Math.Sqrt(xb * xb + yb * yb)));
            float angle1 = (float)(angle * 180 / Math.PI);

            return angle1;
        }

        //Возвращает истину если есть черные точки по соседству
        private bool CheckBlackPoint(ref Bitmap tmp,int pX,int pY)
        {
            bool Fined = false;

            Color compareColor = Color.Black;

            Color Color1 = tmp.GetPixel(pX+1, pY);
            Color Color2 = tmp.GetPixel(pX+1, pY+1);
            Color Color3 = tmp.GetPixel(pX, pY+1);
            Color Color4 = tmp.GetPixel(pX-1, pY+1);
            Color Color5 = tmp.GetPixel(pX-1, pY);
            Color Color6 = tmp.GetPixel(pX-1, pY-1);
            Color Color7 = tmp.GetPixel(pX, pY-1);
            Color Color8 = tmp.GetPixel(pX+1, pY-1);

            Fined = (Color1.ToArgb() == compareColor.ToArgb() || Color2.ToArgb() == compareColor.ToArgb() || Color3.ToArgb() == compareColor.ToArgb() || Color4.ToArgb() == compareColor.ToArgb() || Color5.ToArgb() == compareColor.ToArgb() || Color6.ToArgb() == compareColor.ToArgb() || Color7.ToArgb() == compareColor.ToArgb() || Color8.ToArgb() == compareColor.ToArgb());

            return Fined;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxGkod.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Любой файл|*.*";
            saveFileDialog1.Title = "Сохранить файл";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                File.WriteAllText(saveFileDialog1.FileName, textBoxGkod.Text);
            }
        }

        private void GenerateGkode_Click(object sender, EventArgs e)
        {

            // применим дельты, для смещения в начало координат
            float deltaX = _maxX - (_maxX - _minX);
            float deltaY = _maxY - (_maxY - _minY);

            float koefX = (_maxX - _minX) / (float)numericUpDownCalcX.Value;
            float koefY = (_maxY - _minY) / (float)numericUpDownCalcY.Value;

            decimal dZ0 = numericUpDownZ0.Value;
            decimal dZ1 = numericUpDownZ1.Value;

            if (checkBoxForLaser.Checked)
            {
                dZ0 = 0;
                dZ1 = 0;
            }

            string sZ0 = dZ0.ToString("#0.###");
            string sZ1 = dZ1.ToString("#0.###");

            string Gkode =  "%" + Environment.NewLine;

            if (checkBoxForLaser.Checked)
            {
                Gkode += "M5" + Environment.NewLine;

                sZ0 = (0).ToString("#0.###");
                sZ1 = (0).ToString("#0.###");

                Gkode += "G0 F" + numericUpDownSpeedLaser.Value.ToString("00000") + Environment.NewLine;
                Gkode += "G1 F" + numericUpDownSpeedLaser.Value.ToString("00000") + Environment.NewLine;

            }
            else
            {
                Gkode += "M3" + Environment.NewLine;
            }


            Gkode += "g0 x0 y0 z" + sZ1 + Environment.NewLine;
            
            foreach (List<PointF> line in _lines)
            {

                double x = (line[0].X - deltaX);
                double y = (line[0].Y - deltaY);

                if (checkBoxMirrorY.Checked)
                {
                    y = -y + (_maxY - _minY);
                }

                x = x / koefX;
                y = y / koefY;

                if (!checkBoxForLaser.Checked)
                {
                    //опускание с высоты подхода до высоты фрезеровки
                    Gkode += "g0 x" + x.ToString("#0.###") + " y" + y.ToString("#0.###") + " z" + sZ1 + Environment.NewLine;                    
                }



                bool firstPoint = true;

                foreach (PointF point in line)
                {

                    x = (point.X-deltaX);
                    y = (point.Y-deltaY);

                    if (checkBoxMirrorY.Checked)
                    {
                        y = -y + (_maxY - _minY);
                    }

                    x = x / koefX;
                    y = y / koefY;


                    Gkode += "g1 x" + x.ToString("#0.###") + " y" + y.ToString("#0.###") + " z" + sZ0 + Environment.NewLine;

                    if (firstPoint)
                    {
                        //перед началом линии включим лазер
                        if (checkBoxForLaser.Checked)
                        {
                            Gkode += "M3" + Environment.NewLine;
                        }
                        firstPoint = false;
                    }
                }

                //после завершения линии выключим лазер
                if (checkBoxForLaser.Checked)
                {
                    Gkode += "M5" + Environment.NewLine;
                }

                x = (line[line.Count - 1].X-deltaX);
                y = (line[line.Count - 1].Y-deltaY);

                if (checkBoxMirrorY.Checked)
                {
                    y = -y + (_maxY - _minY);
                }

                x = x / koefX;
                y = y / koefY;

                if (!checkBoxForLaser.Checked)
                {
                    // поднятие на безопастную высоту
                    Gkode += "g0 x" + x.ToString("#0.###") + " y" + y.ToString("#0.###") + " z" + sZ1 + Environment.NewLine;
                }
            }

            if (checkBoxUsePoint.Checked)
            {
                Gkode = Gkode.Replace(",", ".");
            }

            Gkode += "M5" + Environment.NewLine;

            textBoxGkod.Text = Gkode;

        }


        private Bitmap GetTraectory(int _posLine = -1, int _posPoint = -1)
        {
            _minX = 99999;
            _maxX = 0;
            _minY = 99999;
            _maxY = 0;

            foreach (List<PointF> line in _lines)
            {
                foreach (PointF point in line)
                {
                    if (_minX > point.X) _minX = point.X;
                    if (_maxX < point.X) _maxX = point.X;
                    if (_minY > point.Y) _minY = point.Y;
                    if (_maxY < point.Y) _maxY = point.Y;
                }
            }



            Bitmap bitmap = new Bitmap((int)_maxX + 2, (int)_maxY + 2, PixelFormat.Format24bppRgb);
            // Создаем объект Graphics для вычисления высоты и ширины текста.
            Graphics graphics = Graphics.FromImage(bitmap);
            // Задаем цвет фона.
            graphics.Clear(Color.White);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

            //********************************************************



            Pen blackPen = new Pen(Color.BlueViolet, 1);
            Pen redPen = new Pen(Color.Red, 1);
            int indxLine = 0;
            int indxPoint = 0;

            foreach (List<PointF> line in _lines)
            {
                int oldX = 0;
                int oldY = 0;
                bool firstRecord = true;
                indxPoint = 0;

                foreach (PointF point in line)
                {


                    if (_posPoint != -1)
                    {
                        if (indxPoint == _posPoint && indxLine == _posLine)
                        {
                            try
                            {
                                //отбразим точку
                                graphics.DrawEllipse(new Pen(Color.Brown), point.X - 1, point.Y - 1, 3, 3);
                            }
                            catch (Exception)
                            {
                                //что-бы не париться при выходе за размеры изображения 
                                // throw;
                            }
                        }
                    }


                    // это первая точка, и поэтому пока не рисуем линию
                    if (firstRecord)
                    {
                        firstRecord = false;
                        oldX = (int)point.X;
                        oldY = (int)point.Y;
                    }
                    else
                    {
                        if (indxLine == _posLine) graphics.DrawLine(redPen, oldX, oldY, point.X, point.Y);
                        else graphics.DrawLine(blackPen, oldX, oldY, point.X, point.Y);

                        oldX = (int)point.X;
                        oldY = (int)point.Y;                        
                    }






                    indxPoint++;


                    if (_posPoint != -1)
                    {
                        if (indxPoint == _posPoint && indxLine == _posLine)
                        {
                            try
                            {
                                //отбразим точку
                                graphics.DrawEllipse(new Pen(Color.Brown), oldX - 1, oldY - 1, 3, 3);
                            }
                            catch (Exception)
                            {
                                //что-бы не париться при выходе за размеры изображения 
                                // throw;
                            }
                        }
                    }

                }
                indxLine++;
            }

            //*******************************************

            graphics.Flush();

            return bitmap;
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = -1; //расположение 0-выбран узел линии, 1-точка линии
            int pos = -1;   // если это линия то её индекс, если точка то её индекс
            int posParent = -1; //если выбрана точка, то тут индекс линии


            if (treeView1.SelectedNode != null)
            {
                level = treeView1.SelectedNode.Level;
                pos = treeView1.SelectedNode.Index;
                if (level > 0) posParent = treeView1.SelectedNode.Parent.Index;

                if (level == 0) pictureBoxPreview.Image = GetTraectory(pos);
                if (level == 1) pictureBoxPreview.Image = GetTraectory(posParent, pos);
            }
            else
            {
                pictureBoxPreview.Image = GetTraectory();
            }
        }



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abfrm = new About();
            abfrm.ShowDialog();
        }


        private string getLaserCode()
        {
             // применим дельты, для смещения в начало координат
            //int deltaX = (int)((float)numericUpDownLazerX.Value/ _maxX);
            //int deltaY = (int)((float)numericUpDownLazerY.Value /_maxY);

            double koefX = (_maxX - _minX) / (double)numericUpDownCalcX.Value;
            double koefY = (_maxY - _minY) / (double)numericUpDownCalcY.Value;


            //string sZ0 = numericUpDownZ0.Value.ToString("#0.###");
            //string sZ1 = numericUpDownZ1.Value.ToString("#0.###");



            string gkode = "";

            foreach (List<PointF> line in _lines)
            {
                int X1 = (int)line[0].X * (int)numericUpDownzoom.Value;
                int Y1 = (int)line[0].Y * (int)numericUpDownzoom.Value;

                X1 = (int)numericUpDownLazerX.Value * X1;
                Y1 = (int)numericUpDownLazerY.Value * Y1;

                X1 += (int)numericUpDownMoveLazerX.Value;
                Y1 += (int)numericUpDownMoveLazerY.Value;


                //gkode += "M5 " + "X" + X1.ToString("00000") + " Y" + Y1.ToString("00000") + " F" + numericUpDownlaserSpeed.Value.ToString("00000") + "\r\n";
                gkode += "M5 " + "X" + X1.ToString("00000") + " Y" + Y1.ToString("00000") + " F00010" + "\r\n";

                foreach (PointF point in line)
                {
                    //int X2 = deltaX * pnt.X;
                    //int Y2 = deltaY * pnt.Y;

                    int X2 = (int)point.X * (int)numericUpDownzoom.Value;
                    int Y2 = (int)point.Y * (int)numericUpDownzoom.Value;

                    X2 += (int)numericUpDownMoveLazerX.Value;
                    Y2 += (int)numericUpDownMoveLazerY.Value;

                    gkode += "M3 " + "X" + X2.ToString("00000") + " Y" + Y2.ToString("00000") + " F" + numericUpDownlaserSpeed.Value.ToString("00000") + "\r\n";
                }

            }
            gkode += "M5 " + "\r\n";

            return gkode;


        }


        private void button9_Click(object sender, EventArgs e)
        {


            textBoxGkodLaser.Text = getLaserCode();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label19.Text = @"x: " + _maxX + @" y: " + _maxY;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = textBox1.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string gkode = getLaserCode();

            try
            {
                serialPort1.BaudRate = 115200;
                serialPort1.PortName = textBox1.Text;
                serialPort1.Open();
                serialPort1.Write("*"); // очистим от предыдущих данных
                serialPort1.Write(gkode);
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка посылки данных! " + ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.WriteLine("@");
            serialPort1.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.WriteLine("#");
            serialPort1.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.WriteLine("!");
            serialPort1.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.WriteLine("*");
            serialPort1.Close();
        }

        private void radioButton_Zoom_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxPreview.SizeMode = ImageBoxSizeMode.Fit;
        }

        private void radioButton_FullSize_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxPreview.SizeMode = ImageBoxSizeMode.Normal;
        }



        private void button18_Click(object sender, EventArgs e)
        {
            //удаление последней точки, если она такая-же как и первая
            int index = 0;
            foreach (List<PointF> line in _lines)
            {
                PointF startPoint = line[0];
                PointF endPoint = line[line.Count - 1];

                if (startPoint == endPoint) _lines[index].RemoveAt(line.Count - 1);

                index++;
            }

            RefreshTree();

            pictureBoxPreview.Image = GetTraectory();

  


        }

        private void buttonDelLine_Click(object sender, EventArgs e)
        {
            int level = -1; //расположение 0-выбран узел линии, 1-точка линии
            int pos = -1;   // если это линия то её индекс, если точка то её индекс
            int posParent = -1; //если выбрана точка, то тут индекс линии

            if (treeView1.SelectedNode == null) return;

            level = treeView1.SelectedNode.Level;
            pos = treeView1.SelectedNode.Index;

            if (level > 0) posParent = treeView1.SelectedNode.Parent.Index;

            if (level == 0) _lines.RemoveAt(pos);
            if (level == 1) _lines[posParent].RemoveRange(pos, 1);

            RefreshTree();


            pictureBoxPreview.Image = GetTraectory();
        }

        private void buttonOptimize_Click(object sender, EventArgs e)
        {
            //отсортируем отрезки

            List<List<PointF>> tmpSource = _lines; //отсюда дергаем линии
            List<List<PointF>> tmpDestination = new List<List<PointF>>();  //и сюда помещаем    
            //перенесем первый отрезок
            tmpDestination.Add(tmpSource[0]);
            tmpSource.RemoveAt(0);


            int delta = 0; //растояние между точками отрезков, которые выстраиваются в очередь
            // цикл крутиться пока есть записи в tmpSource

            


            while (tmpSource.Count > 0)
            {
                List<PointF> tmpline = tmpDestination[tmpDestination.Count - 1];
                PointF tmppoint = tmpline[tmpline.Count-1]; //это последняя точка, последней линии нового массива отрезков

                int indexSource = 0;
                bool finded = false;
                foreach (List<PointF> sourceline in tmpSource)
                {
                    PointF tmpSourcepoint = sourceline[0];


                    bool xgood = ((tmpSourcepoint.X >= tmppoint.X - delta && tmpSourcepoint.X <= tmppoint.X + delta));
                    bool ygood = ((tmpSourcepoint.Y >= tmppoint.Y - delta && tmpSourcepoint.Y <= tmppoint.Y + delta));
                    
                    if (xgood && ygood)
                    {
                        tmpDestination.Add(sourceline);
                        tmpSource.RemoveAt(indexSource);
                        finded = true;
                        break;
                    }


                    indexSource++;
                }

                if (!finded) delta++; // если дошли до этого места значит не нашли очень близких отрезков
            }

            _lines = tmpDestination;

            //2 массива для сверки
            //List<cLines> LineWithStartPoint = new List<cLines>();
            //List<cLines> LineWithEndPoint = new List<cLines>();

            //    LineWithStartPoint.Add(new cLines(index, line[0].X, line[0].Y));
            //    LineWithEndPoint.Add(new cLines(index, line[line.Count - 1].X, line[line.Count - 1].Y));

            //LineWithStartPoint.Sort((a,b)=);
            //теперь отсортируем массивы










        }


        




        private void RefreshInfo()
        {

            labelZoomSize.Text = @"Масштаб " + pictureBoxPreview.Zoom + @"%";

            if (pictureBoxPreview.Image != null)
            {
                labelZoomSize.Text += @" размер: " + pictureBoxPreview.Image.Width + "x" + pictureBoxPreview.Image.Height + " пикселей";
            }
        }

        private void pictureBoxPreview_ZoomChanged(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void pictureBoxPreview_ImageChanged(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }



    public class cLines
    {
        public int index;
        public float X;
        public float Y;

        public cLines()
        {
            index = 0;
            X = 0;
            Y = 0;
        }

        public cLines(int _index, float _X, float _Y)
        {
            index = _index;
            X = _X;
            Y = _Y;
        }





    }


    ////массив отрезков
    //public class LPoints
    //{
    //    public List<PointF> points = new List<PointF>();

    //    public LPoints(List<PointF> points)
    //    {
    //        this.points = points;
    //    }

    //    public LPoints()
    //    {
    //        this.points = null;
    //    }
    //}
    
    ////описание 1-й точки
    //public class mPoint
    //{
    //    public int X;
    //    public int Y;

    //    public mPoint(int px,int py)
    //    {
    //        X = px;
    //        Y = py;
    //    }
    //}
}








///// <summary>
///// Rotates the image by angle.
///// </summary>
///// <param name="oldBitmap">The old bitmap.</param>
///// <param name="angle">The angle.</param>
///// <returns></returns>
//private static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
//{

//    Bitmap newBitmap;

//    if (angle != 0)
//    {
//        int maxValue = oldBitmap.Height;
//        if (oldBitmap.Width > oldBitmap.Height) maxValue = oldBitmap.Width;
//        newBitmap = new Bitmap(maxValue, maxValue);
//    }
//    else
//    {
//        newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
//    }

//    var graphics = Graphics.FromImage(newBitmap);
//    graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
//    graphics.RotateTransform(angle);
//    graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
//    graphics.DrawImage(oldBitmap, new Point(0, 0));
//    return newBitmap;
//}