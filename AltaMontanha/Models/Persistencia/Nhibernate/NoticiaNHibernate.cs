using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class NoticiaNHibernate : Abstracao.INoticiaDAO
	{
		public void Alterar(Dominio.Noticia objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Noticia Cadastrar(Dominio.Noticia objeto)
		{
			try
			{
				MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();

				objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
				conteudoDAO.VincularFotos(objeto);
				conteudoDAO.VincularPalavraChave(objeto);
			
				return objeto;
			}
			catch
			{ 
				throw;
			}
			
		}

		public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, short qtde)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));
			criteria.SetMaxResults(qtde);
			criteria.AddOrder(Order.Desc("Data"));

			if (objeto == null)
				return criteria.List<Dominio.Noticia>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));
			
			criteria = criteria.Add(Expression.Eq("Destaque", objeto.Destaque));

			IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

			return noticias;
		}

		public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));
			criteria.AddOrder(Order.Desc("Data"));

			if (objeto == null)
				return criteria.List<Dominio.Noticia>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

			return noticias;
		}

		public Dominio.Noticia Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Noticia>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Noticia noticia = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(noticia);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, notícia não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}