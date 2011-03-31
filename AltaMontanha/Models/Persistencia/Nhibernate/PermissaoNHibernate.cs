using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class PermissaoNHibernate : Abstracao.IPermissaoDAO
	{
		public void Alterar(Dominio.Permissao objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Permissao Cadastrar(Dominio.Permissao objeto)
		{
			return (Dominio.Permissao)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Permissao> Pesquisar(Dominio.Permissao objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Permissao>().List<Dominio.Permissao>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Nome);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Permissao>();
		}

		public Dominio.Permissao Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Permissao>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Permissao permissao = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(permissao);
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