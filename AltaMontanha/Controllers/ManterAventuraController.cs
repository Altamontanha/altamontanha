using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterAventuraController : Controller
    {
        //
        // GET: /ManterAventura/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterAventura/VisualizarUsuario/5
        public ActionResult PesquisarAventura(int id)
        {
            return View();
        }
        //
        // GET: /ManterAventura/CadastrarUsuario
        public ActionResult CarregarAventura()
        {
            return View();
        } 
        //
        // POST: /ManterAventura/CadastrarUsuario
        [HttpPost]
		public ActionResult CarregarAventura(FormCollection collection)
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
        // GET: /ManterAventura/AlterarUsuario/5
        public ActionResult SalvarAventura(int id)
        {
            return View();
        }
        //
        // POST: /ManterAventura/AlterarUsuario/5
        [HttpPost]
		public ActionResult SalvarAventura(int id, FormCollection collection)
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
        // GET: /ManterAventura/ExcluirUsuario/5
		public ActionResult ExcluirAventura(int id)
        {
            return View();
        }
        //
        // POST: /ManterAventura/ExcluirUsuario/5
        [HttpPost]
		public ActionResult ExcluirAventura(int id, FormCollection collection)
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
