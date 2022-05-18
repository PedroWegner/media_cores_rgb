using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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
        double[] vetor = new double[1];
        List<double[]> pessoas = new List<double[]>();



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
            startButton_Click(sender, e);
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
            count = 0;
            for (int i = 0; i < array.Length; i += 3)
            {
                if (!(array[i + 0] < 128 && array[i + 1] < 128 && array[i + 2] < 128))
                {
                    count++;
                    array[i + 0] = 255;
                    array[i + 1] = 255;
                    array[i + 2] = 255;
                }
                else
                {
                    somaB += array[i + 0];
                    somaG += array[i + 1];
                    somaR += array[i + 2];
                }



            }
            var mediaB = somaB / ((array.Length / 3) - count);
            var mediaG = somaG / ((array.Length / 3) - count);
            var mediaR = somaR / ((array.Length / 3) - count);
            var total = Math.Sqrt(mediaB * mediaB + mediaG * mediaG + mediaR * mediaR); // usando esse total aqui


            labelBlue.Text = (mediaB / mediaG).ToString();
            labelGreen.Text = (mediaG / mediaR).ToString();
            labelRed.Text = (mediaR / mediaB).ToString();
            labelByte.Text = total.ToString();

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

        private void salvarBtn_Click(object sender, EventArgs e)
        {
            // a ideia eh trocar esse teste para 'qtd_bytes'
            double teste = double.Parse(labelByte.Text);
            double[] pessoa = { double.Parse(labelBlue.Text) * teste, double.Parse(labelGreen.Text) * teste, double.Parse(labelRed.Text)* teste };
            pessoas.Add(pessoa);

        }
        StringBuilder st = null;
        private void showPessoas_Click(object sender, EventArgs e)
        {
            st = new StringBuilder("");
            foreach (double[] i in pessoas)
            {
                st.AppendLine(i[0].ToString() + " - " + i[1].ToString() + " - " + i[2].ToString());
            }
            label1.Text = st.ToString();


            double teste = double.Parse(labelByte.Text);
            // a ideia eh trocar esse teste para 'qtd_bytes'
            double[] rosto = {double.Parse(labelBlue.Text) * teste, double.Parse(labelGreen.Text) * teste, double.Parse(labelRed.Text)* teste };
            StringBuilder rosto_str = new StringBuilder("");

            for(int i = 0; i < rosto.Length; i++)
            {
                rosto_str.Append(rosto[i].ToString());
            }
            label2.Text = rosto_str.ToString();

            double menor_dist = 99999.0;
            double dist = 0.0;
            double[] rosto_parecido = null;
            foreach (double[] i in pessoas)
            {
                dist = Math.Sqrt(Math.Pow((rosto[0] - i[0]), 2) + Math.Pow((rosto[1] - i[1]), 2) + Math.Pow((rosto[2] - i[2]), 2));

                if(dist < menor_dist)
                {
                    menor_dist = dist;
                    rosto_parecido = i;
                }
            }

            StringBuilder testes = new StringBuilder("");
            for (int i = 0; i < rosto_parecido.Length; i++)
            {
                testes.Append(rosto_parecido[i].ToString());
            }
            label3.Text = menor_dist.ToString();
            label4.Text = testes.ToString();
        }
    }
}
