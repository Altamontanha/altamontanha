using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PalavraChaveNHibernate : Abstracao.IPalavraChaveDAO
	{
		public void Alterar(Dominio.PalavraChave objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.PalavraChave Cadastrar(Dominio.PalavraChave objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.PalavraChave> Pesquisar(Dominio.PalavraChave objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.PalavraChave));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.PalavraChave>().List<Dominio.PalavraChave>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Nome))
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

			IList<Dominio.PalavraChave> palavraChave = criteria.List<Dominio.PalavraChave>();

			return palavraChave;
		}

		public Dominio.PalavraChave Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.PalavraChave>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.PalavraChave palavraChave = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(palavraChave);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, palavra-chave não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}