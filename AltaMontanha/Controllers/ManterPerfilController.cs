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
			Perfil perfil = facade.PesquisarPerfil(Codigo);

            return View(perfil);
        }
        //
		// GET: /ManterPerfil/CadastrarPerfil
		[Authorize]
		public ActionResult CadastrarPerfil()
        {
			ViewData["Telas"] = facade.PesquisarTela(null);
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
				IList<Permissao> permissoes = new List<Permissao>();
				foreach (Permissao p in perfil.ListaPermissoes)
				{
					if (p.Tela != null)
						permissoes.Add(p);
				}
				perfil.ListaPermissoes = permissoes;
				facade.SalvarPerfil(perfil);
				return RedirectToAction("Index");
            }
            catch
            {
				ViewData["Telas"] = facade.PesquisarTela(null);
				return View(perfil);
			}
        }
        //
        // GET: /ManterPerfil/AlterarPerfil/5
		[Authorize]
		public ActionResult AlterarPerfil(int Codigo)
        {
			Perfil perfil = facade.PesquisarPerfil(Codigo);
			ViewData["Telas"] = facade.PesquisarTela(null);

			return View(perfil);
		}
        //
		// POST: /ManterPerfil/AlterarPerfil/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarPerfil(Perfil perfil)
        {
			try
			{
				IList<Permissao> permissoes = new List<Permissao>();
				foreach (Permissao p in perfil.ListaPermissoes)
				{
					if (p.Tela != null)
						permissoes.Add(p);
				}
				perfil.ListaPermissoes = permissoes;
				facade.SalvarPerfil(perfil);
				return RedirectToAction("Index");
			}
			catch
			{
				ViewData["Telas"] = facade.PesquisarTela(null);
				return View(perfil);
			}
		}
		//
        // GET: /ManterPerfil/ExcluirPerfil/5
		[Authorize]
		public ActionResult ExcluirPerfil(int Codigo)
        {
			// TODO: validar a existência de usuários
			facade.ExcluirPerfil(Codigo);
			return RedirectToAction("Index");
        }
    }
}