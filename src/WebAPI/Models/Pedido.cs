using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubiaDivinaWebAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name = "Valor a pagar")]
        public decimal? ValorTotal { get; set; }
        public bool Pagado { get; set; } = false;
        [Display(Name = "Fecha de solicitur")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        [Display(Name = "Fecha de pago")]
        public DateTime? FechaPago { get; set; }
        ICollection<Producto>? Productos { get; set; } = new List<Producto>();
    }
}
