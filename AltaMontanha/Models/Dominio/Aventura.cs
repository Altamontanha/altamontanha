using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Aventura : Conteudo
	{
		public Aventura AventuraAnterior { get; set; }
		public Rota Rota { get; set; }
		public Usuario Autor { get; set; }
	}
}