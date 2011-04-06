SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `altamontanha` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
USE `altamontanha` ;

-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Perfil`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Perfil` (
  `CodPerfil` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do Perfil' ,
  `Nome` VARCHAR(45) NOT NULL COMMENT 'Nome do perfil' ,
  `Descricao` VARCHAR(200) NULL COMMENT 'Descrição do perfil' ,
  `Noticia` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Notícias' ,
  `Link` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Links' ,
  `Foto` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Fotos' ,
  `Coluna` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Colunas' ,
  `Banner` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Banners' ,
  `Aventura` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Aventuras' ,
  `Artigo` TINYINT(1)  NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Artigos' ,
  PRIMARY KEY (`CodPerfil`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Foto`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Foto` (
  `CodFoto` INT NOT NULL AUTO_INCREMENT COMMENT 'Código da foto' ,
  `Caminho` VARCHAR(255) NOT NULL COMMENT 'Caminho (físico) do arquivo de foto' ,
  `Fonte` VARCHAR(75) NOT NULL COMMENT 'Fonte da foto' ,
  `Autor` VARCHAR(150) NOT NULL COMMENT 'Autor da foto' ,
  `Legenda` VARCHAR(50) NULL COMMENT 'Legenda da foto' ,
  `Resolucao` VARCHAR(50) NULL COMMENT 'Resolução da foto' ,
  `CodFotoPai` INT NULL COMMENT 'Código da foto pai' ,
  PRIMARY KEY (`CodFoto`) ,
  INDEX `fk_TB_Foto_TB_Foto1` (`CodFotoPai` ASC) ,
  CONSTRAINT `fk_TB_Foto_TB_Foto1`
    FOREIGN KEY (`CodFotoPai` )
    REFERENCES `altamontanha`.`TB_Foto` (`CodFoto` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Usuario`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Usuario` (
  `CodUsuario` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do usuário' ,
  `CodPerfil` INT NOT NULL COMMENT 'Código do perfil' ,
  `Nome` VARCHAR(100) NOT NULL COMMENT 'Nome do usuário' ,
  `Email` VARCHAR(150) NOT NULL COMMENT 'E-mail do usuário' ,
  `Senha` VARCHAR(32) NOT NULL COMMENT 'Senha (criptografada) do usuário' ,
  `CodFoto` INT NOT NULL COMMENT 'Código da foto' ,
  PRIMARY KEY (`CodUsuario`) ,
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC) ,
  INDEX `FK_TB_Perfil_CodPerfil` (`CodPerfil` ASC) ,
  INDEX `fk_TB_Usuario_TB_Foto1` (`CodFoto` ASC) ,
  CONSTRAINT `FK_TB_Perfil_CodPerfil`
    FOREIGN KEY (`CodPerfil` )
    REFERENCES `altamontanha`.`TB_Perfil` (`CodPerfil` )
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_TB_Usuario_TB_Foto1`
    FOREIGN KEY (`CodFoto` )
    REFERENCES `altamontanha`.`TB_Foto` (`CodFoto` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Conteudo`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Conteudo` (
  `CodConteudo` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do conteúdo' ,
  `Titulo` VARCHAR(150) NOT NULL COMMENT 'Título do conteúdo' ,
  `AnteTitulo` VARCHAR(250) NOT NULL COMMENT 'Ante-título do conteúdo' ,
  `Resumo` TEXT NOT NULL COMMENT 'Resumo do conteúdo' ,
  `Texto` TEXT NOT NULL COMMENT 'Texto do conteúdo' ,
  `Data` DATETIME NOT NULL COMMENT 'Data do conteúdo' ,
  `CodUsuario` INT NOT NULL COMMENT 'Código do usuário que inseriu o conteúdo' ,
  PRIMARY KEY (`CodConteudo`) ,
  INDEX `fk_TB_Conteudo_TB_Usuario1` (`CodUsuario` ASC) ,
  CONSTRAINT `fk_TB_Conteudo_TB_Usuario1`
    FOREIGN KEY (`CodUsuario` )
    REFERENCES `altamontanha`.`TB_Usuario` (`CodUsuario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Categoria`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Categoria` (
  `CodCategoria` INT NOT NULL AUTO_INCREMENT COMMENT 'Código da categoria' ,
  `Titulo` VARCHAR(50) NOT NULL COMMENT 'Título da categoria' ,
  `Descricao` VARCHAR(255) NULL COMMENT 'Descrição da categoria' ,
  PRIMARY KEY (`CodCategoria`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Artigo`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Artigo` (
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo do artigo' ,
  `CodCategoria` INT NOT NULL COMMENT 'Código da categoria do artigo' ,
  PRIMARY KEY (`CodConteudo`) ,
  INDEX `fk_TB_Artigo_TB_Categoria1` (`CodCategoria` ASC) ,
  INDEX `fk_TB_Artigo_TB_Conteudo1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_Artigo_TB_Categoria1`
    FOREIGN KEY (`CodCategoria` )
    REFERENCES `altamontanha`.`TB_Categoria` (`CodCategoria` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_Artigo_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Noticia`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Noticia` (
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo da notícia' ,
  `Fonte` VARCHAR(75) NOT NULL COMMENT 'Fonte da notícia' ,
  `Destaque` TINYINT(1)  NOT NULL COMMENT 'Indica se a notícia é destaque' ,
  PRIMARY KEY (`CodConteudo`) ,
  INDEX `fk_TB_Noticia_TB_Conteudo1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_Noticia_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Coluna`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Coluna` (
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo da coluna' ,
  `CodUsuario` INT NOT NULL COMMENT 'Código do usuário autor da coluna' ,
  PRIMARY KEY (`CodConteudo`) ,
  INDEX `fk_TB_Coluna_TB_Conteudo1` (`CodConteudo` ASC) ,
  INDEX `fk_TB_Coluna_TB_Usuario1` (`CodUsuario` ASC) ,
  CONSTRAINT `fk_TB_Coluna_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_Coluna_TB_Usuario1`
    FOREIGN KEY (`CodUsuario` )
    REFERENCES `altamontanha`.`TB_Usuario` (`CodUsuario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Aventura`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Aventura` (
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo' ,
  `CodUsuario` INT NOT NULL COMMENT 'Código do usuário autor da coluna' ,
  `CodConteudoPai` INT NULL COMMENT 'Código do contéudo pai' ,
  PRIMARY KEY (`CodConteudo`) ,
  INDEX `fk_TB_Aventura_TB_Conteudo1` (`CodConteudo` ASC) ,
  INDEX `fk_TB_Aventura_TB_Usuario1` (`CodUsuario` ASC) ,
  INDEX `fk_TB_Aventura_TB_Aventura1` (`CodConteudoPai` ASC) ,
  CONSTRAINT `fk_TB_Aventura_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_Aventura_TB_Usuario1`
    FOREIGN KEY (`CodUsuario` )
    REFERENCES `altamontanha`.`TB_Usuario` (`CodUsuario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_Aventura_TB_Aventura1`
    FOREIGN KEY (`CodConteudoPai` )
    REFERENCES `altamontanha`.`TB_Aventura` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Comentario`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Comentario` (
  `CodComentario` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do comentário' ,
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo do comentário' ,
  `Nome` VARCHAR(100) NOT NULL COMMENT 'Nome do autor do comentário' ,
  `Email` VARCHAR(150) NOT NULL COMMENT 'E-mail do autor do comentário' ,
  `Texto` TEXT NOT NULL COMMENT 'Texto do comentário' ,
  `Habilitado` TINYINT(1)  NOT NULL DEFAULT false COMMENT 'Indica se o comentário está habilitado para aparecer' ,
  `Data` DATETIME NOT NULL DEFAULT now() COMMENT 'Data de cadastro do comentário' ,
  PRIMARY KEY (`CodComentario`) ,
  INDEX `fk_TB_Comentario_TB_Conteudo1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_Comentario_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_PalavraChave`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_PalavraChave` (
  `CodPalavraChave` INT NOT NULL AUTO_INCREMENT COMMENT 'Código da palavra-chave' ,
  `Nome` VARCHAR(150) NOT NULL COMMENT 'Nome da palavra-chave' ,
  PRIMARY KEY (`CodPalavraChave`) ,
  UNIQUE INDEX `Nome_UNIQUE` (`Nome` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_ConteudoPalavraChave`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_ConteudoPalavraChave` (
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo' ,
  `CodPalavraChave` INT NOT NULL COMMENT 'Código da palavra-chave' ,
  PRIMARY KEY (`CodConteudo`, `CodPalavraChave`) ,
  INDEX `fk_TB_ConteudoPalavraChave_TB_PalavraChave1` (`CodPalavraChave` ASC) ,
  INDEX `fk_TB_ConteudoPalavraChave_TB_Conteudo1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_ConteudoPalavraChave_TB_PalavraChave1`
    FOREIGN KEY (`CodPalavraChave` )
    REFERENCES `altamontanha`.`TB_PalavraChave` (`CodPalavraChave` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_ConteudoPalavraChave_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Link`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Link` (
  `CodLink` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do link' ,
  `Titulo` VARCHAR(75) NOT NULL COMMENT 'Título do link' ,
  `Url` VARCHAR(255) NOT NULL COMMENT 'URL do link' ,
  PRIMARY KEY (`CodLink`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Local`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Local` (
  `CodLocal` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do local' ,
  `Descricao` VARCHAR(50) NOT NULL COMMENT 'Descrição do local' ,
  `Altura` INT NOT NULL COMMENT 'Altura (em pixels) do local' ,
  `Largura` INT NOT NULL COMMENT 'Largura (em pixels) do local' ,
  `Local` VARCHAR(50) NOT NULL COMMENT 'Local do banner' ,
  PRIMARY KEY (`CodLocal`) ,
  UNIQUE INDEX `Local_UNIQUE` (`Local` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Banner`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Banner` (
  `CodBanner` INT NOT NULL AUTO_INCREMENT COMMENT 'Código do banner' ,
  `CodLocal` INT NOT NULL COMMENT 'Código do local' ,
  `Titulo` VARCHAR(75) NOT NULL COMMENT 'Título do banner' ,
  `Multimidia` TINYINT(1)  NOT NULL COMMENT 'Indica se o banner é multimídia (SWF)' ,
  `Caminho` VARCHAR(150) NOT NULL COMMENT 'Caminho (físico) do arquivo do banner' ,
  `Randomico` TINYINT(1)  NOT NULL COMMENT 'Indica se o banner é randômico (não para fixo)' ,
  `DataInicial` DATETIME NOT NULL COMMENT 'Data inicial que o banner será exibido' ,
  `DataFinal` DATETIME NOT NULL COMMENT 'Data limite para o banner ser exibido' ,
  `Ativo` TINYINT(1)  NOT NULL COMMENT 'Indica se o banner está ativo' ,
  PRIMARY KEY (`CodBanner`) ,
  INDEX `fk_TB_Banner_TB_Local1` (`CodLocal` ASC) ,
  CONSTRAINT `fk_TB_Banner_TB_Local1`
    FOREIGN KEY (`CodLocal` )
    REFERENCES `altamontanha`.`TB_Local` (`CodLocal` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_Rota`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_Rota` (
  `CodRota` INT NOT NULL AUTO_INCREMENT COMMENT 'Código da rota' ,
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo (Sua-Aventura)' ,
  `Caminho` VARCHAR(255) NOT NULL COMMENT 'Caminho (físico) do arquivo de rota' ,
  PRIMARY KEY (`CodRota`) ,
  INDEX `fk_TB_Rota_TB_Aventura1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_Rota_TB_Aventura1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Aventura` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `altamontanha`.`TB_ConteudoFoto`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `altamontanha`.`TB_ConteudoFoto` (
  `CodFoto` INT NOT NULL COMMENT 'Código da foto' ,
  `CodConteudo` INT NOT NULL COMMENT 'Código do conteúdo' ,
  PRIMARY KEY (`CodFoto`, `CodConteudo`) ,
  INDEX `fk_TB_ConteudoFoto_TB_Conteudo1` (`CodConteudo` ASC) ,
  CONSTRAINT `fk_TB_ConteudoFoto_TB_Foto1`
    FOREIGN KEY (`CodFoto` )
    REFERENCES `altamontanha`.`TB_Foto` (`CodFoto` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_ConteudoFoto_TB_Conteudo1`
    FOREIGN KEY (`CodConteudo` )
    REFERENCES `altamontanha`.`TB_Conteudo` (`CodConteudo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MOD
