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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		#endregion

		#region Usuario

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
				throw new Exception(e.Message);
			}
		}

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
				throw new Exception(e.Message);
			}
		}

		public Dominio.Usuario SalvarUsuario(Dominio.Usuario usuario)
		{
			try
			{
				if(usuario == null)
					throw new ArgumentNullException("usuario");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				usuario.Senha = Utilitario.Seguranca.Criptografar(usuario.Senha);

				if (usuario.Codigo <= 0)
					return usuarioDAO.Cadastrar(usuario);

				usuarioDAO.Alterar(usuario);
				return usuario;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool ExcluirUsuario(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
							
				return usuarioDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

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

		public bool VerificarAcesso(Dominio.Usuario usuario, string nomeFormulario)
		{
			IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
			IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
			IList<Usuario> listaUsuarios = usuarioDAO.Pesquisar(usuario);

			//TODO : Criar Mapeamento do objeto permissão.

			//if (listaUsuarios.Count != 1)
			//	return false;
			//Permissao permissao = listaUsuarios[0].Perfil.ListaPermissoes.FirstOrDefault(obj=> obj.Nome == nomeFormulario);
			//if (permissao == null)
			//    return false;
			
			return true;
		}

		#endregion
	}
}