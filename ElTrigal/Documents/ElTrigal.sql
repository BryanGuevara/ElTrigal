use master;
GO
Drop Database ElTrigal;
GO
Create DataBAse ElTrigal;
GO
use ElTrigal;
GO

--Personas

Create Table Roles(
    ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(50)
);

Create Table Usuarios(
    ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(50),
	NameTag Varchar(20),
	RolID uniqueidentifier,
	Pass Varchar(20)
);

Create Table Clientes(
    ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre varchar(50),
	Telefono varchar(15),
	Direccion Varchar(100)
);

--Producto

Create Table Proveedor(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre varchar(20)
);

Create Table Marca(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(20),
	ProveedorId uniqueidentifier
);

Create Table Categoria(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(30)
);

Create Table Productos(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Nombre Varchar(20),
	MarcaID uniqueidentifier,
	CategoriaID uniqueidentifier,
    Precio Money
);

--Cotizaci�n

Create Table Cotizaciones(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	ClienteID uniqueidentifier,
	Fecha Date
);

Create Table Detalle(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	CotizacionID uniqueidentifier,
	ProductoID uniqueidentifier,
	Cantidad Int
);

Insert INTO Roles(ID, Nombre)
Values
	(NEWID(), 'Usuario'),
	(NEWID(), 'Administrador');

-- Insertar usuarios
INSERT INTO Usuarios (ID, Nombre, NameTag, RolID, Pass)
VALUES
    (NEWID(), 'Primera Cuenta', 'SysAdmin', (SELECT ID FROM Roles WHERE Nombre = 'Administrador'), 'Admin2024');

-- Insertar clientes
INSERT INTO Clientes (ID, Nombre, Telefono, Direccion)
VALUES
    (NEWID(), 'Jaime Guevara', '123456789', 'Calle 123, Ciudad A'),
    (NEWID(), 'Don Douglas', '987654321', 'Avenida XYZ, Ciudad B');

-- Insertar proveedores
INSERT INTO Proveedor (ID, Nombre)
VALUES
    (NEWID(), 'Truper'),
    (NEWID(), 'Total'),
    (NEWID(), 'Trupper');

-- Insertar marcas
INSERT INTO Marca (ID, Nombre, ProveedorId)
VALUES
    (NEWID(), 'Truper', (SELECT ID FROM Proveedor WHERE Nombre = 'Truper')),
    (NEWID(), 'Total', (SELECT ID FROM Proveedor WHERE Nombre = 'Total')),
    (NEWID(), 'Viduc', (SELECT ID FROM Proveedor WHERE Nombre = 'Truper')),
    (NEWID(), 'Trupper', (SELECT ID FROM Proveedor WHERE Nombre = 'Trupper')),
    (NEWID(), 'Foset', (SELECT ID FROM Proveedor WHERE Nombre = 'Truper'));

INSERT INTO Categoria (ID, Nombre)
VALUES
    (NEWID(), 'Herramientas'),
    (NEWID(), 'Materiales de construcci�n');

-- Insertar productos
INSERT INTO Productos (ID, Nombre, MarcaID, CategoriaID, Precio)
VALUES
    (NEWID(), 'Martillo', (SELECT ID FROM Marca WHERE Nombre = 'Truper'), (SELECT ID FROM Categoria WHERE Nombre = 'Herramientas'), 19.99),
    (NEWID(), 'Cemento', (SELECT ID FROM Marca WHERE Nombre = 'Total'), (SELECT ID FROM Categoria WHERE Nombre = 'Materiales de construcci�n'), 29.99);

-- Insertar cotizaciones
INSERT INTO Cotizaciones (ID, ClienteID, Fecha)
VALUES
    (NEWID(), (SELECT ID FROM Clientes WHERE Nombre = 'Jaime Guevara'), GETDATE()),
    (NEWID(), (SELECT ID FROM Clientes WHERE Nombre = 'Don Douglas'), GETDATE());

-- Insertar detalles de cotizaci�n
INSERT INTO Detalle (ID, CotizacionID, ProductoID, Cantidad)
VALUES
    (NEWID(), (SELECT ID FROM Cotizaciones WHERE ClienteID = (SELECT ID FROM Clientes WHERE Nombre = 'Jaime Guevara')), (SELECT ID FROM Productos WHERE Nombre = 'Martillo'), 2),
    (NEWID(), (SELECT ID FROM Cotizaciones WHERE ClienteID = (SELECT ID FROM Clientes WHERE Nombre = 'Don Douglas')), (SELECT ID FROM Productos WHERE Nombre = 'Cemento'), 3);

SELECT * FROM ROL;
SELECT * FROM Usuarios;
SELECT * FROM Clientes;
SELECT * FROM Proveedor;
SELECT * FROM Marca;
SELECT * FROM Categoria;
SELECT * FROM Productos;
SELECT * FROM Cotizaciones;
SELECT * FROM Detalle;
