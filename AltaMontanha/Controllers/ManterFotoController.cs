using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Controllers
{
    public class ManterFotoController : Controller
    {
		Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
	
        //
        // GET: /ManterFoto/
		public ActionResult Index()
        {
			IList<Foto> fotos = facade.PesquisarFoto(null);
            return View(fotos);
        }

		public ActionResult CadastrarFoto()
		{
			return View();
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult CadastrarFoto(Models.Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				facade.SalvarFoto(foto, file);

				return RedirectToAction("Index");
			}
			catch
			{
				return View(foto);
			}
		}
		
		//
		// GET: /ManterFotoAlterarFoto/5
		public ActionResult AlterarFoto(int Codigo)
		{
			Foto foto = facade.PesquisarFoto(Codigo);
			return View(foto);
		}

		//
		// POST: /ManterFoto/AlterarFoto/5
		[HttpPost]
		public ActionResult AlterarFoto(Foto foto, HttpPostedFileBase file)
		{
			try
			{
				facade.SalvarFoto(foto, file);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(foto);
			}
		}

		//
		// GET: /ManterFoto/ExluirFoto/5
		public ActionResult ExcluirFoto(int Codigo)
		{
			facade.ExcluirFoto(Codigo);
			return RedirectToAction("Index");
		}
	}
}
