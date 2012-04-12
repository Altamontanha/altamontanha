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
    public class ManterDepoimentoController : Utilitario.BaseController
    {
        DepoimentoFacade facade = new DepoimentoFacade();

        //
        // GET: /ManterDepoimento/
		[Authorize]
		public ActionResult Index()
        {
            IList<Depoimento> depoimentos = facade.PesquisarDepoimento(null);
            return View(depoimentos);
        }
        //
        // GET: /ManterDepoimento/VisualizarUsuario/5
		[Authorize]
		public ActionResult VisualizarDepoimento(int Codigo)
        {
			return View(facade.PesquisarDepoimento(Codigo));
        }
		//
        // GET: /ManterDepoimento/CadastrarDepoimento
		[Authorize]
		public ActionResult CadastrarDepoimento()
        {
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Autor", "Data");

            return View();
        } 
		//
        // POST: /ManterUsuario/CadastrarUsuario
        [HttpPost]
		[Authorize]
		public ActionResult CadastrarDepoimento(Depoimento depoimento, HttpPostedFileBase file)
        {
			try
			{
                depoimento.Data = DateTime.Now;
                facade.SalvarDepoimento(depoimento, file);
				return RedirectToAction("Index");
			}
			catch
			{
				ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");
                return View(depoimento);
			}
        }
        //
        // GET: /ManterUsuario/AlterarUsuario/5
		[Authorize]
        public ActionResult AlterarDepoimento(int Codigo)
        {
			ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Autor", "Nome");
			return View(facade.PesquisarDepoimento(Codigo));
        }
		//
        // POST: /ManterUsuario/AlterarUsuario/5
        [HttpPost]
		[Authorize]
        public ActionResult AlterarDepoimento(Depoimento depoimento, HttpPostedFileBase file)
        {
			//TODO: na alteração a senha não é obrigatória (?)
            try
            {
                depoimento.Data = DateTime.Now;
                facade.SalvarDepoimento(depoimento, file);
				return RedirectToAction("Index");
			}
            catch
            {
				ViewData["Perfis"] = new SelectList(facade.PesquisarPerfil(null).ToList(), "Codigo", "Nome");
                return View(depoimento);
            }
        }
        //
        // GET: /ManterUsuario/ExcluirUsuario/5
		[Authorize]
        public ActionResult ExcluirDepoimento(int Codigo)
        {
            facade.ExcluirDepoimento(Codigo);
            return RedirectToAction("Index");
        }
    }
}