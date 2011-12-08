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
		Models.Fachada.UsuarioFacade usuarioFacade = new Models.Fachada.UsuarioFacade();

		public ActionResult PesquisarNoticia()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			IList<Noticia> noticias = conteudoFacade.PesquisarNoticia(null);
			return View(noticias);
		}

		public ActionResult VisualizarNoticia(int Codigo)
		{
			Noticia noticia = conteudoFacade.PesquisarNoticia(Codigo);
			this.RegistrarBannerInternas();
			
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
			this.RegistrarBannerInternas();

			if (aventura == null)
				return RedirectToAction("Index");

			return View(aventura);
		}

		public ActionResult PesquisarColuna(int Codigo)
		{
			Models.Dominio.Coluna coluna = new Coluna() { Autor = new Usuario() { Codigo = Codigo } };

			IList<Coluna> colunas = conteudoFacade.PesquisarColuna(coluna);
			this.RegistrarBannerInternas();

			return View(colunas);
		}

		public ActionResult PesquisarColunista()
		{
			Usuario usuario = new Usuario(){ Perfil = new Perfil() { Codigo = 3 } };

			IList<Usuario> colunistas = usuarioFacade.PesquisarColunista();
			this.RegistrarBannerInternas();

			return View(colunistas);
		}
		
		public ActionResult VisualizarColuna(int Codigo)
		{
			Coluna coluna = conteudoFacade.PesquisarColuna(Codigo);
			this.RegistrarBannerInternas();
			
			if (coluna == null)
				return RedirectToAction("Index");

			return View(coluna);
		}
		
		public ActionResult RedirecionarConteudo()
		{
			int codigo = Convert.ToInt32(Request.QueryString["NewsID"]);

			Conteudo conteudo = conteudoFacade.PesquisarConteudo(codigo);
			
			HttpContext.Response.StatusCode = 302;
            HttpContext.Response.Clear();
			
			return RedirectToAction(string.Format("{0}/{1}/{2}", conteudo.GetType(), conteudo.Codigo, conteudo.Slug));
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
			this.RegistrarBannerInternas();

			if (artigo == null)
				return RedirectToAction("Index");
			return View(artigo);
		}

		public ActionResult VisualizarBusca()
		{
			ViewData["BannerBaixo"] = multimidiaFacade.PesquisarBannerPorLocal(3);

			return View(ViewData);
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
			ViewData["ListaColunas"] = conteudoFacade.PesquisarColuna(null, 6, true);
			ViewData["ListaAventuras"] = conteudoFacade.PesquisarAventura(null,5);
			ViewData["ListaArtigos"] = conteudoFacade.PesquisarArtigoArtigoTecnico(null);
			ViewData["ListaArtigosHistoria"] = conteudoFacade.PesquisarArtigoHistoria(null);

			return View(ViewData);
        }
		
		private void RegistrarBannerInternas()
		{
			ViewData["BannerInterna"] = multimidiaFacade.PesquisarBannerPorLocal(4);
			ViewData["BannerBaixo"] = multimidiaFacade.PesquisarBannerPorLocal(3);
		}
    }
}
