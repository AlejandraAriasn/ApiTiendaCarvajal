USE [PruebaCarvajal]
GO
/****** Object:  User [UserTest]    Script Date: 27/05/2022 8:11:28 p. m. ******/
CREATE USER [UserTest] FOR LOGIN [UserTest] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Configuracion]    Script Date: 27/05/2022 8:11:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracion](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[Parametro] [varchar](600) NULL,
	[Valor] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleCompra]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleCompra](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[IdRecordEncabezado] [bigint] NOT NULL,
	[IdProducto] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioCompraUnidad] [money] NOT NULL,
	[Descuento] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncabezadoCompra]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncabezadoCompra](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[IdUsuario] [bigint] NOT NULL,
	[SubTotal] [money] NULL,
	[Impuestos] [money] NULL,
	[Total] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrupoUsuarios]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrupoUsuarios](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](600) NOT NULL,
	[Descripcion] [varchar](600) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProducto] [bigint] NOT NULL,
	[Descuento] [decimal](2, 1) NULL,
	[Estado] [int] NOT NULL,
	[CantidadDisponible] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogEvents]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEvents](
	[IdRecord] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
	[Modulo] [nvarchar](max) NULL,
	[Metodo] [nvarchar](max) NULL,
	[Error] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogEvents] PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[CodProducto] [varchar](max) NOT NULL,
	[NombreProducto] [varchar](max) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Precio] [money] NOT NULL,
	[Imagen] [varchar](max) NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdRecord] [bigint] IDENTITY(1,1) NOT NULL,
	[NumeroDocumento] [varchar](600) NOT NULL,
	[NombreCompleto] [varchar](600) NOT NULL,
	[NombreUsuario] [varchar](600) NOT NULL,
	[CorreoElectronico] [varchar](600) NOT NULL,
	[ClaveAccesoCifrada] [varchar](max) NULL,
	[FechaClave] [datetime] NULL,
	[NDiasClave] [int] NULL,
	[Salt] [varchar](max) NULL,
	[Estado] [int] NOT NULL,
	[TipoUsuario] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleCompra]  WITH CHECK ADD  CONSTRAINT [FK_DetalleCompra_EncabezadoCompra] FOREIGN KEY([IdRecordEncabezado])
REFERENCES [dbo].[EncabezadoCompra] ([IdRecord])
GO
ALTER TABLE [dbo].[DetalleCompra] CHECK CONSTRAINT [FK_DetalleCompra_EncabezadoCompra]
GO
ALTER TABLE [dbo].[DetalleCompra]  WITH CHECK ADD  CONSTRAINT [FK_DetalleCompra_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdRecord])
GO
ALTER TABLE [dbo].[DetalleCompra] CHECK CONSTRAINT [FK_DetalleCompra_Productos]
GO
ALTER TABLE [dbo].[EncabezadoCompra]  WITH CHECK ADD  CONSTRAINT [FK_EncabezadoCompra_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdRecord])
GO
ALTER TABLE [dbo].[EncabezadoCompra] CHECK CONSTRAINT [FK_EncabezadoCompra_Usuarios]
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD  CONSTRAINT [FK_Inventario_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdRecord])
GO
ALTER TABLE [dbo].[Inventario] CHECK CONSTRAINT [FK_Inventario_Productos]
GO
/****** Object:  StoredProcedure [dbo].[spActualizarInventario]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spActualizarInventario]
@CantidadDisponible int,
@IdProducto bigint
as
begin
	update inventario set CantidadDisponible=@CantidadDisponible where IdProducto=@IdProducto
	select @@ROWCOUNT Actualizado
end
GO
/****** Object:  StoredProcedure [dbo].[spActualizarProducto]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spActualizarProducto]

@IdRecordProducto bigint
as
begin
	
		update Productos set Estado=0 where IdRecord=@IdRecordProducto
		select @@ROWCOUNT Actualizado
	
end
GO
/****** Object:  StoredProcedure [dbo].[spReporteVentas]    Script Date: 27/05/2022 8:11:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spReporteVentas]
		@opcion varchar(600)
		as
		begin
					select sum(a.cantidad) CantidadVendido,b.CodProducto,b.NombreProducto into #Ventas
					from DetalleCompra a
					join productos b on a.IdProducto=b.IdRecord
					group by b.NombreProducto,b.CodProducto

			if @opcion='MasVendidos'
			begin
					select * from #Ventas order by 1 desc
			end

			if @opcion='MenosVendidos'
			begin
					select * from #Ventas order by 1 asc
			end
		end
GO
