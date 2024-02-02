use master;
GO
Drop Database ElTrigal;
GO
Create DataBAse ElTrigal;
GO
use ElTrigal;
GO

--Personas
CREATE TABLE Roles (
    ID UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Nombre VARCHAR(50),
    VerTodo BIT,

    -- Permisos de Usuarios
    VerUsuario BIT,
    EditUsuario BIT,
    EliUsuario BIT,
    CrearUsuario BIT,
    EditPass BIT,
    
    -- Permisos de Clientes
    VerCliente BIT,
    EditCliente BIT,
    EliCliente BIT,
    CrearCliente BIT,
    
    -- Permisos de Proveedores
    VerProveedor BIT,
    EditProveedor BIT,
    EliProveedor BIT,
    CrearProveedor BIT,

    -- Permisos de Marca
    VerMarca BIT,
    EditMarca BIT,
    EliMarca BIT,
    CrearMarca BIT,

    -- Permisos de Categoria
    VerCategoria BIT,
    EditCategoria BIT,
    EliCategoria BIT,
    CrearCategoria BIT,

    -- Permisos de Productos
    VerProductos BIT,
    EditProductos BIT,
    EliProductos BIT,
    CrearProductos BIT,
    VerCatalogo BIT,
    Pedido BIT,

    -- Permisos de Cotizaciones
    VerCotizaciones BIT,
    EditCotizaciones BIT,
    EliCotizaciones BIT,
    CrearCotizaciones BIT,

    -- Permisos de Ventas
    VerVentas BIT,
    EditVentas BIT,
    EliVentas BIT,
    CrearVentas BIT,

    -- Permisos de Rol
    VerRol BIT,
    EditRol BIT,
    EliRol BIT,
    CrearRol BIT,

    -- Permisos de Analisis
    VerAnalisis BIT
);


Create Table Usuarios(
    ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(50),
	NameTag Varchar(20),
	Telefono varchar(20),
    Correo Varchar(100),
	RolID uniqueidentifier,
	Pass Varchar(20)
);

Create Table Clientes(
    DUI varchar(20),
    ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre varchar(50),
	Telefono varchar(20),
    Correo Varchar(100),
    Municipio varchar(30),
    Departamento varchar(30),
	Direccion Varchar(100)
);

--Producto

Create Table Proveedor(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre varchar(50),
    SitioWeb varchar(50),
    Descripcion Varchar(MAX),
    DescripcionCorta Varchar(255),
    Direccion Varchar(255),
    Telefono Varchar(20),
    ServicioTel Varchar(20),
    ServicioCorreo Varchar(50),
    AtencionTel Varchar(20),
    AtencionCorreo Varchar(50),
    GerenciaTel Varchar(20),
    GerenciaCorreo Varchar(50),
    DespachoTel Varchar(20),
    DespachoCorreo Varchar(50),
    CobrosTel Varchar(20),
    CobrosCorreo Varchar(50),
    CondicionPago Varchar(20)
);

Create Table Marca(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(50),
	ProveedorId uniqueidentifier,
    Descripcion Varchar(255),
    Especialidad Varchar(30)
);

Create Table Categoria(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(30),
    Descripcion Varchar(50)
);

Create Table Productos(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
    Codigo varchar (20) NOT NULL,
	Nombre Varchar(50),
    Descripcion Varchar(100),
	MarcaID uniqueidentifier,
	CategoriaID uniqueidentifier,
    Cantidad int,
    ventas varchar(5),
    Precio Money
);

--Cotización y Ventas

Create Table Cotizaciones(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
    CodigoFactura Varchar(8),
    CodigoPedido Varchar(7),
	ClienteID uniqueidentifier,
	Fecha Date,
    Vencimiento Date,
    Entregado BIT,
    IVA BIT,
    Comentario varchar(255),
    Pagado BIT,
    Abono Money
);


CREATE TABLE Ventas (
    ID uniqueidentifier PRIMARY KEY NOT NULL,
    ClienteID uniqueidentifier,
    FechaVenta DATE
);

Create Table Detalle(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	PerteneceID uniqueidentifier,
	ProductoID uniqueidentifier,
	Cantidad Int,
    Descuento int
);

-- Informes y Análisis
CREATE TABLE InformesAnalisis (
    ID uniqueidentifier PRIMARY KEY NOT NULL,
    TipoInforme VARCHAR(50),
    DetallesInforme VARCHAR(MAX),
    FechaGeneracion DATE
);

GO

-- Insertar datos de Roles
INSERT INTO Roles (ID, Nombre, VerTodo, VerUsuario, EditUsuario, EliUsuario, CrearUsuario, EditPass,
                    VerCliente, EditCliente, EliCliente, CrearCliente,
                    VerProveedor, EditProveedor, EliProveedor, CrearProveedor,
                    VerMarca, EditMarca, EliMarca, CrearMarca,
                    VerCategoria, EditCategoria, EliCategoria, CrearCategoria,
                    VerProductos, EditProductos, EliProductos, CrearProductos, VerCatalogo, Pedido,
                    VerCotizaciones, EditCotizaciones, EliCotizaciones, CrearCotizaciones,
                    VerVentas, EditVentas, EliVentas, CrearVentas,
                    VerRol, EditRol, EliRol, CrearRol, VerAnalisis)
VALUES
('D209709B-33F8-4D16-856F-793B68468717', 'Administrador', 1, 1, 1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1,
                                                        1, 1, 1, 1, 1);

-- Insertar datos de Usuarios
INSERT INTO Usuarios (ID, Nombre, NameTag, Telefono, Correo, RolID, Pass)
VALUES
('4C985811-C722-4456-A622-9CDDF3C90320', 'Primera Cuenta', 'SysAdmin', '(+503)7123-4569', 'sysadmin@example.com', 'D209709B-33F8-4D16-856F-793B68468717', 'Admin2024');

GO