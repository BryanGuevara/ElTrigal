using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Detalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("CotizacionId o VentaId")]
    public Guid PerteneceId { get; set; }

    [DisplayName("Producto")]
    public Guid? ProductoId { get; set; }

    [DisplayName("Cantidad de Producto")]
    public int? Cantidad { get; set; }

    [DisplayName("Descuento por Producto")]
    public int? Descuento { get; set; }

    [DisplayName("Nombre del Producto")]
    public Producto? Producto { get; set; }

    [ForeignKey("PerteneceId")]
    public Cotizacion Cotizacion { get; set; }

    [ForeignKey("PerteneceId")]
    public Venta Venta { get; set; }
}