using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public abstract partial class Conteudo
	{
		public virtual int Codigo { get; set; }
		[Required(ErrorMessage = "Título é um campo obrigatório")]
		[StringLength(150)]
		public virtual string Titulo { get; set; }

		[Required(ErrorMessage = "Ante-título é um campo obrigatório")]
		[StringLength(250)]
		public virtual string AnteTitulo { get; set; }

		[Required(ErrorMessage = "Resumo é um campo obrigatório")]
		public virtual string Resumo { get; set; }

		[Required(ErrorMessage = "Texto é um campo obrigatório")]
		public virtual string Texto { get; set; }

		[Required(ErrorMessage = "Data é um campo obrigatório")]
		[DataType(DataType.Date)]
		public virtual DateTime Data { get; set; }
		
		//public virtual List<Foto> ListaFotos { get; set; }
		//public virtual List<PalavraChave> ListaPalavrasChave { get; set; }
		//public virtual List<Comentario> ListaComentarios { get; set; }
		public virtual Usuario UsuarioCadastro { get; set; } // TODO: Adicionar na Documentação.
	}
}