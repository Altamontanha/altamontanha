using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Foto
	{
		public virtual int Codigo { get; set; }

		[Required(ErrorMessage = "Autor é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Autor { get; set; }

		[Required(ErrorMessage = "Legenda é um campo obrigatório")]
		[StringLength(50)]
		public virtual string Legenda { get; set; }

		[Required(ErrorMessage = "Caminho é um campo obrigatório")]
		[StringLength(255)]
		public virtual string Caminho { get; set; }

		[Required(ErrorMessage = "Fonte é um campo obrigatório")]
		[StringLength(75)]
		public virtual string Fonte { get; set; }

		public virtual bool Galeria { get; set; }
	}
}