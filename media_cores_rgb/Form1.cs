using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace media_cores_rgb
{
    public partial class Form1 : Form
    {
        FilterInfoCollection fic;
        VideoCaptureDevice vdc;
        Bitmap bmp = null;
        Graphics g = null;

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
            pictureWeb.Image?.Dispose();
            pictureWeb.Image = (Bitmap)eventArgs.Frame.Clone();

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
