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
        // GET: /ManterCategoria/Details/5
        public ActionResult PesquisarCategoria(int id)
        {
            return View();
        }
        //
        // GET: /ManterCategoria/Create
        public ActionResult CarregarCategoria()
        {
            return View();
        } 
        //
        // POST: /ManterCategoria/Create
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
        // GET: /ManterCategoria/Edit/5
		public ActionResult SalvarCategoria(int id)
        {
            return View();
        }
        //
        // POST: /ManterCategoria/Edit/5
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
        // GET: /ManterCategoria/Delete/5
        public ActionResult ExcluirCategoria(int id)
        {
            return View();
        }
        //
        // POST: /ManterCategoria/Delete/5
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
