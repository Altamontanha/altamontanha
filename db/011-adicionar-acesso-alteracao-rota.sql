INSERT INTO `altamontanha`.`TB_Tela` (`Nome`)
VALUES 
	('/ManterAventura/'), 
	('/ManterAventura/CadastrarAventura'),
	('/ManterAventura/AlterarAventura'),
	('/ManterAventura/ExcluirAventura'),
	('/ManterFoto/'), 
	('/ManterFoto/CadastrarFoto'),
	('/ManterFoto/AlterarFoto'),
	('/ManterFoto/ExcluirFoto'),
	('/ManterFoto/VincularFoto');
	
	INSERT INTO `altamontanha`.`TB_Permissao` 
	(
		`CodTela` ,
		`CodPerfil`
	)
	VALUES 
		('25', '1'),
		('25', '1'),
		('26', '1'),
		('27', '1'),
		('29', '1'),
		('30', '1');




ALTER TABLE `TB_Rota` DROP FOREIGN KEY `fk_TB_Rota_TB_Aventura1` ;
ALTER TABLE TB_Rota DROP INDEX fk_TB_Rota_TB_Aventura1;
ALTER TABLE `TB_Rota` DROP `CodConteudo;
ALTER TABLE `TB_Aventura` CHANGE `CodRota` `CodRota` INT( 11 ) NULL 
ALTER TABLE `TB_Aventura` ADD FOREIGN KEY ( `CodRota` ) 
	REFERENCES `altamontanha`.`TB_Rota` (`CodRota`) ON UPDATE CASCADE ;
