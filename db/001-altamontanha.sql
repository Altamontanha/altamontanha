-- phpMyAdmin SQL Dump
-- version 3.3.7deb5build0.10.10.1
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tempo de Geração: Abr 20, 2011 as 02:28 PM
-- Versão do Servidor: 5.1.49
-- Versão do PHP: 5.3.3-1ubuntu9.3

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Banco de Dados: `altamontanha`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Artigo`
--

CREATE TABLE IF NOT EXISTS `TB_Artigo` (
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo do artigo',
  `CodCategoria` int(11) NOT NULL COMMENT 'Código da categoria do artigo',
  PRIMARY KEY (`CodConteudo`),
  KEY `fk_TB_Artigo_TB_Categoria1` (`CodCategoria`),
  KEY `fk_TB_Artigo_TB_Conteudo1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_Artigo`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Aventura`
--

CREATE TABLE IF NOT EXISTS `TB_Aventura` (
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo',
  `CodUsuario` int(11) NOT NULL COMMENT 'Código do usuário autor da coluna',
  `CodConteudoPai` int(11) DEFAULT NULL COMMENT 'Código do contéudo pai',
  PRIMARY KEY (`CodConteudo`),
  KEY `fk_TB_Aventura_TB_Conteudo1` (`CodConteudo`),
  KEY `fk_TB_Aventura_TB_Usuario1` (`CodUsuario`),
  KEY `fk_TB_Aventura_TB_Aventura1` (`CodConteudoPai`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_Aventura`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Banner`
--

CREATE TABLE IF NOT EXISTS `TB_Banner` (
  `CodBanner` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do banner',
  `CodLocal` int(11) NOT NULL COMMENT 'Código do local',
  `Titulo` varchar(75) NOT NULL COMMENT 'Título do banner',
  `Multimidia` tinyint(1) NOT NULL COMMENT 'Indica se o banner é multimídia (SWF)',
  `Caminho` varchar(150) NOT NULL COMMENT 'Caminho (físico) do arquivo do banner',
  `Randomico` tinyint(1) NOT NULL COMMENT 'Indica se o banner é randômico (não para fixo)',
  `DataInicial` datetime NOT NULL COMMENT 'Data inicial que o banner será exibido',
  `DataFinal` datetime NOT NULL COMMENT 'Data limite para o banner ser exibido',
  `Ativo` tinyint(1) NOT NULL COMMENT 'Indica se o banner está ativo',
  PRIMARY KEY (`CodBanner`),
  KEY `fk_TB_Banner_TB_Local1` (`CodLocal`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Extraindo dados da tabela `TB_Banner`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Categoria`
--

CREATE TABLE IF NOT EXISTS `TB_Categoria` (
  `CodCategoria` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código da categoria',
  `Titulo` varchar(50) NOT NULL COMMENT 'Título da categoria',
  `Descricao` varchar(255) DEFAULT NULL COMMENT 'Descrição da categoria',
  PRIMARY KEY (`CodCategoria`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Extraindo dados da tabela `TB_Categoria`
--

INSERT INTO `TB_Categoria` (`CodCategoria`, `Titulo`, `Descricao`) VALUES
(1, 'teste 1', 'isso é um teste'),
(2, 'teste 2', 'isso é um teste 2');

-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Coluna`
--

CREATE TABLE IF NOT EXISTS `TB_Coluna` (
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo da coluna',
  `CodUsuario` int(11) NOT NULL COMMENT 'Código do usuário autor da coluna',
  PRIMARY KEY (`CodConteudo`),
  KEY `fk_TB_Coluna_TB_Conteudo1` (`CodConteudo`),
  KEY `fk_TB_Coluna_TB_Usuario1` (`CodUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_Coluna`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Comentario`
--

CREATE TABLE IF NOT EXISTS `TB_Comentario` (
  `CodComentario` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do comentário',
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo do comentário',
  `Nome` varchar(100) NOT NULL COMMENT 'Nome do autor do comentário',
  `Email` varchar(150) NOT NULL COMMENT 'E-mail do autor do comentário',
  `Texto` text NOT NULL COMMENT 'Texto do comentário',
  `Habilitado` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Indica se o comentário está habilitado para aparecer',
  `Data` datetime NOT NULL COMMENT 'Data de cadastro do comentário',
  PRIMARY KEY (`CodComentario`),
  KEY `fk_TB_Comentario_TB_Conteudo1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Extraindo dados da tabela `TB_Comentario`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Conteudo`
--

CREATE TABLE IF NOT EXISTS `TB_Conteudo` (
  `CodConteudo` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do conteúdo',
  `Titulo` varchar(150) NOT NULL COMMENT 'Título do conteúdo',
  `AnteTitulo` varchar(250) NOT NULL COMMENT 'Ante-título do conteúdo',
  `Resumo` text NOT NULL COMMENT 'Resumo do conteúdo',
  `Texto` text NOT NULL COMMENT 'Texto do conteúdo',
  `Data` datetime NOT NULL COMMENT 'Data do conteúdo',
  `CodUsuario` int(11) NOT NULL COMMENT 'Código do usuário que inseriu o conteúdo',
  `Tipo` varchar(15) NOT NULL,
  PRIMARY KEY (`CodConteudo`),
  KEY `fk_TB_Conteudo_TB_Usuario1` (`CodUsuario`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Extraindo dados da tabela `TB_Conteudo`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_ConteudoFoto`
--

CREATE TABLE IF NOT EXISTS `TB_ConteudoFoto` (
  `CodFoto` int(11) NOT NULL COMMENT 'Código da foto',
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo',
  PRIMARY KEY (`CodFoto`,`CodConteudo`),
  KEY `fk_TB_ConteudoFoto_TB_Conteudo1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_ConteudoFoto`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_ConteudoPalavraChave`
--

CREATE TABLE IF NOT EXISTS `TB_ConteudoPalavraChave` (
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo',
  `CodPalavraChave` int(11) NOT NULL COMMENT 'Código da palavra-chave',
  PRIMARY KEY (`CodConteudo`,`CodPalavraChave`),
  KEY `fk_TB_ConteudoPalavraChave_TB_PalavraChave1` (`CodPalavraChave`),
  KEY `fk_TB_ConteudoPalavraChave_TB_Conteudo1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_ConteudoPalavraChave`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Foto`
--

CREATE TABLE IF NOT EXISTS `TB_Foto` (
  `CodFoto` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código da foto',
  `Caminho` varchar(255) NOT NULL COMMENT 'Caminho (físico) do arquivo de foto',
  `Fonte` varchar(75) NOT NULL COMMENT 'Fonte da foto',
  `Autor` varchar(150) NOT NULL COMMENT 'Autor da foto',
  `Legenda` varchar(50) DEFAULT NULL COMMENT 'Legenda da foto',
  `Resolucao` varchar(50) DEFAULT NULL COMMENT 'Resolução da foto',
  `CodFotoPai` int(11) DEFAULT NULL COMMENT 'Código da foto pai',
  PRIMARY KEY (`CodFoto`),
  KEY `fk_TB_Foto_TB_Foto1` (`CodFotoPai`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Extraindo dados da tabela `TB_Foto`
--

INSERT INTO `TB_Foto` (`CodFoto`, `Caminho`, `Fonte`, `Autor`, `Legenda`, `Resolucao`, `CodFotoPai`) VALUES
(1, '/', 'teste', 'teste', 'teste', '60*80', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Link`
--

CREATE TABLE IF NOT EXISTS `TB_Link` (
  `CodLink` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do link',
  `Titulo` varchar(75) NOT NULL COMMENT 'Título do link',
  `Url` varchar(255) NOT NULL COMMENT 'URL do link',
  PRIMARY KEY (`CodLink`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Extraindo dados da tabela `TB_Link`
--

INSERT INTO `TB_Link` (`CodLink`, `Titulo`, `Url`) VALUES
(1, 'A Montanha', 'www.amontanha.com.br');

-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Local`
--

CREATE TABLE IF NOT EXISTS `TB_Local` (
  `CodLocal` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do local',
  `Descricao` varchar(50) NOT NULL COMMENT 'Descrição do local',
  `Altura` int(11) NOT NULL COMMENT 'Altura (em pixels) do local',
  `Largura` int(11) NOT NULL COMMENT 'Largura (em pixels) do local',
  `Local` varchar(50) NOT NULL COMMENT 'Local do banner',
  PRIMARY KEY (`CodLocal`),
  UNIQUE KEY `Local_UNIQUE` (`Local`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Extraindo dados da tabela `TB_Local`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Noticia`
--

CREATE TABLE IF NOT EXISTS `TB_Noticia` (
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo da notícia',
  `Fonte` varchar(75) NOT NULL COMMENT 'Fonte da notícia',
  `Destaque` tinyint(1) NOT NULL COMMENT 'Indica se a notícia é destaque',
  PRIMARY KEY (`CodConteudo`),
  KEY `fk_TB_Noticia_TB_Conteudo1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `TB_Noticia`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_PalavraChave`
--

CREATE TABLE IF NOT EXISTS `TB_PalavraChave` (
  `CodPalavraChave` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código da palavra-chave',
  `Nome` varchar(150) NOT NULL COMMENT 'Nome da palavra-chave',
  PRIMARY KEY (`CodPalavraChave`),
  UNIQUE KEY `Nome_UNIQUE` (`Nome`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Extraindo dados da tabela `TB_PalavraChave`
--


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Perfil`
--

CREATE TABLE IF NOT EXISTS `TB_Perfil` (
  `CodPerfil` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do Perfil',
  `Nome` varchar(45) NOT NULL COMMENT 'Nome do perfil',
  `Descricao` varchar(200) DEFAULT NULL COMMENT 'Descrição do perfil',
  `Noticia` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Notícias',
  `Link` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Links',
  `Foto` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Fotos',
  `Coluna` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Colunas',
  `Banner` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Banners',
  `Aventura` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Aventuras',
  `Artigo` tinyint(1) NOT NULL COMMENT 'Indica se o perfil tem acesso a gerenciar Artigos',
  PRIMARY KEY (`CodPerfil`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Extraindo dados da tabela `TB_Perfil`
--

INSERT INTO `TB_Perfil` (`CodPerfil`, `Nome`, `Descricao`, `Noticia`, `Link`, `Foto`, `Coluna`, `Banner`, `Aventura`, `Artigo`) VALUES
(1, 'Administradores', 'Grupo de Admin', 1, 1, 1, 1, 1, 1, 1);

-- --------------------------------------------------------


-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Rota`
--

CREATE TABLE IF NOT EXISTS `TB_Rota` (
  `CodRota` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código da rota',
  `CodConteudo` int(11) NOT NULL COMMENT 'Código do conteúdo (Sua-Aventura)',
  `Caminho` varchar(255) NOT NULL COMMENT 'Caminho (físico) do arquivo de rota',
  PRIMARY KEY (`CodRota`),
  KEY `fk_TB_Rota_TB_Aventura1` (`CodConteudo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Extraindo dados da tabela `TB_Rota`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `TB_Usuario`
--

CREATE TABLE IF NOT EXISTS `TB_Usuario` (
  `CodUsuario` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Código do usuário',
  `CodPerfil` int(11) NOT NULL COMMENT 'Código do perfil',
  `Login` varchar(30) DEFAULT NULL,
  `Nome` varchar(100) NOT NULL COMMENT 'Nome do usuário',
  `Email` varchar(150) NOT NULL COMMENT 'E-mail do usuário',
  `Senha` varchar(32) NOT NULL COMMENT 'Senha (criptografada) do usuário',
  `CodFoto` int(11) NOT NULL COMMENT 'Código da foto',
  PRIMARY KEY (`CodUsuario`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Login` (`Login`),
  KEY `FK_TB_Perfil_CodPerfil` (`CodPerfil`),
  KEY `fk_TB_Usuario_TB_Foto1` (`CodFoto`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Extraindo dados da tabela `TB_Usuario`
--

INSERT INTO `TB_Usuario` (`CodUsuario`, `CodPerfil`, `Login`, `Nome`, `Email`, `Senha`, `CodFoto`) VALUES
(7, 1, 'otaviano', 'Otaviano Montes Zibetti', 'otaviano@altamontanha.com', 'RYm49RbShVizhQw3d2ytyA==', 1),
(8, 1, 'leonardo', 'Leonardo Saraiva', 'vyper@maneh.org', '4QrcOUm6Wau+VuBX8g+IPg==', 1);

--
-- Restrições para as tabelas dumpadas
--

--
-- Restrições para a tabela `TB_Artigo`
--
ALTER TABLE `TB_Artigo`
  ADD CONSTRAINT `fk_TB_Artigo_TB_Categoria1` FOREIGN KEY (`CodCategoria`) REFERENCES `TB_Categoria` (`CodCategoria`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_Artigo_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Aventura`
--
ALTER TABLE `TB_Aventura`
  ADD CONSTRAINT `fk_TB_Aventura_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_Aventura_TB_Usuario1` FOREIGN KEY (`CodUsuario`) REFERENCES `TB_Usuario` (`CodUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_Aventura_TB_Aventura1` FOREIGN KEY (`CodConteudoPai`) REFERENCES `TB_Aventura` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Banner`
--
ALTER TABLE `TB_Banner`
  ADD CONSTRAINT `fk_TB_Banner_TB_Local1` FOREIGN KEY (`CodLocal`) REFERENCES `TB_Local` (`CodLocal`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Coluna`
--
ALTER TABLE `TB_Coluna`
  ADD CONSTRAINT `fk_TB_Coluna_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_Coluna_TB_Usuario1` FOREIGN KEY (`CodUsuario`) REFERENCES `TB_Usuario` (`CodUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Comentario`
--
ALTER TABLE `TB_Comentario`
  ADD CONSTRAINT `fk_TB_Comentario_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Conteudo`
--
ALTER TABLE `TB_Conteudo`
  ADD CONSTRAINT `fk_TB_Conteudo_TB_Usuario1` FOREIGN KEY (`CodUsuario`) REFERENCES `TB_Usuario` (`CodUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_ConteudoFoto`
--
ALTER TABLE `TB_ConteudoFoto`
  ADD CONSTRAINT `fk_TB_ConteudoFoto_TB_Foto1` FOREIGN KEY (`CodFoto`) REFERENCES `TB_Foto` (`CodFoto`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_ConteudoFoto_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_ConteudoPalavraChave`
--
ALTER TABLE `TB_ConteudoPalavraChave`
  ADD CONSTRAINT `fk_TB_ConteudoPalavraChave_TB_PalavraChave1` FOREIGN KEY (`CodPalavraChave`) REFERENCES `TB_PalavraChave` (`CodPalavraChave`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_TB_ConteudoPalavraChave_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Foto`
--
ALTER TABLE `TB_Foto`
  ADD CONSTRAINT `fk_TB_Foto_TB_Foto1` FOREIGN KEY (`CodFotoPai`) REFERENCES `TB_Foto` (`CodFoto`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Noticia`
--
ALTER TABLE `TB_Noticia`
  ADD CONSTRAINT `fk_TB_Noticia_TB_Conteudo1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Conteudo` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Permissao`
--
ALTER TABLE `TB_Permissao`
  ADD CONSTRAINT `TB_Permissao_ibfk_8` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`),
  ADD CONSTRAINT `TB_Permissao_ibfk_1` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`),
  ADD CONSTRAINT `TB_Permissao_ibfk_2` FOREIGN KEY (`CodTela`) REFERENCES `TB_Tela` (`CodTela`),
  ADD CONSTRAINT `TB_Permissao_ibfk_3` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`),
  ADD CONSTRAINT `TB_Permissao_ibfk_4` FOREIGN KEY (`CodTela`) REFERENCES `TB_Tela` (`CodTela`),
  ADD CONSTRAINT `TB_Permissao_ibfk_5` FOREIGN KEY (`CodTela`) REFERENCES `TB_Tela` (`CodTela`),
  ADD CONSTRAINT `TB_Permissao_ibfk_6` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`),
  ADD CONSTRAINT `TB_Permissao_ibfk_7` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`);

--
-- Restrições para a tabela `TB_Rota`
--
ALTER TABLE `TB_Rota`
  ADD CONSTRAINT `fk_TB_Rota_TB_Aventura1` FOREIGN KEY (`CodConteudo`) REFERENCES `TB_Aventura` (`CodConteudo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Restrições para a tabela `TB_Usuario`
--
ALTER TABLE `TB_Usuario`
  ADD CONSTRAINT `FK_TB_Perfil_CodPerfil` FOREIGN KEY (`CodPerfil`) REFERENCES `TB_Perfil` (`CodPerfil`),
  ADD CONSTRAINT `fk_TB_Usuario_TB_Foto1` FOREIGN KEY (`CodFoto`) REFERENCES `TB_Foto` (`CodFoto`) ON DELETE NO ACTION ON UPDATE NO ACTION;
