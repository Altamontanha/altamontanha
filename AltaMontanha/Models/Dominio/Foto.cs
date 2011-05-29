using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	/// <summary>
	/// Entidade referente as fotos do portal
	/// </summary>
	public class Foto
	{ 
		/// <summary>
		/// Código da foto
		/// </summary>
		public virtual int Codigo { get; set; }

		/// <summary>
		/// Autor da foto
		/// </summary>
		[Required(ErrorMessage = "Autor é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Autor { get; set; }

		/// <summary>
		/// Legenda da foto
		/// </summary>
		[Required(ErrorMessage = "Legenda é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Legenda { get; set; }

		/// <summary>
		/// Caminho relativo para o arquivo físico da foto
		/// </summary>
		[Required(ErrorMessage = "Caminho é um campo obrigatório")]
		[StringLength(255)]
		public virtual string Caminho { get; set; }

		/// <summary>
		/// Fonte da foto
		/// </summary>
		[Required(ErrorMessage = "Fonte é um campo obrigatório")]
		[StringLength(75)]
		public virtual string Fonte { get; set; }

		/// <summary>
		/// Indica se a foto pertence a galeria
		/// </summary>
		public virtual bool Galeria { get; set; }

		/// <summary>
		/// Lista de palavras-chave da foto
		/// </summary>
		public virtual IList<PalavraChave> ListaPalavrasChave { get; set; }
	}
}