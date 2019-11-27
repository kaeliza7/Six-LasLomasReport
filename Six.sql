Use Master
Go
If(db_id('Six')Is Not Null)
Drop DataBase Six
Create DataBase Six
Go

Use Six
Go

Create Table Categoria
(IdCategoria Int Identity Primary Key,
Descripcion Varchar(50) Not Null
)
Go

Create Table Producto
(
IdProducto Int Identity Primary Key,
IdCategoria Int Not Null References Categoria,
Nombre Varchar(50) Not Null,
Stock Int Not Null,
PrecioCompra Decimal(6,2) Not Null,
)
Go

Create Table Cargo
(
IdCargo Int Identity Primary Key,
Descripcion Varchar(30) Not Null
)
Go

Create Table Empleado
(
IdEmpleado Int Identity Primary Key,
IdCargo Int Not Null References Cargo,
Dni Char(8) Not Null,
Apellidos Varchar(30) Not Null,
Nombres Varchar(30) Not Null,
Sexo Char(1) Not Null,
FechaNac Date Not Null,
Direccion Varchar(80) Not Null,
)
Go

Create Table Venta
(
IdVenta Int Identity Primary Key,
IdEmpleado Int Not Null References Empleado,
FechaVenta Date Not Null,
Total Money Not Null
)
go

Create Table DetalleVenta
(
IdDetalleVenta Int Identity Primary Key,
IdProducto Int Not Null References Producto,
IdVenta Int Not Null References Venta,
Cantidad Int Not Null,
SubTotal Money Not Null
)
Go


Insert Categoria Values('Cerveza'),('Mezcal'),('RTB'),('Cigarros'),('Sueros'),('Ence')
Go

Insert Cargo Values('Administrador'),('Empleado')
Go


--PROCEDIMIENTOS ALMACENADOS EN SQL SERVER 2012

Create Proc FiltrarDatosProducto
@Datos Varchar(50)
As Begin
	Select IdProducto,IdCategoria,Nombre,PrecioCompra,Stock 
	From Producto where Nombre+' ' + Nombre=@Datos 
End
Go

--FiltrarDatosProducto 'G'
Create Proc ListadoProductos
As Begin
	Select IdProducto,IdCategoria,Nombre,PrecioCompra,Stock 
	From Producto
End
go

Create Proc ListarCategoria
As Begin
	Select * From Categoria
End
Go



Create proc RegistrarCategoria
@Descripcion Varchar(50),
@Mensaje Varchar(50)  Out
As Begin
	If(Exists(Select * From Categoria Where Descripcion=@Descripcion))
		Set @Mensaje='Categoria ya se encuentra Registrada.'
	Else Begin
		Insert Categoria Values(@Descripcion)
		Set @Mensaje='Registrado Correctamente.'
	End
End
Go

Create proc ActualizarCategoria
@IdC Int,
@Descripcion Varchar(50),
@Mensaje Varchar(50)  Out
As Begin
	If(Not Exists(Select * From Categoria Where IdCategoria=@IdC))
		Set @Mensaje='Categoria no se encuentra Registrada.'
	Else Begin
		Update Categoria Set Descripcion=@Descripcion Where IdCategoria=@IdC
		Set @Mensaje='Se ha Actualizado los Datos Correctamente.'
	End
End
Go

Create Proc BuscarCategoria
@Datos Varchar(50)
As Begin
	Select * From Categoria Where Descripcion=@Datos
End
Go

Create Proc RegistrarProducto
@IdCategoria Int,
@Nombre Varchar(50),
@Stock Int,
@PrecioCompra Decimal(6,2),
@Mensaje varchar(50) Out
As Begin
	If(Exists(Select * From Producto Where Nombre=@Nombre))
		Set @Mensaje='Este Producto ya ha sido Registrado.'
	Else Begin
		Insert Producto Values(@IdCategoria,@Nombre,@Stock,@PrecioCompra)
		Set @Mensaje='Registrado Correctamente.'
	End
End
Go

