using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IDepoimentoDAO : IDAO<Dominio.Depoimento>
	{
        IList<Dominio.Depoimento> Pesquisar();
	}
}