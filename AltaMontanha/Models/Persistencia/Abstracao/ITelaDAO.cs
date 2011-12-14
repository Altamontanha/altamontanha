using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface ITelaDAO : IDAO<Dominio.Tela>
	{
		new IList<Dominio.Tela> Pesquisar(Dominio.Tela objeto, int pagina = 0);
	}
}