Create Proc ActualizarProducto
@IdProducto Int,
@IdCategoria Int,
@Nombre Varchar(50),
@Stock Int,
@PrecioCompra Decimal(6,2),
@Mensaje varchar(50) Out
As Begin
	If(Not Exists(Select * From Producto Where IdProducto=@IdProducto))
		Set @Mensaje='Producto no se encuentra Registrado.'
	Else Begin
		Update Producto Set IdCategoria=@IdCategoria,Nombre=@Nombre,Stock=@Stock,
		PrecioCompra=@PrecioCompra
		Where IdProducto=@IdProducto
	Set @Mensaje='Registro Actualizado Correctamente.'
	End
End
Go


Create Proc ListarCargo
As Begin
	Select * From Cargo
	End
Go

Create Proc RegistrarCargo
@Descripcion Varchar(30),
@Mensaje Varchar(50) Out
As Begin
	If(Exists(Select * From Cargo Where Descripcion=@Descripcion))
		Set @Mensaje='El Cargo ya se Encuentra Registrado.'
	Else Begin
		Insert Cargo values(@Descripcion)
		Set @Mensaje='Registrado Correctamente.'
	End
End
Go

Create Proc ActualizarCargo
@IdCargo Int,
@Descripcion Varchar(30),
@Mensaje Varchar(100) Out
As Begin
	If(Exists(Select * From Cargo Where Descripcion=@Descripcion))
		Set @Mensaje='No se ha Podido Actualizar los Datos porque ya Existe el cargo. '+@Descripcion
	Else Begin
		If(Not Exists(Select * From Cargo Where IdCargo=@IdCargo))
			Set @Mensaje='El Cargo no se Encuentra Registrado.'
		Else Begin
		Update Cargo Set Descripcion=@Descripcion Where IdCargo=@IdCargo
			Set @Mensaje='Los datos se han Actualizado Correctamente.'
			End
		End
	End
Go


Create Proc BuscarCargo
@Descripcion varchar(30)
as begin
	Select * From Cargo Where Descripcion=@Descripcion
End
Go

 
Create Table Usuario
(IdUsuario Int Identity Primary Key,
IdEmpleado Int Not Null References Empleado,
Usuario Varchar(20) Not Null,
Contrase�a Varchar(12) Not Null
)
Go

Create Proc RegistrarUsuario
@IdEmpleado Int,
@Usuario Varchar(20),
@Contrase�a Varchar(12),
@Mensaje Varchar(50) Out
As Begin
	If(Not Exists(Select * From Empleado Where IdEmpleado=@IdEmpleado))
		Set @Mensaje='Empleado no Registrado Ok.'
	Else Begin
		If(Exists(Select * From Usuario Where IdEmpleado=@IdEmpleado))
			Set @Mensaje='Este Empleado Ya Tiene una Cuenta de Usuario.'
		Else Begin
			If(Exists(Select * From Usuario Where Usuario=@Usuario))
				Set @Mensaje='El Usuario: '+@Usuario+' No est� Disponible.'
			Else Begin
				Insert Usuario Values(@IdEmpleado,@Usuario,@Contrase�a)
					Set @Mensaje='Usuario Registrado Correctamente.'
				 End
			 End
		 End
   End
Go

Create Proc IniciarSesion
@Usuario Varchar(20),
@Contrase�a Varchar(12),
@Mensaje Varchar(50) Out
As Begin
	Declare @Empleado Varchar(50)
	If(Not Exists(Select Usuario From Usuario Where Usuario=@Usuario))
		Set @Mensaje='El Nombre de Usuario no Existe.'
		Else Begin
			If(Not Exists(Select Contrase�a From Usuario Where Contrase�a=@Contrase�a))
				Set @Mensaje='Su Contrase�a es Incorrecta.'
				Else Begin
					Set @Empleado=(Select E.Nombres+', '+E.Apellidos From Empleado E Inner Join Usuario U 
								   On E.IdEmpleado=U.IdEmpleado Where U.Usuario=@Usuario)
					    Begin
					Select Usuario,Contrase�a From Usuario Where Usuario=@Usuario And Contrase�a=@Contrase�a
							Set @Mensaje='Bienvenido Sr(a): '+@Empleado+'.'
						End
				  End
		   End
   End
