using System;
using System.Windows.Forms;

namespace CNCImporterGkode
{
    public partial class SelectImage : UserControl
    {
        public SelectImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие при изменении параметров на данной форме
        /// </summary>
        public event EventHandler IsChange;

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.Multiselect = false;
                openFileDialog1.Title = @"Выбор рисунка";
                //openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxFileName.Text = openFileDialog1.FileName;
                }

            CreateEvent();

        }

        void CreateEvent()
        {
            EventArgs e = new EventArgs();
            //вызовем событие
            if (IsChange != null) IsChange(this, e);
        }

        private void btShowOriginalImage_Click(object sender, EventArgs e)
        {
            CreateEvent();
        }
    }
}
