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

			IList<Dominio.Categoria> categorias = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Categoria>().List<Dominio.Categoria>().Where(categoria => categoria.Codigo == objeto.Codigo).ToList();

			return categorias;
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

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}