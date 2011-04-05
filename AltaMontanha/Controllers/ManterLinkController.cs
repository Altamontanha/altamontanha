using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
    public class ManterLinkController : Controller
    {
		private InformativoFacade facade = new InformativoFacade();

        //
        // GET: /ManterLink/

        public ActionResult Index()
        {
			IList<Link> links = facade.PesquisarLink(null);
            return View(links.ToList());
        }

        //
		// GET: /ManterLink/CadastrarLink

		public ActionResult CadastrarLink()
        {
            return View();
        } 

        //
		// POST: /ManterLink/CadastrarLink

        [HttpPost]
		public ActionResult CadastrarLink(Link link)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarLink(link);

					return RedirectToAction("Index");
				}
				else
				{
					return View(link);
				}
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ManterLink/AlterarLink/5
 
        public ActionResult AlterarLink(int Codigo)
        {
			IList<Link> links = facade.PesquisarLink(new Link() { Codigo = Codigo });
            return View(links[0]);
        }

        //
        // POST: /ManterLink/AlterarLink/5

        [HttpPost]
		public ActionResult AlterarLink(Link link)
        {
            try
            {
				if (ModelState.IsValid)
				{
					facade.SalvarLink(link);

					return RedirectToAction("Index");
				}
				else
				{
					return View(link);
				}
			}
            catch
            {
                return View();
            }
        }

        //
        // GET: /ManterLink/ExcluirLink/5
 
        public ActionResult ExcluirLink(int Codigo)
        {
			facade.ExcluirLink(Codigo);
			return RedirectToAction("Index");
		}
    }
}
