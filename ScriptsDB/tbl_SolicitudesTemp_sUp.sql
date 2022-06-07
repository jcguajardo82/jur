
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
