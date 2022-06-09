IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesRetroById_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesRetroById_sUp
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: Consulta las retro por folio de solicitud,
-- =============================================
CREATE PROCEDURE dbo.tbl_VoBoSolicitudesRetroById_sUp
	-- Add the parameters for the stored procedure here
		@Id_voBoSolRetro int
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


 SELECT  A.*,B.comentarios  
 ,  Folio + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,FechaCreacion)) AS FolioCompleto
  ,S.id_Solicitud
 FROM tbl_VoBoSolicitudesRetro AS A  
 JOIN tbl_VoBoSolicitudes AS B  
  ON B.Id_voBoSol=A.Id_voBoSol   
 JOIN tbl_Solicitud S 
	ON S.id_Solicitud =B.idSolicitud

	WHERE Id_voBoSolRetro=@Id_voBoSolRetro



END
GO
GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetroById_sUp TO produccion;
GO