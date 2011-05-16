DROP TABLE IF EXISTS `TB_Permissao`;

CREATE TABLE IF NOT EXISTS `TB_Permissao` 
(
  `CodPermissao` int(11) NOT NULL AUTO_INCREMENT,
  `CodTela` int(11) NOT NULL,
  `CodPerfil` int(11) NOT NULL,
  PRIMARY KEY (`CodPermissao`)
) 
ENGINE=InnoDB  
DEFAULT CHARSET=latin1 AUTO_INCREMENT=25 ;

INSERT INTO `TB_Permissao` (`CodPermissao`, `CodTela`, `CodPerfil`) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(6, 6, 1),
(7, 7, 1),
(8, 8, 1),
(9, 9, 1),
(10, 10, 1),
(11, 11, 1),
(12, 12, 1),
(13, 13, 1),
(14, 14, 1),
(15, 15, 1),
(16, 16, 1),
(17, 17, 1),
(18, 18, 1),
(19, 19, 1),
(20, 20, 1),
(21, 21, 1),
(22, 22, 1),
(23, 23, 1),
(24, 24, 1);
