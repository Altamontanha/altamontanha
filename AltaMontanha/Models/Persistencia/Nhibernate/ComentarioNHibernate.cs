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

			IList<Dominio.Comentario> comentarios = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Comentario>().List<Dominio.Comentario>().Where(comentario => comentario.Codigo == objeto.Codigo).ToList();

			return comentarios;
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

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}