using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AltaMontanha.Controllers
{
	public class ManterAutenticacaoController : Controller
    {
		Models.Fachada.UsuarioFacade fachada = new Models.Fachada.UsuarioFacade();

		//
		// GET: /ManterAutenticacaoController/Autenticar
		public ActionResult Autenticar()
		{
			return View();
		}
		//
		// POST: /ManterAutenticacaoController/Autenticar
		[HttpPost]
		public ActionResult Autenticar(Models.Dominio.Usuario usuario, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (fachada.AutenticarUsuario(usuario))
				{
					FormsAuthentication.SetAuthCookie(usuario.Login, false);

					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
							&& returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
							&& !returnUrl.StartsWith("/\\"))
						return Redirect(returnUrl);
					else
						return Redirect("/ManterUsuario");
				}
				else
				{
					ModelState.AddModelError("", "Usuário ou senha inválidos!");
				}
			}
						
			return View(usuario);
		}
		/// <summary>
		/// Finaliza a autenticação do Usuário.
		/// </summary>
		/// <returns></returns>
		public ActionResult Sair()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Autenticar");
		}
    }
}
