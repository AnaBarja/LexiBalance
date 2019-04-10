using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Producto
    {
   
        public enum Colores
        {
            Negro,
            Blanco,
            Azul,
            Rojo
        }

        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public Colores Color { get; set; }
        public string Caracteristicas { get; set; }
    }
}
