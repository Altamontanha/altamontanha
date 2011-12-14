﻿using System;
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

		public IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto, short qtde)
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

		public IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Aventura));
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

		public Dominio.Aventura Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Aventura>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Aventura aventura = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(aventura);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, aventura não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}