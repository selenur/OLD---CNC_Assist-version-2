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
            this.Enabled = Controller.IsConnectedToController;
            Bitmap bGrey = Properties.Resources.draw_ellipse;
            Bitmap bRed = Properties.Resources.ball_red;

            labelXmax.Image = Controller.Info.AxesXLimitMax ? bRed : bGrey;
            labelXmin.Image = Controller.Info.AxesXLimitMin ? bRed : bGrey;
            labelYmax.Image = Controller.Info.AxesYLimitMax ? bRed : bGrey;
            labelYmin.Image = Controller.Info.AxesYLimitMin ? bRed : bGrey;
            labelZmax.Image = Controller.Info.AxesZLimitMax ? bRed : bGrey;
            labelZmin.Image = Controller.Info.AxesZLimitMin ? bRed : bGrey;
            //TODO: add AxesA
        }
    }
}
