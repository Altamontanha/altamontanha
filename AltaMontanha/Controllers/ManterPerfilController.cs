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
	public class ManterPerfilController : Utilitario.BaseController
    {
		UsuarioFacade facade = new UsuarioFacade();

        //
        // GET: /ManterPerfil/
		[Authorize]
        public ActionResult Index()
        {
            return View(facade.PesquisarPerfil(null));
        }
        //
        // GET: /ManterPerfil/VisualizarPerfil/5
		[Authorize]
		public ActionResult VisualizarPerfil(int Codigo)
        {
			// TODO: adicionar overload
			IList<Perfil> perfis = facade.PesquisarPerfil(new Perfil() { Codigo = Codigo });

            return View(perfis[0]);
        }
        //
		// GET: /ManterPerfil/CadastrarPerfil
		[Authorize]
		public ActionResult CadastrarPerfil()
        {
            return View();
        } 
        //
		// POST: /ManterPerfil/CadastrarPerfil
		[Authorize]
        [HttpPost]
		public ActionResult CadastrarPerfil(Perfil perfil)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarPerfil(perfil);
					return RedirectToAction("Index");
				}
				else
				{
					return View(perfil);
				}
            }
            catch
            {
//                return View(perfil);
				// TODO: verificar erro object to int32
				return RedirectToAction("Index");
			}
        }
        //
        // GET: /ManterPerfil/AlterarPerfil/5
		[Authorize]
		public ActionResult AlterarPerfil(int Codigo)
        {
			// TODO: adicionar overload
			IList<Perfil> perfis = facade.PesquisarPerfil(new Perfil() { Codigo = Codigo });

			return View(perfis[0]);
		}
        //
		// POST: /ManterPerfil/AlterarPerfil/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarPerfil(Perfil perfil)
        {
			try
			{
				if (ModelState.IsValid)
				{
					facade.SalvarPerfil(perfil);
					return RedirectToAction("Index");
				}
				else
				{
					return View(perfil);
				}
			}
			catch
			{
				return View();
			}
		}
		//
        // GET: /ManterPerfil/ExcluirPerfil/5
		[Authorize]
		public ActionResult ExcluirPerfil(int Codigo)
        {
			facade.ExcluirPerfil(Codigo);
			return RedirectToAction("Index");
        }
    }
}
