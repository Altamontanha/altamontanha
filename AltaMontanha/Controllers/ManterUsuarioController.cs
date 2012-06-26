using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;
using System.Collections;

namespace AltaMontanha.Controllers
{
	[HandleError]
    public class ManterUsuarioController : Utilitario.BaseController
    {

        //
        // GET: /ManterUsuario/
		[Authorize]
		public ActionResult Index()
        {
            UsuarioFacade facade = new UsuarioFacade();
			IList<Usuario> usuarios = facade.PesquisarUsuario(null);
            return View(usuarios);
        }
        //
        // GET: /ManterUsuario/VisualizarUsuario/5
		[Authorize]
		public ActionResult VisualizarUsuario(int Codigo)
        {
            UsuarioFacade facade = new UsuarioFacade();
			return View(facade.PesquisarUsuario(Codigo));
        }
		//
        // GET: /ManterUsuario/CadastrarUsuario
		[Authorize]
		public ActionResult CadastrarUsuario()
        {
            UsuarioFacade facade = new UsuarioFacade();
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");

            return View();
        } 
		//
        // POST: /ManterUsuario/CadastrarUsuario
        [HttpPost]
		[Authorize]
		public ActionResult CadastrarUsuario(Usuario usuario, HttpPostedFileBase file)
        {
            UsuarioFacade facade = new UsuarioFacade();
			try
			{
				facade.SalvarUsuario(usuario, file);
				return RedirectToAction("Index");
			}
			catch
			{
				ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");
				return View(usuario);
			}
        }
        //
        // GET: /ManterUsuario/AlterarUsuario/5
		[Authorize]
        public ActionResult AlterarUsuario(int Codigo)
        {
            UsuarioFacade facade = new UsuarioFacade();
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");

            Usuario usuario = facade.PesquisarUsuario(Codigo);

            return View(usuario);
        }
		//
        // POST: /ManterUsuario/AlterarUsuario/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarUsuario(Usuario usuario, HttpPostedFileBase file)
        {
            UsuarioFacade facade = new UsuarioFacade();
			//TODO: na alteração a senha não é obrigatória (?)
            try
            {
				facade.SalvarUsuario(usuario, file);
				return RedirectToAction("Index");
			}
            catch
            {
				ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");
				return View(usuario);
            }
        }
        //
        // GET: /ManterUsuario/ExcluirUsuario/5
		[Authorize]
        public ActionResult ExcluirUsuario(int Codigo)
        {
            UsuarioFacade facade = new UsuarioFacade();
			facade.ExcluirUsuario(Codigo);
            return RedirectToAction("Index");
        }
    }
}