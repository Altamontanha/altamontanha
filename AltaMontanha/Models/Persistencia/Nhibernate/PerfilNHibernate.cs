using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PerfilNHibernate : Abstracao.IPerfilDAO
	{
		public void Alterar(Dominio.Perfil objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Perfil Cadastrar(Dominio.Perfil objeto)
		{
			return (Dominio.Perfil) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Perfil> Pesquisar(Dominio.Perfil objeto)
		{
			try
			{
				if (objeto == null)
					return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Perfil>().List<Dominio.Perfil>();

				Dictionary<string, object> filtros = new Dictionary<string, object>();

				filtros.Add("Codigo", objeto.Codigo);
				filtros.Add("Nome", objeto.Nome);
				filtros.Add("Descricao", objeto.Descricao);
			
				// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
				return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Perfil>();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Dominio.Perfil Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Perfil>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				NHibernate.HttpModule.RecuperarSessao.Delete(codigo);

				return true;
			}
			catch (Exception ex)
			{
				// TODO : Tratar Exceções
				throw ex;
			}
		}
	}
}