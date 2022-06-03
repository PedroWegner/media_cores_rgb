using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace media_cores_rgb.Entities
{
    internal class Usuario
    {
        public string NomeSobrenome { get; set; }
        public double[] BGRList { get; set; }
        public int[] Colors { get; set; }

        public Usuario(string nomesobrenome, double[] bgr, int[] colors)
        {
            NomeSobrenome = nomesobrenome;
            BGRList = bgr;
            Colors = colors;
        }
    }
}
