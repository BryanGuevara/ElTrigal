using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models;

public partial class Proveedor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Proveedor")]
    public string? Nombre { get; set; }

    [DisplayName("Sitio Web")]
    public string? SitioWeb { get; set; }

    [DisplayName("Descripción")]
    public string? Descripcion { get; set; }

    [DisplayName("Descipción Corta")]
    public string? DescripcionCorta { get; set; }

    [DisplayName("Diección")]
    public string? Direccion { get; set; }

    [DisplayName("Linea de Comunicacion Directa")]
    public string? Telefono { get; set; }

    [DisplayName("Telefono - Centro de Servicio")]
    public string? ServicioTel { get; set; }

    [DisplayName("Correo - Centro de Servicio")]
    public string? ServicioCorreo { get; set; }

    [DisplayName("Telefono - Atencion al Cliente")]
    public string? AtencionTel { get; set; }

    [DisplayName("Correo - Atencion al Cliente")]
    public string? AtencionCorreo { get; set; }

    [DisplayName("Telefono - Gerencia Comercial")]
    public string? GerenciaTel { get; set; }

    [DisplayName("Correo - Gerencia Comercial")]
    public string? GerenciaCorreo { get; set; }

    [DisplayName("Telefono - Despacho")]
    public string? DespachoTel { get; set; }

    [DisplayName("Correo - Despacho")]
    public string? DespachoCorreo { get; set; }

    [DisplayName("Telefono - Creditos y Cobros")]
    public string? CobrosTel { get; set; }

    [DisplayName("Correo - Credito y Cobros")]
    public string? CobrosCorreo { get; set; }

    [DisplayName("Condiciones de Pago")]
    public string? CondicionPago { get; set; }

    public List<Marca> Marca { get; set; }
}
