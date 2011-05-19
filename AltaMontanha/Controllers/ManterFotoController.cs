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
		[Authorize]
		public ActionResult ExcluirFoto(int Codigo)
		{
			facade.ExcluirFoto(Codigo);
			return RedirectToAction("Index");
		}
		//
		// GET: /ManterFotoAlterarFoto/5
		[Authorize]
		public ActionResult VincularFoto()//(int codConteudo)
		{
			//Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
			//Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO();

			//ViewData["Fotos"] = fotoDAO.Pesquisar( new Foto() { Galeria=true } );

			return View();

			//Foto foto = facade.PesquisarFoto(Codigo);
			//return View(foto);
		}
		//
		// POST: /ManterFoto/AlterarFoto/5
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
		/// <summary>
		/// vinculação de fotos com conteudo
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public ActionResult SalvarFoto(FormCollection collection)
		{
			IList<Models.Dominio.Foto> listaFotos = new List<Models.Dominio.Foto>();
			Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
			Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO();

			foreach (string key in collection.AllKeys) 
			{
				bool teste = collection[key].Contains("true");
				
				string[] values = collection.GetValues(key);

				if(values.Count() > 1)
				{
					int codigo = Convert.ToInt32(values[0]);
					listaFotos.Add(fotoDAO.Pesquisar(codigo));
				}
			}

			if (listaFotos.Count() > 0)
				Utilitario.Sessao.ListaFotos = listaFotos;

			return RedirectToAction("VincularFoto");
		}
	}
}
