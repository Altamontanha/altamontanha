using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	/// <summary>
	/// Entidade perfil 
	/// </summary>
	public class Perfil
	{
		/// <summary>
		/// Código do perfil
		/// </summary>
		public int Codigo { get; set; }

		/// <summary>
		/// Nome do perfil
		/// </summary>
		[Required(ErrorMessage = "Nome é um campo obrigatório")]
		[StringLength(45)]
		public string Nome { get; set; }

		/// <summary>
		/// Descrição do perfil
		/// </summary>
		[Required(ErrorMessage = "Descrição é um campo obrigatório")]
		[StringLength(200)]
		public string Descricao { get; set; }
		
		/// <summary>
		/// Lista de usuários vinculados ao perfil
		/// </summary>
		public IList<Usuario> ListaUsuarios { get; set; }

		/// <summary>
		/// Lista de permissões do perfil
		/// </summary>
		public IList<Permissao> ListaPermissoes { get; set; }
	}
}