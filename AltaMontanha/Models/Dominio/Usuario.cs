using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace AltaMontanha.Models.Dominio
{
	public class Usuario
	{
		public virtual int Codigo { get; set; }

		[Required(ErrorMessage = "Nome é um campo obrigatório")]
		[StringLength(100)]
		public virtual string Nome { get; set; }

		[Required(ErrorMessage = "Login é um campo obrigatório")]
		[StringLength(30)]
		public virtual string Login { get; set; } // TODO: Adicionar na documentação.

		[Required(ErrorMessage = "Senha é um campo obrigatório")]
		[StringLength(100)]
		public virtual string Senha { get; set; }

		[Required(ErrorMessage = "Email é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Email { get; set; }

		[Required(ErrorMessage = "Perfil é um campo obrigatório")]
		public virtual Perfil Perfil { get; set; }

		[Required(ErrorMessage = "Foto é um campo obrigatório")]
		public virtual Foto Foto { get; set; }

		public override string ToString()
		{
			return Login;
		}
		/// <summary>
		/// Slug do Nome utilizado para url amigável 
		/// </summary>
		public string Slug
		{
			get
			{
				string slug;
				slug = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(this.Nome));
				slug = HttpUtility.HtmlDecode(slug);
				slug = Regex.Replace(slug, @"[^\w\ ]", "").Trim().Replace(" ", "-");
				return slug.ToLower();
			}
		}
	}
}