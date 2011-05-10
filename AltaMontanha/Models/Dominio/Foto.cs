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
		public virtual string Autor { get; set; }
		public virtual string Legenda { get; set; }
		public virtual string Caminho { get; set; }
		public virtual string Fonte { get; set; }
		
		// Atributos da imagem.
		public double Bottom { get; set; }
		public double Top { get; set; }
		public double Left { get; set; }
		public double Right { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public virtual Foto FotoReduzida { get; set; }
	}
}