Go

Create Proc DevolverDatosSesion
@Usuario Varchar(20),
@Contrase�a Varchar(12)
As Begin
	Select E.IdEmpleado,E.Apellidos+', '+E.Nombres 
	From Empleado E Inner Join Usuario U On E.IdEmpleado=U.IdEmpleado 
	Where U.Usuario=@Usuario And U.Contrase�a=@Contrase�a
	End
Go
--Insertar y modificar Empleados
Create Proc MantenimientoEmpleados
@IdEmpleado Int,
@IdCargo Int,
@Dni Char(8),
@Apellidos Varchar(30),
@Nombres Varchar(30),
@Sexo Char(1),
@FechaNac Date,
@Direccion Varchar(80),
@Mensaje Varchar(100) Out
As Begin
	If(Not Exists(Select * From Empleado Where Dni=@Dni))
		Begin
		Insert Empleado Values(@IdCargo,@Dni,@Apellidos,@Nombres,@Sexo,@FechaNac,@Direccion)
			Set @Mensaje='Registrado Correctamente Ok.'
		End
	Else Begin
	If(Exists(Select * From Empleado Where Dni=@Dni))
		Begin
		Update Empleado Set IdCargo=@IdCargo,Dni=@Dni,Apellidos=@Apellidos,Nombres=@Nombres,Sexo=@Sexo,
		FechaNac=@FechaNac,Direccion=@Direccion Where IdEmpleado=@IdEmpleado
			Set @Mensaje='Se Actualizaron los Datos Correctamente Ok.'
		End
	End
End
Go

Create Proc ListadoEmpleados
As Begin
	Select E.*,C.Descripcion From Cargo C Inner Join Empleado E On C.IdCargo=E.IdCargo
End
Go

select * from Usuario

Create Proc GenerarIdEmpleado
@IdEmpleado Int Out
As Begin
	Declare @Cant As Int
	If(Not Exists(Select IdEmpleado From Empleado))
		Set @IdEmpleado=1
	Else Begin
		Set @IdEmpleado=(Select Max(IdEmpleado)+1 FROM Empleado)
	End
End
Go

Create proc Buscar_Empleado(
@Datos Varchar(50)
)
As Begin
		Select E.*,C.Descripcion From Cargo C Inner Join Empleado E On C.IdCargo=E.IdCargo
		where E.Nombres like @Datos +'%' or E.Apellidos like @Datos +'%'
End
go

Create Proc GenerarIdVenta
@IdVenta Int Out
As Begin
	If(Not Exists(Select IdVenta From Venta))
		Set @IdVenta=1
	Else Begin
		Set @IdVenta=(Select Max(IdVenta)+1 FROM Venta)
		End
	End
Go

Create Proc RegistrarVenta
@IdEmpleado Int,
@FechaVenta Date,
@Total Money,
@Mensaje Varchar(100) Out
As Begin
	Insert Venta Values(@IdEmpleado,@FechaVenta,@Total)
		Set @Mensaje='La Venta se ha Generado Correctamente.'
	End
Go


Create Proc RegistrarDetalleVenta
@IdProducto Int,
@IdVenta Int,
@Cantidad Int,
@SubTotal Money,
@Mensaje Varchar(100) Out
As Begin
	Declare @Stock As Int
	Set @Stock=(Select Stock From Producto Where IdProducto=@IdProducto)
	Begin
		Insert DetalleVenta Values(@IdProducto,@IdVenta,@Cantidad,@SubTotal)
			Set @Mensaje='Registrado Correctamente.'
	End
		Update Producto Set Stock=@Stock-@Cantidad Where IdProducto=@IdProducto
End
Go

Insert Empleado Values(1,'34004387','Silva Terrones','Miguel','M','11/01/1990','Urb. Santa Rosa')
Go
Insert Usuario Values(1,'Karen','1234')
Go

Insert Producto Values(1,'Gaseosa','Pepsi',5,4.90)
Go

Select * From Usuario
select * from De
truncate table DetalleVenta
Go