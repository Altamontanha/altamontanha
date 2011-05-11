using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using System.IO;
using System.Transactions;
using System.Drawing;
using System.Drawing.Imaging;

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
				Dominio.Foto fotoPequena = null;
				Dominio.Foto fotoMedia = null;
				Dominio.Foto fotoGrande = null;

				//using (TransactionScope transacao = new TransactionScope())
				//{
				if (foto == null)
					throw new ArgumentNullException("foto");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();

				// Grande
				fotoGrande = foto.Clone();
				fotoGrande.Caminho = this.GerarCaminhoImagem(file, "Grande");
				
				this.SalvarImagem
				(
					this.RedimensionarImagem(file.InputStream, 640, 480),
					fotoGrande.Caminho
				);

				// Media
				fotoMedia = foto.Clone();
				fotoMedia.FotoPai = fotoGrande;
				fotoMedia.Caminho = this.GerarCaminhoImagem(file, "Media");
				
				this.SalvarImagem
				(
					this.RedimensionarImagem(file.InputStream, 320, 240),
					fotoMedia.Caminho
				);

				// Pequena
				fotoPequena = foto.Clone();
				fotoPequena.FotoPai = fotoMedia;
				fotoPequena.Caminho = this.GerarCaminhoImagem(file, "Pequena");

				this.SalvarImagem
				(
					this.RedimensionarImagem(file.InputStream, 160, 120),
					fotoPequena.Caminho
				);
				
				fotoDAO.Cadastrar(fotoPequena);
				
				//transacao.Complete();

				return fotoPequena;
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
				if (banner == null)
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
		/// <summary>
		/// Redimensiona o tamanho da imagem.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="altura"></param>
		/// <param name="largura"></param>
		/// <returns></returns>
		private Bitmap RedimensionarImagem(Stream stream, int altura, int largura)
		{
			// Carrega imagem original
			Bitmap original = (Bitmap)Image.FromStream(stream);
			// Bitmap para nova imagem com o novo tamanho
			Bitmap modificada = new Bitmap(altura, largura);
			// Redimensiona imagem
			Graphics g = Graphics.FromImage(modificada);
			g.DrawImage(original, new Rectangle(0, 0, modificada.Width, modificada.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
			g.Dispose();

			return modificada;
		}
		// TODO : Corrigir nome das pastas
		/// <summary>
		/// Cria o caminho que a imagem será salva.
		/// </summary>
		/// <param name="file"></param>
		/// <param name="tamanho"></param>
		/// <returns></returns>
		private string GerarCaminhoImagem(HttpPostedFileBase file, string tamanho)
		{
			string caminho = string.Empty;
			string nome = string.Empty;
			string pasta = string.Empty;
			DirectoryInfo dir = null;

			if (file.ContentLength > 0)
			{
				nome = DateTime.Now.ToString().Replace("/","").Replace(":", "").Replace(" ", "") + Path.GetFileName(file.FileName);
				caminho = HttpContext.Current.Server.MapPath("~/App_Data/Foto");
				pasta = string.Format("{0}_{1}", DateTime.Now.Month, DateTime.Now.Year);

				caminho = string.Format(@"{0}\{1}\{2}", caminho, tamanho, pasta);

				if (!Directory.Exists(caminho))
					dir = Directory.CreateDirectory(caminho);
				
				caminho = string.Format(@"{0}\{1}", caminho, nome);

			}

			return caminho;
		}
		/// <summary>
		/// Salva uma imagem com jpeg.
		/// </summary>
		/// <param name="imagem"></param>
		/// <param name="caminho"></param>
		public void SalvarImagem(Bitmap imagem, string caminho)
		{
			if (imagem == null)
				throw new ArgumentNullException("imagem");
			if (string.IsNullOrEmpty(caminho))
				throw new ArgumentNullException("caminho");

			imagem.Save(caminho, ImageFormat.Jpeg);
		}
	}
}