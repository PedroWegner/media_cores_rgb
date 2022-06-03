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
using kinectlibrary;

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
        Color[] userColor = null;
        int[] teste = null;
        double[] lista_bgr = null; //{ bluePerGreen, greenPerRed, redPerBlue, bluePerRed, greenPerBlue, redPerGreen, normaDark }
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
            CalculateImage();
            string nomesobrenome = nomePessoa.Text.ToString().Replace(" ", "").ToLower();
            var usuario = new Usuario(nomesobrenome, lista_bgr, teste);
            usuario.NomeSobrenome = nomesobrenome;
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
            CalculateImage();
            Usuario lox = new Usuario("", lista_bgr, teste); // pessoa na frente do pc
            string pessoa_semelhante = null;
            menorDist = double.MaxValue;
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
            int minorError = int.MaxValue;
            foreach (Usuario usuario in listaUsuario)
            {
                //double dist = Math.Sqrt((usuario.BGRList[0] - lox.BGRList[0]) * (usuario.BGRList[0] - lox.BGRList[0]) +
                //    (usuario.BGRList[1] - lox.BGRList[1]) * (usuario.BGRList[1] - lox.BGRList[1]) +
                //     (usuario.BGRList[2] - lox.BGRList[2]) * (usuario.BGRList[2] - lox.BGRList[2]));
                //if (dist < menorDist)
                //{
                //    menorDist = dist;
                //    pessoa_semelhante = usuario.NomeSobrenome;
                //}
                int error = CalculaErro(lox.Colors,usuario.Colors);
                if(error < minorError)
                {
                    minorError = error;
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
            showPessoas_Click();
            
        }

        public void CalculateImage()
        {
            vdc.Stop();
            ImageFormt img = new ImageFormt(bmp);
            Kmeans kmeans = new Kmeans();
            List<Color> colors = kmeans.Clusterize(img.byteArray, 64, img.Height, img.Stride).OrderBy(c => c.R + c.G + c.B).ToList();
            int qtdColors = 16;
            int index = 0;
            userColor = new Color[qtdColors];
            teste = new int[qtdColors * 3];


            flp.Controls.Clear();
            foreach (var color in colors.OrderByDescending(c =>
            {
                int drg = c.R - c.G;
                int drb = c.R - c.B;
                int dgb = c.G - c.B;
                int result = drg * drg + drb * drb + dgb * dgb;
                return result;
            }).Take(qtdColors))
            {
                Panel p = new Panel();
                p.BackColor = color;
                p.Size = new Size(30, 30);
                flp.Controls.Add(p);
                teste[index] = color.R;
                teste[index + 1] = color.G;
                teste[index + 2] = color.B;
                index += 3;
            }

            #region imgAvarage
            bluePerGreen = img.blueAverage / img.greenAverage;
            greenPerRed = img.greenAverage / img.redAverage;
            redPerBlue = img.redAverage / img.blueAverage;
            normaDark = img.normaDark;
            lista_bgr = new double[4] { bluePerGreen, greenPerRed, redPerBlue, normaDark };
            #endregion

            Console.WriteLine();
            vdc.Start();
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void nomePessoa_TextChanged(object sender, EventArgs e)
        {

        }

        public int CalculaErro(int[] userEntrado, int[] usuarioLista) // cada lista tem 48 int, separado em 3 e 3
        {
            int errorTotal = 0;
            
            for(int i = 0; i < (userEntrado.Length); i+=3)
            {
                int minorError = int.MaxValue;

                for(int j = 0; j < (usuarioLista.Length); j +=3)
                {
                    int error = ((userEntrado[i + 0] - usuarioLista[j + 0]) * (userEntrado[i + 0] - usuarioLista[j + 0]) +
                        (userEntrado[i + 1] - usuarioLista[j + 1]) * (userEntrado[i + 1] - usuarioLista[j + 1]) +
                        (userEntrado[i + 2] - usuarioLista[j + 2]) * (userEntrado[i + 2] - usuarioLista[j + 2]));

                    if(error < minorError)
                    {
                        minorError = error;
                    }
                }
                errorTotal += minorError;
            }


            return errorTotal;
        }
    }
}
