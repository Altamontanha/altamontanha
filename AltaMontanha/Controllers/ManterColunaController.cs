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
		UsuarioFacade usuarioFacade = new UsuarioFacade();

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
			ViewData["Autores"] = new SelectList(usuarioFacade.PesquisarUsuario(null), "Codigo", "Nome");
			return View();
		}
		//
		// POST: /ManterColuna/CadastrarColuna
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult CadastrarColuna(Coluna coluna)
		{
			try
			{
				// TODO: alterar para usuário logado
				coluna.UsuarioCadastro = new Usuario() { Codigo = 1 };
				facade.SalvarColuna(coluna);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(coluna);
			}
		}
		//
		// GET: /ManterColuna/AlterarColuna/5
		[Authorize]
		public ActionResult AlterarColuna(int Codigo)
		{
			Coluna coluna = facade.PesquisarColuna(Codigo);

			return View(coluna);
		}
		//
		// POST: /ManterColuna/AlterarColuna/5
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult AlterarColuna(Coluna coluna)
		{
			try
			{
				facade.SalvarColuna(coluna);
				return RedirectToAction("Index");
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