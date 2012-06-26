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
	public class ManterPalavraChaveController : Controller
    {

		[HttpPost]
		public JsonResult CadastrarPalavraChave(string palavras)
        {
            ConteudoFacade facade = new ConteudoFacade();
			IList<PalavraChave> palavrasChave = facade.SalvarPalavraChave(palavras.Split(','));

			return Json(palavrasChave, JsonRequestBehavior.AllowGet);
		}
    }
}
