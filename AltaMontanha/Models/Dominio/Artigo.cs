using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public partial class Artigo : Conteudo
	{
		[Required(ErrorMessage = "Categoria é um campo obrigatório")]
		public virtual Categoria Categoria { get; set; }
	}
}