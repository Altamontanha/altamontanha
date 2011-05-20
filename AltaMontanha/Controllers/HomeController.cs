using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
			ViewData["ListaNoticiasDestaque"] = new List<Models.Dominio.Noticia>();
			ViewData["ListaNoticias"] = new List<Models.Dominio.Noticia>();
			ViewData["ListaColunas"] = new List<Models.Dominio.Coluna>();
			ViewData["ListaArtigos"] = new List<Models.Dominio.Artigo>();
			ViewData["ListaArtigosHistoria"] = new List<Models.Dominio.Artigo>();
			ViewData["ListaAventuras"] = new List<Models.Dominio.Aventura>();

            return View();
        }
    }
}
