using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class ColunaNHibernate : Abstracao.IColunaDAO
	{
		public void Alterar(Dominio.Coluna objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Coluna Cadastrar(Dominio.Coluna objeto)
		{
			return (Dominio.Coluna)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto)
		{
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Coluna>().List<Dominio.Coluna>();

			IList<Dominio.Coluna> colunas = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Coluna>().List<Dominio.Coluna>().Where(coluna => coluna.Codigo == objeto.Codigo).ToList();

			return colunas;
		}

		public Dominio.Coluna Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Coluna>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Coluna coluna = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(coluna);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}