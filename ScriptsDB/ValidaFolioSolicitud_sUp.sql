-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
