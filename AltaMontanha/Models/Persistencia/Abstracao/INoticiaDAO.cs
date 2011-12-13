using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface INoticiaDAO : IDAO<Dominio.Noticia>
	{
		IList<Dominio.Noticia> Pesquisar(Dominio.Noticia objeto, short qtde);
	}
}
