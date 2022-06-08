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
