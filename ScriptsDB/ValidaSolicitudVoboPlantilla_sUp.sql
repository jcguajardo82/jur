-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 15-05-2022
-- Description:	Consulta que la solicitud, con base a su plantilla, no requiera Vobo adicional, 
--				de lo contrario, valida que y tenga todos los vistos buenos autorizados
-- =============================================
CREATE PROCEDURE dbo.ValidaSolicitudVoboPlantilla_sUp 
	-- Add the parameters for the stored procedure here
	 @SolicitudId Int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @voBO bit
		,@Message varchar(50) = '0'
		,@Id_voBoSol INT 
		,@VobosPendientes int =0
	SELECT @voBO=B.voBo FROM tbl_Solicitud A 
	JOIN  tbl_PlantillasJuridicas B
	ON A.id_plantilla = B.Id_PlantillaJuridica
	WHERE A.id_Solicitud = @SolicitudId


	IF(@voBO =1)
	BEGIN
		SELECT @Id_voBoSol=Id_voBoSol FROM tbl_VoBoSolicitudes
		WHERE idSolicitud = @SolicitudId

		IF(@Id_voBoSol IS NULL)
		BEGIN
			SET @Message='1' -- SE NECESITA CREAR LA SOLICITUD DE VOBO
		END
		ELSE
		BEGIN
			SELECT @VobosPendientes = COUNT(*) FROM tbl_VoBoSolicitudesRetro
			WHERE Id_voBoSol = @Id_voBoSol
			AND autorizado is null


			SELECT @VobosPendientes =@VobosPendientes + COUNT(*) FROM tbl_VoBoSolicitudesRetro
			WHERE Id_voBoSol = @Id_voBoSol
			AND autorizado = 0

			IF(@VobosPendientes<>0)
			BEGIN
			SET @Message='2' -- ESTAN PENDIENTES VOBOS 
			END
		END

	END

	SELECT @Message as [Message]
END
GO
