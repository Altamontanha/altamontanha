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
		/// <summary>
		/// Pesquisa links pelos atributos do link
		/// </summary>
		/// <param name="link">objeto para pesquisa</param>
		public IList<Dominio.Link> PesquisarLink(Dominio.Link link)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();

				return linkDAO.Pesquisar(link);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Pesquisa link pelo código
		/// </summary>
		/// <param name="codigo">código para pesquisa</param>
		public Dominio.Link PesquisarLink(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();

				return linkDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Salva o link
		/// </summary>
		/// <param name="link">link para salvar</param>
		public Dominio.Link SalvarLink(Dominio.Link link)
		{
			try
			{
				if(link == null)
					throw new ArgumentNullException("link");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();

				if (link.Codigo <= 0)
					return linkDAO.Cadastrar(link);

				linkDAO.Alterar(link);
				return link;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Exclui link pelo código
		/// </summary>
		/// <param name="codigo">código do link</param>
		public bool ExcluirLink(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILinkDAO linkDAO = fabrica.GetLinkDAO();
							
				return linkDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}