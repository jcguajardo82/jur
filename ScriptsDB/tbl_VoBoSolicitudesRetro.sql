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

