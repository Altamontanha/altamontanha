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

		//
		// GET: /ManterAventura/
		[Authorize]
		public ActionResult Index()
        {
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
			IList<Aventura> aventuras = facade.PesquisarAventura(null);
			return View(aventuras);
		}
		//
		// GET: /ManterAventura/CadastrarAventura
		[Authorize]
		public ActionResult CadastrarAventura()
        {
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
			ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
			ViewData["AventurasAnteriores"] = new SelectList(facade.PesquisarAventura(null), "Codigo", "Titulo");

            Aventura aventura = new Aventura()
            {
                Data = DateTime.Now
            };
            
			return View(aventura);
		}
		//
		// POST: /ManterAventura/CadastrarAventura
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarAventura(Aventura aventura, HttpPostedFileBase Rota)
        {
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
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

            return RedirectToAction("Index");
		}
		//
		// GET: /ManterAventura/AlterarAventura/5
		[Authorize]
		public ActionResult AlterarAventura(int Codigo)
        {
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
			facade.SalvarAventura(aventura, file);
			//TODO: throw new Exception("hello");
			return RedirectToAction("Index");
		}
		//
		// GET: /ManterAventura/ExcluirAventura/5
		[Authorize]
		public ActionResult ExcluirAventura(int Codigo)
        {
            ConteudoFacade facade = new ConteudoFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();
			facade.ExcluirAventura(Codigo);
			return RedirectToAction("Index");
		}
	}
}