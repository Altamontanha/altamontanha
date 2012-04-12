using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Web.Security;

namespace AltaMontanha.Utilitario
{
	public class BaseController : Controller
	{
		private static ILog log = LogManager.GetLogger("Log Altamontanha");

		/// <summary>
		/// Controle de Erros do sistema.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnException(ExceptionContext filterContext)
		{
			// TODO : Configurar Log.
			log.Debug(filterContext.Exception);
			base.OnException(filterContext);
		}
		/// <summary>
		/// No momento que é verificado o acesso á página
		/// verifica-se também se o usuário tem acesso ao formulário.
		/// </summary>
		/// <param name="filterContext"></param>	 
		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);

			Models.Fachada.UsuarioFacade fachada = new Models.Fachada.UsuarioFacade();
			Models.Dominio.Usuario usuario = new Models.Dominio.Usuario() { Login = HttpContext.User.Identity.Name };
			string controle = HttpContext.Request.Url.Segments[1].Replace("/","");
			string acao = (HttpContext.Request.Url.Segments.Count() > 2) ? HttpContext.Request.Url.Segments[2].Replace("/", "") : string.Empty;

			string tela = string.Format("/{0}/{1}", controle, acao);

			if (!fachada.VerificarAcesso(usuario, tela))
				FormsAuthentication.RedirectToLoginPage();
			else
			{
				IList<Models.Dominio.Permissao> permissoes = fachada.PesquisarUsuario(usuario).First().Perfil.ListaPermissoes;

				IList<Models.Dominio.Tela> menu = new List<Models.Dominio.Tela>();
				foreach (Models.Dominio.Permissao permissao in permissoes) 
				{
					if (permissao.Tela.Nome.Equals("/ManterArtigo/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterColuna/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterNoticia/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterAventura/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterFoto/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterUsuario/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterPerfil/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterLink/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
					else if (permissao.Tela.Nome.Equals("/ManterBanner/"))
						menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
                    else if (permissao.Tela.Nome.Equals("/ManterDepoimento/"))
                        menu.Add(new Models.Dominio.Tela() { Nome = permissao.Tela.Nome });
				}
				
				ViewData["Menu"] = menu;
			}
		}

		/// <summary>
		/// Usado por handlers que necessitem recuperar parâmetros através da URL
		/// </summary>
		/// <param name="parametro">Nome do parâmetro na url</param>
		/// <returns></returns>
		protected int RecuperarParametroInteiro(string parametro)
		{
			try
			{
				string strValor = this.RecuperarParametroString(parametro);
				if (!string.IsNullOrEmpty(strValor))
					return int.Parse(strValor);
			}
			catch
			{
				throw new ApplicationException("Valor incorreto informado no endereço web.");
			}

			return -1;
		}
		/// <summary>
		/// Usado por handlers que necessitem recuperar parâmetros através da URL ou post.
		/// </summary>
		/// <param name="parametro">Nome do parâmetro na url ou form (post)</param>
		/// <returns></returns>
		protected string RecuperarParametroString(string parametro)
		{
			string valor = string.Empty;

			// QueryString
			if (this.Request.QueryString[parametro] != null)
				valor = this.Server.UrlDecode(this.Request.QueryString[parametro].ToString());
			// Form
			if (string.IsNullOrEmpty(valor))
				if (this.Request.Form[parametro] != null)
					valor = this.Server.UrlDecode(this.Request.Form[parametro].ToString());

			return valor;
		}
	}
}