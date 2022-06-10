IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.SelSolicitudUsuario') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.SelSolicitudUsuario
GO
-- =======================================================================================================  
-- Author:  Jose Garcia  
-- Create date: Dec/12/2014  
-- Modified:    Jan/12/2015 (Humberto Sanchez)  
-- Description: Consulta todas las solicitudes de un usuario y las muestra en el grid de Consultar.aspx  
-- Modified:    Jan/12/2015 (Humberto Sanchez)  -- Se agrega al resultado las columnas Correo,EstatusAutPrev
--				asi como un union a la tabla de VoBo
-- =======================================================================================================  
CREATE PROCEDURE [dbo].[SelSolicitudUsuario]  
 @UsuarioId INT  
  
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 SELECT [S].[id_Solicitud]  
   ,[S].[id_Status]  
   ,[S].[id_autorizador]  
   ,[S].[RutaArchivo]  
   ,[S].[id_user]  
   ,[S].[fechaCreacion]  
   ,[S].[fechaAutorizacion]  
   ,[S].[Folio] + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,[S].[FechaCreacion])) AS Folio  
   ,[S].[Consecutivo]  
   ,[S].[fechaAsignada]  
   ,[S].[Autorizador]  
   ,[T].[Descripcion] AS [TipoPlantilla]  
   ,[P].[Id_PlantillaJuridica]  
   ,[P].[Nombre] AS [Nombre]  
   ,[St].[Descripcion] AS [Status]  
   ,[U].[NEmpleado]  
   ,[U].[Nombre] AS [Usuario]  
   ,'' Correo
   ,'' EstatusAutPrev
   ,0 Id_voBoSol
 FROM   
 [dbo].[tbl_Solicitud] [S]  
  
 INNER JOIN [dbo].[tbl_PlantillasJuridicas] [P] ON [S].[id_plantilla] = [P].[id_plantillaJuridica]  
 INNER JOIN [dbo].[tbl_TipoPlantilla] [T] ON [P].[id_tipoplantilla] = [T].[id_tipoplantilla]  
 INNER JOIN [dbo].[tbl_Status] [St] ON [S].[id_Status] = [St].[id_Status]  
 INNER JOIN [dbo].[tbl_usuario] [U] ON [U].[id_usuario] = [S].[id_user]  
  
 WHERE   
      [S].[id_user] = @UsuarioId 
 --AND  [S].[id_Status] IN (1, 2, 3)  
     AND S.id_Solicitud NOT IN (SELECT idSolicitud FROM tbl_VoBoSolicitudes ) 
    --ORDER BY   
    --[S].[FechaCreacion] DESC  

UNION

SELECT [S].[id_Solicitud]  
   ,[S].[id_Status]  
   ,[S].[id_autorizador]  
   ,[S].[RutaArchivo]  
   ,[S].[id_user]  
   ,[S].[fechaCreacion]  
   ,[S].[fechaAutorizacion]  
   ,[S].[Folio] + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,[S].[FechaCreacion])) AS Folio  
   ,[S].[Consecutivo]  
   ,[S].[fechaAsignada]  
   ,[S].[Autorizador]  
   ,[T].[Descripcion] AS [TipoPlantilla]  
   ,[P].[Id_PlantillaJuridica]  
   ,[P].[Nombre] AS [Nombre]  
   ,[St].[Descripcion] AS [Status]  
   ,[U].[NEmpleado]  
   ,[U].[Nombre] AS [Usuario]  
   , A.correo
	,CASE WHEN autorizado is null THEN
	'En visto bueno'
	ELSE
	CASE WHEN autorizado =1 THEN
	'Autorizado'
	ELSE
	'Rechazado'
	END
	END as EstatusAutPrev
	,A.Id_voBoSol 
 FROM   
 dbo.tbl_VoBoSolicitudesRetro A
 JOIN dbo.tbl_VoBoSolicitudes B
 ON B.Id_voBoSol = A.Id_voBoSol
LEFT JOIN  [dbo].[tbl_Solicitud] [S]  
  ON S.id_Solicitud = B.idSolicitud
 INNER JOIN [dbo].[tbl_PlantillasJuridicas] [P] ON [S].[id_plantilla] = [P].[id_plantillaJuridica]  
 INNER JOIN [dbo].[tbl_TipoPlantilla] [T] ON [P].[id_tipoplantilla] = [T].[id_tipoplantilla]  
 INNER JOIN [dbo].[tbl_Status] [St] ON [S].[id_Status] = [St].[id_Status]  
 INNER JOIN [dbo].[tbl_usuario] [U] ON [U].[id_usuario] = [S].[id_user]  
  WHERE 
  [S].[id_user] = @UsuarioId  
 --AND  [S].[id_Status] IN (1, 2, 3)  
     --AND S.id_Solicitud NOT IN (SELECT idSolicitud FROM tbl_VoBoSolicitudes ) 
    ORDER BY   
    [S].[FechaCreacion] DESC 

END  
  
GRANT EXECUTE ON OBJECT::dbo.SelSolicitudUsuario TO produccion;
GO