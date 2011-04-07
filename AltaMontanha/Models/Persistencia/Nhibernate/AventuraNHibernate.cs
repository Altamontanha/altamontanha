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
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Aventura>().List<Dominio.Aventura>();

			IList<Dominio.Aventura> aventuras = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Aventura>().List<Dominio.Aventura>().Where(aventura => aventura.Codigo == objeto.Codigo).ToList();

			return aventuras;
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

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}