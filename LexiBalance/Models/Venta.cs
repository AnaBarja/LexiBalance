using System;
using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Venta
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string Cliente { get; set; }
        [Required]
        public string Trabajador { get; set; }
        public DateTime Fecha { get; set; }
    }
}
