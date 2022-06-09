
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE tbl_PlantillasCorreoById_sUp 
	-- Add the parameters for the stored procedure here
	@IdPlantillaCorreo int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tbl_PlantillasCorreo 
	WHERE IdPlantillaCorreo = @IdPlantillaCorreo
END
GO
