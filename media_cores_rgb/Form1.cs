using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using media_cores_rgb.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace media_cores_rgb
{
    public partial class Form1 : Form
    {
        #region Variables
        FilterInfoCollection fic;
        VideoCaptureDevice vdc;
        Bitmap bmp = null;
        double somaRDark = 0;
        double somaGDark = 0;
        double somaBDark = 0;

        double qtdPixelLight = 0;

        double normaDark = 0.0;
        double menorDist = 99999.0;
        double dist = 0.0;


        double mediaBDark = 0.0;
        double mediaGDark = 0.0;
        double mediaRDark = 0.0;

        double somaRLight = 0;
        double somaGLight = 0;
        double somaBLight = 0;
        double mediaBLight = 0.0;
        double mediaGLight = 0.0;
        double mediaRLight = 0.0;
        double normaLight = 0.0;

        double bluePerGreen = 0.0;
        double greenPerRed = 0.0;
        double redPerBlue = 0.0;
        double bluePerRed = 0.0;
        double greenPerBlue = 0.0;
        double redPerGreen = 0.0;

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
            //tm.Start();
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
            //var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //var array = new byte[data.Height * data.Stride];
            //Marshal.Copy(data.Scan0, array, 0, array.Length);


            //somaBDark = 0;
            //somaGDark = 0;
            //somaRDark = 0;
            //somaBLight = 0;
            //somaGLight = 0;
            //somaRLight = 0;
            //qtdPixelLight = 0;
            //for (int i = 0; i < array.Length; i += 3)
            //{
            //    if (!(array[i + 0] < 85 && array[i + 1] < 85 && array[i + 2] < 85))
            //    {
            //        qtdPixelLight++;
            //        somaBLight += array[i + 0];
            //        somaGLight += array[i + 1];
            //        somaRLight += array[i + 2];

            //    }
            //    else
            //    {
            //        somaBDark += array[i + 0];
            //        somaGDark += array[i + 1];
            //        somaRDark += array[i + 2];
            //    }


            //}
            //double qtdPixel = (array.Length / 3);
            //double qtdPixelEscuro = qtdPixel - qtdPixelLight;
            //// media de BGR em pixel escuros
            //mediaBDark = somaBDark / (qtdPixel - qtdPixelLight);
            //mediaGDark = somaGDark / (qtdPixel - qtdPixelLight);
            //mediaRDark = somaRDark / (qtdPixel - qtdPixelLight);

            //// meida de BGR em pixels claros
            //mediaBLight = somaBLight / qtdPixelLight;
            //mediaGLight = somaGLight / qtdPixelLight;
            //mediaRLight = somaRLight / qtdPixelLight;
            //// norma das medias
            //normaLight = ((Math.Sqrt(mediaBLight * mediaBLight + mediaGLight * mediaGLight + mediaRLight * mediaRLight)) / (255));
            //normaDark = ((Math.Sqrt(mediaBDark * mediaBDark + mediaGDark * mediaGDark + mediaRDark * mediaRDark)) / (255));

            //// uma cor pela outra pixels escuros
            //bluePerGreen = mediaBDark / mediaGDark;
            //greenPerRed = mediaGDark / mediaRDark;
            //redPerBlue = mediaRDark / mediaBDark;

            //bluePerRed = mediaBDark / mediaRDark;
            //greenPerBlue = mediaGDark / mediaBDark;
            //redPerGreen = mediaRDark / mediaGDark;

            //Marshal.Copy(array, 0, data.Scan0, array.Length);
            //bmp.UnlockBits(data);

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
            double[] lista_bgr = { bluePerGreen, greenPerRed, redPerBlue, bluePerRed, greenPerBlue, redPerGreen, normaDark };
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

        private void showPessoas_Click()
        {
            double[] rosto = { bluePerGreen, greenPerRed, redPerBlue, bluePerRed, greenPerBlue, redPerGreen, normaDark };

            string pessoa_semelhante = null;
            menorDist = 9999;
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
                dist = Math.Sqrt(Math.Pow((rosto[0] - usuario.BGR[0]), 2) + Math.Pow((rosto[1] - usuario.BGR[1]), 2) + Math.Pow((rosto[2] - usuario.BGR[2]), 2) + Math.Pow((rosto[3] - usuario.BGR[3]), 2) + Math.Pow((rosto[4] - usuario.BGR[4]), 2) + Math.Pow((rosto[5] - usuario.BGR[5]), 2) + Math.Pow((rosto[6] - usuario.BGR[6]), 2));

                if (dist < menorDist)
                {
                    menorDist = dist;
                    pessoa_semelhante = usuario.NomeSobrenome;
                }
            }
            label8.Text = ($"Semelhante a {pessoa_semelhante}");
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            showPessoas_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vdc.Stop();
            ImageFormt im = new ImageFormt(bmp);
            Kmeans kmeans = new Kmeans();
            List<int[]> colors = kmeans.Clusterize(im.pixelsArray, 8);
            panel1.BackColor = Color.FromArgb(colors[0][0], colors[0][1], colors[0][2]);
            panel2.BackColor = Color.FromArgb(colors[1][0], colors[1][1], colors[1][2]);
            panel3.BackColor = Color.FromArgb(colors[2][0], colors[2][1], colors[2][2]);
            panel4.BackColor = Color.FromArgb(colors[3][0], colors[3][1], colors[3][2]);
            panel5.BackColor = Color.FromArgb(colors[4][0], colors[4][1], colors[4][2]);
            panel6.BackColor = Color.FromArgb(colors[5][0], colors[5][1], colors[5][2]);
            panel7.BackColor = Color.FromArgb(colors[6][0], colors[6][1], colors[6][2]);
            panel8.BackColor = Color.FromArgb(colors[7][0], colors[7][1], colors[7][2]);
            Console.WriteLine();
            vdc.Start();
        }
    }
}
