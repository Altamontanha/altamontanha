using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ColunaNHibernate : Abstracao.IColunaDAO
	{
		public void Alterar(Dominio.Coluna objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Coluna Cadastrar(Dominio.Coluna objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto, int qtde)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Coluna));
			criteria.SetMaxResults(qtde);
			criteria.AddOrder(Order.Desc("Data"));
			
			// TODO : O Resultado deve ser apenas da coluna mais nova "SEM REPETIR O AUTOR".

			if (objeto == null)
				return criteria.List<Dominio.Coluna>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.Autor != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.Autor.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuarioCadastro", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Coluna> colunas = criteria.List<Dominio.Coluna>();

			return colunas;
		}

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Coluna));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Coluna>().List<Dominio.Coluna>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.Autor != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.Autor.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuarioCadastro", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Coluna> colunas = criteria.List<Dominio.Coluna>();

			return colunas;
		}

		public Dominio.Coluna Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Coluna>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Coluna coluna = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(coluna);
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