using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterNoticiaController : Controller
    {
        //
        // GET: /ManterNoticia/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterNoticia/VisualizarUsuario/5
        public ActionResult PesquisarNoticia(int id)
        {
            return View();
        }
        //
        // GET: /ManterNoticia/CadastrarUsuario
        public ActionResult CarregarNoticia()
        {
            return View();
        } 
        //
        // POST: /ManterNoticia/CadastrarUsuario
        [HttpPost]
		public ActionResult CarregarNoticia(FormCollection collection)
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
        //
        // GET: /ManterNoticia/AlterarUsuario/5
        public ActionResult SalvarNoticia(int id)
        {
            return View();
        }
        //
        // POST: /ManterNoticia/AlterarUsuario/5
        [HttpPost]
		public ActionResult SalvarNoticia(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /ManterNoticia/ExcluirUsuario/5
        public ActionResult ExcluirNoticia(int id)
        {
            return View();
        }
        //
        // POST: /ManterNoticia/ExcluirUsuario/5
        [HttpPost]
		public ActionResult ExcluirNoticia(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
