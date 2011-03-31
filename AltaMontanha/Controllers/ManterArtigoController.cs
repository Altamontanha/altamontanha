using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterArtigoController : Controller
    {
        //
        // GET: /ManterArtigo/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterArtigo/Details/5
        public ActionResult PesquisarArtigo(int id)
        {
            return View();
        }
        //
        // GET: /ManterArtigo/Create
        public ActionResult CarregarArtigo()
        {
            return View();
        } 
        //
        // POST: /ManterArtigo/Create
        [HttpPost]
		public ActionResult CarregarArtigo(FormCollection collection)
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
        // GET: /ManterArtigo/Edit/5
        public ActionResult SalvarArtigo(int id)
        {
            return View();
        }
        //
        // POST: /ManterArtigo/Edit/5
        [HttpPost]
		public ActionResult SalvarArtigo(int id, FormCollection collection)
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
        // GET: /ManterArtigo/Delete/5
        public ActionResult ExcluirArtigo(int id)
        {
            return View();
        }
        //
        // POST: /ManterArtigo/Delete/5
        [HttpPost]
		public ActionResult ExcluirArtigo(int id, FormCollection collection)
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
