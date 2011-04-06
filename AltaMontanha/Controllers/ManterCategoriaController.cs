using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterCategoriaController : Controller
    {
        //
        // GET: /ManterCategoria/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterCategoria/VisualizarUsuario/5
        public ActionResult PesquisarCategoria(int id)
        {
            return View();
        }
        //
        // GET: /ManterCategoria/CadastrarUsuario
        public ActionResult CarregarCategoria()
        {
            return View();
        } 
        //
        // POST: /ManterCategoria/CadastrarUsuario
        [HttpPost]
		public ActionResult CarregarCategoria(FormCollection collection)
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
        // GET: /ManterCategoria/AlterarUsuario/5
		public ActionResult SalvarCategoria(int id)
        {
            return View();
        }
        //
        // POST: /ManterCategoria/AlterarUsuario/5
        [HttpPost]
		public ActionResult SalvarCategoria(int id, FormCollection collection)
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
        // GET: /ManterCategoria/ExcluirUsuario/5
        public ActionResult ExcluirCategoria(int id)
        {
            return View();
        }
        //
        // POST: /ManterCategoria/ExcluirUsuario/5
        [HttpPost]
		public ActionResult ExcluirCategoria(int id, FormCollection collection)
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
