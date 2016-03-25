using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace ToolsImporterVectors
{
    public partial class PreviewBox : UserControl
    {

        //для управления смещением центра осей в одну из сторон
        public int centerX = 0;
        public int centerY = 0;

        //масштам выводимых данных
        public float zoom = 1F;


        // список отрезков
        public List<SegmentCollections> segments = new List<SegmentCollections>();

        public PreviewBox()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Очистка рисунка от всех данных
        /// </summary>
        public void Clear()
        {
            

        }

        /// <summary>
        /// Отобразить отрезок
        /// </summary>
        /// <param name="start">Начальная точка</param>
        /// <param name="end">Конечная точка</param>
        public void Draw_Line(PointF start, PointF end)
        {
            
        }


        private Bitmap mybitmap;
        
        public void DRAW()
        {
            mybitmap = new Bitmap(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height, PixelFormat.Format32bppArgb);

            using (Graphics newgraphic = Graphics.FromImage(mybitmap))
            using (GraphicsPath path = new GraphicsPath())
            {
                newgraphic.SmoothingMode = SmoothingMode.AntiAlias;
                newgraphic.Clear(Color.CadetBlue);
                newgraphic.ScaleTransform(zoom,zoom);
                //newgraphic.TranslateClip(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height);
                newgraphic.TranslateTransform(pictureBox.ClientRectangle.Width/2, pictureBox.ClientRectangle.Height/2);
                




                foreach (SegmentCollections seg in ArrayData.segments)
                {

                    PointF[] arr = seg.Points.ToArray();



                    Pen p = new Pen(Color.Black,0.1f);

                    //newgraphic.DrawLines(p, arr);



                    newgraphic.DrawCurve(p,arr);



                    //вариант 1
                    ////Now you need to create a matrix object to apply transformation on your graphic
                    //Matrix mat = new Matrix();
                    //mat.Scale(zoom, zoom, MatrixOrder.Append); //zoom to 150%
                    //newgraphic.Transform = mat;


                    //newgraphic.     PageScale = (float)zoom;

                    //foreach (PointF pnt in seg.Points)
                    //{

                    //}

                }




            }
            pictureBox.Image = mybitmap;
        }


        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox_Move(object sender, EventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void zoomValue_ValueChanged(object sender, EventArgs e)
        {
            zoom = ((float) zoomValue.Value)/100;
            DRAW();

        }


        
    }

}
