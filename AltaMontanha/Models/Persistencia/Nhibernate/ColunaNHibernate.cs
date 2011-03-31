using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ColunaNHibernate : Abstracao.IColunaDAO
	{
		public void Alterar(Dominio.Coluna objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Coluna Cadastrar(Dominio.Coluna objeto)
		{
			return (Dominio.Coluna)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Coluna>().List<Dominio.Coluna>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Titulo", objeto.Titulo);
			filtros.Add("Data", objeto.Data);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Coluna>();
		}

		public Dominio.Coluna Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Coluna>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Coluna coluna = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(coluna);
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