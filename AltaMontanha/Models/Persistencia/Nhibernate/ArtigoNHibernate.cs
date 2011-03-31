using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ArtigoMySQL : Abstracao.IArtigoDAO
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
				NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Artigo>().List<Dominio.Artigo>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("AnteTitulo", objeto.AnteTitulo);
			filtros.Add("Titulo", objeto.Titulo);
			filtros.Add("UsuarioCadastro", objeto.UsuarioCadastro);

			if (objeto.Categoria != null)
				filtros.Add("Titulo", objeto.Categoria.Titulo);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Artigo>();
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