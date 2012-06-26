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

        //
        // GET: /ManterCategoria/
		[Authorize]
        public ActionResult Index()
        {
            ConteudoFacade facade = new ConteudoFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
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
            ConteudoFacade facade = new ConteudoFacade();
			return View(facade.PesquisarCategoria(Codigo));
        }

        //
		// POST: /ManterCategoria/AlterarCategoria/5
        [HttpPost]
		[Authorize]
		public ActionResult AlterarCategoria(Categoria categoria)
        {
            ConteudoFacade facade = new ConteudoFacade();
            try
            {
                if (categoria.Descricao == null)
                    categoria.Descricao = "";

                if (categoria.Titulo == null)
                    categoria.Titulo = "";

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
            ConteudoFacade facade = new ConteudoFacade();
			// TODO: validar a existência de conteúdo ligado
			facade.ExcluirCategoria(Codigo);
            return RedirectToAction("Index");
        }
    }
}