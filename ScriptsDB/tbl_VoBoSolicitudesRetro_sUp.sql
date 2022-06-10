IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesRetro_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesRetro_sUp
GO
-- =============================================  
-- Author:  Pedro Castillo  
-- Create date: 13-05-2022,  
-- Description: Consulta las retro por folio de solicitud,  
-- =============================================  
CREATE PROCEDURE tbl_VoBoSolicitudesRetro_sUp  
 -- Add the parameters for the stored procedure here  
  @Id_voBoSol int  
    
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
  
  
 SELECT * FROM tbl_VoBoSolicitudesRetro  
 WHERE Id_voBoSol=@Id_voBoSol  
  
  
  
END  
GO
GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetro_sUp TO produccion;
GO