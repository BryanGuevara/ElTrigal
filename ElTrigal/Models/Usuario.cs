using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }


    [DisplayName("Nombre Completo del Usuario")]

    public string? Nombre { get; set; }

    public Guid? RolId { get; set; }

    [DisplayName("Usuario")]

    public string? NameTag { get; set; }

    [DisplayName("Contraseña")]

    public string? Pass { get; set; }

    public Rol Rol { get; set; }
}
