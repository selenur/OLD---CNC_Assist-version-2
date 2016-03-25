using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ToolsImporterVectors
{
    public partial class PLTcontrol : UserControl
    {
        public string FileNAME = "";



        public PLTcontrol()
        {
            InitializeComponent();
        }


        //float xmin;
        //float xmax;
        //float ymin;
        //float ymax;


        private void buttonGetFromFile_Click(object sender, EventArgs e)
        {
            //xmin = 9999;
            //xmax = -9999;
            //ymin = 9999;
            //ymax = -9999;
            
            List<PointF> tmPoints = new List<PointF>();
            ArrayData.segments.Clear();

            int numRow = 1;

            StreamReader fs = new StreamReader(FileNAME);
            string s = fs.ReadLine();

            while (s != null)
            {
                s = s.Trim();

                if (checkBoxIsCorel.Checked)
                {
                    int pos1 = -1, pos2 = -1, pos3 = -1;

                    //начальная точка
                    if (s.Substring(0, 2) == "PU" && s.Length > 3)
                    {
                        //это не самый первый сегмент, поэтому перед заполнением нового, предыдущий сохраним
                        if (tmPoints.Count > 0)
                        {
                            ArrayData.segments.Add(new SegmentCollections(tmPoints));
                            tmPoints = new List<PointF>();
                        }

                        pos1 = s.IndexOf('U');
                        pos2 = s.IndexOf(' ');
                        pos3 = s.IndexOf(';');
                    }

                    //продолжение
                    if (s.Substring(0, 2) == "PD" && s.Length > 3)
                    {
                        pos1 = s.IndexOf('D');
                        pos2 = s.IndexOf(' ');
                        pos3 = s.IndexOf(';');
                    }

                    // завершение
                    if (s.Substring(0, 3) == "SP0")
                    {
                        ArrayData.segments.Add(new SegmentCollections(tmPoints));
                        tmPoints = new List<PointF>();
                        s = fs.ReadLine();
                        numRow++;
                        continue;
                    }

                    if (pos1 == -1 || pos2 == -1 || pos3 == -1)//какая-то ненужная пока строка
                    {
                        s = fs.ReadLine();
                        numRow++;
                        continue;
                    }

                    float posX, posY;

                    if (!float.TryParse(s.Substring(pos1 + 1, pos2 - pos1 - 1), out posX))
                    {
                        MessageBox.Show(@"Ошибка преобразования координаты X в строке № " + numRow.ToString());
                        break;
                    }

                    if (!float.TryParse(s.Substring(pos2 + 1, pos3 - pos2 - 1), out posY))
                    {
                        MessageBox.Show(@"Ошибка преобразования координаты Y в строке № " + numRow.ToString());
                        break;
                    }

                    // Пересчет в милиметры
                    posX = posX / 40;
                    posY = posY / 40;


                    //if (posX > xmax) xmax = posX;

                    //if (posX < xmin) xmin = posX;

                    //if (posY > ymax) ymax = posY;

                    //if (posY < ymin) ymin = posY;


                    tmPoints.Add(new PointF(posX, posY));
                }
                else
                {
                    //начальная точка отрезка, или конечная
                    if (s.Trim().Substring(0, 3) == "PU;" || s.Trim().Substring(0, 3) == "PD;")
                    {
                        if (tmPoints.Count > 2)
                        {
                            //добавим последнюю точку с координатами первой, для получения замкнутого котура
                            tmPoints.Add(tmPoints[0]);
                            ArrayData.segments.Add(new SegmentCollections(tmPoints));
                        }
                        tmPoints = new List<PointF>();
                    }

                    //продолжение
                    if (s.Trim().Substring(0, 2) == "PA" && s.Trim().Length > 3)
                    {
                        int pos1 = s.IndexOf('A');
                        int pos2 = s.IndexOf(',');
                        int pos3 = s.IndexOf(';');

                        if (pos1 == -1 || pos2 == -1 || pos3 == -1)
                        {
                            MessageBox.Show(@"Ошибка парсинга строки с координатами в строке № " + numRow.ToString());
                            break;
                        }

                        float posX, posY;

                        if (!float.TryParse(s.Substring(pos1 + 1, pos2 - pos1 - 1), out posX))
                        {
                            MessageBox.Show(@"Ошибка преобразования координаты X в строке № " + numRow.ToString());
                            break;
                        }

                        if (!float.TryParse(s.Substring(pos2 + 1, pos3 - pos2 - 1), out posY))
                        {
                            MessageBox.Show(@"Ошибка преобразования координаты Y в строке № " + numRow.ToString());
                            break;
                        }

                        // Пересчет в милиметры
                        posX = posX / 40;
                        posY = posY / 40;

                        tmPoints.Add(new PointF(posX, posY));
                    }
                }




                s = fs.ReadLine();
                numRow++;
            }
          
            fs = null;

            labelNumTools.Text = ArrayData.segments.Count.ToString();

            int countPoint = 0;

            foreach (SegmentCollections seg in ArrayData.segments)
            {
                countPoint += seg.Points.Count;
            }

            labelNumDrills.Text = countPoint.ToString();

            treeViewPLT.Nodes.Clear();

            int numsegment = 1;
            foreach (SegmentCollections seg in ArrayData.segments)
            {
                TreeNode trc = new TreeNode("Отрезок № " + (numsegment++).ToString() + " точек: " + seg.Points.Count.ToString());

                foreach (PointF pnt in seg.Points)
                {
                    trc.Nodes.Add("X: " + pnt.X.ToString() + " Y: " + pnt.Y.ToString());
                }
                treeViewPLT.Nodes.Add(trc);
            }

            //labelSizeX.Text = "X от: " + xmin.ToString() + " до " + xmax.ToString();
            //labelSizeY.Text = "Y от: " + ymin.ToString() + " до " + ymax.ToString();
        }


    }


}


//if (indexList == -1)
//{
//    //первый раз
//    indexList++;
//}
//else
//{
//    indexList++;
//    //checkedListBox1.Items.Add("линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек");
//    //trc.Text = "линия - " + indexList.ToString() + ", " + points.Count.ToString() + " точек";

//    data.Add(new DataCollections(points));
//    points = new List<Point>();

//    //  treeView1.Nodes.Add(trc);
//    //  trc = new TreeNode("");
//}
//points.Add(new Point(posX, posY));

//float posX = float.Parse(s.Substring(pos1 + 1, pos2 - pos1 - 1));
//float posY = float.Parse(s.Substring(pos2 + 1, pos3 - pos2 - 1));

//// Пересчет в милиметры
//posX = posX / 40;
//posY = posY / 40;

//points.Add(new Point(posX, posY));
////   trc.Nodes.Add("Точка - X: " + posX.ToString() + "  Y: " + posY.ToString());