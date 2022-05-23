IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesRetro_uUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesRetro_uUp
GO


-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: Consulta las retro por folio de solicitud,
-- =============================================
CREATE PROCEDURE dbo.tbl_VoBoSolicitudesRetro_uUp
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

GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetro_uUp TO produccion;
GO