using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Nombre")]
    public string? Nombre { get; set; }

    [DisplayName("Descripción")]
    public string? Descripcion {get; set;}
    public List<Producto> Productos { get; set; }
}
