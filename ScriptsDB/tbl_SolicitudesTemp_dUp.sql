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



