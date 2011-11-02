using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace MigracaoFotos
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Migracao
    { 
           /// <summary>
        /// Array com os tamanhos necessários das fotos que serão redimensionadas
        /// </summary>
        public static dynamic[] tamanhos = { 
											   new { largura = 50,  altura = 50,  caminho = "50x50"   },
											   new { largura = 145, altura = 95,  caminho = "145x95"  },
											   new { largura = 145, altura = 105, caminho = "145x105" },
											   new { largura = 160, altura = 115, caminho = "160x115" },
											   new { largura = 220, altura = 165, caminho = "220x165" },
											   new { largura = 340, altura = 240, caminho = "340x240" },
											   new { largura = 640, altura = 480, caminho = "640x480" }
										   };

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
		/// Cria o caminho que a imagem será salva.
		/// </summary>
		/// <param name="arquivo"></param>
		/// <returns></returns>
        public string GerarCaminhoImagem(string nomeArquivo)
		{
			string arquivo = string.Empty;
			string data = string.Empty;
			string diretorio = string.Empty;
			string caminho = string.Empty;
			DirectoryInfo dir = null;

			// formato: ANO_MES\DATAHORANOMEARQUIVO, exemplo: 2011_05\20110510102011Arquivo.jpg
			data = "2010/11";
				
			foreach (dynamic tamanho in tamanhos)
			{

				diretorio = string.Format(@"{0}\{1}\{2}", @"c:\Temp\AppData\Foto", tamanho.caminho, data);

				if (!Directory.Exists(diretorio))
					dir = Directory.CreateDirectory(diretorio);

                caminho = string.Format(@"{0}\{1}", data, nomeArquivo);
			}


			return caminho;
		}

        public void Importar()
        {
            string[] ListaNomeArquivos = Directory.GetFiles("c:/temp/OLD");

            foreach (var nomeArquivo in ListaNomeArquivos)
            {
                string caminho = this.GerarCaminhoImagem(nomeArquivo);

                foreach (dynamic tamanho in tamanhos)
                {
                    this.SalvarImagem
                    (
                        this.RedimensionarImagem
                        (
                            new FileStream("c:/temp/OLD" + nomeArquivo, FileMode.Open),
                            tamanho.largura,
                            tamanho.altura
                        ),
                        caminho
                    );
                } 
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
            Bitmap original = (Bitmap) Image.FromStream(stream);
            // Bitmap para nova imagem com o novo tamanho
            Bitmap modificada = new Bitmap(altura, largura);
            // Redimensiona imagem
            Graphics g = Graphics.FromImage(modificada);
            g.DrawImage(original, new Rectangle(0, 0, modificada.Width, modificada.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            g.Dispose();

            return modificada;
        }

    }
}
