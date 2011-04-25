using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

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
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Link));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Link>().List<Dominio.Link>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Link> links = criteria.List<Dominio.Link>();

			return links;
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

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}