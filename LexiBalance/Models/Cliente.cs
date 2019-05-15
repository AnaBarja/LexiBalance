using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Usa sólo letras.")]
        public string Nombre { get; set; }
        [Required]
        public int CP { get; set; }
        [Required]
        public int Telefono { get; set; }
    }
}
