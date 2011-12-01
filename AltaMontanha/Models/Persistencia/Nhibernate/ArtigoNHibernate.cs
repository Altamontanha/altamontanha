using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ArtigoNHibernate : Abstracao.IArtigoDAO
	{
		public void Alterar(Dominio.Artigo objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Artigo Cadastrar(Dominio.Artigo objeto)
		{
			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
					objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

					conteudoDAO.VincularFotos(objeto);
					conteudoDAO.VincularPalavraChave(objeto);
					transaction.Commit();

					return objeto;
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
			}
		}
        /// <summary>
        /// Pesquisa de Artigos
        /// </summary>
        /// <param name="artigo"></param>
        /// <param name="qtde">LIMIT RESULT SET</param>
        /// <returns></returns>
		public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo artigo, int qtde)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));
			criteria.SetMaxResults(qtde);
			criteria.AddOrder(Order.Desc("Data"));

			if (artigo == null)
				return criteria.List<Dominio.Artigo>();
			
			if (artigo.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", artigo.Codigo));
			if (artigo.ObjCategoria != null)
				criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", artigo.ObjCategoria.Codigo));
			if (artigo.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", artigo.Data));
			if (!string.IsNullOrEmpty(artigo.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", artigo.Titulo));

			IList<Dominio.Artigo> artigos = criteria.List<Dominio.Artigo>();

			return artigos;
		}

		public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo objeto)
		{

			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Artigo>().List<Dominio.Artigo>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.ObjCategoria != null)
				criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", objeto.ObjCategoria.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));
			
			IList<Dominio.Artigo> artigos = criteria.List<Dominio.Artigo>();

			return artigos;
		}

		public Dominio.Artigo Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Artigo>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Artigo artigo = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(artigo);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, artigo não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}
	}
}