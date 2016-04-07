using System.Diagnostics;
using System.Windows.Forms;

namespace ToolsImporterVectors
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://selenur.ru");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://money.yandex.ru/to/41001112863318");

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void About_Load(object sender, System.EventArgs e)
        {

        }
    }
}
