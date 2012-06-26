using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
    public class FotoNHibernate : Abstracao.IFotoDAO
    {
        public void Alterar(Dominio.Foto objeto)
        {
            NHibernate.HttpModule.RecuperarSessao.Update(objeto);

            NHibernate.HttpModule.RecuperarSessao.Flush();
        }

        public Dominio.Foto Cadastrar(Dominio.Foto objeto)
        {
            try
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();
                objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);
                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();
            }
            catch (Exception e)
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
            }

            return objeto;
        }

        public IList<Dominio.Foto> Pesquisar(Dominio.Foto objeto, int qtde = 0)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Foto));
            criteria.AddOrder(Order.Desc("Codigo"));

            if (qtde > 0)
            {
                criteria.SetMaxResults(qtde);
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

        public IList<Dominio.Foto> Pesquisar(Dominio.Foto objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Foto));
            criteria.AddOrder(Order.Desc("Codigo"));

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
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