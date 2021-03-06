﻿using System.ComponentModel.DataAnnotations;

namespace LexiBalance.Models
{
    public class Producto
    {

        public enum Colores
        {
            Negro,
            Blanco,
            Azul,
            Rojo,
            Amarillo,
            Verde,
            Lila,
            Naranja
        }

        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Usa sólo letras.")]
        public string Nombre { get; set; }
        [Required]
        public string Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public Colores Color { get; set; }
        [Required]
        public string Caracteristicas { get; set; }
    }
}
