using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubiaDivinaWebAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [StringLength(50), Required]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(6,2)"), Required]
        public decimal Precio { get; set; }
        [Range(0, 1000), Required]
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
