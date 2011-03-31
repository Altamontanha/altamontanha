using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class NoticiaNHibernate : Abstracao.INoticiaDAO
	{
		public void Alterar(Dominio.Noticia objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Noticia Cadastrar(Dominio.Noticia objeto)
		{
			return (Dominio.Noticia)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Noticia>().List<Dominio.Noticia>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Data", objeto.Data);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Noticia>();
		}

		public Dominio.Noticia Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Noticia>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Noticia noticia = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(noticia);
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