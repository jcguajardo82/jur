IF NOT EXISTS(
  SELECT TOP 1 1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE 
    [TABLE_NAME] = 'tbl_PlantillasJuridicas'
    AND [COLUMN_NAME] = 'voBo')
BEGIN

	ALTER TABLE tbl_PlantillasJuridicas
	ADD voBo bit null
	
	update tbl_PlantillasJuridicas
	set voBo = 0

END
GO

IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_PlantillasCorreo')
	CREATE TABLE [dbo].[tbl_PlantillasCorreo](
		[IdPlantillaCorreo] [int] NOT NULL,
		[Subject] [nvarchar](max) NULL,
		[Body] [nvarchar](max) NULL,
		[Descripcion] [nvarchar](50) NULL,
	 CONSTRAINT [PK_tbl_PlantillasCorreo] PRIMARY KEY CLUSTERED 
	(
		[IdPlantillaCorreo] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
GO


IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_VoBoSolicitudesArchivos')
	CREATE TABLE [dbo].tbl_VoBoSolicitudesArchivos(
		[idArchivo] [int] IDENTITY(1,1) NOT NULL,
		Id_voBoSol [int] NOT NULL,
		[PlantillaArchivo] [varbinary](max) NOT NULL,
		[Nombre] [varchar](70) NOT NULL,
	 CONSTRAINT [PK_PlantillaArchivo] PRIMARY KEY CLUSTERED 
	(
		[idArchivo] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO
GO

IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_PlantillasCorreo')

	CREATE TABLE [dbo].[tbl_VoBoSolicitudesRetro](
		[Id_voBoSolRetro] [int] IDENTITY(1,1) NOT NULL,
		[Id_voBoSol] [int] NOT NULL,	
		[correo] [varchar](50) NULL,
		[comentariosNegocio] [varchar](200) NOT NULL,
		[riesgosDestacados] [varchar](200) NOT NULL,
		[autorizado] bit null,
		[fec_autorizado] datetime null,
		fec_movto datetime not null,
		[id_user] [int] NOT NULL,
	 CONSTRAINT [PK_tbl_VoBoSolicitudesRetro] PRIMARY KEY CLUSTERED 
	(
		[Id_voBoSolRetro] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	GO
GO



IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.PlantillasVoBo_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.PlantillasVoBo_sUp
GO

-- =============================================
-- Author:		P. Castillo
-- Create date: 11/05/2022
-- Description:	Lista las plantillas disponibles para marcar el VoBo
-- =============================================
CREATE PROCEDURE dbo.PlantillasVoBo_sUp

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.Id_PlantillaJuridica, A.Nombre,A.Descripcion
	,ISNULL(B.Descripcion,'--') AS DescClas 
	,A.voBo
	FROM dbo.tbl_PlantillasJuridicas as A
	LEFT JOIN	[dbo].[tbl_ClasificacionPlantilla] B
	ON A.id_tipoplantilla = B.id_tipoPlantilla
	AND A.id_clasificacionplantilla = B.id_clasificacionplantilla
	WHERE A.activo=1

END
GO

GRANT EXECUTE ON OBJECT::dbo.PlantillasVoBo_sUp TO produccion;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.PlantillasVoBo_uUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.PlantillasVoBo_uUp
GO
-- =============================================
-- Author:		P. Castillo
-- Create date: 11/05/2022
-- Description:	Lista las plantillas disponibles para marcar el VoBo
-- =============================================
CREATE PROCEDURE dbo.PlantillasVoBo_uUp
	@Id_PlantillaJuridica int
	,@voBo bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.tbl_PlantillasJuridicas
	SET voBo= @voBo
	WHERE Id_PlantillaJuridica=@Id_PlantillaJuridica
    
END
GO

GRANT EXECUTE ON OBJECT::dbo.PlantillasVoBo_uUp TO produccion;
GO
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
      [S].[id_user] = 1  
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
  [S].[id_user] = 1  
 --AND  [S].[id_Status] IN (1, 2, 3)  
     --AND S.id_Solicitud NOT IN (SELECT idSolicitud FROM tbl_VoBoSolicitudes ) 
    ORDER BY   
    [S].[FechaCreacion] DESC 

END  
  
GRANT EXECUTE ON OBJECT::dbo.SelSolicitudUsuario TO produccion;
GO




IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesArchivos_iUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesArchivos_iUp
GO
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
 
GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesArchivos_iUp TO produccion;
GO 




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
	FROM tbl_VoBoSolicitudesRetro AS A
	JOIN tbl_VoBoSolicitudes AS B
		ON B.Id_voBoSol=A.Id_voBoSol 

	WHERE Id_voBoSolRetro=@Id_voBoSolRetro



END
GO
GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetroById_sUp TO produccion;
GO
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
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudesRetro_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudesRetro_sUp
GO
-- =============================================
-- Author:		Pedro Castillo
-- Create date: 13-05-2022,
-- Description: crea los registros para la retroalimentacion de las solicitudes,
-- =============================================
CREATE PROCEDURE dbo.tbl_VoBoSolicitudesRetro_sUp
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
GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudesRetro_sUp TO produccion;
GO
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
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.tbl_VoBoSolicitudes_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.tbl_VoBoSolicitudes_sUp
GO

-- =============================================
-- Author:		P. Castillo
-- Create date: 12-05-2022
-- Description:	Consulta la tabla
-- 
-- =============================================
CREATE PROCEDURE [dbo].[tbl_VoBoSolicitudes_sUp]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


	SELECT A.*
	,[S].[Folio] + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,[S].[FechaCreacion]))  folio
	
	FROM [dbo].[tbl_VoBoSolicitudes] A
	JOIN tbl_Solicitud  [S] 
	ON S.id_Solicitud=A.idSolicitud

	--WHERE [S].[Folio] + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,[S].[FechaCreacion]))  = @folio 

END
GO

GRANT EXECUTE ON OBJECT::dbo.tbl_VoBoSolicitudes_sUp TO produccion;
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.ValidaFolioSolicitud_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.ValidaFolioSolicitud_sUp
GO
-- =============================================
-- Author:		P. Castillo
-- Create date: 12-05-2022
-- Description:	Valida que el folio ingresado exista en la tabla [tbl_Solicitud]
-- =============================================
CREATE PROCEDURE dbo.ValidaFolioSolicitud_sUp --'C-0004/01/2000'
	-- Add the parameters for the stored procedure here
	@folio nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 *  FROM   
	[dbo].[tbl_Solicitud] [S]  
	WHERE [S].[Folio] + '/' + (CASE WHEN Consecutivo < 10 THEN '0'+CONVERT(char(1),Consecutivo) ELSE CONVERT(char(2),Consecutivo) END) + '/' + CONVERT(varchar(4),DATEPART(year,[S].[FechaCreacion]))  = @folio 

END
GO
GRANT EXECUTE ON OBJECT::dbo.ValidaFolioSolicitud_sUp TO produccion;
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('dbo.ValidaSolicitudVoboPlantilla_sUp') and sysstat & 0xf = 4)
DROP PROCEDURE dbo.ValidaSolicitudVoboPlantilla_sUp
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

GRANT EXECUTE ON OBJECT::dbo.ValidaSolicitudVoboPlantilla_sUp TO produccion;
GO
