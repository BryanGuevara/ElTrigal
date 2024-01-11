using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Detalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid CotizacionId { get; set; }

    public Guid? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public Producto? Producto { get; set; }

    public Cotizacion Cotizacion { get; set; }
}