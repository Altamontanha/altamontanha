using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ArtigoNHibernate : Abstracao.IArtigoDAO
	{
		public void Alterar(Dominio.Artigo objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Artigo Cadastrar(Dominio.Artigo objeto)
		{
			return (Dominio.Artigo)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Artigo>().List<Dominio.Artigo>();

			IList<Dominio.Artigo> artigos = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Artigo>().List<Dominio.Artigo>().Where(artigo => artigo.Codigo == objeto.Codigo).ToList();
			
			return artigos;
		}

		public Dominio.Artigo Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Artigo>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Artigo artigo = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(artigo);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}