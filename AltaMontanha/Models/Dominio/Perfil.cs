using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Perfil
	{
		public int Codigo { get; set; }

		[Required(ErrorMessage = "Nome é um campo obrigatório")]
		[StringLength(45)]
		public string Nome { get; set; }

		[Required(ErrorMessage = "Descrição é um campo obrigatório")]
		[StringLength(200)]
		public string Descricao { get; set; }
		
		public IList<Usuario> ListaUsuarios { get; set; }
		// TODO: mapear corretamente: 
		public IList<Permissao> ListaPermissoes { get; set; }
	}
}