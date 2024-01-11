using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? Nombre { get; set; }

    public Guid? MarcaId { get; set; }

    public Guid? CategoriaId { get; set; }

    public decimal? Precio { get; set; }

    public Marca Marca { get; set; }
    public Categoria Categoria { get; set; }
    public List<Detalle> Detalles { get; set; }
}
