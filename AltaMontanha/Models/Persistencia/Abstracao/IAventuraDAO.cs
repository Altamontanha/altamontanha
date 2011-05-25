using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IAventuraDAO :IDAO<Dominio.Aventura>
	{
		IList<Dominio.Aventura> Pesquisar(Dominio.Aventura objeto, int qtde);
	}
}
