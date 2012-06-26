using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaMontanha.Models.Persistencia.Fabrica
{
	public interface IFactoryDAO
	{
		Abstracao.IArtigoDAO GetArtigoDAO();
		Abstracao.IAventuraDAO GetAventuraDAO();
		Abstracao.IBannerDAO GetBannerDAO();
		Abstracao.ICategoriaDAO GetCategoriaDAO();
		Abstracao.IColunaDAO GetColunaDAO(bool hibernate = true);
		Abstracao.IComentarioDAO GetComentarioDAO();
		// Abstracao.IConteudoDAO GetArtigoDAO(); // TODO: Verificar necessidade desse método.
		Abstracao.IFotoDAO GetFotoDAO();
		Abstracao.ILinkDAO GetLinkDAO();
		Abstracao.ILocalDAO GetLocalDAO();
		Abstracao.INoticiaDAO GetNoticiaDAO();
		Abstracao.IPalavraChaveDAO GetPalavraChaveDAO();
		Abstracao.IPerfilDAO GetPerfilDAO();
		Abstracao.IRotaDAO GetRotaDAO();
		Abstracao.IUsuarioDAO GetUsuarioDAO();
        Abstracao.IPermissaoDAO GetPermissaoDAO();
        Abstracao.ITelaDAO GetTelaDAO();
        Abstracao.IServicoDAO GetServicoDAO();
        Abstracao.ICategoriaEmpresaDAO GetCategoriaEmpresaDAO();
	}
}
