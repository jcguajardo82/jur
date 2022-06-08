IF NOT EXISTS (SELECT 0 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'tbl_SolicitudesTemp')


	CREATE TABLE [dbo].[tbl_SolicitudesTemp](
		[idUsuario] [int] NOT NULL,
		[idTipoSolicitud] [int] NOT NULL,
		[idPlantilla] [int] NOT NULL,
		[fechaCreacion] [datetime] NULL,
	 CONSTRAINT [PK_tbl_SolicitudesTemp] PRIMARY KEY CLUSTERED 
	(
		[idUsuario] ASC,
		[idTipoSolicitud] ASC,
		[idPlantilla] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	GO

	ALTER TABLE [dbo].[tbl_SolicitudesTemp] ADD  CONSTRAINT [DF_tbl_SolicitudesTemp_fechaCreacion]  DEFAULT (getdate()) FOR [fechaCreacion]
	GO


GO




GO