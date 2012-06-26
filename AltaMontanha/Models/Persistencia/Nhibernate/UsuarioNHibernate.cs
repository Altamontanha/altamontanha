using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class UsuarioNHibernate : Abstracao.IUsuarioDAO
	{
		public void Alterar(Dominio.Usuario objeto)
		{
            try
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

                NHibernate.HttpModule.RecuperarSessao.Update(objeto);

                NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();
            }
            catch
            {
                NHibernate.HttpModule.RecuperarSessao.Transaction.Rollback();
                throw;
            }
		}

		public Dominio.Usuario Cadastrar(Dominio.Usuario objeto)
        {
            NHibernate.HttpModule.RecuperarSessao.Transaction.Begin();

            objeto.Codigo = (int)NHibernate.HttpModule.RecuperarSessao.Save(objeto);

            NHibernate.HttpModule.RecuperarSessao.Transaction.Commit();
			return objeto;
		}

		public IList<Dominio.Usuario> Pesquisar(Dominio.Usuario objeto, int pagina = 0)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Usuario));

            criteria.AddOrder(Order.Asc("Nome"));

			if (objeto == null)
				return criteria.List<Dominio.Usuario>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Login))
				criteria = criteria.Add(Expression.Eq("Login", objeto.Login));
			if (!string.IsNullOrEmpty(objeto.Senha))
				criteria = criteria.Add(Expression.Eq("Senha", objeto.Senha));
			if (objeto.Perfil != null)
				criteria = criteria.Add(Expression.Eq("Perfil.Codigo", objeto.Perfil.Codigo));
			
			IList<Dominio.Usuario> usuarios = criteria.List<Dominio.Usuario>();
			
			return usuarios;
		}

        public IList<Dominio.Usuario> Pesquisar(Dominio.Usuario objeto, int qtde, int pagina)
        {
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Usuario));

            criteria.AddOrder(Order.Asc("Nome"));

            if (objeto == null)
                return criteria.List<Dominio.Usuario>();

            if (objeto.Codigo > 0)
                criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
            if (!string.IsNullOrEmpty(objeto.Login))
                criteria = criteria.Add(Expression.Eq("Login", objeto.Login));
            if (!string.IsNullOrEmpty(objeto.Senha))
                criteria = criteria.Add(Expression.Eq("Senha", objeto.Senha));
            if (objeto.Perfil != null)
                criteria = criteria.Add(Expression.Eq("Perfil.Codigo", objeto.Perfil.Codigo));

            IList<Dominio.Usuario> usuarios = criteria.List<Dominio.Usuario>();

            return usuarios;
        }
		
		public IList<Dominio.Usuario> PesquisarColunista()
		{
            ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Coluna));
            
            criteria.CreateCriteria("Autor", JoinType.InnerJoin).Add(Expression.Eq("Colunista", true)).AddOrder(Order.Asc("Nome"));
            criteria.SetProjection(Projections.GroupProperty("Autor"));
			
			IList<Dominio.Usuario> usuarios = criteria.List<Dominio.Usuario>();

			return usuarios;
		}
		
		public Dominio.Usuario Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Usuario>(codigo);
		}

		public bool Excluir(int codigo)
		{
			Dominio.Usuario usuario = Pesquisar(codigo);

			using (ISession session = NHibernate.HttpModule.RecuperarSessao)
			using (ITransaction transaction = session.BeginTransaction())
			{
				try
				{
					NHibernate.HttpModule.RecuperarSessao.Delete(usuario);
					transaction.Commit();
				}
				catch (HibernateException e)
				{
					transaction.Rollback();
					throw new ApplicationException("Existem outros registros vinculados, usuário não pode ser excluído", e.InnerException);
				}
			}

			return true;
		}

    }
}