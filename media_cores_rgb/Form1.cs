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
using media_cores_rgb.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;

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

        double total = 0.0;
        double menor_dist = 99999.0;
        double dist = 0.0;


        double mediaB = 0.0;
        double mediaG = 0.0;
        double mediaR = 0.0;

        double blue = 0.0;
        double green = 0.0;
        double red = 0.0;
        List<Usuario> listaUsuario = new List<Usuario>();
        #endregion

        #region Json variables
        string file = null;

        #endregion
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
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
                if (!(array[i + 0] < 76 && array[i + 1] < 76 && array[i + 2] < 76))
                {
                    count++;
                }
                else
                {
                    somaB += array[i + 0];
                    somaG += array[i + 1];
                    somaR += array[i + 2];
                }

            }
            double x = (array.Length / 3);
            mediaB = somaB / (x - count);
            mediaG = somaG / (x - count);
            mediaR = somaR / (x - count);
            total = (Math.Sqrt(mediaB * mediaB + mediaG * mediaG + mediaR * mediaR)) / 255;

            blue = mediaB / mediaG;
            green = mediaG / mediaR;
            red = mediaR / mediaB;

            Marshal.Copy(array, 0, data.Scan0, array.Length);
            bmp.UnlockBits(data);

            pictureWeb.Image?.Dispose(); // para liberar consumo de memoria
            pictureWeb.Image = bmp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (vdc.IsRunning)
            {
                vdc.Stop();
            }
        }

        private void salvarBtn_Click(object sender, EventArgs e)
        {
            double[] lista_bgr = { blue * total, green * total, red * total };
            string nomesobrenome = nomePessoa.Text.ToString().Replace(" ", "").ToLower();
            var usuario = new Usuario(nomesobrenome, lista_bgr);
            file = $@"C:\temp\json\{nomesobrenome}.json";
            var json = JsonConvert.SerializeObject(usuario);

            if (!File.Exists(file))
            {
                File.WriteAllText(file, json);
            }
            else
            {
                File.Delete(file);
                File.WriteAllText(file, json);
            }
        }

        private void showPessoas_Click(object sender, EventArgs e)
        {
            double[] rosto = { blue * total, green * total, red * total };

            string pessoa_semelhante = null;
            menor_dist = 9999;
            listaUsuario.Clear();

            string[] jsonFiles = Directory.GetFiles(@"c:\temp\json", "*.json");
            foreach (string file in jsonFiles)
            {
                using (StreamReader r = new StreamReader($"{file}"))
                {
                    string json = r.ReadToEnd();
                    Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
                    listaUsuario.Add(usuario);
                    
                }
            }
            
            foreach (Usuario usuario in listaUsuario)
            {
                dist = Math.Sqrt(Math.Pow((rosto[0] - usuario.BGR[0]), 2) + Math.Pow((rosto[1] - usuario.BGR[1]), 2) + Math.Pow((rosto[2] - usuario.BGR[2]), 2));

                if(dist < menor_dist)
                {
                    menor_dist = dist;
                    pessoa_semelhante = usuario.NomeSobrenome;
                }
            }
            MessageBox.Show($"Semelhante a {pessoa_semelhante}");
        }
    }
}
