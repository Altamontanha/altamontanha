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
		private static string ambiente;
		
		#endregion

		#region Métodos

		/// <summary>
		/// Recupera o valor do web.config
		/// </summary>
		/// <param name="chave"></param>
		/// <returns></returns>
		public static string Get(string chave)
		{
			string ab = ambiente;
			if (ab == null)
			{
				travaEscritaLeitura.AcquireReaderLock(500);
				ambiente = ConfigurationManager.AppSettings.Get("Ambiente");
				travaEscritaLeitura.ReleaseLock();
				ab = ambiente;
			}

			ab = string.Format("{0}_{1}", ab, chave);
			return ConfigurationManager.AppSettings.Get(ab);
		}
		
		#endregion
	}
}