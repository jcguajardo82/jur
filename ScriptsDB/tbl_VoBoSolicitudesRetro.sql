USE [AdminJurProdDB]
GO

/****** Object:  Table [dbo].[tbl_VoBoSolicitudes]    Script Date: 16/05/2022 11:41:22 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


