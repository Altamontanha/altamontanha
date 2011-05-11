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
		/// <summary>
		/// Cadastra uma nova foto em banco
		/// </summary>
		/// <param name="foto">objetos com os dados da foto</param>
		/// <param name="file">arquivo da foto</param>
		/// <returns>Foto cadastrada com o código gerado</returns>
		public Dominio.Foto SalvarFoto(Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				if (foto == null)
					throw new ArgumentNullException("foto");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();
				string path = "~/App_Data/Foto/";

				foto.Caminho = string.Format("Usuario/{0}.jpg", foto.Legenda);

				this.SalvarImagem
				(
					this.RedimensionarImagem(file.InputStream, 160, 120),
					HttpContext.Current.Server.MapPath(path + foto.Caminho)
				);
				
				if (foto.Codigo == 0)
					return fotoDAO.Cadastrar(foto);

				fotoDAO.Alterar(foto);

				return foto;
				//}
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
		/// <param name="file">arquivo da foto</param>
		/// <returns>Foto cadastrada com o código gerado</returns>
		public Dominio.Foto SalvarFotoGaleria(Dominio.Foto foto, HttpPostedFileBase file)
		{
			try
			{
				// TODO: utilizar Enum?
				List<string> tamanhos = new List<string>(3);
				tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Grande");
				tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Media");
				tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Pequena");

				// TODO: verificar transação
				// using (TransactionScope transacao = new TransactionScope())
				//{
				if (foto == null)
					throw new ArgumentNullException("foto");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IFotoDAO fotoDAO = fabrica.GetFotoDAO();
				string caminho = string.Empty;

				foto.Caminho = this.GerarCaminhoImagem(file);

				Regex r = new Regex(@"^.*\\([A-z]+)$");
				int largura = 640;
				int altura = 480;

				foreach (string tamanho in tamanhos)
				{
					caminho = string.Format("{0}\\{1}", tamanho, foto.Caminho.Replace("/", @"\"));

					// TODO: Refactor
					if (r.Replace(tamanho, "$1").Equals("Pequena"))
					{
						largura = 160;
						altura = 120;
					}
					else if (r.Replace(tamanho, "$1").Equals("Media"))
					{
						largura = 320;
						altura = 240;
					}

					this.SalvarImagem
					(
						this.RedimensionarImagem(file.InputStream, largura, altura),
						caminho
					);
				}

				//transacao.Complete();

				if (foto.Codigo == 0)
					fotoDAO.Cadastrar(foto);

				fotoDAO.Alterar(foto);

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

				this.ExcluirImagem(fotoDAO.Pesquisar(codigo).Caminho);

				return fotoDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		//
		public void VincularFoto()
		{

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
		/// <param name="file"></param>
		/// <returns></returns>
		private string GerarCaminhoImagem(HttpPostedFileBase file)
		{
			// TODO: utilizar Enum?
			List<string> tamanhos = new List<string>(3);
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Grande");
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Media");
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Pequena");

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
				
				foreach (string tamanho in tamanhos)
				{

					diretorio = string.Format(@"{0}\{1}", tamanho, data);

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
			// TODO: utilizar Enum?
			List<string> tamanhos = new List<string>(3);
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Grande");
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Media");
			tamanhos.Add(HttpContext.Current.Server.MapPath("~/App_Data/Foto") + "\\Pequena");
			
			foreach (string tamanho in tamanhos)
				if (File.Exists(string.Format(@"{0}\{1}", tamanho, caminho.Replace("/", @"\"))))
					File.Delete(string.Format(@"{0}\{1}", tamanho, caminho.Replace("/", @"\")));	
		}

		/// <summary>
		/// Exclui a imagem referentes a uma foto do disco.
		/// </summary>
		/// <param name="caminho">Caminho relativo da Imagem</param>
		public void ExcluirImagem(string caminho)
		{
			string path = HttpContext.Current.Server.MapPath("~/App_Data/Foto");

			if (File.Exists(string.Format(@"{0}\{1}",path, caminho.Replace("/", @"\"))))
				File.Delete(string.Format(@"{0}\{1}", path, caminho.Replace("/", @"\")));
		}

		#endregion

		#region Banner

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
				throw e;
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
				throw e;
			}
		}
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

		#endregion
	}
}