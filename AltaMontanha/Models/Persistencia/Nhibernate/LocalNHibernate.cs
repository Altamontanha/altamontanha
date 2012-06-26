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

            NHibernate.HttpModule.RecuperarSessao.Flush();
		}

		public Dominio.Local Cadastrar(Dominio.Local objeto)
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

		public IList<Dominio.Local> Pesquisar(Dominio.Local objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Local));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Local>().List<Dominio.Local>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Descricao))
				criteria = criteria.Add(Expression.Eq("Descricao", objeto.Descricao));
			
			IList<Dominio.Local> locais = criteria.List<Dominio.Local>();

			return locais;
		}

        public IList<Dominio.Local> Pesquisar(Dominio.Local objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Local));

            if (objeto == null)
                return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Local>().List<Dominio.Local>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (!string.IsNullOrEmpty(objeto.Descricao))
                criteria = criteria.Add(Expression.Eq("Descricao", objeto.Descricao));

            IList<Dominio.Local> locais = criteria.List<Dominio.Local>();

            return locais;
        }

		public Dominio.Local Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Local>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Local local = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(local);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, local não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}

    }
}