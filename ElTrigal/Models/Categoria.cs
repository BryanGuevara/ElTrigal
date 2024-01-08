using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElTrigal.Models;

public partial class Categoria
{
    public Guid Id { get; set; }

    [DisplayName("Categoria")]
    public string? Nombre { get; set; }
    public List<Producto> Productos { get; set; }
}
