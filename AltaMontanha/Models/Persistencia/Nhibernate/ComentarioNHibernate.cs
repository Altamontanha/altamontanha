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
			return (Dominio.Comentario)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
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
			try
			{
				Dominio.Comentario comentario = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(comentario);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}