/*
 * ru - Данная панель является частью графического интерфейса для отображения координат станка
 * 
 * en - This panel is part of a graphical user interface for displaying the machine coordinate
 * 
 */


using System;
using System.Windows.Forms;

namespace CNC_Assist
{
    public partial class GuiPanelPos : UserControl
    {
        public GuiPanelPos()
        {
            InitializeComponent();

            groupBoxPositions.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_WitgetPosition_");

        }

        private void buttonXtoZero_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.ResetToZeroAxes("X");
        }

        private void buttonYtoZero_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.ResetToZeroAxes("Y");
        }

        private void buttonZtoZero_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.ResetToZeroAxes("Z");
       }

        private void buttonAtoZero_Click(object sender, EventArgs e)
        {
            ControllerPlanetCNC.ResetToZeroAxes("A");
        }

        private void timer_Refresh_Tick(object sender, EventArgs e)
        {
            if (ControllerPlanetCNC.IsConnectedToController)
            {
                numPosX.Value = ControllerPlanetCNC.Info.AxesX_PositionMM;
                numPosY.Value = ControllerPlanetCNC.Info.AxesY_PositionMM;
                numPosZ.Value = ControllerPlanetCNC.Info.AxesZ_PositionMM;
                numPosA.Value = ControllerPlanetCNC.Info.AxesA_PositionMM;

                if (ControllerPlanetCNC.IsAvailability)
                {
                    Enabled = true;
                    buttonXtoZero.Enabled = true;
                    buttonYtoZero.Enabled = true;
                    buttonZtoZero.Enabled = true;
                    buttonAtoZero.Enabled = true;




                }
                else
                {
                    buttonXtoZero.Enabled = false;
                    buttonYtoZero.Enabled = false;
                    buttonZtoZero.Enabled = false;
                    buttonAtoZero.Enabled = false;
                }
            }
            else
            {
                Enabled = false;
            }
        }

        private void GUI_panel_POS_Load(object sender, EventArgs e)
        {
            //тут настроим вывод осей X,Y,Z,A
            int heightNow = 14;

            if (GlobalSetting.ControllerSetting.AxleX.UsedAxes)
            {
                buttonXtoZero.Visible = true;
                labelposX.Visible = true;
                numPosX.Visible = true;
                heightNow += 33;
            }
            else
            {
                buttonXtoZero.Visible = false;
                labelposX.Visible = false;
                numPosX.Visible = false;
            }


            if (GlobalSetting.ControllerSetting.AxleY.UsedAxes)
            {
                buttonYtoZero.Visible = true;
                labelposY.Visible = true;
                numPosY.Visible = true;

                buttonYtoZero.Top = heightNow;
                labelposY.Top = heightNow + 6;
                numPosY.Top = heightNow;

                heightNow += 33;
            }
            else
            {
                buttonYtoZero.Visible = false;
                labelposY.Visible = false;
                numPosY.Visible = false;
            }


            if (GlobalSetting.ControllerSetting.AxleZ.UsedAxes)
            {
                buttonZtoZero.Visible = true;
                labelposZ.Visible = true;
                numPosZ.Visible = true;

                buttonZtoZero.Top = heightNow;
                labelposZ.Top = heightNow + 6;
                numPosZ.Top = heightNow;

                heightNow += 33;
            }
            else
            {
                buttonZtoZero.Visible = false;
                labelposZ.Visible = false;
                numPosZ.Visible = false;
            }


            if (GlobalSetting.ControllerSetting.AxleA.UsedAxes)
            {
                buttonAtoZero.Visible = true;
                labelposA.Visible = true;
                numPosA.Visible = true;

                buttonAtoZero.Top = heightNow;
                labelposA.Top = heightNow + 6;
                numPosA.Top = heightNow;

                heightNow += 33;
            }
            else
            {
                buttonAtoZero.Visible = false;
                labelposA.Visible = false;
                numPosA.Visible = false;
            }

            groupBoxPositions.Height = heightNow;
            Height = heightNow + 6;
        }
    }
}
