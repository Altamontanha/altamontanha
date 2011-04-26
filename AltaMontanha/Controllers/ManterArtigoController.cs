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
				if (ModelState.IsValid)
				{
					// TODO: pegar usuário logado
					artigo.UsuarioCadastro = new Usuario() { Codigo = 1 };
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
				//ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
				//return View(artigo);
				// TODO: verificar erro object to int32
				return RedirectToAction("Index");
            }
        }
        //
        // GET: /ManterArtigo/AlterarArtigo/5
		[Authorize]
		public ActionResult AlterarArtigo(int Codigo)
        {
			ViewData["Categorias"] = new SelectList(facade.PesquisarCategoria(null), "Codigo", "Titulo");
			// TODO: implementar sobrecarga
			IList<Artigo> artigos = facade.PesquisarArtigo(new Artigo() { Codigo = Codigo });
			return View(artigos[0]);
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
				if (ModelState.IsValid)
				{
					// TODO: verificar como não validar usuário no alterar
					artigo.UsuarioCadastro = new Usuario() { Codigo = 1 };
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
		[Authorize]
        public ActionResult ExcluirArtigo(int Codigo)
        {
			facade.ExcluirArtigo(Codigo);
            return RedirectToAction("Index");
        }
    }
}