using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
    public class ManterCategoriaController : Controller
    {
		ConteudoFacade facade = new ConteudoFacade();

        //
        // GET: /ManterCategoria/
        public ActionResult Index()
        {
            return View(facade.PesquisarCategoria(null));
        }
        //
        // GET: /ManterCategoria/CadastrarCategoria
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
				if (ModelState.IsValid)
				{
					facade.SalvarCategoria(categoria);
					return RedirectToAction("Index");
				}
				else
				{
					return View(categoria);
				}
			}
            catch
            {
//                return View(categoria);
				// TODO: verificar erro object to int32
				return RedirectToAction("Index");
			}
        }
        
        //
        // GET: /ManterCategoria/AlterarCategoria/5

		public ActionResult AlterarCategoria(int Codigo)
        {
			// TODO: implementar pesquisa por Codigo
			IList<Categoria> categorias = facade.PesquisarCategoria(new Categoria() { Codigo = Codigo });
            return View(categorias[0]);
        }

        //
		// POST: /ManterCategoria/AlterarCategoria/5

        [HttpPost]
		public ActionResult AlterarCategoria(Categoria categoria)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarCategoria(categoria);
					return RedirectToAction("Index");
				}
				else
				{
					return View(categoria);
				}
			}
			catch
			{
				return View(categoria);
			}
        }

        //
        // GET: /ManterCategoria/ExcluirCategoria/5

		public ActionResult ExcluirCategoria(int Codigo)
        {
			facade.ExcluirCategoria(Codigo);
            return RedirectToAction("Index");
        }
    }
}
