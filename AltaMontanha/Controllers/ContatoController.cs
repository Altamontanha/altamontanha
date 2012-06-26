using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Persistencia.Nhibernate;
using System.Collections.Specialized;
using AltaMontanha.Models;

namespace AltaMontanha.Controllers
{
    public class ContatoController : Controller
    {
        public ActionResult Index(string assunto = "")
        {
            Email email = new Email();

            if (assunto != "")
                email.Assunto = assunto;

            ViewBag.Codigo = 0;
            RegistrarBannerInternas();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string Nome, string EMail, string Assunto, string Comentario)
        {
            Email email = new Email()
            {
                Nome = Nome,
                EMail = EMail,
                Assunto = Assunto,
                Comentario = Comentario
            };

            int Codigo = email.Enviar();
            string mensagem = "";

            if (Codigo > 0)
            {
                mensagem = "Erro ao enviar o e-mail! Tente novamente mais tarde!";
            }
            else
            {
                mensagem = "E-mail enviado com sucesso!";
            }

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
