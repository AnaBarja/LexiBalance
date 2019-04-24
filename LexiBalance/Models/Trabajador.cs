using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required, MaxLength(9)]
        public string DNI { get; set; }
        [Required, MaxLength(9)]
        public int Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
    }
}
