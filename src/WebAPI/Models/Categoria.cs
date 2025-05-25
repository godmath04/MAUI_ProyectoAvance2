using System.ComponentModel.DataAnnotations;

namespace RubiaDivinaWebAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        [Display(Name = "Categoría")]
        public string Nombre { get; set; }
    }
}
