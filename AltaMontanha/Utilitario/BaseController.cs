using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Web.Security;

namespace AltaMontanha.Utilitario
{
	public class BaseController : Controller
	{
		private static ILog log = LogManager.GetLogger("Log Altamontanha");

		/// <summary>
		/// Controle de Erros do sistema.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnException(ExceptionContext filterContext)
		{
			// TODO : Configurar Log.
			log.Debug(filterContext.Exception);
			base.OnException(filterContext);
		}
		/// <summary>
		/// No momento que é verificado o acesso á página
		/// verifica-se também se o usuário tem acesso ao formulário.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);

			Models.Fachada.UsuarioFacade fachada = new Models.Fachada.UsuarioFacade();
			Models.Dominio.Usuario usuario = new Models.Dominio.Usuario() { Login = HttpContext.User.Identity.Name };
			string controle = HttpContext.Request.Url.Segments[1].Replace("/","");
			string acao = (HttpContext.Request.Url.Segments.Count() > 2) ? HttpContext.Request.Url.Segments[2].Replace("/", "") : string.Empty;

			string tela = string.Format("/{0}/{1}", controle, acao);

			if(!fachada.VerificarAcesso(usuario, tela))
				FormsAuthentication.RedirectToLoginPage();
		}
	}
}