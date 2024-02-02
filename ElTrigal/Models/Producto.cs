using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public Guid Id { get; set; }

    [DisplayName("Codigo del Producto")]
    public string Codigo { get; set; } = null!;

    [DisplayName("Nombre")]
    public string? Nombre { get; set; }

    [DisplayName("Descripcion del Producto")]
    public string? Descripcion { get; set; }

    [DisplayName("Marca")]
    public Guid? MarcaId { get; set; }

    [DisplayName("Categoria")]
    public Guid? CategoriaId { get; set; }

    [DisplayName("Cantidad en Inventario")]
    public int? Cantidad { get; set; }

    [DisplayName("Unidad")]
    public string? Ventas { get; set; }

    [DisplayName("Precio unitario")]
    public decimal? Precio { get; set; }

    [NotMapped]
    public int Pedido { get; set; }

    public Marca Marca { get; set; }
    public Categoria Categoria { get; set; }
    public List<Detalle> Detalles { get; set; }
}
