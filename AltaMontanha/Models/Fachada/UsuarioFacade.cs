using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Fachada
{
	public class UsuarioFacade
	{
		MultimidiaFacade MultimidiaFacade = new Fachada.MultimidiaFacade();

		#region Perfil

		public Dominio.Perfil PesquisarPerfil(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPerfilDAO perfilDAO = fabrica.GetPerfilDAO();

				return perfilDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<Dominio.Perfil> PesquisarPerfil(Dominio.Perfil perfil)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPerfilDAO perfilDAO = fabrica.GetPerfilDAO();

				return perfilDAO.Pesquisar(perfil);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Perfil SalvarPerfil(Dominio.Perfil perfil)
		{
			try
			{
				if (perfil == null)
					throw new ArgumentNullException("usuario");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPerfilDAO perfilDAO = fabrica.GetPerfilDAO();

				if (perfil.Codigo <= 0)
					return perfilDAO.Cadastrar(perfil);

				perfilDAO.Alterar(perfil);
				return perfil;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirPerfil(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPerfilDAO perfilDAO = fabrica.GetPerfilDAO();

				return perfilDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion

		#region Usuario

		/// <summary>
		/// consulta de um usuário especifico
		/// </summary>
		/// <param name="codigo">código do usuário</param>
		/// <returns>usuário referente ao código passado</returns>
		public Dominio.Usuario PesquisarUsuario(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				return usuarioDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Consulta de usuarios cadastrados
		/// </summary>
		/// <param name="usuario">objeto com parametros de pesquisa</param>
		/// <returns>lista de usuarios</returns>
		public IList<Dominio.Usuario> PesquisarUsuario(Dominio.Usuario usuario)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				return usuarioDAO.Pesquisar(usuario);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Cadastra ou altera um usuário.
		/// </summary>
		/// <param name="usuario">objeto com as informações de um usuário</param>
		/// <param name="arquivo">arquivo da foto do usuário.</param>
		/// <returns>usuário com o código gerado</returns>
		public Dominio.Usuario SalvarUsuario(Dominio.Usuario usuario, HttpPostedFileBase file)
		{
			try
			{
				if(usuario == null)
					throw new ArgumentNullException("usuario");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				usuario.Senha = Utilitario.Seguranca.Criptografar(usuario.Senha);

				if (usuario.Foto == null)
					usuario.Foto = new Foto() { Autor = usuario.Nome, Fonte = usuario.Email, Legenda = usuario.Login };
				else
					usuario.Foto = MultimidiaFacade.PesquisarFoto(usuario.Foto.Codigo);

				if (file != null)
					usuario.Foto = MultimidiaFacade.SalvarFoto(usuario.Foto, file);

				if (usuario.Codigo <= 0)
					return usuarioDAO.Cadastrar(usuario);

				usuarioDAO.Alterar(usuario);
				
				return usuario;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Exclui um usuário do banco
		/// </summary>
		/// <param name="codigo">código do usuário</param>
		/// <returns></returns>
		public bool ExcluirUsuario(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
				Foto foto	= usuarioDAO.Pesquisar(codigo).Foto;
				bool retorno = usuarioDAO.Excluir(codigo);

				MultimidiaFacade.ExcluirFoto(foto.Codigo);

				return retorno;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// autentica um usuário no sistema.
		/// </summary>
		/// <param name="usuario">objeto com login e senha do usuário</param>
		/// <returns>autenticação</returns>
		public bool AutenticarUsuario(Dominio.Usuario usuario)
		{
			if (usuario == null)
				return false;
			else if (usuario.Login == null)
				return false;
			else if (usuario.Senha == null)
				return false;

			usuario.Senha = Utilitario.Seguranca.Criptografar(usuario.Senha);

			IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
			IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
			IList<Usuario> listaUsuarios = usuarioDAO.Pesquisar(usuario);

			if (listaUsuarios.Count != 1)
				return false;

			return true;
		}
		/// <summary>
		/// Verifica o nivel de acesso de um usuário.
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="nomeFormulario"></param>
		/// <returns></returns>
		public bool VerificarAcesso(Dominio.Usuario usuario, string nomeFormulario)
		{
			IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
			IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
			IList<Usuario> listaUsuarios = usuarioDAO.Pesquisar(usuario);
						
			if (listaUsuarios.Count != 1)
				return false;
			Permissao permissao = listaUsuarios[0].Perfil.ListaPermissoes.FirstOrDefault(obj=> obj.Tela.Nome == nomeFormulario);
			if (permissao == null)
			    return false;
			
			return true;
		}

		#endregion
	}
}