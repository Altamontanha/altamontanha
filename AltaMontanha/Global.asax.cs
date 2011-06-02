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
				"Artigo",
				"Artigo/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarArtigo" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"Artigos",
				"Artigos",
				new { controller = "Home", action = "PesquisarArtigo" }
			);
			routes.MapRoute
			(
				"Noticia",
				"Noticia/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarNoticia" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"Noticias",
				"Noticias",
				new { controller = "Home", action = "PesquisarNoticia" }
			);
			routes.MapRoute
			(
				"Coluna",
				"Coluna/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarColuna" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"Colunas",
				"Colunas",
				new { controller = "Home", action = "PesquisarColuna" }
			);
			routes.MapRoute
			(
				"Aventura",
				"Aventura/{Codigo}/{Titulo}",
				new { controller = "Home", action = "VisualizarAventura" },
				new { Codigo = @"\d+" }
			);
			routes.MapRoute
			(
				"Aventuras",
				"Aventuras",
				new { controller = "Home", action = "PesquisarAventura" }
			);
			routes.MapRoute
			(
				"Default",
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