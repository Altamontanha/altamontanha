
-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Tela`
--

CREATE TABLE IF NOT EXISTS `TB_Tela` (
  `CodTela` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  PRIMARY KEY (`CodTela`),
  UNIQUE KEY `Nome` (`Nome`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=25 ;

--
-- Extraindo dados da tabela `TB_Tela`
--

INSERT INTO `TB_Tela` (`CodTela`, `Nome`) VALUES
(17, '/ManterArtigo/'),
(19, '/ManterArtigo/AlterarArtigo'),
(23, '/ManterArtigo/AlterarColuna'),
(18, '/ManterArtigo/CadastrarArtigo'),
(22, '/ManterArtigo/CadastrarColuna'),
(20, '/ManterArtigo/ExcluirArtigo'),
(24, '/ManterArtigo/ExcluirColuna'),
(9, '/ManterCategoria/'),
(11, '/ManterCategoria/AlterarCategoria'),
(10, '/ManterCategoria/CadastrarCategoria'),
(12, '/ManterCategoria/ExcluirCategoria'),
(21, '/ManterColuna/'),
(13, '/ManterLink/'),
(15, '/ManterLink/AlterarLink'),
(14, '/ManterLink/CadastrarLink'),
(16, '/ManterLink/ExcluirLink'),
(5, '/ManterPerfil/'),
(7, '/ManterPerfil/AlterarPerfil'),
(6, '/ManterPerfil/CadastrarPerfil'),
(1, '/ManterUsuario/'),
(3, '/ManterUsuario/AlterarUsuario'),
(2, '/ManterUsuario/CadastrarUsuario'),
(8, '/ManterUsuario/ExcluirPerfil'),
(4, '/ManterUsuario/ExcluirUsuario');
