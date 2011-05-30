using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	/// <summary>
	/// Referente a entidade de aventura
	/// </summary>
	public class Aventura : Conteudo
	{
		/// <summary>
		/// Aventurar anterior vinculada
		/// </summary>
		public Aventura AventuraAnterior { get; set; }
		/// <summary>
		/// Rota da aventura
		/// </summary>		
		public Rota Rota { get; set; }
		/// <summary>
		/// Usuário autor da aventura
		/// </summary>
		public Usuario Autor { get; set; }
	}
}