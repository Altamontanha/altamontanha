using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
	public class ManterColunaController : Controller
	{
		ConteudoFacade facade = new ConteudoFacade();

		//
		// GET: /ManterColuna/
		public ActionResult Index()
		{
			IList<Coluna> colunas = facade.PesquisarColuna(null);
			return View(colunas);
		}
		//
		// GET: /ManterColuna/CadastrarColuna
		public ActionResult CadastrarColuna()
		{
			// TODO: incluir autor
			return View();
		}
		//
		// POST: /ManterColuna/CadastrarColuna
		[HttpPost]
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
				return View(coluna);
			}
		}
		//
		// GET: /ManterColuna/AlterarColuna/5
		public ActionResult AlterarColuna(int Codigo)
		{
			// TODO: incluir autor
			// TODO: implementar sobrecarga
			return View(facade.PesquisarColuna(new Coluna() { Codigo = Codigo }));
		}
		//
		// POST: /ManterColuna/AlterarColuna/5
		[HttpPost]
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
		public ActionResult ExcluirColuna(int Codigo)
		{
			facade.ExcluirColuna(Codigo);
			return RedirectToAction("Index");
		}
	}
}