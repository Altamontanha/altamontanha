using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class LocalNHibernate : Abstracao.ILocalDAO
	{
		public void Alterar(Dominio.Local objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Local Cadastrar(Dominio.Local objeto)
		{
			return (Dominio.Local)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Local> Pesquisar(Dominio.Local objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Local>().List<Dominio.Local>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Descricao);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Local>();
		}

		public Dominio.Local Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Local>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Local local = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(local);
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