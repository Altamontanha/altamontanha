using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Coluna : Conteudo
	{
		[Required(ErrorMessage = "Autor é um campo obrigatório")]
		public Usuario Autor { get; set; }
	}
}