using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public partial class Artigo : Conteudo
	{
		public virtual Categoria Categoria { get; set; }
	}
}