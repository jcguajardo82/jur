IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_EtiquetasTemp')

	CREATE TABLE [dbo].[tbl_EtiquetasTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[id_etiquetas] [int] NOT NULL,
		[id_PlantillaJuridica] [int] NOT NULL,
		[respuesta] [nvarchar](max) NULL,
		[fechaCreacion] [datetime] NULL,
	 CONSTRAINT [PK_tbl_EtiquetasTemp] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC,
		[id_etiquetas] ASC,
		[id_PlantillaJuridica] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




GO