using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Depoimento
	{
		public int Codigo { get; set; }

		[Required(ErrorMessage = "Autor é um campo obrigatório")]
		[StringLength(300)]
		public string Autor { get; set; }

        [Required(ErrorMessage = "Texto é um campo obrigatório")]
		public string Texto { get; set; }

        public DateTime Data { get; set; }
	}
}