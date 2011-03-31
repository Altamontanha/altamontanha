using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class CategoriaNHibernate : Abstracao.ICategoriaDAO
	{
		public void Alterar(Dominio.Categoria objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Categoria Cadastrar(Dominio.Categoria objeto)
		{
			return (Dominio.Categoria) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Categoria> Pesquisar(Dominio.Categoria objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Categoria>().List<Dominio.Categoria>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Descricao);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Categoria>();
		}

		public Dominio.Categoria Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Categoria>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Categoria categoria = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(categoria);
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