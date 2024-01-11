using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Rol
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Nivel de Acceso")]

    public string? Nombre { get; set; }
    public List<Usuario> Usuarios { get; set; }
}
