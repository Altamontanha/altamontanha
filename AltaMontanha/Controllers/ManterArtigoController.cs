using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
    public class ManterArtigoController : Controller
    {
		ConteudoFacade facade = new ConteudoFacade();

        //
        // GET: /ManterArtigo/
		public ActionResult Index()
        {
			IList<Artigo> artigos = facade.PesquisarArtigo(null);
            return View(artigos);
        }
        //
        // GET: /ManterArtigo/CadastrarArtigo
		public ActionResult CadastrarArtigo()
        {
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
            return View();
        } 
        //
		// POST: /ManterArtigo/CadastrarArtigo
        [HttpPost]
		public ActionResult CadastrarArtigo(Artigo artigo)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarArtigo(artigo);
					return RedirectToAction("Index");
				}
				else
				{
					ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null).ToList(), "Codigo", "Titulo");
					return View(artigo);
				}
			}
            catch
            {
				ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
				return View(artigo);
            }
        }
        //
        // GET: /ManterArtigo/AlterarArtigo/5
		public ActionResult AlterarArtigo(int Codigo)
        {
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
			// TODO: implementar sobrecarga
			return View(facade.PesquisarArtigo(new Artigo() { Codigo = Codigo }));
        }
        //
		// POST: /ManterArtigo/AlterarArtigo/5
        [HttpPost]
		public ActionResult AlterarArtigo(Artigo artigo)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarArtigo(artigo);
					return RedirectToAction("Index");
				}
				else
				{
					ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null).ToList(), "Codigo", "Titulo");
					return View(artigo);
				}
			}
            catch
            {
				ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
				return View(artigo);
            }
        }
        //
        // GET: /ManterArtigo/ExcluirArtigo/5
        public ActionResult ExcluirArtigo(int Codigo)
        {
			facade.ExcluirArtigo(Codigo);
            return RedirectToAction("Index");
        }
    }
}