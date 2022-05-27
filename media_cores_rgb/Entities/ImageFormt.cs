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

        public int blueAverage { get; set; }
        public int greenAverage { get; set; }
        public int redAverage { get; set; }

        public List<int[]> pixelsArray { get; set; } = new List<int[]>();


        public ImageFormt(Bitmap bmp)
        {
            _bmp = bmp;
            byteArrayMontage();
            pixelArrayMontage();
        }

        public void byteArrayMontage()
        {
            var rect = new Rectangle(0, 0, _bmp.Width, _bmp.Height);
            var data = _bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byteArray = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, byteArray, 0, byteArray.Length);
            _bmp.UnlockBits(data);
        }

        public void BGRAverage()
        {
            double blueSum = 0.0;
            double greenSum = 0.0;
            double redSum = 0.0;

            for (int i = 0; i < byteArray.Length; i += 3)
            {
                blueSum += byteArray[i + 0];
                greenSum += byteArray[i + 1];
                redSum += byteArray[i + 2];
            }
            double qtdPixel = (byteArray.Length / 3);
            blueAverage = (int)(blueSum / qtdPixel);
            greenAverage = (int)(greenSum / qtdPixel);
            redAverage = (int)(redSum / qtdPixel);

        }

        public void pixelArrayMontage()
        {
            for (int i = 0; i < byteArray.Length; i += 3)
            {
                // Red, Green, Blue
                int[] pixel = { byteArray[i + 2], byteArray[i + 1], byteArray[i + 0] };
                pixelsArray.Add(pixel);
            }
        }
    }
}
