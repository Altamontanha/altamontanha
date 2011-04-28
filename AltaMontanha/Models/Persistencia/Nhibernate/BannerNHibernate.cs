using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class BannerNHibernate : Abstracao.IBannerDAO
	{
		public void Alterar(Dominio.Banner objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Banner Cadastrar(Dominio.Banner objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Banner> Pesquisar(Dominio.Banner objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Banner));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Banner>().List<Dominio.Banner>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.DataInicial > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("DataInicial", objeto.DataInicial));
			if (objeto.DataFinal > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("DataFinal", objeto.DataFinal));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Banner> banners = criteria.List<Dominio.Banner>();

			return banners;
		}

		public Dominio.Banner Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Banner>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Banner banner = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(banner);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}