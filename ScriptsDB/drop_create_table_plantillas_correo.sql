USE [AdminJurProdDB]
GO
/****** Object:  Table [dbo].[tbl_PlantillasCorreo]    Script Date: 09/06/2022 05:37:08 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_PlantillasCorreo]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_PlantillasCorreo]
GO
/****** Object:  Table [dbo].[tbl_PlantillasCorreo]    Script Date: 09/06/2022 05:37:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (1, N'Encabezado', N'<table cellpadding="0" cellspacing="0" role="presentation" width="100%">
    <tr>
        <td style="padding: 0 20px; text-align: center;">
            <table cellpadding="0" cellspacing="0" role="presentation" width="100%">
                <tr>
                    <td class="col" width="640" style="padding: 0 10px; text-align: center;">

                        <a href="$url(''Home-Show'')$">
                            <img alt="e-mail-logo" src="https://www.soriana.com/on/demandware.static/-/Library-Sites-SorianaSharedLib/default/dwed74dff8/images/emails/emailLogo.png"
                                style="width: 100%; max-width: 474px;" width="400">
                        </a>

                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>



<table cellpadding="0" cellspacing="0" role="presentation" width="100%">
    <tr>
        <td class="spacer py-sm-20" height="40"></td>
    </tr>
</table>

<table cellpadding="0" cellspacing="0" role="presentation" width="100%" style="width: 700px;" align="center">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" role="presentation" width="100%"
                style="max-width: 700px; margin: 0 auto;">
                <tr>
                    <td class="col" width="100%" style="padding: 0 10px;">

                       
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
', N'Encabezado')
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (2, N'Footer', N'<!-- spacer -->
<table cellpadding="0" cellspacing="0" role="presentation" width="100%">
    <tr>
        <td class="spacer py-sm-20" height="20"></td>
    </tr>
</table>

<table cellpadding="0" cellspacing="0" role="presentation" width="100%" align="center"
    style="width: 700px; padding: 0 10px;">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" role="presentation" width="100%" bgcolor="#B22A21"
                style="font-size: 10px; max-width: 700px; margin: 0 auto;">
                <tr>
                    <td class="col" width="310" style="padding:20px 10px; vertical-align: middle;">
                        <a href="$url(''Page-Show'', ''cid'', ''static-page-terms-and-conditions'')$"
                            style="text-decoration: none; color: #FFFEFE; font-size: 15px; text-align: left;">
                            T&eacute;rminos y Condiciones
                        </a>
                        <span style="display: inline-block; color: #FFFEFE; font-size: 15px;"> | </span>
                        <a href="$url(''Page-Show'', ''cid'', ''static-page-privacy-policy'')$"
                            style="text-decoration: none; color: #FFFEFE; font-size: 15px;">
                            &nbsp;Aviso de Privacidad
                        </a>
                    </td>
                    <td class="col" width="310" style="padding:15px 10px; text-align: right; vertical-align: middle;"
                        bgcolor="#B22A21">
                        <a href="$url(''Page-Show'', ''cid'', ''recompensassoriana'')$"
                            style="text-decoration: none; color: #FFFEFE; font-size: 15px;">
                            Pol&iacute;ticas Programa Recompensas
                        </a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
', NULL)
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (3, N'Entrevista Inicial Proceso de Contrato CSOR @folio', N'@header

<div style="min-width: 600px; width: 72.5%; margin: 0 auto 16px; min-height: 400px; color: #666666;">
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Ha seleccionado una plantilla de Contrato que pudiera tener impactos significativos en diversas direcciones y departamentos de la empresa.  Por ende, para continuar con el presente proceso, será necesario que te comuniques con la Jefatura de Jurídico Corporativo para que agende una entrevista en la que explicarás al abogado asignado el negocio que traes en puerta y se determinarán las áreas de afectación que deberán participar en el proceso.
        </p>
        <p>
            Atentamente.<br />
            Subdirección de Jurídico Corporativo
        </p>
</div>

@footer', NULL)
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (4, N'Solicitud de revisión de Requerimiento - Proceso de Contrato CSOR  @folio', N'@header

<div style="min-width: 600px; width: 72.5%; margin: 0 auto 16px; min-height: 400px; color: #666666;">
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Se solicita su participación en el presente proceso de Contrato, para que desde su área de especialidad analice el negocio que se plantea a continuación y proporcione sus observaciones, razonamientos o requerimientos de modificación al negocio, y otorgue el visto bueno o rechazo a la solicitud según corresponda:
            <br />
            Solicitud de Contrato CSOR @folio:
        </p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            @detalle
        </p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Atentamente.<br />
            Subdirección de Jurídico Corporativo
        </p>
    </div>

@footer', NULL)
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (5, N'Notificación de Autorización de solicitud- Proceso de Contrato CSOR @folio', N'@header

<div style="min-width: 600px; width: 72.5%; margin: 0 auto 16px; min-height: 400px; color: #666666;">
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Se le informa que su Solicitud de Contrato CSOR C-0004/01/2018 ha sido autorizada por el área de afectación @area, y se han emitido los siguientes comentarios:
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Detalle y descripción del negocio a revisar :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle1
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Comentarios al negocio :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle2
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Riesgos detectados :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle3
        </p>
		
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Atentamente.<br />
            Subdirección de Jurídico Corporativo
        </p>
</div>

@footer', NULL)
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (6, N'Notificación de Rechazo de solicitud- Proceso de Contrato CSOR @folio', N'@header

<div style="min-width: 600px; width: 72.5%; margin: 0 auto 16px; min-height: 400px; color: #666666;">
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Se le informa que su Solicitud de Contrato CSOR C-0004/01/2018 ha sido rechazada por el área de afectación @area, y se han emitido los siguientes comentarios:
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Detalle y descripción del negocio a revisar :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle1
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Comentarios al negocio :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle2
        </p>
		<p style="font-size: 25px; letter-spacing: 0px;text-align:justify"><b>Riesgos detectados :</b></p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
			@detalle3
        </p>
		
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Atentamente.<br />
            Subdirección de Jurídico Corporativo
        </p>
</div>

@footer', NULL)
GO
INSERT [dbo].[tbl_PlantillasCorreo] ([IdPlantillaCorreo], [Subject], [Body], [Descripcion]) VALUES (7, N'Notificación de Autorización total de la solicitud- Proceso de Contrato CSOR @folio', N'@header

<div style="min-width: 600px; width: 72.5%; margin: 0 auto 16px; min-height: 400px; color: #666666;">
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Se le informa que su Solicitud de Contrato CSOR @folio ha sido autorizada por todas las áreas de afectación asignadas para el presente proceso.
            Favor de informar a tu subdirector o director, para que acceda al Sistema y autorice la solicitud conforme a lo siguiente:
        </p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">

            <ol type="1">

                <li>Accede con su número de empleado y la contraseña es el Nip que usa para MiPagina.</li>
                <li>Selecciona “Autorizar Solicitud”.</li>
                <li>Identifica el folio que le proporcionaste.</li>
                <li>Elige “Revisar”.</li>
                <li>Revisa el contenido de tu solicitud y si está de acuerdo selecciona “Visto Bueno”.</li>
            </ol>
        </p>
        <p style="font-size: 25px; letter-spacing: 0px;text-align:justify">
            Atentamente.<br />
            Subdirección de Jurídico Corporativo
        </p>
    </div>

@footer', NULL)
GO
