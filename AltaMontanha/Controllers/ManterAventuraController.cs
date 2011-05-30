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
			ViewData["AventurasAnteriores"] = new SelectList(facade.PesquisarAventura(null), "Codigo", "Titulo");
			return View();
		}
		//
		// POST: /ManterAventura/CadastrarAventura
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarAventura(Aventura aventura, HttpPostedFileBase Rota)
		{
			try
			{
				facade.SalvarAventura(aventura, Rota);
				
				return RedirectToAction("Index");
			}
			catch
			{
				ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
				ViewData["AventurasAnteriores"] = new SelectList(facade.PesquisarAventura(null), "Codigo", "Titulo");
				return View(aventura);
			}
		}
		//
		// GET: /ManterAventura/AlterarAventura/5
		[Authorize]
		public ActionResult AlterarAventura(int Codigo)
		{
			ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
			ViewData["AventurasAnteriores"] = new SelectList(facade.PesquisarAventura(null), "Codigo", "Titulo");
			Aventura aventura = facade.PesquisarAventura(Codigo);

			return View(aventura);
		}
		//
		// POST: /ManterAventura/AlterarAventura/5
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult AlterarAventura(Aventura aventura, HttpPostedFileBase file)
		{
			facade.SalvarAventura(aventura, file);
			//TODO: throw new Exception("hello");
			return RedirectToAction("Index");
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