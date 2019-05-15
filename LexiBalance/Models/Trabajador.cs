using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Usa sólo letras.")]
        public string Nombre { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Usa sólo números y letra.")]
        public string DNI { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        [RegularExpression(@"^[0-9a-zA-Z/ /,]+$", ErrorMessage = "Usa sólo letras, números y coma.")]
        public string Direccion { get; set; }
    }
}
