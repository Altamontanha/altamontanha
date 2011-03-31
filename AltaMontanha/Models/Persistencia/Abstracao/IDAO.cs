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
		IList<T> Pesquisar(T objeto);
		T Pesquisar(int codigo); // TODO: Adicionar na documentação.
		bool Excluir(int codigo);
	}
}
