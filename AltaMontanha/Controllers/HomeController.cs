using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
    public class HomeController : Controller
    {
		Models.Fachada.ConteudoFacade conteudoFacade = new Models.Fachada.ConteudoFacade();

        //
        // GET: /Home/
        public ActionResult Index()
        {
			ViewData["ListaNoticiasDestaque"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = true }, 7);
			ViewData["ListaNoticias"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = false }, 4);
			ViewData["ListaColunas"] = conteudoFacade.PesquisarColuna(null, 6);
			ViewData["ListaAventuras"] = conteudoFacade.PesquisarAventura(null,5);
			ViewData["ListaArtigos"] = conteudoFacade.PesquisarArtigo(null, 4);
			//ViewData["ListaArtigosHistoria"] = new List<Artigo>();
			
			// TODO : Corrigir erro no filtro de categoria...
			Artigo filtro = new Artigo();
			filtro.ObjCategoria = new Categoria() { Codigo = 1 };
			ViewData["ListaArtigosHistoria"] = conteudoFacade.PesquisarArtigo(filtro, 2);

			return View(ViewData);
        }
    }
}
