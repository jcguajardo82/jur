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