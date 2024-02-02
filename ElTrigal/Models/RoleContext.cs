using ElTrigal.Models;
using Microsoft.AspNetCore.Http;

public static class RoleContext
{
    private static Rol DefaultRole;
    static RoleContext()
    {
        DefaultRole = new Rol
        {
            Nombre = "Baneado",
            VerTodo = false,
            VerUsuario = false,
            EditUsuario = false,
            EliUsuario = false,
            CrearUsuario = false,
            EditPass = false,
            VerCliente = false,
            EditCliente = false,
            EliCliente = false,
            CrearCliente = false,
            VerProveedor = false,
            EditProveedor = false,
            EliProveedor = false,
            CrearProveedor = false,
            VerMarca = false,
            EditMarca = false,
            EliMarca = false,
            CrearMarca = false,
            VerCategoria = false,
            EditCategoria = false,
            EliCategoria = false,
            CrearCategoria = false,
            VerProductos = false,
            EditProductos = false,
            EliProductos = false,
            CrearProductos = false,
            VerCotizaciones = false,
            EditCotizaciones = false,
            EliCotizaciones = false,
            CrearCotizaciones = false,
            VerVentas = false,
            EditVentas = false,
            EliVentas = false,
            CrearVentas = false,
            VerRol = false,
            EditRol = false,
            EliRol = false,
            CrearRol = false,
            VerAnalisis = false
        };
    }
    private static IHttpContextAccessor HttpContextAccessor { get; set; }

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public static Rol CurrentRole
    {
        get
        {
            if (HttpContextAccessor?.HttpContext?.Items["CurrentRole"] is Rol currentRole)
            {
                return currentRole;
            }
            return DefaultRole;
        }
        set
        {
            HttpContextAccessor.HttpContext.Items["CurrentRole"] = value;
            UpdateDefaultRole(value);
        }
    }

    private static void UpdateDefaultRole(Rol role)
    {
        if (role != null)
        {
            DefaultRole = new Rol
            {
                Nombre = role.Nombre,
                VerTodo = role.VerTodo,
                VerUsuario = role.VerUsuario,
                EditUsuario = role.EditUsuario,
                EliUsuario = role.EliUsuario,
                CrearUsuario = role.CrearUsuario,
                EditPass = role.EditPass,
                VerCliente = role.VerCliente,
                EditCliente = role.EditCliente,
                EliCliente = role.EliCliente,
                CrearCliente = role.CrearCliente,
                VerProveedor = role.VerProveedor,
                EditProveedor = role.EditProveedor,
                EliProveedor = role.EliProveedor,
                CrearProveedor = role.CrearProveedor,
                VerMarca = role.VerMarca,
                EditMarca = role.EditMarca,
                EliMarca = role.EliMarca,
                CrearMarca = role.CrearMarca,
                VerCategoria = role.VerCategoria,
                EditCategoria = role.EditCategoria,
                EliCategoria = role.EliCategoria,
                CrearCategoria = role.CrearCategoria,
                VerProductos = role.VerProductos,
                EditProductos = role.EditProductos,
                EliProductos = role.EliProductos,
                CrearProductos = role.CrearProductos,
                VerCotizaciones = role.VerCotizaciones,
                EditCotizaciones = role.EditCotizaciones,
                EliCotizaciones = role.EliCotizaciones,
                CrearCotizaciones = role.CrearCotizaciones,
                VerVentas = role.VerVentas,
                EditVentas = role.EditVentas,
                EliVentas = role.EliVentas,
                CrearVentas = role.CrearVentas,
                VerRol = role.VerRol,
                EditRol = role.EditRol,
                EliRol = role.EliRol,
                CrearRol = role.CrearRol,
                VerAnalisis = role.VerAnalisis
            };
        }
        else
        {
            DefaultRole = new Rol
            {
                Nombre = "Baneado",
                VerTodo = false,
                VerUsuario = false,
                EditUsuario = false,
                EliUsuario = false,
                CrearUsuario = false,
                EditPass = false,
                VerCliente = false,
                EditCliente = false,
                EliCliente = false,
                CrearCliente = false,
                VerProveedor = false,
                EditProveedor = false,
                EliProveedor = false,
                CrearProveedor = false,
                VerMarca = false,
                EditMarca = false,
                EliMarca = false,
                CrearMarca = false,
                VerCategoria = false,
                EditCategoria = false,
                EliCategoria = false,
                CrearCategoria = false,
                VerProductos = false,
                EditProductos = false,
                EliProductos = false,
                CrearProductos = false,
                VerCotizaciones = false,
                EditCotizaciones = false,
                EliCotizaciones = false,
                CrearCotizaciones = false,
                VerVentas = false,
                EditVentas = false,
                EliVentas = false,
                CrearVentas = false,
                VerRol = false,
                EditRol = false,
                EliRol = false,
                CrearRol = false,
                VerAnalisis = false
            };
        }

    }
    public static Rol DefaultR { get; internal set; }

    public static void UpdateDefaultR(Rol role)
    {
        DefaultRole = role;
    }
}
