using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PermissaoNHibernate : Abstracao.IPermissaoDAO
	{
		public void Alterar(Dominio.Permissao objeto)
		{
            NHibernate.HttpModule.RecuperarSessao.Update(objeto);

            NHibernate.HttpModule.RecuperarSessao.Flush();
		}

		public Dominio.Permissao Cadastrar(Dominio.Permissao objeto)
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

		public IList<Dominio.Permissao> Pesquisar(Dominio.Permissao objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Permissao));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Permissao>().List<Dominio.Permissao>();

			if (objeto.Perfil != null)
				criteria = criteria.Add(Expression.Eq("CodPerfil", objeto.Perfil.Codigo));
			if (objeto.Tela != null)
				criteria = criteria.Add(Expression.Eq("CodTela", objeto.Tela.Codigo));
			
			IList<Dominio.Permissao> permissoes = criteria.List<Dominio.Permissao>();

			return permissoes;
		}

        public IList<Dominio.Permissao> Pesquisar(Dominio.Permissao objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Permissao));

            if (objeto == null)
                return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Permissao>().List<Dominio.Permissao>();

            if (objeto.Perfil != null)
                criteria = criteria.Add(Expression.Eq("CodPerfil", objeto.Perfil.Codigo));
            if (objeto.Tela != null)
                criteria = criteria.Add(Expression.Eq("CodTela", objeto.Tela.Codigo));

            IList<Dominio.Permissao> permissoes = criteria.List<Dominio.Permissao>();

            return permissoes;
        }

		public Dominio.Permissao Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Permissao>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Permissao permissao = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(permissao);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, permissão não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}


    }
}