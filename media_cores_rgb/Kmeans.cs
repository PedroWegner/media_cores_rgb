using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace media_cores_rgb
{
    public class Kmeans
    {
        public List<Color> Clusterize(byte[] pixels, int k, int height, int stride)
        {
            #region Variables
            List<Color> colors = new List<Color>();
            Random random = new Random();

            double blue = 0;
            double green = 0;
            double red = 0;

            List<long> countCluster = new List<long>();
            List<long> blueSum = new List<long>();
            List<long> greenSum = new List<long>();
            List<long> redSum = new List<long>();

            #endregion
            for (int i = 0; i < k; i++)
            {
                colors.Add(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                countCluster.Add(0);
                redSum.Add(0);
                greenSum.Add(0);
                blueSum.Add(0);
            }
            int threads = 32;

            // aqui sao as iteracoes
            for (int it = 0; it < 60; it++)
            {
                Parallel.For(0, threads, j =>
                {

                    #region ThreadLocalVariables
                    Random r = new Random();
                    long[] count = new long[k];
                    long[] redsum = new long[k];
                    long[] greensum = new long[k];
                    long[] bluesum = new long[k];
                    double minorDist = double.MaxValue;
                    Color[] array = new Color[k];
                    colors.CopyTo(array);
                    int cluster = 0;
                    #endregion
                    for (int i = j * stride * height / threads; i < (j + 1) * stride * height / threads;
                        i += 3 * r.Next(1, 11))
                    {
                        minorDist = double.MaxValue;
                        for (int c = 0; c < k; c++)
                        {
                            double db = pixels[i + 0] - array[c].B;
                            double dg = pixels[i + 1] - array[c].G;
                            double dr = pixels[i + 2] - array[c].R;
                            double dist = (dr * dr) + (dg * dg) + (db * db);
                            if (dist < minorDist)
                            {
                                minorDist = dist;
                                cluster = c;
                            }
                        }

                        count[cluster]++;
                        bluesum[cluster] += pixels[i + 0];
                        greensum[cluster] += pixels[i + 1];
                        redsum[cluster] += pixels[i + 2];
                    }
                    for (int i = 0; i < k; i++)
                    {
                        countCluster[i] += count[i];
                        redSum[i] += redsum[i];
                        greenSum[i] += greensum[i];
                        blueSum[i] += bluesum[i];
                    }
                });
                // esse for recalcula o cetroide do cluster
                for (int n = 0; n < countCluster.Count; n++)
                {
                    if (countCluster[n] > 0)
                    {
                        red = redSum[n] / countCluster[n]; // (179+169+189+36) / 4
                        green = greenSum[n] / countCluster[n];
                        blue = blueSum[n] / countCluster[n];
                        colors[n] = Color.FromArgb((byte)red, (byte)green, (byte)blue);
                    }
                    else
                    {
                        colors[n] = Color.White;
                    }
                    countCluster[n] = 0;
                    redSum[n] = 0;
                    greenSum[n] = 0;
                    blueSum[n] = 0;
                }
            }

            return colors;
        }
    }
}
