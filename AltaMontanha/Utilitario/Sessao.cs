using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Utilitario
{
	public static class Sessao
	{
		private const string chaveFotos = "6565ASDASD4-ASDD3366ASD-PE566775252";

		public static IList<Models.Dominio.Foto> ListaFotos
		{
			get 
			{
				return (IList<Models.Dominio.Foto>) HttpContext.Current.Session[chaveFotos];
			}
			set 
			{
				HttpContext.Current.Session.Add(chaveFotos, value);
			}
		}
	}
}