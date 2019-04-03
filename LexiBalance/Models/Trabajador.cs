using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
