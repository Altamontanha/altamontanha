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

		public IList<Dominio.Foto> Pesquisar(Dominio.Foto objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Foto));
			
			if (pagina > 0)
			{
				criteria.SetFirstResult(pagina * Utilitario.Constante.TamanhoPagina);
				criteria.SetMaxResults(Utilitario.Constante.TamanhoPagina);
			}

			if (objeto == null)
				return criteria.List<Dominio.Foto>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Legenda))
				criteria = criteria.Add(Expression.InsensitiveLike("Legenda", string.Format("%{0}%", objeto.Legenda)));
			if (!string.IsNullOrEmpty(objeto.Fonte))
				criteria = criteria.Add(Expression.Eq("Fonte", objeto.Fonte));

			criteria = criteria.Add(Expression.Eq("Galeria", objeto.Galeria));

			IList<Dominio.Foto> fotos = criteria.List<Dominio.Foto>();

			return fotos;
		}

		public Dominio.Foto Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Foto>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Foto foto = this.Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(foto);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, foto não pode ser excluída", e.InnerException);
				}
			}

			return true;
		}
	}
}