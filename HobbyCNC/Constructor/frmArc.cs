using System;
using System.Windows.Forms;

namespace CNC_Assist.Constructor
{
    public partial class frmArc : Form
    {
        public frmArc()
        {
            InitializeComponent();
        }

        private void btGetPosition_Click(object sender, EventArgs e)
        {
            num_centerX.Value = Controller.Info.AxesX_PositionMM;
            num_centerY.Value = Controller.Info.AxesY_PositionMM;
            num_centerZ.Value = Controller.Info.AxesZ_PositionMM;
            CalculatePos();
        }

        // Вычисление начальной и конечной точки
        private void CalculatePos()
        {
            arcStartX.Value = (decimal)((double)num_centerX.Value + (double)num_rotateRadius.Value * Math.Cos((double)num_rotateStartAngle.Value * (Math.PI / 180)));
            arcStartY.Value = (decimal)((double)num_centerY.Value + (double)num_rotateRadius.Value * Math.Sin((double)num_rotateStartAngle.Value * (Math.PI / 180)));

            arcStopX.Value = (decimal)((double)num_centerX.Value + (double)num_rotateRadius.Value * Math.Cos((double)num_rotateStopAngle.Value * (Math.PI / 180)));
            arcStopY.Value = (decimal)((double)num_centerY.Value + (double)num_rotateRadius.Value * Math.Sin((double)num_rotateStopAngle.Value * (Math.PI / 180)));

            arcStartZ.Value = num_centerZ.Value;
            arcStopZ.Value = num_centerZ.Value;

        }

        private void centerX_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void centerY_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void centerZ_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void rotateRadius_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void rotateStartAngle_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void rotateStopAngle_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void rotateStepAngle_ValueChanged(object sender, EventArgs e)
        {
            CalculatePos();
        }

        private void frmRotate_Load(object sender, EventArgs e)
        {

        }

        private void cb_DublicateData_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
