﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PerfilNHibernate : Abstracao.IPerfilDAO
	{
		public void Alterar(Dominio.Perfil objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Perfil Cadastrar(Dominio.Perfil objeto)
		{
			return (Dominio.Perfil) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Perfil> Pesquisar(Dominio.Perfil objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Perfil));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Perfil>().List<Dominio.Perfil>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Nome))
				criteria = criteria.Add(Expression.Eq("Nome", objeto.Nome));

			IList<Dominio.Perfil> perfis = criteria.List<Dominio.Perfil>();

			return perfis;
		}

		public Dominio.Perfil Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Perfil>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				NHibernate.HttpModule.RecuperarSessao.Delete(codigo);

				return true;
			}
			catch (Exception ex)
			{
				// TODO : Tratar Exceções
				throw ex;
			}
		}
	}
}