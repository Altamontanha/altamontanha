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
		/// <summary>
		/// Responsável pelo carregamento da tela de cadastro de foto.
		/// </summary>
		/// <returns></returns>
		public ActionResult CadastrarFoto()
		{
			return View();
		}
		/// <summary>
		/// Responsável pelo cadastro da foto
		/// </summary>
		/// <param name="foto"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult CadastrarFoto(Models.Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				facade.SalvarFotoGaleria(foto, file);

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
				facade.SalvarFotoGaleria(foto, file);
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
		//
		// GET: /ManterFotoAlterarFoto/5
		public ActionResult VincularFoto()//(int codConteudo)
		{
			Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
			Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO();
			
			ViewData["Fotos"] = fotoDAO.Pesquisar(null);

			return View(ViewData);

			//Foto foto = facade.PesquisarFoto(Codigo);
			//return View(foto);
		}
		//
		// POST: /ManterFoto/AlterarFoto/5
		[HttpPost]
		public ActionResult VincularFoto(FormCollection collection)
		{
			Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
			Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO();
			Foto foto = new Foto();
			
			foto.Legenda = collection["txtLegenda"];

			ViewData["Fotos"] = fotoDAO.Pesquisar(foto);

			return View(ViewData);
		}
	}
}
