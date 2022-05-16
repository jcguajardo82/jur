
DECLARE @idMenu int =0
		,@descripcion nvarchar(40) = 'Consulta VoBo Solicitudes'
		,@padre int =3
		,@posicion int =0
		,@habilitada bit =1 
		,@url nvarchar(50) ='../Solicitudes/ConsultaSolicitudVoBo.aspx'

select @idMenu= max(idMenu)+1  from tbl_Menu

select @posicion = MAX(posicion)+1 from tbl_Menu where PadreId=3



INSERT INTO [dbo].[tbl_Menu]
           ([Descripcion]
           ,[PadreId]
           ,[Posicion]
           ,[Habilitada]
           ,[Url]
           ,[P1]
           ,[P2]
           ,[P3]
           ,[P4]
           ,[P5]
           ,[P6]
           ,[P7])
     VALUES
           (@descripcion
           ,@padre
           ,@posicion
           ,@habilitada
           ,@url
           ,1
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0)
GO
