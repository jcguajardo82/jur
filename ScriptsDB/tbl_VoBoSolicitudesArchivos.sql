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