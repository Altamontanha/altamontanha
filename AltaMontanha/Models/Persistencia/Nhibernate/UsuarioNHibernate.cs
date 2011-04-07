using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NH = NHibernate;

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
			if (objeto == null)
				return NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Usuario>().List<Dominio.Usuario>();

			IList<Dominio.Usuario> usuarios = NHibernate.HttpModule.RecuperarSessao.CreateCriteria<Dominio.Usuario>().List<Dominio.Usuario>().Where(usuario => usuario.Codigo == objeto.Codigo).ToList();

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