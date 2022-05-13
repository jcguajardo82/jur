-- =============================================================================================================  
-- Author:  Pedro Cstillo  
-- Create date: 13/05/2022  
-- Description: Inserta un archivo de una plantilla  
-- =============================================================================================================  
CREATE PROCEDURE [dbo].tbl_VoBoSolicitudesArchivos_iUp  
	@Id_voBoSol int,  
	@Archivo varbinary(max),  
	@Nombre varchar(70)  
AS  
BEGIN  
 SET NOCOUNT ON;  
  
 INSERT INTO [dbo].tbl_VoBoSolicitudesArchivos  
           (Id_voBoSol  
           ,[PlantillaArchivo],Nombre)  
     VALUES  
           (@Id_voBoSol  
			,@Archivo  
           ,@Nombre)  
  
END  
  