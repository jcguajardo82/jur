
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 01-06-2022
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.tbl_PlantillasJuridicasById_sUp 
	-- Add the parameters for the stored procedure here
	@Id_PlantillaJuridica INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tbl_PlantillasJuridicas
	WHERE Id_PlantillaJuridica =@Id_PlantillaJuridica
END
GO
