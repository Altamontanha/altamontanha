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
        // GET: /ManterNoticia/Details/5
        public ActionResult PesquisarNoticia(int id)
        {
            return View();
        }
        //
        // GET: /ManterNoticia/Create
        public ActionResult CarregarNoticia()
        {
            return View();
        } 
        //
        // POST: /ManterNoticia/Create
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
        // GET: /ManterNoticia/Edit/5
        public ActionResult SalvarNoticia(int id)
        {
            return View();
        }
        //
        // POST: /ManterNoticia/Edit/5
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
        // GET: /ManterNoticia/Delete/5
        public ActionResult ExcluirNoticia(int id)
        {
            return View();
        }
        //
        // POST: /ManterNoticia/Delete/5
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
