using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ToolsLaserGrav
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private int numStepNow = 0;
        private Bitmap sourceBitmap = null;

        private void menuOpenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = @"Выбор файла шрифта";
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                numStepNow = 1;
                sourceBitmap = new Bitmap(openFileDialog1.FileName);
                DrawImage();
            }
        }



        void DrawImage()
        {
            if (numStepNow == 0) return;

            Bitmap tmp = GetImageFromStep();
            pictureBoxPreview.Image = tmp;

            labelTextSize.Text = @"Размер рисунка: " + tmp.Width.ToString() + " x " + tmp.Height.ToString() + " пикселей";

            if (checkBoxStep1Refresh.Checked)
            {
                pictureBoxPreview.Image = tmp;

                if (radioButton_Zoom.Checked)
                {
                    pictureBoxPreview.Width = panel1.ClientSize.Width - 2;
                    pictureBoxPreview.Height = panel1.ClientSize.Height - 26;
                    panel1.AutoScrollMinSize = new Size(0, 0);
                    panel1.AutoScroll = false;
                }
                else
                {
                    pictureBoxPreview.Width = pictureBoxPreview.Image.Width;
                    pictureBoxPreview.Height = pictureBoxPreview.Image.Height;

                    panel1.AutoScrollMinSize = new Size(pictureBoxPreview.Width, pictureBoxPreview.Height);
                    panel1.AutoScroll = true;
                }
            }


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
        private Bitmap GetImageFromStep()
        {
            Bitmap bitmap = null;

            //// ШАГ 1
            //if (radioButtonTypeSourceText.Checked)
            //{
            //    bitmap = CreateBitmapFromText();
            //}
            //else
            //{
            //bitmap = sourceBitmap.Clone();
            Bitmap newbitmap = new Bitmap(sourceBitmap, sourceBitmap.Width, sourceBitmap.Height);
                //Graphics g = Graphics.FromImage(newbitmap);
                //g.TranslateTransform(1, 1);
                bitmap = newbitmap;
           // }

                if (numStepNow == 1) return bitmap;

            //// ШАГ 2
            bitmap = ConvertImageToBlackWhileColor(bitmap);

            if (numStepNow == 2) return bitmap;

            //bitmap = FlipBitmap(bitmap);

            //if (NumStep == 3) return bitmap;

            //bitmap = RotateBitmap(bitmap);

            //if (NumStep == 4) return bitmap;

            //bitmap = InvertBlackWhileColor(bitmap);

            //if (NumStep == 5) return bitmap;

            //bitmap = BitmapDeleteContent(bitmap);

            //if (NumStep == 6) return bitmap;


            return bitmap;
        }

        private void radioButton_FullSize_CheckedChanged(object sender, EventArgs e)
        {
            DrawImage();
        }

        private void radioButton_Zoom_CheckedChanged(object sender, EventArgs e)
        {
            DrawImage();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

            DrawImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

            int koef = (int)trackBarVar1Koeff.Value;

            // Копируем значения в массив.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int fy = 0; fy < bmpData.Height; fy++)
            {
                for (int fx = 0; fx < bmpData.Width; fx++)
                {
                    int positionRam = (fy * bmpData.Stride) + (fx * 3);

                    int value = rgbValues[positionRam] + rgbValues[positionRam + 1] + rgbValues[positionRam + 2];

                    value = value / 3;

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




        private void button4_Click(object sender, EventArgs e)
        {
            //применим фильт преобразования в ЧБ
            numStepNow = 2;

            Bitmap tmp = GetImageFromStep();


            int newSizeX = (int)(numericUpDownCalcX.Value/numericUpDownVar1SizeLaserLine.Value);
            int newSizeY = (int)(numericUpDownCalcY.Value/numericUpDownVar1SizeLaserLine.Value);

            //перемаштабируем изображение
            Bitmap objBitmap = new Bitmap(tmp, new Size(newSizeX, newSizeY));

            // и глянем результат
            pictureBoxPreview.Image = objBitmap;

            //и пройдемся по рисунку
            string sGkode = "%" + Environment.NewLine;

            sGkode += @"G0 F" + numericUpDownVar1speed.Value.ToString() + Environment.NewLine;
            sGkode += @"M5 X0 Y0 Z0" + Environment.NewLine;

            //для движения змейкой по оси Y
            int maxX = objBitmap.Width - 1;
            decimal x = 0;
            bool toLeftX = true;


            decimal st = numericUpDownVar1SizeLaserLine.Value;


            for (decimal y = 0; y < objBitmap.Height; y++)
            {

                bool OldneedBurn = false;
                bool needBurn = false;

                int startPos = 0;

                bool needLoop = true;

                while (needLoop)
                {
                    Color colorNow = objBitmap.GetPixel((int)x, (int)y);

                    needBurn = (colorNow.R == 0);


                    if (OldneedBurn != needBurn)
                    {
                        OldneedBurn = needBurn;

                        if (needBurn)
                        {
                            sGkode += @"M5 X" + (x*st) + " Y" + (y*st) + " Z0" + Environment.NewLine;
                            sGkode += @"M3" + Environment.NewLine;
                        }
                        else
                        {
                            sGkode += @"X" + (x * st) + " Y" + (y * st) + " Z0" + Environment.NewLine;
                            sGkode += @"M5" + Environment.NewLine;
                        }
                    }

                    if (toLeftX) x++; else x--;

                    if (toLeftX && x > maxX)
                    {
                        x--;
                        needLoop = false;
                        toLeftX = false;
                    }

                    if (!toLeftX && x < 0)
                    {
                        x++;
                        needLoop = false;
                        toLeftX = true;
                    }


                    if (!needLoop)
                    {
                        if (needBurn)
                        {
                            sGkode += @"X" + (x * st) + " Y" + (y * st) + " Z0" + Environment.NewLine;
                            sGkode += @"M5" + Environment.NewLine;
                        }
                    }
                }
            }

            textBox1.Text = sGkode;

        }



        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.Invalid;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }



        private void trackBarVar1Koeff_Scroll(object sender, EventArgs e)
        {
            numStepNow = 2;
            DrawImage();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // и глянем результат
            pictureBoxPreview.Image = resizeImage(sourceBitmap,new Size(100,100));
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }



    }
}
