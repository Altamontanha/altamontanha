using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;

namespace AltaMontanha.Controllers
{
    public class ManterPalavraChaveController : Controller
    {
		ConteudoFacade facade = new ConteudoFacade();

		[HttpPost]
		public JsonResult CadastrarPalavraChave(string palavras)
		{
			string[] palavra = palavras.Split(',');

			List<PalavraChave> retorno = new List<PalavraChave>();
			foreach (string p in palavra) 
			{
				retorno.Add(facade.SalvarPalavraChave(new PalavraChave() { Nome = p.Trim() }));
			}

			return Json(retorno, JsonRequestBehavior.AllowGet);
		}
    }
}
