using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterBannerController : Controller
    {
        //
        // GET: /ManterBanner/
		public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterBanner/Details/5
        public ActionResult PesquisarBanner(int id)
        {
            return View();
        }
        //
        // GET: /ManterBanner/Create
        public ActionResult CarregarBanner()
        {
            return View();
        } 
        //
        // POST: /ManterBanner/Create
        [HttpPost]
		public ActionResult CarregarBanner(FormCollection collection)
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
        // GET: /ManterBanner/Edit/5
		public ActionResult SalvarBanner(int id)
        {
            return View();
        }
        //
        // POST: /ManterBanner/Edit/5
        [HttpPost]
		public ActionResult SalvarBanner(int id, FormCollection collection)
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
        // GET: /ManterBanner/Delete/5
		public ActionResult ExcluirBanner(int id)
        {
            return View();
        }
        //
        // POST: /ManterBanner/Delete/5
        [HttpPost]
		public ActionResult ExcluirBanner(int id, FormCollection collection)
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
