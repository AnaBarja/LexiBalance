using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required, MaxLength(5)]
        public int CP { get; set; }
        [Required, MaxLength(9)]
        public int Telefono { get; set; }
    }
}
