
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		P. Castillo
-- Create date: 11/05/2022
-- Description:	Lista las plantillas disponibles para marcar el VoBo
-- =============================================
CREATE PROCEDURE dbo.PlantillasVoBo_sUp

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.Id_PlantillaJuridica, A.Nombre,A.Descripcion
	,ISNULL(B.Descripcion,'--') AS DescClas 
	,A.voBo
	FROM dbo.tbl_PlantillasJuridicas as A
	LEFT JOIN	[dbo].[tbl_ClasificacionPlantilla] B
	ON A.id_tipoplantilla = B.id_tipoPlantilla
	AND A.id_clasificacionplantilla = B.id_clasificacionplantilla
	WHERE A.activo=1

END
GO
