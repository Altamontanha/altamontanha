ALTER TABLE `TB_Foto` 
	DROP FOREIGN KEY `fk_TB_Foto_TB_Foto1` ;

ALTER TABLE TB_Foto 
	DROP INDEX fk_TB_Foto_TB_Foto1


ALTER TABLE `TB_Foto`
  DROP `Resolucao`,
  DROP `CodFotoPai`;

ALTER TABLE `TB_Foto` 
	ADD `Galeria` BIT( 1 ) NOT NULL AFTER `Legenda` 
