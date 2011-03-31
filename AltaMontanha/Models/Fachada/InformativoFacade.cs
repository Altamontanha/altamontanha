using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;

namespace AltaMontanha.Models.Fachada
{
	public class InformativoFacade
	{
		public IList<Dominio.Link> PesquisarLink(Dominio.Link link)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();

				return linkDAO.Pesquisar(link);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		public Dominio.Link SalvarLink(Dominio.Link link)
		{
			try
			{
				if(link == null)
					throw new ArgumentNullException("link");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();

				return linkDAO.Cadastrar(link);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		public bool ExcluirLink(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();
							
				return linkDAO.Excluir(codigo);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}
	}
}