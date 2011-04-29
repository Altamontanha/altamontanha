using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
	[HandleError]
    public class ManterCategoriaController : Utilitario.BaseController
    {
		ConteudoFacade facade = new ConteudoFacade();

        //
        // GET: /ManterCategoria/
		[Authorize]
        public ActionResult Index()
        {
            return View(facade.PesquisarCategoria(null));
        }
        //
        // GET: /ManterCategoria/CadastrarCategoria
		[Authorize]
		public ActionResult CadastrarCategoria()
        {
            return View();
        } 
        //
		// POST: /ManterCategoria/CadastrarCategoria
        [HttpPost]
		public ActionResult CadastrarCategoria(Categoria categoria)
        {
            try
            {
				facade.SalvarCategoria(categoria);
				return RedirectToAction("Index");
			}
            catch
            {
                return View(categoria);
			}
        }
        
        //
        // GET: /ManterCategoria/AlterarCategoria/5
		[Authorize]
		public ActionResult AlterarCategoria(int Codigo)
        {
			return View(facade.PesquisarCategoria(Codigo));
        }

        //
		// POST: /ManterCategoria/AlterarCategoria/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarCategoria(Categoria categoria)
        {
            try
            {
				facade.SalvarCategoria(categoria);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(categoria);
			}
        }

        //
        // GET: /ManterCategoria/ExcluirCategoria/5
		[Authorize]
		public ActionResult ExcluirCategoria(int Codigo)
        {
			// TODO: validar a existência de conteúdo ligado
			facade.ExcluirCategoria(Codigo);
            return RedirectToAction("Index");
        }
    }
}