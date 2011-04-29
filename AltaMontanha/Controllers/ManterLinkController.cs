﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
	[HandleError]
	public class ManterLinkController : Utilitario.BaseController
    {
		private InformativoFacade facade = new InformativoFacade();

        //
        // GET: /ManterLink/
		[Authorize]
        public ActionResult Index()
        {
			IList<Link> links = facade.PesquisarLink(null);
            return View(links.ToList());
        }
        //
		// GET: /ManterLink/CadastrarLink
		[Authorize]
		public ActionResult CadastrarLink()
        {
            return View();
        } 
		//
		// POST: /ManterLink/CadastrarLink
		[Authorize]
        [HttpPost]
		public ActionResult CadastrarLink(Link link)
        {
            try
            {
				facade.SalvarLink(link);

				return RedirectToAction("Index");
            }
            catch
            {
                return View(link);
			}
        }
        //
        // GET: /ManterLink/AlterarLink/5
		[Authorize]
        public ActionResult AlterarLink(int Codigo)
        {
			return View(facade.PesquisarLink(Codigo));
        }
		//
        // POST: /ManterLink/AlterarLink/5
		[HttpPost]
		[Authorize]
		public ActionResult AlterarLink(Link link)
        {
            try
            {
				facade.SalvarLink(link);

				return RedirectToAction("Index");
			}
            catch
            {
                return View(link);
            }
        }
		//
        // GET: /ManterLink/ExcluirLink/5
		[Authorize]
        public ActionResult ExcluirLink(int Codigo)
        {
			facade.ExcluirLink(Codigo);
			return RedirectToAction("Index");
		}
    }
}