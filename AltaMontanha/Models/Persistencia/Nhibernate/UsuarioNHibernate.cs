using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace AltaMontanha.Models.Persistencia.Nhibernate
{
	public class UsuarioNHibernate : Abstracao.IUsuarioDAO
	{
		public void Alterar(Dominio.Usuario objeto)
		{
			NHibernate.HttpModule.RecuperarSessao.Update(objeto);
		}

		public Dominio.Usuario Cadastrar(Dominio.Usuario objeto)
		{
			return (Dominio.Usuario) NHibernate.HttpModule.RecuperarSessao.Save(objeto);
		}

		public IList<Dominio.Usuario> Pesquisar(Dominio.Usuario objeto)
		{
			ICriteria criteria = NHibernate.HttpModule.RecuperarSessao.CreateCriteria(typeof(Dominio.Usuario)); 

			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Usuario>().List<Dominio.Usuario>();

			if (objeto.Codigo > 0)
				criteria = criteria.Add(Expression.Eq("Codigo", objeto.Codigo));
			if (!string.IsNullOrEmpty(objeto.Login))
				criteria = criteria.Add(Expression.Eq("Login", objeto.Login));
			if (!string.IsNullOrEmpty(objeto.Senha))
				criteria = criteria.Add(Expression.Eq("Senha", objeto.Senha));
			if (objeto.Perfil != null)
				criteria = criteria.Add(Expression.Eq("CodPerfil", objeto.Perfil.Codigo));
			
			IList<Dominio.Usuario> usuarios = criteria.List<Dominio.Usuario>();
			
			return usuarios;
		}
		
		public Dominio.Usuario Pesquisar(int codigo)
		{
			return NHibernate.HttpModule.RecuperarSessao.Get<Dominio.Usuario>(codigo);
		}

		public bool Excluir(int codigo)
		{
			try
			{
				Dominio.Usuario usuario = Pesquisar(codigo);

				NHibernate.HttpModule.RecuperarSessao.Delete(usuario);
			
				return true;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}