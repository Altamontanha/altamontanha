using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
    public class ManterUsuarioController : Controller
    {
        //
        // GET: /ManterUsuario/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /ManterUsuario/Details/5
        public ActionResult PesquisarUsuario(int id)
        {
			UsuarioFacade usuarioFacade = new UsuarioFacade();
			
			if(id > 0)
				usuarioFacade.PesquisarUsuario(id);
			else
			{
				usuarioFacade.PesquisarUsuario(null);
				usuarioFacade.PesquisarUsuario(new Usuario(){Codigo=5});
			}

            return View();
        }
        //
        // GET: /ManterUsuario/Create
        public ActionResult CarregarUsuario()
        {
            return View();
        } 
        //
        // POST: /ManterUsuario/Create
        [HttpPost]
		public ActionResult CarregarUsuario(FormCollection collection)
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
        // GET: /ManterUsuario/Edit/5
        public ActionResult SalvarUsuario()
        {
			try
			{
				UsuarioFacade fachada = new UsuarioFacade();

				Usuario usuario = new Usuario()
				{
					//Codigo = 1,
					Nome  = "root",
					Login = "root",
					Senha = "#C0nd0R1r1#",
					Email = "root@altamontanha.com",
					//Foto  = new Foto(),
					Perfil = new Perfil() { Codigo = 1 }
				};

				fachada.SalvarUsuario(usuario);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
        //
        // POST: /ManterUsuario/Edit/5
        [HttpPost]
		public ActionResult SalvarUsuario(int id, FormCollection collection)
        {
            try
            {
				UsuarioFacade fachada = new UsuarioFacade();
				
				Usuario usuario = new Usuario()
				{
					Login = "root",
					Senha = "#C0nd0R1r1#",
					Perfil = new Perfil() { Codigo = 1 }
				};

				fachada.SalvarUsuario(usuario);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /ManterUsuario/Delete/5
        public ActionResult ExcluirUsuario(int id)
        {

			try
			{
				UsuarioFacade fachada = new UsuarioFacade();

				fachada.ExcluirUsuario(5);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
        }
        //
        // POST: /ManterUsuario/Delete/5
        [HttpPost]
		public ActionResult ExcluirUsuario(int id, FormCollection collection)
        {

			try
			{
				UsuarioFacade fachada = new UsuarioFacade();

				fachada.ExcluirUsuario(id);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
        }
    }
}
