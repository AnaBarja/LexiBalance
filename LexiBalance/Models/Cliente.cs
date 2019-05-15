using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Nombre { get; set; }
        [Required]
        public int CP { get; set; }
        [Required]
        public int Telefono { get; set; }
    }
}
