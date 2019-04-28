using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
    }
}
