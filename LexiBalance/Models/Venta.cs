using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public int IDProducto { get; set; }
        public int IDCliente { get; set; }
        public int IDVenta { get; set; }
        public DateTime Fecha { get; set; }
    }
}
