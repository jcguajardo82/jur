ALTER TABLE tbl_PlantillasJuridicas
ADD voBo bit null
GO
update tbl_PlantillasJuridicas
set voBo = 0
GO