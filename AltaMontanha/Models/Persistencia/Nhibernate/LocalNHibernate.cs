using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class LocalNHibernate : Abstracao.ILocalDAO
	{
		public void Alterar(Dominio.Local objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Local Cadastrar(Dominio.Local objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Local> Pesquisar(Dominio.Local objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Local));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Local>().List<Dominio.Local>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Nome))
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));
			
			IList<Dominio.Local> locais = criteria.List<Dominio.Local>();

			return locais;
		}

		public Dominio.Local Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Local>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Local local = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(local);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}