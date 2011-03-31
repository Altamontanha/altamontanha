using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterFotoController : Controller
    {
        //
        // GET: /ManterFoto/
		public ActionResult Index()
        {
            return View();
        }
		//
        // GET: /ManterFoto/Details/5
		public ActionResult PesquisarFoto(int id)
        {
            return View();
        }
		//
        // GET: /ManterFoto/Create
        public ActionResult CarregarFoto()
        {
            return View();
        } 
        //
        // POST: /ManterFoto/Create
        [HttpPost]
		public ActionResult CarregarFoto(FormCollection collection)
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
        // GET: /ManterFoto/Edit/5
        public ActionResult SalvarFoto(int id)
        {
            return View();
        }
        //
        // POST: /ManterFoto/Edit/5
        [HttpPost]
		public ActionResult SalvarFoto(int id, FormCollection collection)
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
    }
}
