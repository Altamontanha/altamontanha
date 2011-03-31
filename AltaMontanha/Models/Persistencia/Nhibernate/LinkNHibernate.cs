using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class LinkNHibernate : Abstracao.ILinkDAO
	{
		public void Alterar(Dominio.Link objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Link Cadastrar(Dominio.Link objeto)
		{
			return (Dominio.Link)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Link> Pesquisar(Dominio.Link objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Link>().List<Dominio.Link>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Titulo", objeto.Titulo);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Link>();
		}

		public Dominio.Link Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Link>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Link link = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(link);
				NHibernate.HttpModule.RecuperarSessao.Close();

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}