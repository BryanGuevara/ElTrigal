using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElTrigal.Models;

public partial class Rol
{
    public Guid Id { get; set; }

    [DisplayName("Nivel de Acceso")]

    public string? Nombre { get; set; }
    public List<Usuario> Usuarios { get; set; }
}
