using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PermissaoNHibernate : Abstracao.IPermissaoDAO
	{
		public void Alterar(Dominio.Permissao objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Permissao Cadastrar(Dominio.Permissao objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Permissao> Pesquisar(Dominio.Permissao objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Permissao));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Permissao>().List<Dominio.Permissao>();

			if (objeto.Perfil != null)
				criteria = criteria.Add(Expression.Eq("CodPerfil", objeto.Perfil.Codigo));
			if (objeto.Tela != null)
				criteria = criteria.Add(Expression.Eq("CodTela", objeto.Tela.Codigo));
			
			IList<Dominio.Permissao> permissoes = criteria.List<Dominio.Permissao>();

			return permissoes;
		}

		public Dominio.Permissao Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Permissao>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Permissao permissao = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(permissao);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}