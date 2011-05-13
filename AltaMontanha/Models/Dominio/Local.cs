using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Local
	{
		public int Codigo { get; set; }
		public string Descricao { get; set; }
		public int Altura { get; set; }
		public int Largura { get; set; }
		public string Nome { get; set; }
		//public IList<Banner> ListaBanner { get; set; } //TODO: trocar
	}
}