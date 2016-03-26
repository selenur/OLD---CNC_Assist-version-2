using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNC_Assist
{
    public partial class GUI_panel_Limits : UserControl
    {
        public GUI_panel_Limits()
        {
            InitializeComponent();

            groupBoxLimits.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_WigetLimit_");


            labelXmin.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitXmin_");
            labelYmin.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitYmin_");
            labelZmin.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitZmin_");
            labelXmax.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitXmax_");
            labelYmax.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitYmax_");
            labelZmax.Text = Language.GetTranslate(GlobalSetting.AppSetting.Language, "_LimitZmax_");


        }

        private void timerCheckStatus_Tick(object sender, EventArgs e)
        {
            this.Enabled = ControllerPlanetCNC.IsConnectedToController;
            Bitmap bGrey = Properties.Resources.draw_ellipse;
            Bitmap bRed = Properties.Resources.ball_red;

            labelXmax.Image = ControllerPlanetCNC.Info.AxesXLimitMax ? bRed : bGrey;
            labelXmin.Image = ControllerPlanetCNC.Info.AxesXLimitMin ? bRed : bGrey;
            labelYmax.Image = ControllerPlanetCNC.Info.AxesYLimitMax ? bRed : bGrey;
            labelYmin.Image = ControllerPlanetCNC.Info.AxesYLimitMin ? bRed : bGrey;
            labelZmax.Image = ControllerPlanetCNC.Info.AxesZLimitMax ? bRed : bGrey;
            labelZmin.Image = ControllerPlanetCNC.Info.AxesZLimitMin ? bRed : bGrey;
            //TODO: add AxesA
        }
    }
}
