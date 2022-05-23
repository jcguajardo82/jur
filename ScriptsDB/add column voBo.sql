IF NOT EXISTS(
  SELECT TOP 1 1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE 
    [TABLE_NAME] = 'tbl_PlantillasJuridicas'
    AND [COLUMN_NAME] = 'voBo')
BEGIN

	ALTER TABLE tbl_PlantillasJuridicas
	ADD voBo bit null
	
	update tbl_PlantillasJuridicas
	set voBo = 0

END
GO