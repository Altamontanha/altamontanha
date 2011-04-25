using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class RotaNHibernate : Abstracao.IRotaDAO
	{
		public void Alterar(Dominio.Rota objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Rota Cadastrar(Dominio.Rota objeto)
		{
			return (Dominio.Rota) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Rota> Pesquisar(Dominio.Rota objeto)
		{
			return new List<Dominio.Rota>(){this.Pesquisar(objeto.Codigo)};
		}

		public Dominio.Rota Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Rota>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Rota rota = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(rota);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}