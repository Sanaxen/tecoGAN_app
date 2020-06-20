using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace super_resolution_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] cmds = System.Environment.GetCommandLineArgs();
            if ( cmds.Length >= 2)
            {
                openFileDialog1.FileName = cmds[1];
                pictureBox1.Image = System.Drawing.Image.FromFile(cmds[1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var apppath = System.IO.Path.GetDirectoryName(
                System.IO.Path.GetFullPath(
                    Environment.GetCommandLineArgs()[0]));

            //apppath = @"D:\github\tecoGAN_app\super_resolution_Application\super_resolution_Application\dist";
            System.Environment.CurrentDirectory= apppath + "\\main";

            DirectoryInfo target = new DirectoryInfo(@"LR\calendar");
            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }
            target = new DirectoryInfo(@"results\calendar");
            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }
            for (int i = 0; i < 5; i++)
            {
                string ext = System.IO.Path.GetExtension(openFileDialog1.FileName);
                string newfile = string.Format(@"LR\calendar\{0:D4}"+ext, i);
                File.Copy(openFileDialog1.FileName, newfile, true);
            }
            var app = new System.Diagnostics.ProcessStartInfo();
            app.FileName = "main.exe";
            app.UseShellExecute = true;
            app.Arguments = " --cudaID 0";
            app.Arguments += " --output_dir ./results/";
            app.Arguments += " --summary_dir ./results/log/";
            app.Arguments += " --mode inference";
            app.Arguments += " --input_dir_LR ./LR/calendar";
            app.Arguments += " --output_pre calendar";
            app.Arguments += " --num_resblock 16";
            app.Arguments += " --checkpoint ./model/TecoGAN";
            app.Arguments += " --output_ext png";

            string directoryName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            string extension = System.IO.Path.GetExtension(openFileDialog1.FileName);

            System.Diagnostics.Process p = System.Diagnostics.Process.Start(app);
            p.WaitForExit();

            string outfile = "";
            outfile = directoryName + "\\" + fileName + "_super_res" + extension;
            System.IO.File.Copy(@"results\calendar\output_0001.png", outfile, true);
            pictureBox2.Image = System.Drawing.Image.FromFile(outfile);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
