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

        //
        // GET: /ManterNoticia/
        [Authorize]
        public ActionResult Index()
        {
            ConteudoFacade facade = new ConteudoFacade();
            IList<Noticia> noticias = facade.PesquisarNoticia(null);
            return View(noticias);
        }

        //
        // GET: /ManterNoticia/CadastrarNoticia
        [Authorize]
        public ActionResult CadastrarNoticia()
        {
            Noticia noticia = new Noticia()
            {
                Data = DateTime.Now
            };
            return View(noticia);
        }

        //
        // POST: /ManterNoticia/CadastrarNoticia
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CadastrarNoticia(Noticia noticia)
        {
            ConteudoFacade facade = new ConteudoFacade();
            try
            {
                noticia.Codigo = 0;
                facade.SalvarNoticia(noticia);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        //
        // GET: /ManterNoticia/AlterarNoticia/5
        [Authorize]
        public ActionResult AlterarNoticia(int Codigo)
        {
            ConteudoFacade facade = new ConteudoFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
            try
            {
                facade.SalvarNoticia(noticia);

                return Redirect("~/ManterNoticia");
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
            ConteudoFacade facade = new ConteudoFacade();
            facade.ExcluirNoticia(Codigo);
            return RedirectToAction("Index");
        }
    }
}