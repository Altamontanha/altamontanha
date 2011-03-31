using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class FotoNHibernate : Abstracao.IFotoDAO
	{
		public void Alterar(Dominio.Foto objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Foto Cadastrar(Dominio.Foto objeto)
		{
			return (Dominio.Foto)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Foto> Pesquisar(Dominio.Foto objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Foto>().List<Dominio.Foto>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Legenda", objeto.Legenda);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Foto>();
		}

		public Dominio.Foto Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Foto>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Foto foto = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(foto);
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