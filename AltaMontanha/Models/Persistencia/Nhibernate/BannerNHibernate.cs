using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class BannerNHibernate : Abstracao.IBannerDAO
	{
		public void Alterar(Dominio.Banner objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Banner Cadastrar(Dominio.Banner objeto)
		{
			return (Dominio.Banner)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Banner> Pesquisar(Dominio.Banner objeto)
		{
			if (objeto == null)
				NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Aventura>().List<Dominio.Aventura>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Titulo", objeto.Titulo);
			filtros.Add("DataInicial", objeto.DataInicial);
			filtros.Add("DataFinal", objeto.DataFinal);
			filtros.Add("Ativo", objeto.Ativo);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Banner>();
		}

		public Dominio.Banner Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Banner>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Banner banner = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(banner);
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