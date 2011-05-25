using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IArtigoDAO : IDAO<Dominio.Artigo>
	{
		IList<Dominio.Artigo> Pesquisar(Dominio.Artigo artigo, int qtde);
 	}
}
