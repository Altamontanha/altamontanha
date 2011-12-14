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
using System.Text.RegularExpressions;

namespace AltaMontanha.Models.Fachada
{
	public class MultimidiaFacade
	{
		#region Foto

		/// <summary>
		/// Array com os tamanhos necessários das fotos que serão redimensionadas
		/// </summary>
		private static dynamic[] tamanhos = { 
											   new { largura = 50,  altura = 50,  caminho = "50x50"   },
											   new { largura = 145, altura = 95,  caminho = "145x95"  },
											   new { largura = 145, altura = 105, caminho = "145x105" },
											   new { largura = 160, altura = 115, caminho = "160x115" },
											   new { largura = 220, altura = 165, caminho = "220x165" },
											   new { largura = 340, altura = 240, caminho = "340x240" },
											   new { largura = 640, altura = 480, caminho = "640x480" },
										   };

		/// <summary>
		/// Consulta de uma foto já cadastrada.
		/// </summary>
		/// <param name="codigo">código da foto</param>
		/// <returns>foto referente ao código informado</returns>
		public Dominio.Foto PesquisarFoto(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();
				return fotoDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Consulta de fotos cadastradas.
		/// </summary>
		/// <param name="foto">objeto fotos com os parametros de consulta</param>
		/// <returns>lista das fotos referentes a pesquisa.</returns>
		public IList<Dominio.Foto> PesquisarFoto(Dominio.Foto foto, int pagina = 0)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();

				return fotoDAO.Pesquisar(foto, pagina);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Cadastra uma nova foto em banco
		/// </summary>
		/// <param name="foto">objetos com os dados da foto</param>
		/// <param name="arquivo">arquivo da foto</param>
		/// <returns>Foto cadastrada com o código gerado</returns>
		public Dominio.Foto SalvarFoto(Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				if (foto == null)
					throw new ArgumentNullException("foto");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();
				string path = "~/AppData/Foto/";

				foto.Caminho = string.Format("Usuario/{0}.jpg", foto.Legenda);

				this.SalvarImagem
				(
					this.RedimensionarImagem(file.InputStream, 90, 90),HttpContext.Current.Server.MapPath(path + foto.Caminho)
				);

				foto.Galeria = false;

				if (foto.Codigo == 0)
					return fotoDAO.Cadastrar(foto);

				fotoDAO.Alterar(foto);

				return foto;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Cadastra uma nova foto em banco,
		/// Salvando todos os tamanhos necessários para galeria.
		/// </summary>
		/// <param name="foto">objetos com os dados da foto</param>
		/// <param name="arquivo">arquivo da foto</param>
		/// <returns>Foto cadastrada com o código gerado</returns>
		public Dominio.Foto SalvarFotoGaleria(Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				// TODO: verificar transação
				// using (TransactionScope transacao = new TransactionScope())
				//{
				if (foto == null)
					throw new ArgumentNullException("foto");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();
				string caminho = string.Empty;

				foto.Caminho = this.GerarCaminhoImagem(file);

				foreach (dynamic tamanho in tamanhos)
				{
					caminho = string.Format("{0}\\{1}\\{2}", HttpContext.Current.Server.MapPath("~/AppData/Foto"), tamanho.caminho, foto.Caminho.Replace("/", @"\"));

					this.SalvarImagem
					(
						this.RedimensionarImagem(file.InputStream, tamanho.largura, tamanho.altura),
						caminho
					);
				}

				foto.Galeria = true;

				if (foto.Codigo == 0)
					fotoDAO.Cadastrar(foto);

				fotoDAO.Alterar(foto);

				//transacao.Complete();

				return foto;
				//}
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Exclui uma foto do sistema.
		/// </summary>
		/// <param name="codigo">código da foto</param>
		/// <returns>caso a foto tenha sido excluida</returns>
		public bool ExcluirFoto(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();

				Dominio.Foto foto = fotoDAO.Pesquisar(codigo);

				if (fotoDAO.Excluir(foto.Codigo))
					this.ExcluirImagemGaleria(foto.Caminho);
				else
					return false;
				
				return true;
			}
			catch (Exception e)
			{
				throw e;
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
		/// <summary>
		/// Cria o caminho que a imagem será salva.
		/// </summary>
		/// <param name="arquivo"></param>
		/// <returns></returns>
		private string GerarCaminhoImagem(HttpPostedFileBase file)
		{
			string arquivo = string.Empty;
			string data = string.Empty;
			string diretorio = string.Empty;
			string caminho = string.Empty;
			DirectoryInfo dir = null;

			if (file.ContentLength > 0)
			{
				// formato: ANO_MES\DATAHORANOMEARQUIVO, exemplo: 2011_05\20110510102011Arquivo.jpg
				data = string.Format("{0}_{1}", DateTime.Now.Year, DateTime.Now.Month.ToString("00"));
				arquivo = new Regex(@"[^0-9]").Replace(DateTime.Now.ToString(), "") + Path.GetFileName(file.FileName);
				
				foreach (dynamic tamanho in tamanhos)
				{

					diretorio = string.Format(@"{0}\{1}\{2}", HttpContext.Current.Server.MapPath("~/AppData/Foto"), tamanho.caminho, data);

					if (!Directory.Exists(diretorio))
						dir = Directory.CreateDirectory(diretorio);
				
					caminho = string.Format(@"{0}/{1}", data, arquivo);
				}

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
		/// <summary>
		/// Exclui as imagens referentes a uma foto de galeria do disco.
		/// </summary>
		/// <param name="caminho">Caminho relativo da Imagem</param>
		public void ExcluirImagemGaleria(string caminho)
		{
			foreach (dynamic tamanho in tamanhos)
				if (File.Exists(string.Format(@"{0}\{1}\{2}", HttpContext.Current.Server.MapPath("~/AppData/Foto"), tamanho.caminho, caminho.Replace("/", @"\"))))
					File.Delete(string.Format(@"{0}\{1}\{2}", HttpContext.Current.Server.MapPath("~/AppData/Foto"), tamanho.caminho, caminho.Replace("/", @"\")));	
		}
		/// <summary>
		/// Exclui a imagem referentes a uma foto do disco.
		/// </summary>
		/// <param name="caminho">Caminho relativo da Imagem</param>
		public void ExcluirImagem(string caminho)
		{
			string path = HttpContext.Current.Server.MapPath("~/AppData/Foto");

			if (File.Exists(string.Format(@"{0}\{1}", path, caminho.Replace("/", @"\"))))
				File.Delete(string.Format(@"{0}\{1}", path, caminho.Replace("/", @"\")));
		}

		#endregion

		#region Banner

		/// <summary>
		/// Pesquisa banner com o código do local
		/// </summary>
		/// <param name="banner">Banner para pesquisa</param>
		/// <returns>List<Banner></returns>
		public Dominio.Banner PesquisarBannerPorLocal(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				Dominio.Banner banner = new Dominio.Banner();
				banner.Local = new Dominio.Local() { Codigo = codigo };

				IList<Dominio.Banner> banners = bannerDAO.Pesquisar(banner);
				return banners.First();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Pesquisar banner por código
		/// </summary>
		/// <param name="codigo">Código do banner</param>
		/// <returns>Banner</returns>
		public Dominio.Banner PesquisarBanner(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				return bannerDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Pesquisa banner com os atributos que estiverem preenchidos
		/// </summary>
		/// <param name="banner">Banner para pesquisa</param>
		/// <returns>List<Banner></returns>
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
				throw e;
			}
		}
		/// <summary>
		/// Pesquisa local pelos atributos que estiverem preenchidos
		/// </summary>
		/// <param name="local">Local para pesquisa</param>
		public IList<Dominio.Local> PesquisarLocal(Dominio.Local local)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ILocalDAO localDAO = fabrica.GetLocalDAO();

				return localDAO.Pesquisar(local);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Salva o banner e o arquivo
		/// </summary>
		/// <param name="banner">Banner</param>
		/// <param name="arquivo">Arquivo de imagem ou multimídia</param>
		public Dominio.Banner SalvarBanner(Dominio.Banner banner, HttpPostedFileBase arquivo)
		{
			try
			{
				if (banner == null)
					throw new ArgumentNullException("usuario");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				banner.Caminho = new Regex(@"[^0-9]").Replace(DateTime.Now.ToString(), "") + arquivo.FileName;
				string path = "~/AppData/Banner/" + banner.Caminho;

				this.SalvarArquivo(path, arquivo);

				if (banner.Codigo == 0)
					return bannerDAO.Cadastrar(banner);

				bannerDAO.Alterar(banner);

				return banner;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Exclui o registro de banner.
		/// </summary>
		/// <param name="codigo"></param>
		/// <returns></returns>
		public bool ExcluirBanner(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IBannerDAO bannerDAO = fabrica.GetBannerDAO();

				string path = HttpContext.Current.Server.MapPath("~/AppData/Banner");

				Dominio.Banner banner = bannerDAO.Pesquisar(codigo);
				if (File.Exists(string.Format(@"{0}\{1}", path, banner.Caminho)))
					File.Delete(string.Format(@"{0}\{1}", path, banner.Caminho));

				return bannerDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// Salva um arquivo de Multimidia em disco.
		/// </summary>
		/// <param name="caminho">Caminho para salvar o arquivo</param>
		/// <param name="arquivo">Arquivo de upload</param>
		/// <returns>Caminho</returns>
		public string SalvarArquivo(string caminho, HttpPostedFileBase arquivo)
		{
			try
			{
				if (arquivo.ContentLength > 0)
				{
					caminho = HttpContext.Current.Server.MapPath(caminho);
					string nome = Path.GetFileName(caminho);
					caminho = Path.GetDirectoryName(caminho);

					DirectoryInfo dir = null;
					if (!Directory.Exists(caminho))
						dir = Directory.CreateDirectory(caminho);

					caminho = string.Format(@"{0}\{1}", caminho, nome);

					arquivo.SaveAs(caminho);
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

		#endregion
	}
}