using System;
using AltaMontanha.Models.Persistencia.Nhibernate;

namespace AltaMontanha.Models.Persistencia.Fabrica
{
	public class FactoryNHibernate : IFactoryDAO
	{
		public Abstracao.IArtigoDAO GetArtigoDAO()
		{
			return new Nhibernate.ArtigoNHibernate();
		}

		public Abstracao.IAventuraDAO GetAventuraDAO()
		{
			return new Nhibernate.AventuraNHibernate();
		}

		public Abstracao.IBannerDAO GetBannerDAO()
		{
			return new Nhibernate.BannerNHibernate();
		}

		public Abstracao.ICategoriaDAO GetCategoriaDAO()
		{
			return new Nhibernate.CategoriaNHibernate();
		}

		public Abstracao.IColunaDAO GetColunaDAO(bool hibernate = true)
		{
			if (!hibernate)
				return new MySQL.ColunaMySQL();

			return new Nhibernate.ColunaNHibernate();
		}

		public Abstracao.IComentarioDAO GetComentarioDAO()
		{
			return new Nhibernate.ComentarioNHibernate();
		}

		public Abstracao.IFotoDAO GetFotoDAO()
		{
			return new Nhibernate.FotoNHibernate();
		}

		public Abstracao.ILinkDAO GetLinkDAO()
		{
			return new Nhibernate.LinkNHibernate();
		}

		public Abstracao.ILocalDAO GetLocalDAO()
		{
			return new Nhibernate.LocalNHibernate();
		}

		public Abstracao.INoticiaDAO GetNoticiaDAO()
		{
			return new Nhibernate.NoticiaNHibernate();
		}

		public Abstracao.IPalavraChaveDAO GetPalavraChaveDAO()
		{
			return new Nhibernate.PalavraChaveNHibernate();
		}

		public Abstracao.IPerfilDAO GetPerfilDAO()
		{
			return new Nhibernate.PerfilNHibernate();
		}

		public Abstracao.IRotaDAO GetRotaDAO()
		{
			return new Nhibernate.RotaNHibernate();
		}

		public Abstracao.ITelaDAO GetTelaDAO()
		{
			return new Nhibernate.TelaNHibernate();
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
            return new Nhibernate.PermissaoNHibernate();
        }

        public Abstracao.IServicoDAO GetServicoDAO()
        {
            return new Nhibernate.ServicoNHibernate();
        }

        public Abstracao.ICategoriaEmpresaDAO GetCategoriaEmpresaDAO()
        {
            return new Nhibernate.CategoriaEmpresaNHibernate();
        }
	}
}