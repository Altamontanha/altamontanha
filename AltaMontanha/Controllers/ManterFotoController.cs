using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;

namespace AltaMontanha.Controllers
{
    public class ManterFotoController : Controller
    {
		Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
	
        //
        // GET: /ManterFoto/
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult CadastrarFoto()
		{
			return View();
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult CadastrarFoto(Models.Dominio.Foto foto, HttpPostedFileBase file)
		{

			facade.SalvarFoto(foto, file);
			
			return RedirectToAction("Index");
		}


		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Upload(HttpPostedFileBase file)
		{
			string caminho = string.Empty;
			string nome = string.Empty;

			if (file.ContentLength > 0)
			{
				nome = Path.GetFileName(file.FileName);
				caminho = string.Format(@"{0}\{1}", Server.MapPath("~/Temp"), nome);

				file.SaveAs(caminho);
			}


			return View();
		}
    }
}
