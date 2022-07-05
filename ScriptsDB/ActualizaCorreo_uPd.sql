
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 05-07-2022
-- Description: Actualiza el correo del usuario
-- =============================================
CREATE PROCEDURE dbo.ActualizaCorreo_uPd
	@email varchar(70)
	,@idusuario int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE tbl_Usuario
	SET email = @email
	where id_usuario =@idusuario

END
GO
