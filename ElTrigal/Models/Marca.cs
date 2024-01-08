using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElTrigal.Models;

public partial class Marca
{
    public Guid Id { get; set; }

    [DisplayName("Marca")]
    public string? Nombre { get; set; }

    public Guid? ProveedorId { get; set; }
    
    public Proveedor Proveedor { get; set; }
    public List<Producto> Productos { get; set; }
}
