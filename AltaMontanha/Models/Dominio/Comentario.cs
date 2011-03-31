using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Comentario
	{
		public int Codigo { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Texto { get; set; }
		public bool Habilitado { get; set; }
	}
}