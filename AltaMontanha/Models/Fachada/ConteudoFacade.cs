using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;

namespace AltaMontanha.Models.Fachada
{
	public class ConteudoFacade
	{
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
				throw new Exception(e.Message);
			}
		}

		public IList<Dominio.Noticia> PesquisarNoticia(Dominio.Noticia noticia)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				INoticiaDAO noticiaDAO = fabrica.GetNoticiaDAO();

				return noticiaDAO.Pesquisar(noticia);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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

				if (noticia.Codigo <= 0)
					return noticiaDAO.Cadastrar(noticia);

				noticiaDAO.Alterar(noticia);
				return noticia;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		public IList<Dominio.Coluna> PesquisarColuna(Dominio.Coluna coluna)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IColunaDAO colunaDAO = fabrica.GetColunaDAO();

				return colunaDAO.Pesquisar(coluna);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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

				if (coluna.Codigo <= 0)
					return colunaDAO.Cadastrar(coluna);

				colunaDAO.Alterar(coluna);
				return coluna;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		public IList<Dominio.Aventura> PesquisarAventura(Dominio.Aventura aventura)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();

				return aventuraDAO.Pesquisar(aventura);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Dominio.Aventura SalvarAventura(Dominio.Aventura aventura)
		{
			try
			{
				if(aventura == null)
					throw new ArgumentNullException("aventura");

				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();

				if (aventura.Codigo <= 0)
					return aventuraDAO.Cadastrar(aventura);

				aventuraDAO.Alterar(aventura);
				return aventura;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		public void ConvertToKml()
		{
			// TODO : Implementar
			throw new NotImplementedException();
		}

		public void SalvaArquivo()
		{
			// TODO : Implementar
			throw new NotImplementedException();
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		public IList<Dominio.Artigo> PesquisarArtigo(Dominio.Artigo artigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();

				return artigoDAO.Pesquisar(artigo);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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

				if (artigo.Codigo <= 0)
					return artigoDAO.Cadastrar(artigo);

				artigoDAO.Alterar(artigo);
				return artigo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				throw new Exception(e.Message);
			}
		}

		#endregion
	}
}