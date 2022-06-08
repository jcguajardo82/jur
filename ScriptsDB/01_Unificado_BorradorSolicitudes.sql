IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'ArchivoSolicitudTemp')
	CREATE TABLE [dbo].[ArchivoSolicitudTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[idArchivo] [int] IDENTITY(1,1) NOT NULL,
		[idTipoDocumento] [int] NOT NULL,
		[Nombre] [varchar](70) NOT NULL,
		[Ruta] [nvarchar](100) NULL,
		[Archivo] [varbinary](max) NULL,
	 CONSTRAINT [PK_Archivo_1] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC,
		[idArchivo] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
GO

IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_EtiquetasTemp')

	CREATE TABLE [dbo].[tbl_EtiquetasTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[id_etiquetas] [int] NOT NULL,
		[id_PlantillaJuridica] [int] NOT NULL,
		[respuesta] [nvarchar](max) NULL,
		[fechaCreacion] [datetime] NULL,
	 CONSTRAINT [PK_tbl_EtiquetasTemp] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC,
		[id_etiquetas] ASC,
		[id_PlantillaJuridica] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
GO

IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_SolicitudesTemp')


	CREATE TABLE [dbo].[tbl_SolicitudesTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[fechaCreacion] [datetime] NULL,
	 CONSTRAINT [PK_tbl_SolicitudesTemp] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	GO

	ALTER TABLE [dbo].[tbl_SolicitudesTemp] ADD  CONSTRAINT [DF_tbl_SolicitudesTemp_fechaCreacion]  DEFAULT (getdate()) FOR [fechaCreacion]
	GO
	GO
GO


IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.[DelArchivoSolicitudTemp]') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.[DelArchivoSolicitudTemp]
GO


-- =================================================================================
-- Author:		Pedro Castillo
-- Create date: 11/11/14
-- Description:	Elimina un archivo correspondiente a una solicitud temporal.
-- =================================================================================
CREATE PROCEDURE [dbo].[DelArchivoSolicitudTemp]
	@idUsuario int ,
	@idTipoSolicitud int ,
	@idPlantilla int ,
	@idTipoDocumento int ,

	@Nombre nvarchar(50)
	

AS
BEGIN
	SET NOCOUNT ON;

DELETE FROM dbo.[ArchivoSolicitudTemp]
WHERE idUsuario=@idUsuario
AND idTipoSolicitud=@idTipoSolicitud
AND idPlantilla=@idPlantilla
AND idTipoDocumento=@idTipoDocumento
and Nombre=@Nombre

END

GO

GRANT EXECUTE ON OBJECT::dbo.[DelArchivoSolicitudTemp] TO produccion;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.InsArchivoSolicitudTemp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.[InsArchivoSolicitudTemp]
GO


-- =================================================================================
-- Author:		Pedro Castillo
-- Create date: 11/11/14
-- Description:	Inserta un archivo correspondiente a una solicitud temporal.
-- =================================================================================
CREATE PROCEDURE [dbo].[InsArchivoSolicitudTemp]
	@idUsuario int ,
	@idTipoSolicitud int ,
	@idPlantilla int ,
	@idTipoDocumento int ,

	@Nombre nvarchar(50),
	@Ruta nvarchar(100) = null,
	@Archivo varbinary(max)

AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[ArchivoSolicitudTemp]
           ([idUsuario]
           ,[idTipoSolicitud]
           ,[idPlantilla]
           ,[idTipoDocumento]
           ,[Nombre]
           ,[Ruta]
           ,[Archivo])
     VALUES
           (@idUsuario 
           ,@idTipoSolicitud 
           ,@idPlantilla 
           ,@idTipoDocumento  
           ,@Nombre 
           ,@Ruta 
           ,@Archivo  )

END

GO

GRANT EXECUTE ON OBJECT::dbo.InsArchivoSolicitudTemp TO produccion;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.SelArchivoSolicitudTemp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.SelArchivoSolicitudTemp
GO

-- =================================================================================
-- Author:		Pedro Castillo
-- Create date: 11/11/14
-- Description:	Selecciona los archivos correspondiente a una solicitud temporal.
-- =================================================================================
CREATE PROCEDURE [dbo].[SelArchivoSolicitudTemp]
	@idUsuario int ,
	@idTipoSolicitud int ,
	@idPlantilla int 


AS
BEGIN
	SET NOCOUNT ON;

SELECT A.*,D.TipoDocumento FROM dbo.[ArchivoSolicitudTemp] A
LEFT JOIN tblTipoDocumento D 
	ON A.idTipoDocumento = D.idTipoDocumento
WHERE idUsuario=@idUsuario
AND idTipoSolicitud=@idTipoSolicitud
AND idPlantilla=@idPlantilla


END

GO
GRANT EXECUTE ON OBJECT::dbo.SelArchivoSolicitudTemp TO produccion;
GO



IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_EtiquetasTemp_iUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_EtiquetasTemp_iUp
GO


-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	Registra las respuestas para la plantilla
-- =============================================
CREATE PROCEDURE dbo.tbl_EtiquetasTemp_iUp 
	-- Add the parameters for the stored procedure here
	@idUsuario int
	,@idTipoSolicitud int
	,@idPlantilla int
	,@id_etiquetas int
	,@id_PlantillaJuridica int
	,@respuesta nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
IF (NOT EXISTS(SELECT * FROM [dbo].[tbl_EtiquetasTemp] WHERE 
				@idUsuario =idUsuario
			   AND @idTipoSolicitud =idTipoSolicitud
			   AND @idPlantilla =idPlantilla
			   AND @id_etiquetas =id_etiquetas
			   AND @id_PlantillaJuridica =id_PlantillaJuridica
) )
BEGIN
	INSERT INTO [dbo].[tbl_EtiquetasTemp]
			   ([idUsuario]
			   ,[idTipoSolicitud]
			   ,[idPlantilla]
			   ,[id_etiquetas]
			   ,[id_PlantillaJuridica]
			   ,[respuesta]
			   ,fechaCreacion)
		 VALUES
			   (@idUsuario 
			   ,@idTipoSolicitud 
			   ,@idPlantilla 
			   ,@id_etiquetas 
			   ,@id_PlantillaJuridica 
			   ,@respuesta
			   ,getdate())

END

END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_EtiquetasTemp_iUp TO produccion;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_EtiquetasTemp_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_EtiquetasTemp_sUp
GO


-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	Obtiene las respuestas para la plantilla
-- =============================================
CREATE PROCEDURE dbo.tbl_EtiquetasTemp_sUp 
	-- Add the parameters for the stored procedure here
	@idUsuario int
	,@idTipoSolicitud int
	,@idPlantilla int
	,@id_etiquetas int
	,@id_PlantillaJuridica int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	select respuesta from tbl_EtiquetasTemp where
				@idUsuario =idUsuario
			   AND @idTipoSolicitud =idTipoSolicitud
			   AND @idPlantilla =idPlantilla
			   AND @id_etiquetas =id_etiquetas
			   AND @id_PlantillaJuridica =id_PlantillaJuridica


END
GO
GRANT EXECUTE ON OBJECT::dbo.tbl_EtiquetasTemp_sUp TO produccion;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_EtiquetasTemp_uUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_EtiquetasTemp_uUp
GO


-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	Axtualiza las respuestas para la plantilla
-- =============================================
CREATE PROCEDURE dbo.tbl_EtiquetasTemp_uUp 
	-- Add the parameters for the stored procedure here
	@idUsuario int
	,@idTipoSolicitud int
	,@idPlantilla int
	,@id_etiquetas int
	,@id_PlantillaJuridica int
	,@respuesta nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	update [dbo].[tbl_EtiquetasTemp]
		SET respuesta = @respuesta

	WHERE  
				@idUsuario =idUsuario
			   AND @idTipoSolicitud =idTipoSolicitud
			   AND @idPlantilla =idPlantilla
			   AND @id_etiquetas =id_etiquetas
			   AND @id_PlantillaJuridica =id_PlantillaJuridica


END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_EtiquetasTemp_uUp TO produccion;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_SolicitudesTemp_dUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_SolicitudesTemp_dUp
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01/08/2022
-- Description:	Elimina la  informacion temporal (borrador) del usuario para el tipo de plantilla indicada
-- =============================================
CREATE PROCEDURE [dbo].tbl_SolicitudesTemp_dUp
	@idUsuario int ,
	@idTipoSolicitud int ,
	@idPlantilla int 

AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM ArchivoSolicitudTemp
	WHERE idUsuario=@idUsuario
	AND idTipoSolicitud=@idTipoSolicitud
	AND idPlantilla=@idPlantilla

	DELETE FROM tbl_SolicitudesTemp 
	WHERE idUsuario=@idUsuario
	AND idTipoSolicitud=@idTipoSolicitud
	AND idPlantilla=@idPlantilla

	DELETE FROM tbl_EtiquetasTemp
	WHERE idUsuario=@idUsuario
	AND idTipoSolicitud=@idTipoSolicitud
	AND idPlantilla=@idPlantilla
END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_SolicitudesTemp_dUp TO produccion;
GO




IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_SolicitudesTemp_iUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_SolicitudesTemp_iUp
GO


-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	Inserta el encabezado de las solicitudes temporales por usuario (borradores)
-- =============================================
CREATE PROCEDURE dbo.tbl_SolicitudesTemp_iUp 
	-- Add the parameters for the stored procedure here
	@idUsuario int 
	,@idTipoSolicitud int 
	,@idPlantilla int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

IF (NOT EXISTS(SELECT * FROM[dbo].[tbl_SolicitudesTemp] WHERE 
@idUsuario  =idUsuario
			   and @idTipoSolicitud  =idTipoSolicitud
			   and @idPlantilla =idPlantilla ) )
	BEGIN
	INSERT INTO [dbo].[tbl_SolicitudesTemp]
			   ([idUsuario]
			   ,[idTipoSolicitud]
			   ,[idPlantilla]
			   ,[fechaCreacion])
		 VALUES
			   (@idUsuario  
			   ,@idTipoSolicitud  
			   ,@idPlantilla  
			   ,getdate())
	END


END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_SolicitudesTemp_iUp TO produccion;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_SolicitudesTemp_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_SolicitudesTemp_sUp
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	obtiene el encabezado de las solicitudes temporales por usuario (borradores)
-- =============================================
CREATE PROCEDURE dbo.tbl_SolicitudesTemp_sUp 
	-- Add the parameters for the stored procedure here
	@idUsuario int 
	,@idTipoSolicitud int 
	,@idPlantilla int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

SELECT * FROM[dbo].[tbl_SolicitudesTemp] WHERE 
			@idUsuario  =idUsuario
			   and @idTipoSolicitud  =idTipoSolicitud
			   and @idPlantilla =idPlantilla 


END
GO



GRANT EXECUTE ON OBJECT::dbo.tbl_SolicitudesTemp_sUp TO produccion;
GO
