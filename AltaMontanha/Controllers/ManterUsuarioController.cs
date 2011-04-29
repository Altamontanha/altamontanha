using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
	[HandleError]
    public class ManterUsuarioController : Utilitario.BaseController
    {
		UsuarioFacade facade = new UsuarioFacade();

        //
        // GET: /ManterUsuario/
		[Authorize]
		public ActionResult Index()
        {
			IList<Usuario> usuarios = facade.PesquisarUsuario(null);
            return View(usuarios);
        }
        //
        // GET: /ManterUsuario/VisualizarUsuario/5
		[Authorize]
		public ActionResult VisualizarUsuario(int Codigo)
        {
			return View(facade.PesquisarUsuario(Codigo));
        }
		//
        // GET: /ManterUsuario/CadastrarUsuario
		[Authorize]
		public ActionResult CadastrarUsuario()
        {
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");

            return View();
        } 
		//
        // POST: /ManterUsuario/CadastrarUsuario
        [HttpPost]
		[Authorize]
		public ActionResult CadastrarUsuario(Usuario usuario)
        {
			try
			{
				facade.SalvarUsuario(usuario);
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
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");
			return View(facade.PesquisarUsuario(Codigo));
        }
		//
        // POST: /ManterUsuario/AlterarUsuario/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarUsuario(Usuario usuario)
        {
			//TODO: na alteração a senha não é obrigatória (?)
            try
            {
				facade.SalvarUsuario(usuario);
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
			facade.ExcluirUsuario(Codigo);
            return RedirectToAction("Index");
        }
    }
}