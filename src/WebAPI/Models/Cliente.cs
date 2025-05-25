using System.ComponentModel.DataAnnotations;

namespace RubiaDivinaWebAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [StringLength(50), Required]
        public string Nombre { get; set; }
        [StringLength(50), Required]
        public string Apellido { get; set; }
        [Required, Phone, StringLength(14)]
        public string Telefono { get; set; }
        [Display(Name = "Correo electrónico")]
        [EmailAddress, StringLength(100)]
        public string Correo { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
