using System;
using System.Collections.Generic;

namespace ElTrigal.Models;

public partial class Detalle
{
    public Guid Id { get; set; }

    public Guid CotizacionId { get; set; }

    public Guid? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public Producto? Producto { get; set; }

    public Cotizacion Cotizacion { get; set; }
}