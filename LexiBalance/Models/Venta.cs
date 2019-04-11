using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public List<Producto> Producto { get; set; }
        public List<Cliente> Cliente { get; set; }
        public List<Trabajador> Trabajador { get; set; }
        public DateTime Fecha { get; set; }
    }
}
