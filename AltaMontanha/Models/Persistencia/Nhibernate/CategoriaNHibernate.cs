using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class CategoriaNHibernate : Abstracao.ICategoriaDAO
	{
		public void Alterar(Dominio.Categoria objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Categoria Cadastrar(Dominio.Categoria objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Categoria> Pesquisar(Dominio.Categoria objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Categoria));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Categoria>().List<Dominio.Categoria>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Categoria> categorias = criteria.List<Dominio.Categoria>();

			return categorias;
		}

		public Dominio.Categoria Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Categoria>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Categoria categoria = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(categoria);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}