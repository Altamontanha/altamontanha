using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterLinkController : Controller
    {
        //
        // GET: /ManterLink/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterLink/Details/5
        public ActionResult PesquisarLink(int id)
        {
            return View();
        }
        //
        // GET: /ManterLink/Create
        public ActionResult CarregarLink()
        {
            return View();
        } 
        //
        // POST: /ManterLink/Create
        [HttpPost]
		public ActionResult CarregarLink(FormCollection collection)
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
        // GET: /ManterLink/Edit/5
        public ActionResult SalvarLink(int id)
        {
            return View();
        }
        //
        // POST: /ManterLink/Edit/5
        [HttpPost]
		public ActionResult SalvarLink(int id, FormCollection collection)
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
        // GET: /ManterLink/Delete/5
        public ActionResult ExcluirLink(int id)
        {
            return View();
        }
        //
        // POST: /ManterLink/Delete/5
        [HttpPost]
		public ActionResult ExcluirLink(int id, FormCollection collection)
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
