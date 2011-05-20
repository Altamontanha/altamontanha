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
    public class ManterArtigoController : Utilitario.BaseController
    {
		ConteudoFacade facade = new ConteudoFacade();

        //
        // GET: /ManterArtigo/
		[Authorize]
		public ActionResult Index()
        {
			IList<Artigo> artigos = facade.PesquisarArtigo(null);
            return View(artigos);
        }
        //
        // GET: /ManterArtigo/CadastrarArtigo
		[Authorize]
		public ActionResult CadastrarArtigo()
        {
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
            return View();
        } 
        //
		// POST: /ManterArtigo/CadastrarArtigo
        [HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarArtigo(Artigo artigo)
        {
            try
            {
				facade.SalvarArtigo(artigo);
				return RedirectToAction("Index");
			}
            catch
            {
				ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
				return View(artigo);
            }
        }
        //
        // GET: /ManterArtigo/AlterarArtigo/5
		[Authorize]
		public ActionResult AlterarArtigo(int Codigo)
        {
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
			Artigo artigo = facade.PesquisarArtigo(Codigo);
			return View(artigo);
        }
        //
		// POST: /ManterArtigo/AlterarArtigo/5
        [HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult AlterarArtigo(Artigo artigo)
        {
            try
            {
				facade.SalvarArtigo(artigo);
				return RedirectToAction("Index");
			}
            catch
            {
				ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
				return View(artigo);
            }
        }
        //
        // GET: /ManterArtigo/ExcluirArtigo/5
		[Authorize]
        public ActionResult ExcluirArtigo(int Codigo)
        {
			facade.ExcluirArtigo(Codigo);
            return RedirectToAction("Index");
        }
    }
}