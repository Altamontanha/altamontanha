using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Link
	{
		[Key]
		public int Codigo { get; set; }

		[Required(ErrorMessage="Título é um campo obrigatório")]
		[StringLength(150)]
		public string Titulo { get; set; }

		[Required(ErrorMessage = "Url é um campo obrigatório")]
		[StringLength(250)]
		[DataType(DataType.Url)]
		public string Url { get; set; }
	}
}