using System;
using System.Threading;
using System.Configuration;

namespace AltaMontanha.Utilitario
{
	public static class Configuracao
	{
		#region Atributos
		
		//Necessário para sincronizar a leitura do registro
		private static ReaderWriterLock travaEscritaLeitura = new ReaderWriterLock();
		
		#endregion

		#region Métodos

		/// <summary>
		/// Recupera o valor do web.config
		/// </summary>
		/// <param name="chave"></param>
		/// <returns></returns>
		public static string Get(string chave)
		{
			return ConfigurationManager.AppSettings.Get(chave);
		}
		
		#endregion
	}
}
