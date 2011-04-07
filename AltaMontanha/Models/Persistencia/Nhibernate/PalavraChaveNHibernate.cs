using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.PalavraChave>().List<Dominio.PalavraChave>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Nome);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.PalavraChave>();
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