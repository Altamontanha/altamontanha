using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class DepoimentoNHibernate : Abstracao.IDepoimentoDAO
	{
		public void Alterar(Dominio.Depoimento objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

        public Dominio.Depoimento Cadastrar(Dominio.Depoimento objeto)
		{
			objeto.Codigo = (int) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

        public IList<Dominio.Depoimento> Pesquisar(Dominio.Depoimento objeto, int pagina = 0)
		{
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Depoimento)); 

			if (objeto == null)
                return criteria.List<Dominio.Depoimento>();

            //if (objeto.Codigo > 0)
            //    criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            //if (!string.IsNullOrEmpty(objeto.Login))
            //    criteria = criteria.Add(Expression.Eq("Login", objeto.Login));
            //if (!string.IsNullOrEmpty(objeto.Senha))
            //    criteria = criteria.Add(Expression.Eq("Senha", objeto.Senha));
            //if (objeto.Perfil != null)
            //    criteria = criteria.Add(Expression.Eq("Perfil.Codigo", objeto.Perfil.Codigo));

            IList<Dominio.Depoimento> depoimentos = criteria.List<Dominio.Depoimento>();

            return depoimentos;
		}

        public IList<Dominio.Depoimento> Pesquisar()
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Coluna));

            criteria.CreateCriteria("Autor", JoinType.InnerJoin);
            criteria.SetProjection(Projections.GroupProperty("Autor"));

            IList<Dominio.Depoimento> usuarios = criteria.List<Dominio.Depoimento>();

            return usuarios;
        }

        public IList<Dominio.Depoimento> PesquisarColunista()
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Coluna));
			
			criteria.CreateCriteria("Autor", JoinType.InnerJoin);
			criteria.SetProjection(Projections.GroupProperty("Autor"));

            IList<Dominio.Depoimento> usuarios = criteria.List<Dominio.Depoimento>();

			return usuarios;
		}

        public Dominio.Depoimento Pesquisar(int codigo)
		{
            return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Depoimento>(codigo);
		}

		public bool Excluir(int codigo)
		{
            Dominio.Depoimento depoimento = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
                    NHibernate.HttpModule.RecuperarSessao.Delete(depoimento);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, depoimento não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}
	}
}