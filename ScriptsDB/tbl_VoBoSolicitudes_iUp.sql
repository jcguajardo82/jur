IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudes_iUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudes_iUp
GO

-- =============================================
-- Author:		P. Castillo
-- Create date: 12-05-2022
-- Description:	Da de alta las solicitudes de visto bueno 
-- tbl_VoBoSolicitudes_iUp 18,'123','1@s.com','2@s.com','3@s.com','4@s.com','5@s.com','1'
-- =============================================
CREATE PROCEDURE dbo.tbl_VoBoSolicitudes_iUp
		@idSolicitud int
		,@comentarios varchar(200)
		,@correo1 varchar(50)
		,@correo2 varchar(50)
		,@correo3 varchar(50)
		,@correo4 varchar(50)
		,@correo5 varchar(50)		
		,@id_user int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


INSERT INTO [dbo].[tbl_VoBoSolicitudes]
           ([idSolicitud]
           ,[comentarios]
           ,[correo1]
           ,[correo2]
           ,[correo3]
           ,[correo4]
           ,[correo5]
           ,[fec_movto]
           ,[id_user])
     VALUES
           (@idSolicitud 
           ,@comentarios 
           ,@correo1 
           ,@correo2 
           ,@correo3 
           ,@correo4 
           ,@correo5 
           ,GETDATE()
           ,@id_user)

	 SELECT @@IDENTITY   as Id_voBoSol
END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudes_iUp TO produccion;
GO