using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace media_cores_rgb.Entities
{
    internal class Usuario
    {
        public string NomeSobrenome { get; set; }
        public double[] BGR { get; set; }

        public Usuario(string nomesobrenome, double[] bgr)
        {
            NomeSobrenome = nomesobrenome;
            BGR = bgr;
        }
    }
}
