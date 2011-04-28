using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Perfil
	{
		public int Codigo { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public IList<Usuario> ListaUsuarios { get; set; }
		// TODO: mapear corretamente: 
		// public IList<Permissao> ListaPermissoes { get; set; }
	}
}