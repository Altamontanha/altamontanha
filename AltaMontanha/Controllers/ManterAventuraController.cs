using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
	[HandleError]
	public class ManterAventuraController : Utilitario.BaseController
	{
		ConteudoFacade facade = new ConteudoFacade();
		UsuarioFacade usuarioFacade = new UsuarioFacade();

		//
		// GET: /ManterAventura/
		[Authorize]
		public ActionResult Index()
		{
			IList<Aventura> aventuras = facade.PesquisarAventura(null);
			return View(aventuras);
		}
		//
		// GET: /ManterAventura/CadastrarAventura
		[Authorize]
		public ActionResult CadastrarAventura()
		{
			ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
			return View();
		}
		//
		// POST: /ManterAventura/CadastrarAventura
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarAventura(Aventura aventura)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: alterar para usuário logado
					aventura.UsuarioCadastro = new Usuario() { Codigo = 1 };
					facade.SalvarAventura(aventura);
					return RedirectToAction("Index");
				}
				else
				{
					//TODO: corrigir
					//return View(aventura);
					return RedirectToAction("Index");
				}
			}
			catch
			{
				ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
				return View(aventura);
			}
		}
		//
		// GET: /ManterAventura/AlterarAventura/5
		[Authorize]
		public ActionResult AlterarAventura(int Codigo)
		{
			Aventura aventura = facade.PesquisarAventura(Codigo);

			return View(aventura);
		}
		//
		// POST: /ManterAventura/AlterarAventura/5
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult AlterarAventura(Aventura aventura)
		{
			try
			{
				if (ModelState.IsValid)
				{
					facade.SalvarAventura(aventura);
					return RedirectToAction("Index");
				}
				else
				{
					return View(aventura);
				}
			}
			catch
			{
				ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
				return View(aventura);
			}
		}
		//
		// GET: /ManterAventura/ExcluirAventura/5
		[Authorize]
		public ActionResult ExcluirAventura(int Codigo)
		{
			facade.ExcluirAventura(Codigo);
			return RedirectToAction("Index");
		}
	}
}