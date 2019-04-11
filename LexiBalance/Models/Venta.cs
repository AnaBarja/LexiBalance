using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public string Producto { get; set; }
        public string Cliente { get; set; }
        public string Trabajador { get; set; }
        public DateTime Fecha { get; set; }
    }
}
