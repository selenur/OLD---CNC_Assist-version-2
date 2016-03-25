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
            if (!Controller.TestAllowActions) return;

            string sGcode = @"G0 F" + (int)numericUpDown3.Value + " X" + numericUpDown6.Value + " Y" + numericUpDown5.Value + " Z" + numericUpDown4.Value;

            if (!Controller.TestAllowActions) return;

            Controller.ExecuteCommand(sGcode);
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (Controller.IsConnectedToController)
            {
                Enabled = Controller.TestAllowActions;
            }
            else
            {
                Enabled = false;
            }
        }
    }
}
