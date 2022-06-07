
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
