IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'ArchivoSolicitudTemp')


	CREATE TABLE [dbo].[ArchivoSolicitudTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[idArchivo] [int] IDENTITY(1,1) NOT NULL,
		[idTipoDocumento] [int] NOT NULL,
		[Nombre] [varchar](70) NOT NULL,
		[Ruta] [nvarchar](100) NULL,
		[Archivo] [varbinary](max) NULL,
	 CONSTRAINT [PK_Archivo_1] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC,
		[idArchivo] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO


GO