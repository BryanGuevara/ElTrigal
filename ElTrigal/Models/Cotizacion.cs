using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Cotizacion
{

    [DisplayName("Codigo de Factura")]
    public string? CodigoFactura { get; set; }

    [DisplayName("Codigo de Pedido")]
    public string? CodigoPedido { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Cliente")]
    public Guid? ClienteId { get; set; }

    [DisplayName("Fecha de Creacion")]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Fecha { get; set; }

    [DisplayName("Fecha de Vencimiento")]
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Vencimiento { get; set; }

    [DisplayName("Entregado")]
    public bool Entregado { get; set; }

    [DisplayName("Con IVA")]
    public bool Iva { get; set; }

    [DisplayName("Comentarios")]
    public string? Comentario { get; set; }

    [DisplayName("Pagado")]
    public bool Pagado { get; set; }

    [DisplayName("Cantidad Abonada")]
    public decimal? Abono { get; set; }

    [NotMapped]
    public int Dias { get; set; }

    public Cliente? Cliente { get; set; }

    public List<Detalle> Detalles { get; set; }
}
