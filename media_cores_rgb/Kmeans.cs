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
        public List<int[]> Clusterize(List<int[]> list, int k)
        {
            #region Variables
            List<int[]> colors = new List<int[]>();
            Random random = new Random();
            //List<List<int[]>> listClusters = new List<List<int[]>>();
            double minorDist = 999999.0;
            int cluster = 0;

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
                // Red, Green, Blue
                colors.Add(new int[] { random.Next(255), random.Next(255), random.Next(255) });
                countCluster.Add(0);
                redSum.Add(0);
                greenSum.Add(0);
                blueSum.Add(0);
            }

            // aqui sao as iteracoes
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < list.Count; j++)
                //preciso calcular distancia entre o pixel e o nucleo
                {
                    minorDist = 999999.0;
                    for (int c = 0; c < k; c++)
                    {
                        double dr = list[j][0] - colors[c][0];
                        double dg = list[j][1] - colors[c][1];
                        double db = list[j][2] - colors[c][2];
                        double dist = (dr * dr) + (dg * dg) + (db * db);
                        if (dist < minorDist)
                        {
                            minorDist = dist;
                            cluster = c;
                        }
                    }
                    countCluster[cluster]++;
                    redSum[cluster] += list[j][0];
                    greenSum[cluster] += list[j][1];
                    blueSum[cluster] += list[j][2];

                }

                for (int n = 0; n < countCluster.Count; n++)
                {
                    if (countCluster[n] > 0)
                    {
                        red = redSum[n] / countCluster[n];
                        green = greenSum[n] / countCluster[n];
                        blue = blueSum[n] / countCluster[n];
                        colors[n] = new int[] { (int)(red), (int)(green), (int)(blue) };
                    }
                    else
                    {
                        colors[n] = new int[] { 255, 255, 255 };
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
