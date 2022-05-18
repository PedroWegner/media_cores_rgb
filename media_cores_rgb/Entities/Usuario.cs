using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace media_cores_rgb.Entities
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double[] RGB { get; set; }
        public long Qtd_bytes { get; set; }

        public Usuario(int id, string name, double[] rGB, long qtd_bytes)
        {
            Id = id;
            Name = name;
            RGB = rGB;
            Qtd_bytes = qtd_bytes;
        }
    }
}
