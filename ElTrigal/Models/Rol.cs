using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElTrigal.Models
{
    public partial class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DisplayName("Nombre")]
        public string? Nombre { get; set; }

        [DisplayName("Permiso de entrar")]
        public bool VerTodo { get; set; }

        // Permisos de Usuarios
        [DisplayName("Ver detalles de los Usuarios")]
        public bool VerUsuario { get; set; }

        [DisplayName("Editar Usuarios")]
        public bool EditUsuario { get; set; }

        [DisplayName("Eliminar Usuarios")]
        public bool EliUsuario { get; set; }

        [DisplayName("Añadir nuevo Usuarios")]
        public bool CrearUsuario { get; set; }

        [DisplayName("Editar Contraseñas")]
        public bool EditPass { get; set; }

        // Permisos de Clientes
        [DisplayName("Ver detalles de los Clientes")]
        public bool VerCliente { get; set; }

        [DisplayName("Editar Clientes")]
        public bool EditCliente { get; set; }

        [DisplayName("Eliminar Clientes")]
        public bool EliCliente { get; set; }

        [DisplayName("Añadir nuevo Cliente")]
        public bool CrearCliente { get; set; }

        // Permisos de Proveedores
        [DisplayName("Ver detalles de los Proveedores")]
        public bool VerProveedor { get; set; }

        [DisplayName("Editar Proveedores")]
        public bool EditProveedor { get; set; }

        [DisplayName("Eliminar Proveedores")]
        public bool EliProveedor { get; set; }

        [DisplayName("Añadir nuevos Proveedores")]
        public bool CrearProveedor { get; set; }

        // Permisos de Marca
        [DisplayName("Ver detalles de las Marcas")]
        public bool VerMarca { get; set; }

        [DisplayName("Editar Marca")]
        public bool EditMarca { get; set; }

        [DisplayName("Eliminar Marca")]
        public bool EliMarca { get; set; }

        [DisplayName("Añadir nueva Marca")]
        public bool CrearMarca { get; set; }

        // Permisos de Categoría
        [DisplayName("Ver detalles de las Categorías")]
        public bool VerCategoria { get; set; }

        [DisplayName("Editar Categoría")]
        public bool EditCategoria { get; set; }

        [DisplayName("Eliminar Categoría")]
        public bool EliCategoria { get; set; }

        [DisplayName("Añadir nueva Categoría")]
        public bool CrearCategoria { get; set; }

        // Permisos de Productos
        [DisplayName("Ver detalles de los Productos")]
        public bool VerProductos { get; set; }

        [DisplayName("Editar Productos")]
        public bool EditProductos { get; set; }

        [DisplayName("Eliminar Productos")]
        public bool EliProductos { get; set; }

        [DisplayName("Añadir nuevos Productos")]
        public bool CrearProductos { get; set; }

        [DisplayName("Ver Catálogo")]
        public bool VerCatalogo { get; set; }

        [DisplayName("Hacer Pedido")]
        public bool Pedido { get; set; }

        // Permisos de Cotizaciones
        [DisplayName("Ver detalles de las Cotizaciones")]
        public bool VerCotizaciones { get; set; }

        [DisplayName("Editar Cotizaciones")]
        public bool EditCotizaciones { get; set; }

        [DisplayName("Eliminar Cotizaciones")]
        public bool EliCotizaciones { get; set; }

        [DisplayName("Añadir Cotizaciones")]
        public bool CrearCotizaciones { get; set; }

        // Permisos de Ventas
        [DisplayName("Ver detalles de las Ventas")]
        public bool VerVentas { get; set; }

        [DisplayName("Editar Ventas")]
        public bool EditVentas { get; set; }

        [DisplayName("Eliminar Ventas")]
        public bool EliVentas { get; set; }

        [DisplayName("Añadir nuevas Ventas")]
        public bool CrearVentas { get; set; }

        // Permisos de Rol
        [DisplayName("Ver detalles de los Roles")]
        public bool VerRol { get; set; }

        [DisplayName("Editar Roles")]
        public bool EditRol { get; set; }

        [DisplayName("Eliminar Roles")]
        public bool EliRol { get; set; }

        [DisplayName("Añadir nuevos Roles")]
        public bool CrearRol { get; set; }

        // Permisos de Análisis
        [DisplayName("Ver detalles de los Análisis")]
        public bool VerAnalisis { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
