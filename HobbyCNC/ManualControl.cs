using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNC_Assist
{
    public partial class ManualControl : Form
    {
        public ManualControl()
        {
            InitializeComponent();
        }

        private void KeyInfo_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ControllerPlanetCNC.IsConnectedToController)
            {

                groupBox1.Enabled = ControllerPlanetCNC.IsAvailability;
   
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        private void SendCommand(string _X = "", string _Y = "", string _Z = "", string _A = "")
        {
            if (_X == "" && _Y == "" && _Z == "" && _A == "")
            {
                ControllerPlanetCNC.ExecuteCommand("M201"); //остановка ручного движения
                return;
            }

            string sendValue = "M200 F" + numericUpDown1.Value.ToString("0000");

            if (_X != "") sendValue += " X" + _X;
            if (_Y != "") sendValue += " Y" + _Y;
            if (_Z != "") sendValue += " Z" + _Z;
            if (_A != "") sendValue += " A" + _A;

            ControllerPlanetCNC.ExecuteCommand(sendValue);
        }


        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            button8.BackColor = Color.DarkGreen;
            SendCommand("-","+");
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            button8.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.DarkGreen;
            SendCommand("", "+");
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            button10.BackColor = Color.DarkGreen;
            SendCommand("+", "+");
        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            button10.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            button7.BackColor = Color.DarkGreen;
            SendCommand("+");
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            button7.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            button11.BackColor = Color.DarkGreen;
            SendCommand("+", "-");
        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            button11.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackColor = Color.DarkGreen;
            SendCommand("", "-");
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            button3.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            button9.BackColor = Color.DarkGreen;
            SendCommand("-", "-");
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            button9.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            button6.BackColor = Color.DarkGreen;
            SendCommand("-");
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            button6.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.BackColor = Color.DarkGreen;
            SendCommand("", "", "+");
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            button4.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            button5.BackColor = Color.DarkGreen;
            SendCommand("", "", "-");
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            button5.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            //a+
            button1.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

            //a+
            button1.BackColor = Color.DarkGreen;
            SendCommand("", "", "", "+");
        }

        private void button12_MouseUp(object sender, MouseEventArgs e)
        {
            //a-
            button12.BackColor = Color.FromName("Control");
            SendCommand();
        }

        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            //a-
            button12.BackColor = Color.DarkGreen;
            SendCommand("", "", "", "-");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.EnergyStop();
        }

    }
}
