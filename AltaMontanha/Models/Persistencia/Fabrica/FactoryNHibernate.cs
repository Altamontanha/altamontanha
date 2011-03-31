using System;
using AltaMontanha.Models.Persistencia.Nhibernate;

namespace AltaMontanha.Models.Persistencia.Fabrica
{
	public class FactoryNHibernate : IFactoryDAO
	{
		public Abstracao.IArtigoDAO GetArtigoDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IAventuraDAO GetAventuraDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IBannerDAO GetBannerDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.ICategoriaDAO GetCategoriaDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IColunaDAO GetColunaDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IComentarioDAO GetComentarioDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IFotoDAO GetFotoDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.ILinkDAO GetLinkDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.ILocalDAO GetLocalDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.INoticiaDAO GetNoticiaDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IPalavraChaveDAO GetPalavraChaveDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IPerfilDAO GetPerfilDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}

		public Abstracao.IRotaDAO GetRotaDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}
		/// <summary>
		/// Retorna um objeto DAO para MySQL
		/// </summary>
		/// <returns></returns>
		public Abstracao.IUsuarioDAO GetUsuarioDAO()
		{
			return new UsuarioNHibernate();
		}

		public Abstracao.IPermissaoDAO GetPermissaoDAO()
		{
			// TODO : Implementar.
			throw new NotImplementedException();
		}
	}
}