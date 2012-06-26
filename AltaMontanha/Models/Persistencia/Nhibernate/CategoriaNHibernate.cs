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

            NHibernate.HttpModule.RecuperarSessao.Flush();
		}

		public Dominio.Categoria Cadastrar(Dominio.Categoria objeto)
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

		public IList<Dominio.Categoria> Pesquisar(Dominio.Categoria objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Categoria));
            criteria.AddOrder(Order.Asc("Titulo"));

			if (objeto == null)
                return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Categoria>().AddOrder(Order.Asc("Titulo")).List<Dominio.Categoria>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Categoria> categorias = criteria.List<Dominio.Categoria>();

			return categorias;
		}

        public IList<Dominio.Categoria> Pesquisar(Dominio.Categoria objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Categoria));
            criteria.AddOrder(Order.Asc("Titulo"));

            if (objeto == null)
                return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Categoria>().AddOrder(Order.Asc("Titulo")).List<Dominio.Categoria>();

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
			Dominio.Categoria categoria = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(categoria);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, categoria não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
    }
}