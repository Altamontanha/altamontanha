using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class AventuraNHibernate : Abstracao.IAventuraDAO
	{
		public void Alterar(Dominio.Aventura objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Aventura Cadastrar(Dominio.Aventura objeto)
		{
			return (Dominio.Aventura)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto)
		{
			if (objeto == null)
				NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Aventura>().List<Dominio.Aventura>();

			Dictionary<string, object> filtros = new Dictionary<string, object>();

			filtros.Add("Codigo", objeto.Codigo);
			filtros.Add("AnteTitulo", objeto.AnteTitulo);
			filtros.Add("Titulo", objeto.Titulo);
			filtros.Add("UsuarioCadastro", objeto.UsuarioCadastro);

			if (objeto.Autor != null)
				filtros.Add("Nome", objeto.Autor.Nome);

			// TODO: Verificar se o codigo de consulta com filtros é feita dessa forma.
			return NHibernate.HttpModule.RecuperarSessao.CreateFilter(filtros, "").List<Dominio.Aventura>();
		}

		public Dominio.Aventura Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Aventura>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Aventura artigo = Pesquisar(codigo);

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