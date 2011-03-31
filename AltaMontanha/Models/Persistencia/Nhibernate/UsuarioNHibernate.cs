using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class UsuarioNHibernate : Abstracao.IUsuarioDAO
	{
		public void Alterar(Dominio.Usuario objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Usuario Cadastrar(Dominio.Usuario objeto)
		{
			return (Dominio.Usuario) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Usuario> Pesquisar(Dominio.Usuario objeto)
		{
			if (objeto == null)
				NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Usuario>().List<Dominio.Usuario>();

			Dictionary<string,object> filtros = new Dictionary<string,object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Nome);
			filtros.Add("Login", objeto.Login);
			filtros.Add("Senha", objeto.Senha);
			
			if(objeto.Perfil != null)
				filtros.Add("Descricao", objeto.Perfil.Descricao);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros,"").List<Dominio.Usuario>();
		}
		
		public Dominio.Usuario Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Usuario>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Usuario usuario = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(usuario);
				NHibernate.HttpModule.RecuperarSessao.Close();

				return true;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}