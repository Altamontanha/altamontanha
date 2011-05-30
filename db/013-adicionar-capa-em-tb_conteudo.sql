alter table `TB_Conteudo` add column `CodFotoCapa` integer not null;

ALTER TABLE `TB_Conteudo` ADD FOREIGN KEY ( `CodFotoCapa` ) 
	REFERENCES `TB_Foto` (`CodFoto`) ON UPDATE CASCADE ;
