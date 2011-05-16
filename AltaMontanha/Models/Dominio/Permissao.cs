using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Permissao
	{
		public int Codigo { get; set; }
		public Perfil Perfil { get; set; }
		public Tela Tela { get; set; }
	}
}