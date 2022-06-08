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
