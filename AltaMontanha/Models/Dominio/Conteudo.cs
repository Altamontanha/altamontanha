using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public abstract partial class Conteudo
	{
		public int Codigo { get; set; }
		public string Titulo { get; set; }
		public string AnteTitulo { get; set; }
		public string Resumo { get; set; }
		public string Texto { get; set; }
		public DateTime Data { get; set; }
		public List<Foto> ListaFotos { get; set; }
		public Usuario UsuarioCadastro { get; set; } // TODO: Adicionar na Documentação.
		public List<PalavraChave> ListaPalavrasChave { get; set; }
		public List<Comentario> ListaComentarios { get; set; }
	}
}