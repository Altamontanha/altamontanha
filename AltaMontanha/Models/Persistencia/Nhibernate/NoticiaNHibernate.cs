using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
    public class NoticiaNHibernate : Abstracao.INoticiaDAO
    {
        public void Alterar(Dominio.Noticia objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                NHibernate.HttpModule.RecuperarSessao.Update(objeto);

                NHibernate.HttpModule.RecuperarSessao.Flush();

                conteudoDAO.VincularFotos(objeto);
                if (objeto.ListaPalavrasChave != null)
                    conteudoDAO.VincularPalavraChave(objeto);
            }
            catch
            {
                throw;
            }
        }

        public Dominio.Noticia Cadastrar(Dominio.Noticia objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();
                conteudoDAO.VincularFotos(objeto);
                if (objeto.ListaPalavrasChave != null)
                    conteudoDAO.VincularPalavraChave(objeto);

                return objeto;
            }
            catch
            {
                throw;
            }

        }

        public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, short qtde)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));
            criteria.SetMaxResults(qtde);
            criteria.AddOrder(Order.Desc("Data"));

            if (objeto == null)
                return criteria.List<Dominio.Noticia>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.UsuarioCadastro != null)
                criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            if (objeto.AnteTitulo == "UsarDestaque")
                criteria = criteria.Add(Expression.Eq("Destaque", objeto.Destaque));

            IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

            return noticias;
        }

        public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, int qtde = 0)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));
            criteria.AddOrder(Order.Desc("Data"));

            if (qtde > 0)
            {
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.Noticia>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.UsuarioCadastro != null)
                criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            if (objeto.AnteTitulo == "UsarDestaque")
                criteria = criteria.Add(Expression.Eq("Destaque", objeto.Destaque));

            IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

            return noticias;
        }

        public IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Noticia));
            criteria.AddOrder(Order.Desc("Data"));

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.Noticia>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.UsuarioCadastro != null)
                criteria = criteria.Add(Expression.Eq("CodUsuario", objeto.UsuarioCadastro.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            if (objeto.AnteTitulo == "UsarDestaque")
                criteria = criteria.Add(Expression.Eq("Destaque", objeto.Destaque));

            IList<Dominio.Noticia> noticias = criteria.List<Dominio.Noticia>();

            return noticias;
        }

        public Dominio.Noticia Pesquisar(int codigo)
        {
            return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Noticia>(codigo);
        }

        public bool Excluir(int codigo)
        {
            Dominio.Noticia noticia = this.Pesquisar(codigo);

            using (ISession session = NHibernate.HttpModule.RecuperarSessao)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                    conteudoDAO.DesvincularFotos(noticia);

                    NHibernate.HttpModule.RecuperarSessao.Delete(noticia);
                    transaction.Commit();
                }
                catch (HibernateException e)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Existem outros registros vinculados, notícia não pode ser excluída", e.InnerException);
                }
            }

            return true;
        }

        public void Dispose()
        {
        }

    }
}