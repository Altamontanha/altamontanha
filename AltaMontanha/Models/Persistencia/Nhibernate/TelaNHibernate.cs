using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class TelaNHibernate : Abstracao.ITelaDAO
	{
		public IList<Dominio.Tela> Pesquisar(Dominio.Tela objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Tela));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Tela>().List<Dominio.Tela>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("CodTela", objeto.Codigo));
			if (objeto.Nome != null)
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

			IList<Dominio.Tela> telas = criteria.List<Dominio.Tela>();

			return telas;
		}

		public Dominio.Tela Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Tela>(codigo);
		}

		public void Alterar(Dominio.Tela objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Tela Cadastrar(Dominio.Tela objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public bool Excluir(int codigo)
		{
			Dominio.Tela tela = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(tela);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, tela não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}