using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PerfilNHibernate : Abstracao.IPerfilDAO
	{
		public void Alterar(Dominio.Perfil objeto)
		{
            NHibernate.HttpModule.RecuperarSessao.Update(objeto);

            NHibernate.HttpModule.RecuperarSessao.Flush();
		}

		public Dominio.Perfil Cadastrar(Dominio.Perfil objeto)
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

		public IList<Dominio.Perfil> Pesquisar(Dominio.Perfil objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Perfil));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Perfil>().List<Dominio.Perfil>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Nome))
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

			IList<Dominio.Perfil> perfis = criteria.List<Dominio.Perfil>();

			return perfis;
		}

        public IList<Dominio.Perfil> Pesquisar(Dominio.Perfil objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Perfil));

            if (objeto == null)
                return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Perfil>().List<Dominio.Perfil>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (!string.IsNullOrEmpty(objeto.Nome))
                criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

            IList<Dominio.Perfil> perfis = criteria.List<Dominio.Perfil>();

            return perfis;
        }

		public Dominio.Perfil Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Perfil>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Perfil perfil = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(perfil);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, perfil não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}

    }
}