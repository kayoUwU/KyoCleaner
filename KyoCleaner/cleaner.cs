
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace KyoCleaner
{
    public partial class cleaner : Form
    {

        string temp = Environment.ExpandEnvironmentVariables("%temp%");
        string recent = Environment.ExpandEnvironmentVariables(@"%userprofile%\recent");
        string prefetch = @"C:\Windows\Prefetch";

        string kyo_local_config = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

        public cleaner()
        {
            InitializeComponent();
        }

        private void cleaner_Load(object sender, EventArgs e)
        {

            checkBox1.Text = temp;
            checkBox2.Text = recent;
            checkBox3.Text = prefetch;
            checkBox4.Text = "Limpar lixeira";
        }

        private void apagar(string local)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(local);
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {

                    file.Delete();

                }
                catch
                {
                    // não foi possível apagar
                }

            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch
                {
                    // não foi possível apagar
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                try
                {
                    apagar(temp);
                    label3.ForeColor = Color.LightGreen;
                    label3.Text = "limpeza concluida";
                }
                catch
                {

                }
            }

            if (checkBox2.CheckState == CheckState.Checked)
            {
                try
                {
                    apagar(recent);
                    label3.ForeColor = Color.LightGreen;
                    label3.Text = "limpeza concluida: recent ";
                }
                catch
                {

                }
            }

            if (checkBox3.CheckState == CheckState.Checked)
            {
                try
                {
                    apagar(prefetch);
                    label3.ForeColor = Color.LightGreen;
                    label3.Text = "limpeza concluida";
                }
                catch
                {

                }

            }
            if (checkBox4.CheckState == CheckState.Checked)
            {
                try
                {
                    uint IsSuccess = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHRB_NOCONFIRMATION);
                    label3.Text = "limpeza concluida";
                }
                catch
                {

                }
            }
        }

        enum RecycleFlags : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            SHRB_NOSOUND = 0x00000004
        }


        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);

        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
