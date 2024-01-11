using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Nombre del Cliente")]
    public string? Nombre { get; set; }

    [DisplayName("Numero de Telefono")]
    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public List<Cotizacion> Cotizaciones { get; set; }
}
