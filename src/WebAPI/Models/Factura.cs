using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubiaDivinaWebAPI.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido? Pedido { get; set; }
        [Display(Name = "Fecha de facturación")]
        public DateTime? FechaFactura { get; set; }
        [Display(Name = "Enviar por email")]
        public bool EnviarEmail { get; set; } = false;
    }
}
