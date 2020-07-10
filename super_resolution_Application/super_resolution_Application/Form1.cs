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
        int tecoGAN_output_num = 0;
        System.Diagnostics.Process tecoGAN = null;
        bool stopping = false;
        double Fps = 24;
        bool video = false;
        //string apppath = "";
        string apppath = @"D:\tecoGAN_app\super_resolution_Application\super_resolution_Application\dist";


        public Form1()
        {
            InitializeComponent();

            if (apppath == "")
            {
                apppath = System.IO.Path.GetDirectoryName(
                    System.IO.Path.GetFullPath(
                        Environment.GetCommandLineArgs()[0]));
            }

            string[] cmds = System.Environment.GetCommandLineArgs();
            if ( cmds.Length >= 2)
            {
                openFileDialog1.FileName = cmds[1];
                pictureBox1.Image = CreateImage(cmds[1]);
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
            if ( res == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(openFileDialog1.FileName);

                if (ext.ToLower() == ".bmp" || ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png")
                {
                    pictureBox1.Image = CreateImage(openFileDialog1.FileName);
                }
                if (ext.ToLower() == ".avi" || ext.ToLower() == ".mp4")
                {

                    System.IO.Directory.SetCurrentDirectory(apppath);
                    System.IO.DirectoryInfo target = new System.IO.DirectoryInfo(@"main\LR\calendar");
                    foreach (System.IO.FileInfo file in target.GetFiles())
                    {
                        file.Delete();
                    }
                    int image_num = 1;
                    video = true;
                    OpenCvSharp.VideoCapture vcap = new OpenCvSharp.VideoCapture(openFileDialog1.FileName);
                    while (vcap.IsOpened())
                    {
                        if (stopping) break;

                        OpenCvSharp.Mat mat = new OpenCvSharp.Mat();

                        if (vcap.Read(mat))
                        {
                            if (pictureBox1.Image != null)
                            {
                                pictureBox1.Image.Dispose();//Memory release
                            }

                            if (mat.IsContinuous())
                            {
                                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);

                                //OpenCvSharp.Cv2.Resize(mat, mat, OpenCvSharp.Size(), 0, 0);
                                string filename = string.Format(@"main\LR\calendar\{0:D4}.png", image_num);
                                //pictureBox1.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);

                                OpenCvSharp.Cv2.ImWrite(filename, mat, (int[])null);
                                image_num++;
                                //if (image_num > 10) break;
                            }
                            else
                            {
                                break;
                            }
                            Application.DoEvents(); // 非推奨
                        }
                        else
                        {
                            break;
                        }
                        System.Threading.Thread.Sleep((int)(1000 / vcap.Fps));

                        mat.Dispose();//Memory release
                    }
                    Fps = vcap.Fps;
                    vcap.Dispose();//Memory release
                    stopping = false;

                    pictureBox1.Image = CreateImage(@"main\LR\calendar\0001.png");
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }


        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.CurrentDirectory = apppath + "\\main";

            var target = new DirectoryInfo(@"results\calendar");
            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }

            if (!video )
            {
                if (openFileDialog1.FileName != "")
                {
                    DirectoryInfo target1 = new DirectoryInfo(@"LR\calendar");
                    foreach (FileInfo file in target1.GetFiles())
                    {
                        file.Delete();
                    }
                    var mat = OpenCvSharp.Cv2.ImRead(openFileDialog1.FileName);

                    for (int i = 0; i < 15; i++)
                    {
                        string newfile = string.Format(@"LR\calendar\{0:D4}" + ".png", i);
                        OpenCvSharp.Cv2.ImWrite(newfile, mat);
                    }
                }
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

            string directoryName = apppath;
            string fileName = "temp";
            string extension = ".png";

            if (openFileDialog1.FileName != "")
            {
                directoryName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                extension = System.IO.Path.GetExtension(openFileDialog1.FileName);
            }


            tecoGAN_output_num = 0;
            timer1.Start();
            tecoGAN = System.Diagnostics.Process.Start(app);
            tecoGAN.EnableRaisingEvents = true;
            tecoGAN.Exited += tecoGAN_Exited;
            //tecoGAN.WaitForExit();


        }
        private void tecoGAN_Exited(object sender, EventArgs e)
        {
            MessageBox.Show("終了しました");
            timer1.Stop();
            tecoGAN = null;

            string directoryName = apppath;
            string fileName = "temp";
            string extension = ".png";

            if (openFileDialog1.FileName != "")
            {
                directoryName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                extension = System.IO.Path.GetExtension(openFileDialog1.FileName);
            }

            if (!video)
            {
                string outfile = "";
                outfile = directoryName + "\\" + fileName + "_super_res" + extension;
                System.IO.File.Copy(@"results\calendar\output_0001.png", outfile, true);
                pictureBox2.Image = CreateImage(outfile);
            }
            else
            {
                button3_Click(sender, e);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.CurrentDirectory = apppath + "\\main";

            string directoryName = apppath;
            string fileName = "temp";
            string extension = ".png";

            if (openFileDialog1.FileName != "")
            {
                directoryName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                extension = System.IO.Path.GetExtension(openFileDialog1.FileName);
            }

            pictureBox2.Image = CreateImage(string.Format(@"results\calendar\output_{0:D4}" + ".png", 1));
            string outfile = "";
            outfile = directoryName + "\\" + fileName + "_super_res.avi";

            OpenCvSharp.Size sz = new OpenCvSharp.Size(pictureBox2.Image.Width, pictureBox2.Image.Height);
            int codec = 0; // コーデック(AVI)
            var EncodedFormat = OpenCvSharp.FourCC.MJPG;
            OpenCvSharp.VideoWriter vw = new OpenCvSharp.VideoWriter(outfile, EncodedFormat, Fps, sz, true);

            DirectoryInfo images = new DirectoryInfo(@"results\calendar");
            int filenum = 0;
            foreach (FileInfo file in images.GetFiles())
            {
                filenum++;
            }
            for (int i = 0; i < filenum; i++)
            {
                if (stopping) break;
                string newfile = string.Format(@"results\calendar\output_{0:D4}" + ".png", i);

                if (!System.IO.File.Exists(newfile)) continue;
                var img = OpenCvSharp.Cv2.ImRead(newfile, OpenCvSharp.ImreadModes.Color);
                //if (fileName != "temp")
                //{
                //    OpenCvSharp.Cv2.Resize(img, img, OpenCvSharp.Size.Zero, pictureBox1.Width, pictureBox1.Height, OpenCvSharp.InterpolationFlags.Cubic);
                //}

                pictureBox2.Image = CreateImage(newfile);
                newfile = string.Format(@"LR\calendar\{0:D4}" + ".png", i);
                if (!System.IO.File.Exists(newfile)) continue;
                pictureBox1.Image = CreateImage(newfile);
                vw.Write(img);
            }
            vw.Dispose();
            stopping = false;
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                stopping = true;
                if (tecoGAN != null)
                {
                    if (!tecoGAN.HasExited)
                    {
                        tecoGAN.Kill();
                        tecoGAN.Close();
                        tecoGAN = null;
                        timer1.Stop();
                    }
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tecoGAN == null) return;
            string newfile1 = string.Format(@"results\calendar\output_{0:D4}" + ".png", tecoGAN_output_num);
            string newfile2 = string.Format(@"results\calendar\output_{0:D4}" + ".png", tecoGAN_output_num+1);

            if (!System.IO.File.Exists(newfile1) && !System.IO.File.Exists(newfile2)) return;
            if (System.IO.File.Exists(newfile2))
            {
                try
                {
                    pictureBox2.Image = CreateImage(newfile2);

                    newfile2 = string.Format(@"LR\calendar\{0:D4}" + ".png", tecoGAN_output_num + 1);
                    pictureBox1.Image = CreateImage(newfile2);
                    tecoGAN_output_num += 1;
                }
                catch { }
            }
        }
    }

}
