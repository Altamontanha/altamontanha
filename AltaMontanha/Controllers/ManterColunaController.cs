using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
	[HandleError]
	public class ManterColunaController : Utilitario.BaseController
	{
		ConteudoFacade facade = new ConteudoFacade();

		//
		// GET: /ManterColuna/
		[Authorize]
		public ActionResult Index()
		{
			IList<Coluna> colunas = facade.PesquisarColuna(null);
			return View(colunas);
		}
		//
		// GET: /ManterColuna/CadastrarColuna
		[Authorize]
		public ActionResult CadastrarColuna()
		{
			// TODO: incluir autor
			return View();
		}
		//
		// POST: /ManterColuna/CadastrarColuna
		[HttpPost]
		[Authorize]
		public ActionResult CadastrarColuna(Coluna coluna)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: incluir autor
					facade.SalvarColuna(coluna);
					return RedirectToAction("Index");
				}
				else
				{
					return View(coluna);
				}
			}
			catch
			{
//				return View(coluna);
				// TODO: verificar erro object to int32
				return RedirectToAction("Index");
			}
		}
		//
		// GET: /ManterColuna/AlterarColuna/5
		[Authorize]
		public ActionResult AlterarColuna(int Codigo)
		{
			// TODO: incluir autor
			// TODO: implementar sobrecarga
			return View(facade.PesquisarColuna(new Coluna() { Codigo = Codigo }));
		}
		//
		// POST: /ManterColuna/AlterarColuna/5
		[HttpPost]
		[Authorize]
		public ActionResult AlterarColuna(Coluna coluna)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: incluir autor
					facade.SalvarColuna(coluna);
					return RedirectToAction("Index");
				}
				else
				{
					return View(coluna);
				}
			}
			catch
			{
				return View(coluna);
			}
		}
		//
		// GET: /ManterColuna/ExcluirColuna/5
		[Authorize]
		public ActionResult ExcluirColuna(int Codigo)
		{
			facade.ExcluirColuna(Codigo);
			return RedirectToAction("Index");
		}
	}
}