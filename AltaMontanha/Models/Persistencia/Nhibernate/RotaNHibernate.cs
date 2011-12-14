using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class RotaNHibernate : Abstracao.IRotaDAO
	{
		public void Alterar(Dominio.Rota objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Rota Cadastrar(Dominio.Rota objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
			return objeto;
		}

		public IList<Dominio.Rota> Pesquisar(Dominio.Rota objeto, int pagina = 0)
		{
			return new List<Dominio.Rota>(){this.Pesquisar(objeto.Codigo)};
		}

		public Dominio.Rota Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Rota>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Rota rota = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(rota);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, rota não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}