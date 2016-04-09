using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CNCImporterGkode
{
    public partial class SelectFont : UserControl
    {
        /// <summary>
        /// Событие при изменении параметров на данной форме
        /// </summary>
        public event EventHandler IsChange;

        public bool UseSystemFont = true;
        public bool UseVectorFont = true;

        public SelectFont()
        {
            InitializeComponent();
        }

        private void SelectFont_Load(object sender, EventArgs e)
        {

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            foreach (FontFamily fnt in installedFontCollection.Families)
            {
                comboBoxFont.Items.Add(fnt.Name);
            }
            comboBoxFont.Text = comboBoxFont.Items[0].ToString();

            ChangeVisible();

        }

        private void ChangeVisible()
        {
            UseSystemFont = _UseSystemFont.Checked;
            UseVectorFont = _UseFontVector.Checked;

            if (UseSystemFont)
            {
                buttonSetFontFile.Visible = false;
                nameFontFile.Visible = false;
                comboBoxFont.Visible = true;
            }
            else
            {
                buttonSetFontFile.Visible = true;
                nameFontFile.Visible = true;
                comboBoxFont.Visible = false;
            }
            CreateEvent();
        }

        void CreateEvent()
        {
            EventArgs e = new EventArgs();
            //вызовем событие
            if (IsChange != null) IsChange(this, e); 
        }

        private void radioButtonFromSystem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }

        private void radioButtonFromFile_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }

        private void buttonSetFontFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Title = @"Выбор файла шрифта",
                Filter = @"Font files (*.ttf)|*.ttf",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                nameFontFile.Text = ofDialog.FileName;
                CreateEvent();
            }
        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateEvent();
        }

        private void radioButtonFontVector_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }

        private void radioButtonFontBitmap_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }

        private void textSize_ValueChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }

        private void textString_TextChanged(object sender, EventArgs e)
        {
            ChangeVisible();
        }
    }
}
