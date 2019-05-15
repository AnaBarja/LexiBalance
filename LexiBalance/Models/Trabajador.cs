using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Nombre { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-zA-Z/-]+$")]
        public string DNI { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-zA-Z/ /,]+$")]
        public string Direccion { get; set; }
    }
}
