using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ComentarioNHibernate : Abstracao.IComentarioDAO
	{
		public void Alterar(Dominio.Comentario objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Comentario Cadastrar(Dominio.Comentario objeto)
		{
			return (Dominio.Comentario)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Comentario> Pesquisar(Dominio.Comentario objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Comentario>().List<Dominio.Comentario>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("Nome", objeto.Nome);
			
			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Comentario>();
		}

		public Dominio.Comentario Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Comentario>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Comentario comentario = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(comentario);
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