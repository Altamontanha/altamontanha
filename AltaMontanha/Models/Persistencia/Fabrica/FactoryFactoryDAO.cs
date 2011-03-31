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
			Provider provider = (Provider) Enum.Parse(typeof(Provider), Utilitario.Configuracao.Get("Provider"));

			switch (provider)
			{
				case Provider.NHibernate:
					return new FactoryNHibernate();
				case Provider.MySQL:
					throw new NotImplementedException("Provider não implementado");
				case Provider.SQLServer:
					throw new NotImplementedException("Provider não implementado");
				default:
					throw new ArgumentOutOfRangeException("Provider não existente no contexto da aplicação");
			}
		}
	}

	public enum Provider : byte
	{
		NHibernate,
		MySQL,
		SQLServer
	}
}