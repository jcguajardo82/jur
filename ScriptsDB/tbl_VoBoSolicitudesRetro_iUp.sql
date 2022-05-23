IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesRetro_iUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesRetro_iUp
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: crea los registros para la retroalimentacion de las solicitudes,
-- =============================================
CREATE PROCEDURE dbo.tbl_VoBoSolicitudesRetro_iUp
	-- Add the parameters for the stored procedure here
		@Id_voBoSol int
		,@correo varchar(50)		           
		,@id_user int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


INSERT INTO [dbo].[tbl_VoBoSolicitudesRetro]
           ([Id_voBoSol]
           ,[correo]
           ,[comentariosNegocio]
           ,[riesgosDestacados]
           ,[autorizado]
           ,[fec_autorizado]
           ,[fec_movto]
           ,[id_user])
     VALUES
           (@Id_voBoSol 
           ,@correo
           ,''
           ,''
           ,null
           ,null
           ,GETDATE()
           ,@id_user )



END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetro_iUp TO produccion;
GO