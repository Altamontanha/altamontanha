using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IUsuarioDAO : IDAO<Dominio.Usuario>
	{
		IList<Dominio.Usuario> PesquisarColunista();
	}
}