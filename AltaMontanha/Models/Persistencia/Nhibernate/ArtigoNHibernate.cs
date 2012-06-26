using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using AltaMontanha.Models.Persistencia.MySQL;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
    public class ArtigoNHibernate : Abstracao.IArtigoDAO
    {
        public void Alterar(Dominio.Artigo objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();

                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                NHibernate.HttpModule.RecuperarSessao.Update(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();

                conteudoDAO.VincularFotos(objeto);

                if (objeto.ListaPalavrasChave != null)
                    conteudoDAO.VincularPalavraChave(objeto);
            }
            catch
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
                throw;
            }
        }

        public Dominio.Artigo Cadastrar(Dominio.Artigo objeto)
        {
            MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();

            try
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();

                conteudoDAO.VincularFotos(objeto);
                if (objeto.ListaPalavrasChave != null)
                    conteudoDAO.VincularPalavraChave(objeto);

                return objeto;
            }
            catch (Exception e)
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();

                return null;
            }
        }
        /// <summary>
        /// Pesquisa de Artigos
        /// </summary>
        /// <param name="artigo"></param>
        /// <param name="qtde">LIMIT RESULT SET</param>
        /// <returns></returns>
        public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo artigo, short qtde)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));
            criteria.SetMaxResults(qtde);
            criteria.AddOrder(Order.Desc("Data"));

            if (artigo == null)
                return criteria.List<Dominio.Artigo>();

            if (artigo.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", artigo.Codigo));
            if (artigo.ObjCategoria != null)
                criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", artigo.ObjCategoria.Codigo));
            if (artigo.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", artigo.Data));
            if (!string.IsNullOrEmpty(artigo.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", artigo.Titulo));

            IList<Dominio.Artigo> artigos = criteria.List<Dominio.Artigo>();

            return artigos;
        }

        public IList<Dominio.Artigo> Pesquisar(Dominio.Artigo objeto, int qtde = 0)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));
            criteria.AddOrder(Order.Desc("Data"));

            if (qtde > 0)
            {
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.Artigo>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.ObjCategoria != null)
                criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", objeto.ObjCategoria.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            IList<Artigo> artigos = criteria.List<Artigo>();

            return artigos;
        }

        public IList<Artigo> Pesquisar(Artigo objeto, int qtde, int pagina, int[] CodigosCategorias)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));
            criteria.AddOrder(Order.Desc("Data"));

            if (CodigosCategorias != null)
            {
                if (CodigosCategorias.Length > 0)
                {
                    criteria = criteria.Add(Expression.In("ObjCategoria.Codigo", CodigosCategorias));
                }
            }

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.Artigo>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.ObjCategoria != null)
                criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", objeto.ObjCategoria.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            IList<Artigo> artigos = criteria.List<Artigo>();

            return artigos;
        }

        public IList<Artigo> Pesquisar(Artigo objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Artigo));
            criteria.AddOrder(Order.Desc("Data"));

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.Artigo>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (objeto.ObjCategoria != null)
                criteria = criteria.Add(Expression.Eq("ObjCategoria.Codigo", objeto.ObjCategoria.Codigo));
            if (objeto.Data > DateTime.MinValue)
                criteria = criteria.Add(Expression.Eq("Data", objeto.Data));
            if (!string.IsNullOrEmpty(objeto.Titulo))
                criteria = criteria.Add(Expression.Eq("Titulo", objeto.Titulo));

            IList<Artigo> artigos = criteria.List<Artigo>();

            return artigos;
        }

        /// <summary>
        /// Funcao que traz Artigos aleatorios conforme os parametros
        /// </summary>
        /// <param name="qtde">Maximo de resultaados</param>
        /// <param name="InCategoria">So traz artigos dessas categorias, se esfecificadas</param>
        /// <param name="NotInConteudo">Nao traz nenhum artigo com os codigos conteudo informados</param>
        /// <param name="NotInCategoria">Nao traz nenhum artigo com os codigos categoria informados</param>
        /// <returns></returns>
        public IList<Artigo> PesquisarAleatorio(int qtde, int[] InCategoria, int[] NotInConteudo, int[] NotInCategoria)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Artigo));

            if (InCategoria != null)
            {
                DetachedCriteria c = DetachedCriteria.For<Categoria>()
                    .SetProjection(Projections.Property("Codigo"))
                    .Add(Expression.In("Codigo", InCategoria));

                criteria.Add(Subqueries.PropertyIn("ObjCategoria", c));
            }

            if (NotInConteudo != null)
            {
                DetachedCriteria c = DetachedCriteria.For<Conteudo>()
                    .SetProjection(Projections.Property("Codigo"))
                    .Add(Expression.In("Codigo", NotInConteudo));

                criteria.Add(Subqueries.PropertyNotIn("Codigo", c));
            }

            if (NotInCategoria != null)
            {
                DetachedCriteria c = DetachedCriteria.For<Categoria>()
                    .SetProjection(Projections.Property("Codigo"))
                    .Add(Expression.In("Codigo", NotInCategoria));

                criteria.Add(Subqueries.PropertyNotIn("ObjCategoria", c));
            }

            criteria.AddOrder(new RandomOrder());
            criteria.SetMaxResults(qtde);

            IList<Artigo> artigos = criteria.List<Artigo>();

            return artigos;
        }

        public Artigo Pesquisar(int codigo)
        {
            return NHibernate.HttpModule.RecuperarSessao.Get<Artigo>(codigo);
        }

        public bool Excluir(int codigo)
        {
            Dominio.Artigo artigo = Pesquisar(codigo);

            using (ISession session = NHibernate.HttpModule.RecuperarSessao)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                    conteudoDAO.DesvincularFotos(artigo);

                    NHibernate.HttpModule.RecuperarSessao.Delete(artigo);
                    transaction.Commit();
                }
                catch (HibernateException e)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Existem outros registros vinculados, artigo não pode ser excluído", e.InnerException);
                }
            }

            return true;
        }


    }
}