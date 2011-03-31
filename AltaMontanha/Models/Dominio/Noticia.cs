using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Noticia : Conteudo
	{
		public string Fonte { get; set; }
		public bool Destaque { get; set; }
	}
}