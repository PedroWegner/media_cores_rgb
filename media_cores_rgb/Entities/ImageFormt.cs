using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace media_cores_rgb.Entities
{
    internal class ImageFormt
    {
        private Bitmap _bmp { set; get; }
        public byte[] byteArray { get; set; }
        public int Height { get; set; }
        public int Stride { get; set; }

        public double blueAverage { get; set; }
        public double greenAverage { get; set; }
        public double redAverage { get; set; }
        public double normaDark { get; set; }

        public ImageFormt(Bitmap bmp)
        {
            _bmp = bmp;
            byteArrayMontage();
            BGRAverage();
            normaDark = ((Math.Sqrt(blueAverage * blueAverage + greenAverage * greenAverage + redAverage * redAverage)) / (255));
        }

        public void byteArrayMontage()
        {
            var rect = new Rectangle(0, 0, _bmp.Width, _bmp.Height);
            var data = _bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byteArray = new byte[data.Height * data.Stride];
            Height = data.Height;
            Stride = data.Stride;
            Marshal.Copy(data.Scan0, byteArray, 0, byteArray.Length);
            _bmp.UnlockBits(data);
        }

        public void BGRAverage()
        {
            double blueSum = 0.0;
            double greenSum = 0.0;
            double redSum = 0.0;
            int qtdPixelLight = 0;
            for (int i = 0; i < byteArray.Length; i += 3)
            {
                if (!(byteArray[i + 0] < 85 && byteArray[i + 1] < 85 && byteArray[i + 2] < 85))
                {
                    qtdPixelLight++;
                    //somaBLight += array[i + 0];
                    //somaGLight += array[i + 1];
                    //somaRLight += array[i + 2];

                }
                else
                {
                    blueSum += byteArray[i + 0];
                    greenSum += byteArray[i + 1];
                    redSum += byteArray[i + 2];
                }
            }
            double qtdPixelDark = (byteArray.Length / 3) - qtdPixelLight; 
            blueAverage = (double)(blueSum / qtdPixelDark);
            greenAverage = (double)(greenSum / qtdPixelDark);
            redAverage = (double)(redSum / qtdPixelDark);

        }
    }
}
