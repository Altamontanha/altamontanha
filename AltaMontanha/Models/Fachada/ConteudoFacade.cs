using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using System.IO;
using AltaMontanha.Models.Dominio;
using System.Text.RegularExpressions;

namespace AltaMontanha.Models.Fachada
{
	public class ConteudoFacade
	{
		UsuarioFacade usuarioFacade = new UsuarioFacade();

		#region PalavraChave
		/// <summary>
		/// Recebendo uma lista de palavras, verifica se já existe e não existindo
		/// inclui para retonar a lista com todos os objetos das palavras cadastradas 
		/// ou não.
		/// </summary>
		/// <param name="palavras"></param>
		public IList<Dominio.PalavraChave> SalvarPalavraChave(string[] palavras)
		{
			IList<Dominio.PalavraChave> palavrasChave = new List<Dominio.PalavraChave>();

			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPalavraChaveDAO palavraChaveDAO = fabrica.GetPalavraChaveDAO();
				foreach (string p in palavras)
				{
					IList<Dominio.PalavraChave> palavrasTemp = palavraChaveDAO.Pesquisar(new Dominio.PalavraChave() { Nome = p.Trim() });

					if ((palavrasTemp.Count > 0) && (!palavrasChave.Contains(palavrasTemp.First())))
						palavrasChave.Add(palavrasTemp.First());
					else
						palavrasChave.Add(palavraChaveDAO.Cadastrar(new Dominio.PalavraChave() { Nome = p.Trim() }));
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return palavrasChave;
		}

		/// <summary>
		/// Pesquisa palavra-chave por objeto
		/// </summary>
		/// <param name="palavraChave"></param>
		/// <returns></returns>
		public IList<Dominio.PalavraChave> PesquisarPalavraChave(Dominio.PalavraChave palavraChave)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IPalavraChaveDAO palavraChaveDAO = fabrica.GetPalavraChaveDAO();

				return palavraChaveDAO.Pesquisar(palavraChave);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		#endregion

		#region Noticia

		public Dominio.Noticia PesquisarNoticia(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				INoticiaDAO noticiaDAO = fabrica.GetNoticiaDAO();

				return noticiaDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<Dominio.Noticia> PesquisarNoticia(Dominio.Noticia noticia, int qtde=0)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				INoticiaDAO noticiaDAO = fabrica.GetNoticiaDAO();

				if(qtde > 0)
					return noticiaDAO.Pesquisar(noticia, qtde);

				return noticiaDAO.Pesquisar(noticia);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Noticia SalvarNoticia(Dominio.Noticia noticia)
		{
			try
			{
				if(noticia == null)
					throw new ArgumentNullException("noticia");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				INoticiaDAO noticiaDAO = fabrica.GetNoticiaDAO();

				if (noticia.UsuarioCadastro == null)
					noticia.UsuarioCadastro = Utilitario.Sessao.UsuarioLogado;
				
				if (noticia.Codigo <= 0)
					return noticiaDAO.Cadastrar(noticia);

				noticiaDAO.Alterar(noticia);
				return noticia;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirNoticia(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				INoticiaDAO noticiaDAO = fabrica.GetNoticiaDAO();
							
				return noticiaDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion
		
		#region Coluna

		public Dominio.Coluna PesquisarColuna(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IColunaDAO colunaDAO = fabrica.GetColunaDAO();

				return colunaDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<Dominio.Coluna> PesquisarColuna(Dominio.Coluna coluna, int qtde=0)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IColunaDAO colunaDAO = fabrica.GetColunaDAO();
				
				if (qtde > 0)
					return colunaDAO.Pesquisar(coluna, qtde);

				return colunaDAO.Pesquisar(coluna);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Coluna SalvarColuna(Dominio.Coluna coluna)
		{
			try
			{
				if(coluna == null)
					throw new ArgumentNullException("coluna");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IColunaDAO colunaDAO = fabrica.GetColunaDAO();

				if (coluna.UsuarioCadastro == null)
					coluna.UsuarioCadastro = Utilitario.Sessao.UsuarioLogado;

				if (coluna.Codigo <= 0)
					return colunaDAO.Cadastrar(coluna);

				colunaDAO.Alterar(coluna);
				return coluna;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirColuna(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IColunaDAO colunaDAO = fabrica.GetColunaDAO();
							
				return colunaDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion

		#region Aventura

		public Dominio.Aventura PesquisarAventura(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();

				return aventuraDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<Dominio.Aventura> PesquisarAventura(Dominio.Aventura aventura, int qtde=0)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();

				if(qtde > 0)
					return aventuraDAO.Pesquisar(aventura, qtde);

				return aventuraDAO.Pesquisar(aventura);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Aventura SalvarAventura(Dominio.Aventura aventura, HttpPostedFileBase arquivoRota)
		{
			try
			{
				if(aventura == null)
					throw new ArgumentNullException("aventura");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();
								
				if(aventura.UsuarioCadastro == null)
					aventura.UsuarioCadastro = Utilitario.Sessao.UsuarioLogado;
				
				if (arquivoRota != null)
				{
					string caminho = "~/AppData/Rota/";
					string nomeArquivo = new Regex(@"[^0-9]").Replace(DateTime.Now.ToString(), "") + Path.GetFileName(arquivoRota.FileName);

					if (aventura.Rota == null)
						aventura.Rota = new Rota() { Caminho = string.Format("Rota/{0}", nomeArquivo) };

					this.SalvarArquivo(caminho, nomeArquivo, arquivoRota);
				}

				if (aventura.Codigo <= 0)
					return aventuraDAO.Cadastrar(aventura);

				aventuraDAO.Alterar(aventura);
				return aventura;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirAventura(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();
							
				return aventuraDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public void ConvertToKml()
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public string SalvarArquivo(string caminho, string nomeArquivo, HttpPostedFileBase arquivo)
		{
			try
			{
				if (arquivo.ContentLength > 0)
				{
					caminho = HttpContext.Current.Server.MapPath(caminho);

					DirectoryInfo dir = null;

					if (!Directory.Exists(caminho))
						dir = Directory.CreateDirectory(caminho);

					arquivo.SaveAs(caminho + nomeArquivo);
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

		#region Artigo

		#region Categoria

		public IList<Dominio.Categoria> PesquisarCategoria(Dominio.Categoria categoria)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ICategoriaDAO categoriaDAO = fabrica.GetCategoriaDAO();

				return categoriaDAO.Pesquisar(categoria);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Categoria PesquisarCategoria(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ICategoriaDAO categoriaDAO = fabrica.GetCategoriaDAO();

				return categoriaDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Categoria SalvarCategoria(Dominio.Categoria categoria)
		{
			try
			{
				if (categoria == null)
					throw new ArgumentNullException("categoria");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ICategoriaDAO categoriaDAO = fabrica.GetCategoriaDAO();

				if (categoria.Codigo <= 0)
					return categoriaDAO.Cadastrar(categoria);

				categoriaDAO.Alterar(categoria);
				return categoria;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirCategoria(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				ICategoriaDAO categoriaDAO = fabrica.GetCategoriaDAO();
							
				return categoriaDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion

		public Dominio.Artigo PesquisarArtigo(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();

				return artigoDAO.Pesquisar(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<Dominio.Artigo> PesquisarArtigo(Dominio.Artigo artigo, int qtde=0)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();

				if (qtde > 0)
					return artigoDAO.Pesquisar(artigo, qtde);

				return artigoDAO.Pesquisar(artigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Dominio.Artigo SalvarArtigo(Dominio.Artigo artigo)
		{
			try
			{
				if(artigo == null)
					throw new NotImplementedException("artigo");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();

				if (artigo.UsuarioCadastro == null)
					artigo.UsuarioCadastro = Utilitario.Sessao.UsuarioLogado;

				if (artigo.Codigo <= 0)
					return artigoDAO.Cadastrar(artigo);

				artigoDAO.Alterar(artigo);
				return artigo;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool ExcluirArtigo(int codigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();
							
				return artigoDAO.Excluir(codigo);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion
	}
}