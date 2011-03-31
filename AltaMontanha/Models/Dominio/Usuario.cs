using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Usuario
	{
		public virtual int Codigo { get; set; }
		public virtual string Nome { get; set; }
		public virtual string Login { get; set; } // TODO: Adicionar na documentação.
		public virtual string Senha { get; set; }
		public virtual string Email { get; set; }
		public virtual Perfil Perfil { get; set; }
		public virtual Foto Foto { get; set; }
	}
}