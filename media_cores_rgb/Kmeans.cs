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
        public Color[] Clusterize(byte[] array, int K)
        {
            Color[] colors = new Color[K];
            Random random = new Random();
            for (int i = 0; i < K; i++)
            {
                colors[i] = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            }

            for (int n = 0; n < 2000; n++)
            {

                List<long> count = new List<long>();
                List<long> R = new List<long>();
                List<long> G = new List<long>();
                List<long> B = new List<long>();

                for (int i = 0; i < K; i++)
                {
                    count.Add(0);
                    R.Add(0);
                    G.Add(0);
                    B.Add(0);
                }

                for (int i = 0; i < array.Length; i += 3)
                {
                    int dist = (array[i] - colors[0].B) * (array[i] - colors[0].B) +
                         (array[i + 1] - colors[0].G) * (array[i + 1] - colors[0].G) +
                          (array[i + 2] - colors[0].R) * (array[i + 2] - colors[0].R);
                    int cluster = 0;
                    for (int k = 1; k < K; k++)
                    {
                        int newdist = (array[i] - colors[k].B) * (array[i] - colors[k].B) +
                         (array[i + 1] - colors[k].G) * (array[i + 1] - colors[k].G) +
                          (array[i + 2] - colors[k].R) * (array[i + 2] - colors[k].R);
                        if (newdist < dist)
                        {
                            dist = newdist;
                            cluster = k;
                        }
                    }
                    count[cluster]++;
                    R[cluster] += array[i + 2];
                    G[cluster] += array[i + 1];
                    B[cluster] += array[i];
                }
                for (int k = 0; k < K; k++)
                {
                    if (count[k] == 0)
                        continue;
                    colors[k] = Color.FromArgb((int)(R[k] / count[k]),
                        (int)(G[k] / count[k]),
                        (int)(B[k] / count[k]));
                }
            }


            return colors;
        }
    }
}
