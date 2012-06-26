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

        //
        // GET: /ManterArtigo/
		[Authorize]
		public ActionResult Index()
        {
            ConteudoFacade facade = new ConteudoFacade();
			IList<Artigo> artigos = facade.PesquisarArtigo(null);
            return View(artigos);
        }
        //
        // GET: /ManterArtigo/CadastrarArtigo
		[Authorize]
		public ActionResult CadastrarArtigo()
        {
            ConteudoFacade facade = new ConteudoFacade();
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");

            Artigo artigo = new Artigo()
            {
                Data = DateTime.Now
            };
            
            return View(artigo);
        } 
        //
		// POST: /ManterArtigo/CadastrarArtigo
        [HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarArtigo(Artigo artigo)
        {
            ConteudoFacade facade = new ConteudoFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
			Artigo artigo = facade.PesquisarArtigo(Codigo);
			return View(artigo);
        }
        //
		// POST: /ManterArtigo/AlterarArtigo/5
        [HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult AlterarArtigo(Artigo artigo, FormCollection collection)
        {
            ConteudoFacade facade = new ConteudoFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
			facade.ExcluirArtigo(Codigo);
            return RedirectToAction("Index");
        }
    }
}