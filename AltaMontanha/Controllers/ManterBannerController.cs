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
	public class ManterBannerController : Utilitario.BaseController
    {

        //
        // GET: /Banner/
        public ActionResult Index()
        {
            MultimidiaFacade facade = new MultimidiaFacade();
			IList<Banner> banners = facade.PesquisarBanner(null);
            return View(banners);
        }

		//
        // GET: /ManterBanner/CadastrarBanner
		public ActionResult CadastrarBanner()
        {
            MultimidiaFacade facade = new MultimidiaFacade();
			ViewData["Locais"] = new SelectList(facade.PesquisarLocal(null), "Codigo", "Descricao");
			return View();
        } 

        //
		// POST: /ManterBanner/CadastrarBanner
        [HttpPost]
        [ValidateInput(false)]
		public ActionResult CadastrarBanner(Banner banner, HttpPostedFileBase file)
        {
            MultimidiaFacade facade = new MultimidiaFacade();
            try
            {
				facade.SalvarBanner(banner, file);
                return RedirectToAction("Index");
            }
            catch
            {
				ViewData["Locais"] = new SelectList(facade.PesquisarLocal(null), "Codigo", "Descricao");
				return View(banner);
            }
        }
        
        //
		// GET: /ManterBanner/AlterarBanner/5
		public ActionResult AlterarBanner(int Codigo)
        {
            MultimidiaFacade facade = new MultimidiaFacade();
			Banner banner = facade.PesquisarBanner(Codigo);
			ViewData["Locais"] = new SelectList(facade.PesquisarLocal(null), "Codigo", "Descricao");
			return View(banner);
        }

        //
		// POST: /ManterBanner/AlterarBanner/5
        [HttpPost]
        [ValidateInput(false)]
		public ActionResult AlterarBanner(Banner banner, HttpPostedFileBase file)
        {
            MultimidiaFacade facade = new MultimidiaFacade();
            try
            {
				facade.SalvarBanner(banner, file);
                return RedirectToAction("Index");
            }
            catch
            {
				ViewData["Locais"] = new SelectList(facade.PesquisarLocal(null), "Codigo", "Descricao");
				return View(banner);
            }
        }

        //
        // GET: /ManterBanner/ExluirBanner/5
        public ActionResult ExcluirBanner(int Codigo)
        {
            MultimidiaFacade facade = new MultimidiaFacade();
			facade.ExcluirBanner(Codigo);
			return RedirectToAction("Index");
        }
    }
}
