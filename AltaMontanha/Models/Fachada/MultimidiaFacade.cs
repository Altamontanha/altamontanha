using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Fachada
{
	public class MultimidiaFacade
	{
		#region Foto

		public List<Dominio.Foto> PesquisarFoto(Dominio.Foto foto)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public Dominio.Foto SalvarFoto(Dominio.Foto foto)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public bool ExcluirFoto(int codigo)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		#endregion

		#region Banner

		public List<Dominio.Banner> PesquisarPerfil(Dominio.Banner banner)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public Dominio.Banner SalvarPerfil(Dominio.Banner banner)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public bool ExcluirBanner(int codigo)
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		#endregion

		/// <summary>
		/// Salva um arquivo de Multimidia em disco.
		/// </summary>
		public void SalvarArquivo()
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}
	}
}