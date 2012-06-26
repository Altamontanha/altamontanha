using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Dominio;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
    public class CategoriaEmpresaNHibernate : ICategoriaEmpresaDAO
    {
        public void Alterar(Dominio.CategoriaEmpresa objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();

                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                NHibernate.HttpModule.RecuperarSessao.Update(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();

                NHibernate.HttpModule.RecuperarSessao.Flush();
            }
            catch
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
                throw;
            }
        }

        public Dominio.CategoriaEmpresa Cadastrar(Dominio.CategoriaEmpresa objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                objeto.CodCategoria = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();

                return objeto;
            }
            catch
            {
                throw;
            }
        }


        public IList<CategoriaEmpresa> Pesquisar(CategoriaEmpresa objeto, int qtde = 0)
        {
            return Pesquisar(objeto, qtde, 0);
        }

        public IList<CategoriaEmpresa> Pesquisar(CategoriaEmpresa objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.CategoriaEmpresa));

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
            }
            else if (qtde > 0)
            {
                criteria.SetMaxResults(qtde);
            }

            if (objeto == null)
                return criteria.List<Dominio.CategoriaEmpresa>();

            if (objeto.CodCategoria > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.CodCategoria));

            if (objeto.Categoria != null)
                criteria = criteria.Add(Expression.Eq("Categoria", objeto.Categoria));

            IList<Dominio.CategoriaEmpresa> categoriaEmpresa = criteria.List<Dominio.CategoriaEmpresa>();

            return categoriaEmpresa;
        }

        public CategoriaEmpresa Pesquisar(int codigo)
        {
            return NHibernate.HttpModule.RecuperarSessao.Get<CategoriaEmpresa>(codigo);
        }

        public bool Excluir(int codigo)
        {
            CategoriaEmpresa categoriaEmpresa = this.Pesquisar(codigo);

            using (ISession session = NHibernate.HttpModule.RecuperarSessao)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    NHibernate.HttpModule.RecuperarSessao.Delete(categoriaEmpresa);
                    transaction.Commit();
                }
                catch (HibernateException e)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Existem outros registros vinculados, esta categoria não pode ser excluída", e.InnerException);
                }
            }

            return true;
        }
    }
}