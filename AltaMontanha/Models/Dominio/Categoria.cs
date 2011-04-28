using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Categoria
	{
		public int Codigo { get; set; }

		[Required(ErrorMessage = "Nome é um campo obrigatório")]
		[StringLength(50)]
		public string Titulo { get; set; }

		public string Descricao { get; set; }
	}
}