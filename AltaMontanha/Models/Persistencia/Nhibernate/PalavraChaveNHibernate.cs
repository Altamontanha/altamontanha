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
			return (Dominio.PalavraChave)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.PalavraChave> Pesquisar(Dominio.PalavraChave objeto)
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
			try
			{
				Dominio.PalavraChave palavraChave = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(palavraChave);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}