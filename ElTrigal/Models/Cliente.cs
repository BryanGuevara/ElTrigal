using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Dui { get; set; }

    [DisplayName("Nombre del Cliente")]
    public string? Nombre { get; set; }

    [DisplayName("Numero de Telefono")]
    public string? Telefono { get; set; }

    [DisplayName("Correo Electronico")]
    public string? Correo { get; set; }

    [DisplayName("Municipio")]
    public string? Municipio { get; set; }

    [DisplayName("Departamento")]
    public string? Departamento { get; set; }

    [DisplayName("Dirección")]
    public string? Direccion { get; set; }

    public List<Cotizacion> Cotizaciones { get; set; }
    public List<Venta> Venta { get; set; }
}
