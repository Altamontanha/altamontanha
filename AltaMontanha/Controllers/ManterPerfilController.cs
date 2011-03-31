using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterPerfilController : Controller
    {
        //
        // GET: /ManterPerfil/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterPerfil/Details/5
        public ActionResult PesquisarPerfil(int id)
        {
            return View();
        }
        //
        // GET: /ManterPerfil/Create
        public ActionResult CarregarPerfil()
        {
            return View();
        } 
        //
        // POST: /ManterPerfil/Create
        [HttpPost]
		public ActionResult CarregarPerfil(FormCollection collection)
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
        // GET: /ManterPerfil/Edit/5
        public ActionResult SalvarPerfil(int id)
        {
            return View();
        }
        //
        // POST: /ManterPerfil/Edit/5
        [HttpPost]
        public ActionResult SalvarPerfil(int id, FormCollection collection)
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
        // GET: /ManterPerfil/Delete/5
        public ActionResult ExcluirPerfil(int id)
        {
            return View();
        }
        //
        // POST: /ManterPerfil/Delete/5
        [HttpPost]
		public ActionResult ExcluirPerfil(int id, FormCollection collection)
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
