using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class AventuraNHibernate : Abstracao.IAventuraDAO
	{
		public void Alterar(Dominio.Aventura objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Aventura Cadastrar(Dominio.Aventura objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto; 
		}

		public IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto, int qtde)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Aventura));
			criteria.SetMaxResults(qtde);
			criteria.AddOrder(Order.Desc("Data"));
			
			if (objeto == null)
				return criteria.List<Dominio.Aventura>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.AventuraAnterior != null)
				criteria = criteria.Add(Expression.Eq("CodAventuraAnterior", objeto.AventuraAnterior.Codigo));
			if (objeto.Autor != null)
				criteria = criteria.Add(Expression.Eq("CodAutor", objeto.Autor.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Aventura> aventuras = criteria.List<Dominio.Aventura>();

			return aventuras;
		}

		public IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Aventura));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Aventura>().List<Dominio.Aventura>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.AventuraAnterior != null)
				criteria = criteria.Add(Expression.Eq("CodAventuraAnterior", objeto.AventuraAnterior.Codigo));
			if (objeto.Autor != null)
				criteria = criteria.Add(Expression.Eq("CodAutor", objeto.Autor.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Aventura> aventuras = criteria.List<Dominio.Aventura>();

			return aventuras;
		}

		public Dominio.Aventura Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Aventura>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Aventura artigo = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(artigo);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}