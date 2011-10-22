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
	public class ManterNoticiaController : Utilitario.BaseController
    {
		private ConteudoFacade facade = new ConteudoFacade();

        //
        // GET: /ManterNoticia/
		[Authorize]
		public ActionResult Index()
        {
			IList<Noticia> noticias = facade.PesquisarNoticia(null);
            return View(noticias);
        }

        //
        // GET: /ManterNoticia/CadastrarNoticia
		[Authorize]
		public ActionResult CadastrarNoticia()
        {
            return View();
        } 

        //
		// POST: /ManterNoticia/CadastrarNoticia
		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult CadastrarNoticia(Noticia noticia)
        {
			try
			{
				facade.SalvarNoticia(noticia);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(noticia);
			}
        }
        
        //
		// GET: /ManterNoticia/AlterarNoticia/5
		[Authorize]
		public ActionResult AlterarNoticia(int Codigo)
        {
			Noticia noticia = facade.PesquisarNoticia(Codigo);

            return View(noticia);
        }

        //
		// POST: /ManterNoticia/AlterarNoticia/5
		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult AlterarNoticia(Noticia noticia)
        {
            try
            {
				facade.SalvarNoticia(noticia);
				return RedirectToAction("Index");
			}
            catch
            {
                return View(noticia);
            }
        }

        //
		// GET: /ManterNoticia/ExcluirNoticia/5
		[Authorize]
		public ActionResult ExcluirNoticia(int Codigo)
        {
			facade.ExcluirNoticia(Codigo);
			return RedirectToAction("Index");
        }
    }
}