using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Utilitario
{
	public static class Sessao
	{
		private const string chaveFotos = "6565ASDASD4-ASDD3366ASD-PE566775252";

		public static IList<Models.Dominio.Foto> ListaFotos
		{
			get 
			{
				return (IList<Models.Dominio.Foto>) HttpContext.Current.Session[chaveFotos];
			}
			set 
			{
				HttpContext.Current.Session.Add(chaveFotos, value);
			}
		}
		/// <summary>
		/// Recupera o usuário logado.
		/// </summary>
		public static Usuario UsuarioLogado
		{
			get 
			{
				IList<Usuario> listaUsuarios = (new UsuarioFacade()).PesquisarUsuario(new Usuario() { Login = HttpContext.Current.User.Identity.Name });

				if (listaUsuarios.Count() != 1)
					return null;

				return listaUsuarios[0];
			}
		}
	}
}