using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElTrigal.Models
{
    public class Venta
    {
        public Guid Id { get; set; }

        [DisplayName("Cliente")]
        public Guid? ClienteId { get; set; }

        [DisplayName("Fecha que se realizo la venta")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaVenta { get; set; }

        public Cliente Cliente { get; set; }
        public List<Detalle> Detalles { get; set; }
    }
}
