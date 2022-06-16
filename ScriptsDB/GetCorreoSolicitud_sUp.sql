
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 15/06/2022
-- Description:	Obtiene el correo del usuario que creo la solicitud
-- =============================================
CREATE PROCEDURE dbo.GetCorreoSolicitud_sUp
	@id_Solicitud INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT email FROM tbl_Solicitud  S
	JOIN tbl_Usuario U ON U.id_usuario = S.id_user
	WHERE S.id_Solicitud=@id_Solicitud
END
GO
