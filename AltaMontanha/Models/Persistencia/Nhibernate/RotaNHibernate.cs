using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Rota>().List<Dominio.Rota>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Rota>();
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
				NHibernate.HttpModule.RecuperarSessao.Close();

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}