using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CNCImporterGkode
{
    public partial class SelectPLT : UserControl
    {
        public SelectPLT()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие при изменении параметров на данной форме
        /// </summary>
        public event EventHandler IsChange;


        private void SelectPLT_Load(object sender, EventArgs e)
        {

        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = @"Выбор PLT Corel Draw";
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = @"PLT Corel Draw (*.plt)|*.plt";
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
