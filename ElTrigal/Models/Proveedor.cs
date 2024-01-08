using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElTrigal.Models;

public partial class Proveedor
{
    public Guid Id { get; set; }

    [DisplayName("Proveedor")]
    public string? Nombre { get; set; }

    public List<Marca> Marca { get; set; }
}
