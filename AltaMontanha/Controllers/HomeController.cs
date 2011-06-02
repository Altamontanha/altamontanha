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
		Models.Fachada.MultimidiaFacade multimidiaFacade = new Models.Fachada.MultimidiaFacade();

		public ActionResult PesquisarNoticia()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			IList<Noticia> noticias = conteudoFacade.PesquisarNoticia(null);
			return View(noticias);
		}

		public ActionResult VisualizarNoticia(int Codigo)
		{
			Noticia noticia = conteudoFacade.PesquisarNoticia(Codigo);
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);

			if (noticia == null)
				return RedirectToAction("Index");

			return View(noticia);
		}

		public ActionResult PesquisarAventura()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			IList<Aventura> aventuras = conteudoFacade.PesquisarAventura(null);
			return View(aventuras);
		}

		public ActionResult VisualizarAventura(int Codigo)
		{
			Aventura aventura = conteudoFacade.PesquisarAventura(Codigo);
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);

			if (aventura == null)
				return RedirectToAction("Index");

			return View(aventura);
		}

		public ActionResult PesquisarColuna()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			IList<Coluna> colunas = conteudoFacade.PesquisarColuna(null);
			return View(colunas);
		}

		public ActionResult VisualizarColuna(int Codigo)
		{
			Coluna coluna = conteudoFacade.PesquisarColuna(Codigo);
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);

			if (coluna == null)
				return RedirectToAction("Index");

			return View(coluna);
		}

		public ActionResult PesquisarArtigo()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			IList<Artigo> artigos = conteudoFacade.PesquisarArtigo(null);
			return View(artigos);
		}

		public ActionResult VisualizarArtigo(int Codigo)
		{
			Artigo artigo = conteudoFacade.PesquisarArtigo(Codigo);
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			if (artigo == null)
				return RedirectToAction("Index");
			return View(artigo);
		}
        //
        // GET: /Home/
        public ActionResult Index()
        {
			ViewData["BannerCima"] = multimidiaFacade.PesquisarBannerPorLocal(1);
			ViewData["BannerMeio"] = multimidiaFacade.PesquisarBannerPorLocal(2);
			ViewData["BannerBaixo"] = multimidiaFacade.PesquisarBannerPorLocal(3);
			ViewData["ListaNoticiasDestaque"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = true }, 7);
			ViewData["ListaNoticias"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = false }, 4);
			ViewData["ListaColunas"] = conteudoFacade.PesquisarColuna(null, 6);
			ViewData["ListaAventuras"] = conteudoFacade.PesquisarAventura(null,5);
			ViewData["ListaArtigos"] = conteudoFacade.PesquisarArtigo(null, 4);

			Artigo filtro = new Artigo();
			filtro.ObjCategoria = new Categoria() { Codigo = 1 };
			ViewData["ListaArtigosHistoria"] = conteudoFacade.PesquisarArtigo(filtro, 2);

			return View(ViewData);
        }
    }
}
