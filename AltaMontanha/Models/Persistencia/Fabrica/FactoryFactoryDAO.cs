using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Persistencia.Fabrica
{
	public static class FactoryFactoryDAO
	{
		public static IFactoryDAO GetFabrica()
		{
			// TODO: switch do provider configurado no web.config
			return new FactoryNHibernate();
		}
	}
}