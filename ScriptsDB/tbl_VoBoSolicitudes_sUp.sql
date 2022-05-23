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
