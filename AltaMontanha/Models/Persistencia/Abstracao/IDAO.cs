using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
	public interface IDAO<T>
	{
		void Alterar(T objeto);
		T Cadastrar(T objeto);
		IList<T> Pesquisar(T objeto, int pagina = 0);
		T Pesquisar(int codigo);
		bool Excluir(int codigo);
	}
}
