using System;
using System.Windows.Forms;

namespace CNC_Assist.Constructor
{
    public partial class frmPoint : Form
    {
        public frmPoint()
        {
            InitializeComponent();
        }

        private void btGetPosition_Click(object sender, EventArgs e)
        {
            numPosX.Value = ControllerPlanetCNC.Info.AxesXPositionMm;
            numPosY.Value = ControllerPlanetCNC.Info.AxesYPositionMm;
            numPosZ.Value = ControllerPlanetCNC.Info.AxesZPositionMm;
        }

        private void frmPoint_Load(object sender, EventArgs e)
        {

        }
    }
}
