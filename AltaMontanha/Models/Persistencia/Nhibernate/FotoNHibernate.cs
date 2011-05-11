using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class FotoNHibernate : Abstracao.IFotoDAO
	{
		public void Alterar(Dominio.Foto objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Foto Cadastrar(Dominio.Foto objeto)
		{
			objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

			return objeto;
		}

		public IList<Dominio.Foto> Pesquisar(Dominio.Foto objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Foto));

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Foto>().List<Dominio.Foto>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Legenda))
				criteria = criteria.Add(Expression.Eq("Legenda", objeto.Legenda));
			if (!string.IsNullOrEmpty(objeto.Fonte))
				criteria = criteria.Add(Expression.Eq("Fonte", objeto.Fonte));

			IList<Dominio.Foto> fotos = criteria.List<Dominio.Foto>();

			return fotos;
		}

		public Dominio.Foto Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Foto>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Foto foto = this.Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(foto);

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}