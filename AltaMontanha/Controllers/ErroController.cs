using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
    public class ErroController : Controller
    {
        //
        // GET: /Erro/

        public ActionResult Index(string mensagem = "")
        {
            ViewBag.Mensagem = mensagem;
            RegistrarBannerInternas();

            return View();
        }

        private void RegistrarBannerInternas()
        {
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();

            ViewData["BannerSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(8);
            ViewData["BannerInferior"] = multimidiaFacade.PesquisarBannerPorLocal(9);

            ViewData["BannerInternaSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(4);
            ViewData["BannerInternaInferior"] = multimidiaFacade.PesquisarBannerPorLocal(7);
        }
    }
}
