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
	[HandleError]
	public class ManterFotoController : Utilitario.BaseController
    {
		Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
	
        //
        // GET: /ManterFoto/
		[Authorize]
		public ActionResult Index()
        {
			Foto foto = new Foto();
			foto.Galeria = true;
			IList<Foto> fotos = facade.PesquisarFoto(foto);

            return View(fotos);
        }
		/// <summary>
		/// Responsável pelo carregamento da tela de cadastro de foto.
		/// </summary>
		/// <returns></returns>
		[Authorize]
		public ActionResult CadastrarFoto()
		{
			return View();
		}
		/// <summary>
		/// Responsável pelo cadastro da foto
		/// </summary>
		/// <param name="foto"></param>
		/// <param name="arquivo"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarFoto(Models.Dominio.Foto foto, HttpPostedFileBase file)
		{
			facade.SalvarFotoGaleria(foto, file);
			return RedirectToAction("CadastrarFoto", "ManterFoto");
		}
		//
		// GET: /ManterFotoAlterarFoto/5
		[Authorize]
		public ActionResult AlterarFoto(int Codigo)
		{
			Foto foto = facade.PesquisarFoto(Codigo);
			return View(foto);
		}
		//
		// POST: /ManterFoto/AlterarFoto/5
		[HttpPost]
		[Authorize]
		public ActionResult AlterarFoto(Foto foto, HttpPostedFileBase file)
		{
			facade.SalvarFotoGaleria(foto, file);
			return RedirectToAction("Index");
		}
		//
		// GET: /ManterFoto/ExluirFoto/5
		[Authorize]
		public ActionResult ExcluirFoto(int Codigo)
		{
			facade.ExcluirFoto(Codigo);
			return RedirectToAction("Index");
		}
		//
		// GET: /VincularFoto/
		[Authorize]
		public ActionResult VincularFoto()
		{
			return View();
		}
		//
		// POST: /ManterFoto/VincularFoto/
		[HttpPost]
		[Authorize]
		public ActionResult VincularFoto(FormCollection collection)
		{

			Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
			Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO();
			Foto foto = new Foto();

			foto.Legenda = collection["txtLegenda"];
			foto.Galeria = true;

			ViewData["Fotos"] = fotoDAO.Pesquisar(foto);

			return View();
		}
	}
}
