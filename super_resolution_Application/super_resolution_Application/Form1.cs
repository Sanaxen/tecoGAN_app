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

        /// <summary>
        /// 指定したファイルをロックせずに、System.Drawing.Imageを作成する。
        /// </summary>
        /// <param name="filename">作成元のファイルのパス</param>
        /// <returns>作成したSystem.Drawing.Image。</returns>
        public static System.Drawing.Image CreateImage(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(
                filename,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return img;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = CreateImage(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var apppath = System.IO.Path.GetDirectoryName(
                System.IO.Path.GetFullPath(
                    Environment.GetCommandLineArgs()[0]));

            //apppath = @"D:\tecoGAN_app\super_resolution_Application\super_resolution_Application\dist";
            System.Environment.CurrentDirectory = apppath + "\\main";

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
            for (int i = 0; i < 15; i++)
            {
#if false
                string newfile = string.Format(@"LR\calendar\{0:D4}"+".png", i);
                pictureBox1.Image.Save(newfile, System.Drawing.Imaging.ImageFormat.Png);
                //File.Copy(openFileDialog1.FileName, newfile, true);
#else
                string newfile1 = string.Format(@"LR\calendar\{0:D4}_" + ".png", i);
                pictureBox1.Image.Save(newfile1, System.Drawing.Imaging.ImageFormat.Png);

                string newfile2 = string.Format(@"LR\calendar\{0:D4}" + ".png", i);
                var imagemagick = new System.Diagnostics.ProcessStartInfo();
                imagemagick.FileName = "cmd.exe";
                imagemagick.UseShellExecute = true;
                imagemagick.Arguments = "/c";
                imagemagick.Arguments += " ..\\conv.bat";
                imagemagick.Arguments += " " + newfile1;
                imagemagick.Arguments += " \"1,0,0,1,0," + (-i).ToString()+"\"";
                imagemagick.Arguments += " " + newfile2;
                System.Diagnostics.Process imagemagick_p = System.Diagnostics.Process.Start(imagemagick);
                imagemagick_p.WaitForExit();
                File.Delete(newfile1);
#endif
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
            pictureBox2.Image = CreateImage(outfile);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
