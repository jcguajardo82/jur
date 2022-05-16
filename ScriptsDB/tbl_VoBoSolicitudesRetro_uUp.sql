
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: Consulta las retro por folio de solicitud,
-- =============================================
CREATE PROCEDURE tbl_VoBoSolicitudesRetro_uUp
	-- Add the parameters for the stored procedure here
		@Id_voBoSolRetro int
		,@comentariosNegocio varchar(200)
		,@riesgosDestacados  varchar(200)
		,@autorizado  bit 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here




UPDATE [dbo].[tbl_VoBoSolicitudesRetro]
   SET 
       [comentariosNegocio] = @comentariosNegocio
      ,[riesgosDestacados] = @riesgosDestacados
      ,[autorizado] = @autorizado
      ,[fec_autorizado] = getdate()
      
 WHERE Id_voBoSolRetro=@Id_voBoSolRetro






END
GO
