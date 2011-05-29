using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace AltaMontanha.Models.Dominio
{
	/// <summary>
	/// Classe abstrata de conteúdo
	/// </summary>
	public abstract partial class Conteudo
	{
		/// <summary>
		/// Código do conteúdo
		/// </summary>
		public virtual int Codigo { get; set; }

		/// <summary>
		/// Slug do título utilizado para url amigável 
		/// </summary>
		public string Slug
		{
			get
			{
				string slug;
				slug = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(this.Titulo));
				slug = HttpUtility.HtmlDecode(slug);
				slug = Regex.Replace(slug, @"[^\w\ ]", "").Trim().Replace(" ", "-");
				return slug.ToLower();
			}
		}

		/// <summary>
		/// Título do conteúdo
		/// </summary>
		[Required(ErrorMessage = "Título é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Titulo { get; set; }

		/// <summary>
		/// Ante-título do conteúdo
		/// </summary>
		[Required(ErrorMessage = "Ante-título é um campo obrigatório")]
		[StringLength(250)]
		public virtual string AnteTitulo { get; set; }

		/// <summary>
		/// Resumo do conteúdo
		/// </summary>
		[Required(ErrorMessage = "Resumo é um campo obrigatório")]
		public virtual string Resumo { get; set; }

		/// <summary>
		/// Texto (em html) do conteúdo
		/// </summary>
		[Required(ErrorMessage = "Texto é um campo obrigatório")]
		public virtual string Texto { get; set; }

		/// <summary>
		/// Data da postagem do conteúdo
		/// </summary>
		[Required(ErrorMessage = "Data é um campo obrigatório")]
		[DataType(DataType.Date, ErrorMessage="Formato da data é inválido")]
		public virtual DateTime Data { get; set; }
		
		/// <summary>
		/// Lista de palavras-chave do conteúdo
		/// </summary>
		public virtual IList<PalavraChave> ListaPalavrasChave { get; set; }

		/// <summary>
		/// Lista de fotos do conteúdo
		/// </summary>
		public virtual IList<Foto> ListaFotos { get; set; }

		/// <summary>
		/// Foto utilizada como capa do conteúdo
		/// </summary>
		public virtual Foto FotoCapa { get; set; }
		
		/// <summary>
		/// Usuário que fez o cadastro do conteúdo
		/// </summary>
		public virtual Usuario UsuarioCadastro { get; set; } // TODO: Adicionar na Documentação.
	}
}