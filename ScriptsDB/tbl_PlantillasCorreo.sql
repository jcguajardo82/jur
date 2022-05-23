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





