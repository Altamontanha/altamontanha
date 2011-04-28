using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ArtigoNHibernate : Abstracao.IArtigoDAO
	{
		public void Alterar(Dominio.Artigo objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Artigo Cadastrar(Dominio.Artigo objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto; 
		}

		public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo objeto)
		{

			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Artigo>().List<Dominio.Artigo>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.Categoria != null)
				criteria = criteria.Add(Expression.Eq("CodCategoria", objeto.Categoria.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));
			
			IList<Dominio.Artigo> artigos = criteria.List<Dominio.Artigo>();

			return artigos;
		}

		public Dominio.Artigo Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Artigo>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Artigo artigo = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(artigo);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}