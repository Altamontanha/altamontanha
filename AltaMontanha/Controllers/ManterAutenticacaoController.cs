using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterAutenticacaoController : Controller
    {
		//
        // GET: /ManterAutenticacao/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterAutenticacao/CadastrarUsuario
        public ActionResult CarregarAutenticacao()
        {
            return View();
        } 
        //
        // POST: /ManterAutenticacao/CadastrarUsuario
        [HttpPost]
		public ActionResult CarregarAutenticacao(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		public ActionResult Autenticar()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Autenticar(FormCollection collection)
		{
			return View();
		}
    }
}
