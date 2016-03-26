using System;
using System.Windows.Forms;

namespace CNC_Assist
{
    public partial class GUI_panel_MoveTo : UserControl
    {
        public GUI_panel_MoveTo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ControllerPlanetCNC.IsAvailability) return;

            string sGcode = @"G0 F" + (int)numericUpDown3.Value + " X" + numericUpDown6.Value + " Y" + numericUpDown5.Value + " Z" + numericUpDown4.Value;

            if (!ControllerPlanetCNC.IsAvailability) return;

            ControllerPlanetCNC.ExecuteCommand(sGcode);
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (ControllerPlanetCNC.IsConnectedToController)
            {
                Enabled = ControllerPlanetCNC.IsAvailability;
            }
            else
            {
                Enabled = false;
            }
        }
    }
}
