using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Cotizacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid? ClienteId { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? Fecha { get; set; }

    public Cliente? Cliente { get; set; }

    public List<Detalle> Detalles { get; set; }
}
