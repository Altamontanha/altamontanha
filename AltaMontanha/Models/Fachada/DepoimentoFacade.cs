using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Fachada
{
	public class DepoimentoFacade
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

		#region Tela
		public IList<Dominio.Tela> PesquisarTela(Dominio.Tela tela)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ITelaDAO telaDAO = fabrica.GetTelaDAO();

				return telaDAO.Pesquisar(tela);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		#endregion

		#region Depoimento

		/// <summary>
		/// consulta de um depoimento específico
		/// </summary>
		/// <param name="codigo">código do depoimento</param>
		/// <returns>depoimento referente ao código passado</returns>
		public Dominio.Depoimento PesquisarDepoimento(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
                IDepoimentoDAO depoimentoDAO = fabrica.GetDepoimentoDAO();

                return depoimentoDAO.Pesquisar(codigo);
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
		public IList<Dominio.Depoimento> PesquisarDepoimento(Dominio.Depoimento depoimento)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IDepoimentoDAO depoimentoDAO = fabrica.GetDepoimentoDAO();

                return depoimentoDAO.Pesquisar(depoimento);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Consulta de Colunistas
		/// </summary>
		/// <param name="usuario">objeto com parametros de pesquisa</param>
		/// <returns>lista de usuarios</returns>
		public IList<Dominio.Usuario> PesquisarColunista()
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IUsuarioDAO usuarioDAO = fabrica.GetUsuarioDAO();

				List<Dominio.Usuario> lista = (List<Dominio.Usuario>)usuarioDAO.PesquisarColunista();
				lista.RemoveAll(p => p.Codigo == 1);

				return lista;
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
		public Dominio.Depoimento SalvarDepoimento(Dominio.Depoimento depoimento, HttpPostedFileBase file)
		{
			try
			{
                if (depoimento == null)
					throw new ArgumentNullException("depoimento");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IDepoimentoDAO depoimentoDAO = fabrica.GetDepoimentoDAO();

                if (depoimento.Codigo <= 0)
                    return depoimentoDAO.Cadastrar(depoimento);

                depoimentoDAO.Alterar(depoimento);

                return depoimento;
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
        public bool ExcluirDepoimento(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IDepoimentoDAO depoimentoDAO = fabrica.GetDepoimentoDAO();

                bool retorno = depoimentoDAO.Excluir(codigo);

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