using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IColunaDAO : IDAO<Dominio.Coluna>
    {
        IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto, short qtde);
        IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto, int qtde, int pagina, int[] Codigos);
	}
}
