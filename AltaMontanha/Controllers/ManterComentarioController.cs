using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltaMontanha.Controllers
{
    public class ManterComentarioController : Controller
    {
        //
        // GET: /ManterComentario/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterComentario/VisualizarUsuario/5
        public ActionResult PesquisarComentario(int id)
        {
            return View();
        }
        //
        // GET: /ManterComentario/CadastrarUsuario
		public ActionResult CarregarComentario()
        {
            return View();
        } 
        //
        // POST: /ManterComentario/CadastrarUsuario
        [HttpPost]
		public ActionResult CarregarComentario(FormCollection collection)
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
    }
}