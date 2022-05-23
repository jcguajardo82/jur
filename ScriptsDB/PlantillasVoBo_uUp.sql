IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.PlantillasVoBo_uUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.PlantillasVoBo_uUp
GO
-- =============================================
-- Author:		P. Castillo
-- Create date: 11/05/2022
-- Description:	Lista las plantillas disponibles para marcar el VoBo
-- =============================================
CREATE PROCEDURE dbo.PlantillasVoBo_uUp
	@Id_PlantillaJuridica int
	,@voBo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.tbl_PlantillasJuridicas
	SET voBo= @voBo
	WHERE Id_PlantillaJuridica=@Id_PlantillaJuridica
    
END
GO

GRANT EXECUTE ON OBJECT::dbo.PlantillasVoBo_uUp TO produccion;
GO