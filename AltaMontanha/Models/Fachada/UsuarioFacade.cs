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

		public List<Dominio.Perfil> PesquisarPerfil(Dominio.Perfil perfil)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public Dominio.Perfil SalvarPerfil(Dominio.Perfil perfil)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public bool ExcluirPerfil(int codigo)
		{
			// TODO : Implementar
			throw new NotImplementedException();
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		public Dominio.Usuario SalvarUsuario(Dominio.Usuario usuario)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				usuario.Senha = Utilitario.Seguranca.Criptografar(usuario.Senha);

				return usuarioDAO.Cadastrar(usuario);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
			
		}

		public bool AutenticarUsuario(Dominio.Usuario usuario)
		{
			try
			{
				if (usuario == null)
					return false;

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();
				IList<Usuario> listaUsuarios = usuarioDAO.Pesquisar(usuario);

				if (listaUsuarios.Count != 1)
					return false;

				return true;
			}
			catch (Exception)
			{
				// TODO: TratarErro.
				throw;
			}
		}

		public bool VerificarAcesso(Dominio.Usuario usuario, string nomeFormulario)
		{
			// TODO: Implementar
			throw new NotImplementedException();
		}

		#endregion
	}
}