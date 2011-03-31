using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterColunaController : Controller
    {
        //
        // GET: /ManterColuna/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterColuna/Details/5
        public ActionResult PesquisarColuna(int id)
        {
            return View();
        }
        //
        // GET: /ManterColuna/Create
        public ActionResult CarregarColuna()
        {
            return View();
        } 
        //
        // POST: /ManterColuna/Create
        [HttpPost]
		public ActionResult CarregarColuna(FormCollection collection)
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
        // GET: /ManterColuna/Edit/5
        public ActionResult SalvarColuna(int id)
        {
            return View();
        }
        //
        // POST: /ManterColuna/Edit/5
        [HttpPost]
		public ActionResult SalvarColuna(int id, FormCollection collection)
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
        // GET: /ManterColuna/Delete/5
		public ActionResult ExcluirColuna(int id)
        {
            return View();
        }
        //
        // POST: /ManterColuna/Delete/5
        [HttpPost]
		public ActionResult ExcluirColuna(int id, FormCollection collection)
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
