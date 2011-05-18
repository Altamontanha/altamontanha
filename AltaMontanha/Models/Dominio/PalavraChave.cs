using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace AltaMontanha.Models.Dominio
{
	public class PalavraChave
	{
		/// <summary>
		/// Código da palavra-chave
		/// </summary>
		public int Codigo { get; set; }

		/// <summary>
		/// Nome da palavra-chave
		/// </summary>
		public string Nome { get; set; }

		/// <summary>
		/// Slug da palavra-chave utilizado para url amigável 
		/// </summary>
		public string Slug {
			get
			{
				string slug;
				slug = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(this.Nome));
				slug = HttpUtility.HtmlDecode(slug);
				slug = Regex.Replace(slug, @"[^\w\ ]", "").Trim().Replace(" ", "-");
				return slug;
			}
		}
	}
}