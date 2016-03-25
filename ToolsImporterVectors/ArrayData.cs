using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ToolsImporterVectors
{

    public class DRLPoint
    {
        public decimal X;
        public decimal Y;

        public DRLPoint(decimal _x, decimal _y)
        {
            X = _x;
            Y = _y;
        }
    }

    public class DRLTOOLS
    {
        public List<DRLPoint> points = null;
        public int numTool;
        public decimal DiametrTools;

        public DRLTOOLS(int _num, decimal _diametr, List<DRLPoint> _points)
        {
            numTool = _num;
            DiametrTools = _diametr;
            points = _points;
        }
    }






    //Набор однотипных данных
    public class SegmentCollections
    {
        public List<PointF> Points;

        /// <summary>
        /// Конструктор набора точек
        /// </summary>
        /// <param name="_Points">Список точек</param>
        public SegmentCollections(List<PointF> _Points)
        {
            Points = _Points;
        }
    }

    static class ArrayData
    {

        public static string Gkode = "";

        // Список инструментов, и их траектории полученные из DRL файла
        public static List<DRLTOOLS> DRL_tools = new List<DRLTOOLS>();   

        // список траекторий из PLT файла
        public static List<SegmentCollections> segments = new List<SegmentCollections>();


    }
}
