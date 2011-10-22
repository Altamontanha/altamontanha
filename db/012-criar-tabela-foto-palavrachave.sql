CREATE  TABLE IF NOT EXISTS `TB_FotoPalavraChave` (
  `CodFoto` INT NOT NULL ,
  `CodPalavraChave` INT NOT NULL ,
  PRIMARY KEY (`CodFoto`, `CodPalavraChave`) ,
  INDEX `fk_TB_FotoPalavraChave_TB_PalavraChave1` (`CodPalavraChave` ASC) ,
  CONSTRAINT `fk_TB_FotoPalavraChave_TB_Foto1`
    FOREIGN KEY (`CodFoto` )
    REFERENCES `TB_Foto` (`CodFoto` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TB_FotoPalavraChave_TB_PalavraChave1`
    FOREIGN KEY (`CodPalavraChave` )
    REFERENCES `TB_PalavraChave` (`CodPalavraChave` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;