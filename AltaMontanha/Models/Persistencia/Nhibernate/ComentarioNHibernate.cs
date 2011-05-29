using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ComentarioNHibernate : Abstracao.IComentarioDAO
	{
		public void Alterar(Dominio.Comentario objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Comentario Cadastrar(Dominio.Comentario objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Comentario> Pesquisar(Dominio.Comentario objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Comentario));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Comentario>().List<Dominio.Comentario>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Nome))
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

			IList<Dominio.Comentario> comentarios = criteria.List<Dominio.Comentario>();

			return comentarios;
		}

		public Dominio.Comentario Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Comentario>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Comentario comentario = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(comentario);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException(e.InnerException.Message);
				}
			}

			return true;
		}
	}
}