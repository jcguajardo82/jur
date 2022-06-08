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

