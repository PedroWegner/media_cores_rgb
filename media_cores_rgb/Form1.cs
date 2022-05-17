using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace media_cores_rgb
{
    public partial class Form1 : Form
    {
        #region Variables
        FilterInfoCollection fic;
        VideoCaptureDevice vdc;
        Bitmap bmp = null;
        double somaR = 0;
        double somaG = 0;
        double somaB = 0;
        double count = 0;




        #endregion

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // procura os perifericos capazes de capturar video
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in fic)
            {
                comboWeb.Items.Add(filterInfo.Name);

            }
            comboWeb.SelectedIndex = 0;
            vdc = new VideoCaptureDevice();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            vdc = new VideoCaptureDevice(fic[comboWeb.SelectedIndex].MonikerString);
            vdc.NewFrame += VideoCaptureFrames;
            vdc.Start();
        }

        private void VideoCaptureFrames(object sender, NewFrameEventArgs eventArgs)
        {


            bmp = (Bitmap)eventArgs.Frame.Clone();
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var array = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, array, 0, array.Length);

            somaB = 0;
            somaG = 0;
            somaR = 0;
            for (int i = 0; i < array.Length; i += 3)
            {
                if (!(array[i + 0] < 190 && array[i + 1] < 190 && array[i + 2] < 190))
                {
                    array[i + 0] = 255;
                    array[i + 1] = 0;
                    array[i + 2] = 0;
                }
                somaB += array[i + 0];
                somaG += array[i + 1];
                somaR += array[i + 2];

            }

            labelBlue.Text = "Blue: " + (somaB / (array.Length / 3)).ToString();
            labelGreen.Text = "Green: " + (somaG / (array.Length / 3)).ToString();
            labelRed.Text = "Red: " + (somaR / (array.Length / 3)).ToString();
            labelByte.Text = "Qtd bytes: " + (array.Length / 3).ToString();

            Marshal.Copy(array, 0, data.Scan0, array.Length);
            bmp.UnlockBits(data);

            pictureWeb.Image?.Dispose(); // para liberar consumo de memoria
            pictureWeb.Image = bmp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (vdc.IsRunning == true)
            {
                vdc.Stop();
            }
        }
    }
}
