using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	/// <summary>
	/// Entidade referente a rota
	/// </summary>
	public class Rota
	{
		/// <summary>
		/// Código da rota
		/// </summary>
		public int Codigo { get; set; }

		/// <summary>
		/// Caminho relativo para o arquivo da rota
		/// </summary>
		public string Caminho { get; set; }
	}
}