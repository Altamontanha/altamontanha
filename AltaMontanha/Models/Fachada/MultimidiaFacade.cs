using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using System.IO;
using System.Transactions;

namespace AltaMontanha.Models.Fachada
{
	public class MultimidiaFacade
	{
		#region Foto

		public IList<Dominio.Foto> PesquisarFoto(Dominio.Foto foto)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();

				return fotoDAO.Pesquisar(foto);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Foto SalvarFoto(Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				//using (TransactionScope transacao = new TransactionScope())
				//{
					if (foto == null)
						throw new ArgumentNullException("foto");

					IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
					IFotoDAO fotoDAO = fabrica.GetFotoDAO();

					foto.Caminho = this.SalvarArquivo(file);
					fotoDAO.Cadastrar(foto);
					
					//transacao.Complete();

					return foto;
				//}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirFoto(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();

				return fotoDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		#endregion

		#region Banner

		public IList<Dominio.Banner> PesquisarBanner(Dominio.Banner banner)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				return bannerDAO.Pesquisar(banner);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Dominio.Banner SalvarBanner(Dominio.Banner banner)
		{
			try
			{
				if(banner == null)
					throw new ArgumentNullException("usuario");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				return bannerDAO.Cadastrar(banner);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool ExcluirBanner(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();
							
				return bannerDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		#endregion

		/// <summary>
		/// Salva um arquivo de Multimidia em disco.
		/// </summary>
		/// <param name="file">Arquivo de upload</param>
		/// <returns>Caminho</returns>
		public string SalvarArquivo(HttpPostedFileBase file)
		{
			try
			{
				string caminho = string.Empty;
				string nome = string.Empty;

				if (file.ContentLength > 0)
				{
					nome = Path.GetFileName(file.FileName);
					caminho = HttpContext.Current.Server.MapPath("~/Temp");

					caminho = string.Format(@"{0}\{1}", caminho, nome);

					file.SaveAs(caminho);
				}

				return caminho;
			}
			catch (IOException e)
			{
				throw new ApplicationException("Erro ao salvar arquivo!", e);
			}
			catch (Exception)
			{
				throw;
			}

		}
	}
}