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
            numPosX.Value = ControllerPlanetCNC.Info.AxesX_PositionMM;
            numPosY.Value = ControllerPlanetCNC.Info.AxesY_PositionMM;
            numPosZ.Value = ControllerPlanetCNC.Info.AxesZ_PositionMM;
        }

        private void frmPoint_Load(object sender, EventArgs e)
        {

        }
    }
}
