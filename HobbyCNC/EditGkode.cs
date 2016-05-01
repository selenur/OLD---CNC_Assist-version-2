using System;
using System.Windows.Forms;

namespace CNC_Assist
{
    public partial class EditGkode : Form
    {

        public EditGkode()
        {
            InitializeComponent();
        }

        private void EditGkode_Load(object sender, EventArgs e)
        {

            cbCorrection.Checked = ControllerPlanetCNC.correctionPos.UseCorrection;
            numPosX.Value = (decimal)ControllerPlanetCNC.correctionPos.DeltaX;
            numPosY.Value = (decimal)ControllerPlanetCNC.correctionPos.DeltaY;
            numPosZ.Value = (decimal)ControllerPlanetCNC.correctionPos.DeltaZ;
            numPosA.Value = (decimal)ControllerPlanetCNC.correctionPos.DeltaA;

            //checkBox6.Checked = Setting.deltaFeed;

            //numericUpDown1.Value = (decimal)Setting.koeffSizeX;
            //numericUpDown2.Value = (decimal)Setting.koeffSizeY;

            //groupBox1.Enabled = cbCorrection.Checked;
            groupBox2.Enabled = cbCorrection.Checked;
            checkBoxUseMatrix.Enabled = cbCorrection.Checked;
        }

        private void ccbCorrection_CheckedChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.UseCorrection = cbCorrection.Checked;

            //groupBox1.Enabled = cbCorrection.Checked;
            groupBox2.Enabled = cbCorrection.Checked;
            checkBoxUseMatrix.Enabled = cbCorrection.Checked;
        }

        private void numPosX_ValueChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.DeltaX = numPosX.Value;
        }

        private void numPosY_ValueChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.DeltaY = numPosY.Value;
        }

        private void numPosZ_ValueChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.DeltaZ = numPosZ.Value;
        }

        private void checkBoxUseMatrix_CheckedChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.UseMatrix = checkBoxUseMatrix.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
           // Setting.koeffSizeX = (double)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           // Setting.koeffSizeY = (double)numericUpDown2.Value;
        }

        private void numPosA_ValueChanged(object sender, EventArgs e)
        {
            ControllerPlanetCNC.correctionPos.DeltaA = numPosA.Value;
        }
    }
}
