using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElTrigal.Models;

public partial class Cliente
{
    public Guid Id { get; set; }

    [DisplayName("Nombre del Cliente")]
    public string? Nombre { get; set; }

    [DisplayName("Numero de Telefono")]
    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public List<Cotizacion> Cotizaciones { get; set; }
}
