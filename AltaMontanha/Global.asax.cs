using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AltaMontanha
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute
			(
				"busca",
				"busca",
				new { controller = "Home", action = "VisualizarBusca" }
			);

			routes.MapRoute
			(
				"Artigo",
				"artigo/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarArtigo" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"artigos",
				"artigos",
				new { controller = "Home", action = "PesquisarArtigo" }
			);
			routes.MapRoute
			(
				"noticia",
				"noticia/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarNoticia" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"noticias",
				"noticias",
				new { controller = "Home", action = "PesquisarNoticia" }
			);
			routes.MapRoute
			(
				"coluna",
				"coluna/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarColuna" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"colunas",
				"colunas",
				new { controller = "Home", action = "PesquisarColuna" }
			);
			routes.MapRoute
			(
				"aventura",
				"aventura/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarAventura" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"aventuras",
				"aventuras",
				new { controller = "Home", action = "PesquisarAventura" }
			);
			routes.MapRoute
			(
				"default",
				"{controller}/{action}/{Codigo}",
				new { controller = "Home", action = "Index", Codigo = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}