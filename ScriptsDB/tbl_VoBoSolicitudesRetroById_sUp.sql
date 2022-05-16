
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: Consulta las retro por folio de solicitud,
-- =============================================
CREATE PROCEDURE tbl_VoBoSolicitudesRetroById_sUp
	-- Add the parameters for the stored procedure here
		@Id_voBoSolRetro int
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


	SELECT  A.*,B.comentarios
	FROM tbl_VoBoSolicitudesRetro AS A
	JOIN tbl_VoBoSolicitudes AS B
		ON B.Id_voBoSol=A.Id_voBoSol 

	WHERE Id_voBoSolRetro=@Id_voBoSolRetro



END
GO
