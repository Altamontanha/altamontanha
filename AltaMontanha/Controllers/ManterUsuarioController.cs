using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
    public class ManterUsuarioController : Controller
    {
		UsuarioFacade facade = new UsuarioFacade();

        //
        // GET: /ManterUsuario/

        public ActionResult Index()
        {
			IList<Usuario> usuarios = facade.PesquisarUsuario(null);
            return View(usuarios);
        }

        //
        // GET: /ManterUsuario/VisualizarUsuario/5

        public ActionResult VisualizarUsuario(int Codigo)
        {
			return View(facade.PesquisarUsuario(Codigo));
        }

        //
        // GET: /ManterUsuario/CadastrarUsuario

        public ActionResult CadastrarUsuario()
        {
            return View();
        } 

        //
        // POST: /ManterUsuario/CadastrarUsuario

        [HttpPost]
		public ActionResult CadastrarUsuario(Usuario usuario)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarUsuario(usuario);
					return RedirectToAction("Index");
				}
				else
				{
					return View(usuario);
				}
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ManterUsuario/AlterarUsuario/5
 
        public ActionResult AlterarUsuario(int Codigo)
        {
            return View(facade.PesquisarUsuario(Codigo));
        }

        //
        // POST: /ManterUsuario/AlterarUsuario/5

        [HttpPost]
		public ActionResult AlterarUsuario(Usuario usuario)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarUsuario(usuario);
					return RedirectToAction("Index");
				}
				else
				{
					return View(usuario);
				}
			}
            catch
            {
                return View();
            }
        }

        //
        // GET: /ManterUsuario/ExcluirUsuario/5
 
        public ActionResult ExcluirUsuario(int Codigo)
        {
			facade.ExcluirUsuario(Codigo);
            return RedirectToAction("Index");
        }
    }
}
