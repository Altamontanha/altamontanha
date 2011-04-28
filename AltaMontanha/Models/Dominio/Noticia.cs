using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Noticia : Conteudo
	{
		[Required(ErrorMessage = "Fonte é um campo obrigatório")]
		[StringLength(75)]
		public string Fonte { get; set; }

		public bool Destaque { get; set; }
	}
}