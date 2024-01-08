using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElTrigal.Models;

public partial class Cotizacion
{
    public Guid Id { get; set; }

    public Guid? ClienteId { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? Fecha { get; set; }

    public Cliente? Cliente { get; set; }

    public List<Detalle> Detalles { get; set; }
}
