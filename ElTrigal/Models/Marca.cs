using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Marca
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Nombre")]
    public string? Nombre { get; set; }

    [DisplayName("Proveedor")]
    public Guid? ProveedorId { get; set; }

    [DisplayName("Descripción")]
    public string? Descripcion { get; set; }

    [DisplayName("Especialidad de la Marca")]
    public string? Especialidad { get; set; }

    public Proveedor Proveedor { get; set; }
    public List<Producto> Productos { get; set; }
}
