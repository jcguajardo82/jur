    
-- =================================================================================================    
-- ALLWARE SOLUTIONS    
-- Author:  Juan Carlos Guajardo Chavez    
-- Create date: 31/10/2014    
-- Description: Verifica que exista el usuario para saber si puede acceder al sistema    
-- =================================================================================================    
ALTER PROCEDURE [dbo].[sp_ExistUser]  -- [sp_ExistUser]  900000001  
 @empleado  as varchar(70)--, as int    
AS    
BEGIN    
    
 SET NOCOUNT ON;    
 DECLARE @Existe int     
  If ((SELECT count(1) From [dbo].[tbl_usuario](nolock) WHERE [NEmpleado] = @empleado AND activo = 1 ) > 0)    
 --If ((SELECT count(1) From [dbo].[tbl_usuario](nolock) WHERE  rtrim(ltrim(email)) = rtrim(ltrim(@empleado)) AND activo = 1 ) > 0)    
  BEGIN    
        SET @Existe=1;    
      
  SELECT @Existe as Valido    
   ,[U].[id_nperfil]    
   ,[U].[id_usuario]     
   ,[U].[Nombre]    
   ,[U].[NEmpleado]    
   ,[U].[email]    
   ,[P].[Descripcion]    
    FROM [dbo].[tbl_usuario] [U]    
    JOIN [dbo].[tbl_nperfil] [P] ON [U].[id_nperfil] = [P].[id_nperfil]    
   WHERE [U].[NEmpleado] = rtrim(ltrim(@empleado))     
   --WHERE rtrim(ltrim([U].email)) = rtrim(ltrim(@empleado))     
   AND U.activo = 1    
       
  END    
  ELSE    
  BEGIN    
  SET @Existe=0;    
  END    
END    
    
  