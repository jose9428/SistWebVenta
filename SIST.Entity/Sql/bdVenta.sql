create database bdVenta
go

use bdVenta
go

CREATE TABLE Categoria(
	id_categoria INT IDENTITY PRIMARY KEY ,
	nombre_categoria VARCHAR(70),
    estado int
)
go

CREATE TABLE Producto(
	id_producto INT IDENTITY PRIMARY KEY ,
	id_categoria INT,
    nombre_producto VARCHAR(100),
    precio DECIMAL(12,2),
    stock INT,
	imagen VARCHAR(100),
    eliminado INT,
    FOREIGN KEY(id_categoria)REFERENCES Categoria(id_categoria) 
)
go

insert into Categoria values('Abarrotes' , 1)

select * from Categoria