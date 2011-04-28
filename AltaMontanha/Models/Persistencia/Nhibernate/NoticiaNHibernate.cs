using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

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
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Noticia>().List<Dominio.Noticia>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (objeto.UsuarioCadastro != null)
				criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
			if (objeto.Data > DateTime.MinValue)
				criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
			if (!string.IsNullOrEmpty(objeto.Titulo))
				criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

			IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

			return noticias;
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

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}