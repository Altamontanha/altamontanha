using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Foto
	{
		public int Codigo { get; set; }
		public string Autor { get; set; }
		public string Legenda { get; set; }
		public string Resolucao { get; set; }
		public string Caminho { get; set; }
		public string Fonte { get; set; }
		public Foto FotoReduzida { get; set; }
	}
}