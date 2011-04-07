using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
    public class ManterPerfilController : Controller
    {
		UsuarioFacade facade = new UsuarioFacade();

        //
        // GET: /ManterPerfil/

        public ActionResult Index()
        {
            return View(facade.PesquisarPerfil(null));
        }

        //
        // GET: /ManterPerfil/VisualizarPerfil/5

		public ActionResult VisualizarPerfil(int Codigo)
        {
			// TODO: adicionar overload
			IList<Perfil> perfis = facade.PesquisarPerfil(new Perfil() { Codigo = Codigo });

            return View(perfis[0]);
        }

        //
		// GET: /ManterPerfil/CadastrarPerfil

		public ActionResult CadastrarPerfil()
        {
            return View();
        } 

        //
		// POST: /ManterPerfil/CadastrarPerfil

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

		public ActionResult AlterarPerfil(int Codigo)
        {
			// TODO: adicionar overload
			IList<Perfil> perfis = facade.PesquisarPerfil(new Perfil() { Codigo = Codigo });

			return View(perfis[0]);
		}

        //
		// POST: /ManterPerfil/AlterarPerfil/5

        [HttpPost]
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

		public ActionResult ExcluirPerfil(int Codigo)
        {
			facade.ExcluirPerfil(Codigo);
			return RedirectToAction("Index");
        }
    }
}
