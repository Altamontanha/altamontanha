﻿using System;
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

            NHibernate.HttpModule.RecuperarSessao.Flush();
		}

		public Dominio.Banner Cadastrar(Dominio.Banner objeto)
        {
            try
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();
                objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();
            }
            catch (Exception e)
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
            }
			return objeto;
		}

		public IList<Dominio.Banner> Pesquisar(Dominio.Banner objeto, int pagina = 0)
		{
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Banner));
            criteria.AddOrder(Order.Desc("Codigo"));

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
			if (objeto.Local != null)
				criteria = criteria.Add(Expression.Eq("Local.Codigo", objeto.Local.Codigo));

			IList<Dominio.Banner> banners = criteria.List<Dominio.Banner>();

			return banners;
		}

        public IList<Dominio.Banner> Pesquisar(Dominio.Banner objeto, int qtde, int pagina)
        {
            throw new NotImplementedException();
        }

		public Dominio.Banner Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Banner>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Banner banner = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(banner);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, banner não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}

    }
}