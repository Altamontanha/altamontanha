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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		#endregion

		#region Aventura

		public IList<Dominio.Aventura> PesquisarAventura(Dominio.Aventura aventura)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IAventuraDAO aventuraDAO = fabrica.GetAventuraDAO();

				return aventuraDAO.Pesquisar(aventura);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		#endregion

		public IList<Dominio.Artigo> PesquisarArtigo(Dominio.Artigo artigo)
		{
			try
			{
				IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
				IArtigoDAO artigoDAO = fabrica.GetArtigoDAO();

				return artigoDAO.Pesquisar(artigo);
			}
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
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
			catch (Exception)
			{
				// TODO: Tratar erro.
				throw;
			}
		}

		#endregion
	}
}