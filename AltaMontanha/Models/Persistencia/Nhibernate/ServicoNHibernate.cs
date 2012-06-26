using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Abstracao;
using NHibernate.Criterion;
using NHibernate;
using AltaMontanha.Models.Persistencia.MySQL;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
    public class ServicoNHibernate : IServicoDAO
    {
        public void Alterar(Dominio.Servico objeto)
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

        public Dominio.Servico Cadastrar(Dominio.Servico objeto)
        {
            try
            {
                MySQL.ConteudoMySQL conteudoDAO = new MySQL.ConteudoMySQL();
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();

                return objeto;
            }
            catch
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
                throw;
            }
        }

        public IList<Dominio.Servico> Pesquisar(Dominio.Servico objeto, int qtde = 0)
        {
            return Pesquisar(objeto, qtde, 0, null, new int[0]);
        }

        public IList<Servico> Pesquisar(Servico servico, int[] Codigos)
        {
            return Pesquisar(servico, 0, 0, null, Codigos);
        }

        public IList<Dominio.Servico> Pesquisar(Dominio.Servico objeto, int qtde, int pagina)
        {
            return Pesquisar(objeto, qtde, pagina, null, new int[0]);
        }

        public IList<Servico> Pesquisar(Servico servico, int qtde, int pagina, int[] Codigos)
        {
            return Pesquisar(servico, qtde, pagina, false, Codigos);
        }

        public IList<Dominio.Servico> Pesquisar(Dominio.Servico objeto, int qtde, int pagina, bool? aleatorio, int[] Codigos, int? Pagante = null)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Servico));

            if (pagina > 0)
            {
                criteria.SetFirstResult((pagina - 1) * qtde);
                criteria.SetMaxResults(qtde);
            }
            else if (qtde > 0 && pagina == 0)
            {
                criteria.SetMaxResults(qtde);
            }

            if (Pagante.HasValue)
            {
                criteria = criteria.Add(Expression.Eq("Pagante", Pagante.Value));
            }

            if (aleatorio.HasValue)
            {
                if (aleatorio.Value)
                    criteria.AddOrder(new RandomOrder());
            }
            else
            {
                criteria.AddOrder(Order.Desc("Codigo"));
            }

            if (Codigos != null)
            {
                if (Codigos.Length > 0)
                {
                    criteria = criteria.Add(Expression.In("Categoria.CodCategoria", Codigos));
                }
            }

            if (objeto == null)
                return criteria.List<Dominio.Servico>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));

            if (objeto.Categoria != null)
                criteria = criteria.Add(Expression.Eq("Categoria", objeto.Categoria));

            if (objeto.Ativo.HasValue)
                criteria = criteria.Add(Expression.Eq("Ativo", objeto.Ativo.Value));

            IList<Dominio.Servico> servicos = criteria.List<Dominio.Servico>();

            return servicos;
        }

        public Servico Pesquisar(int codigo)
        {
            return NHibernate.HttpModule.RecuperarSessao.Get<Servico>(codigo);
        }

        public bool Excluir(int codigo)
        {
            Servico servico = this.Pesquisar(codigo);

            using (ISession session = NHibernate.HttpModule.RecuperarSessao)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    NHibernate.HttpModule.RecuperarSessao.Delete(servico);
                    transaction.Commit();
                }
                catch (HibernateException e)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Existem outros registros vinculados, serviço não pode ser excluído", e.InnerException);
                }
            }

            return true;
        }
    }
}