using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Otaviano.Conexao;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AltaMontanha.Models.Persistencia.MySQL
{
	public class ColunaMySQL : Abstracao.IColunaDAO
	{
		private Conexao conexao = ConexaoFactory.GetConexao(TipoProvedorDados.MySQL, ConfigurationManager.ConnectionStrings["StringConexao"].ConnectionString);

		public List<Dominio.Coluna> PesquisarUltimasColunas()
		{
			string sql = string.Empty;
			List<Dominio.Coluna> ListaColuna = new List<Dominio.Coluna>();
			Dominio.Coluna coluna = null;
			IDataReader reader = null;

			try
			{
				sql = @"SELECT DISTINCT
							C.CodConteudo,
							C.Titulo, 
							C.Data, 
							U.CodUsuario, 
							U.Nome,
							U.Login,
							F.CodFoto,
							F.Caminho,
							F.Legenda
						FROM tb_conteudo C
						INNER JOIN tb_coluna COL ON (COL.codConteudo = C.codConteudo)
						INNER JOIN tb_usuario U ON (U.codUsuario = COL.codUsuario)
						INNER JOIN tb_foto F ON (F.CodFoto = U.codFoto)
						INNER JOIN (
										SELECT MAX(DATA), CodConteudo
										FROM tb_conteudo C2
										GROUP BY codUsuario, CodConteudo
									)
									tb ON tb.CodConteudo = C.CodConteudo
						ORDER BY C.Data DESC
						LIMIT 6";

				reader = this.conexao.ExecutarDataReader(sql, CommandType.Text);

				while (reader.Read())
				{
					coluna = new Dominio.Coluna();

					coluna.Codigo = Convert.ToInt32(reader["CodConteudo"]);
					coluna.Titulo = Convert.ToString(reader["Titulo"]);
					coluna.Autor = new Dominio.Usuario()
					{
						Codigo = Convert.ToInt32(reader["CodUsuario"]),
						Nome = Convert.ToString(reader["Nome"]),
						Login = Convert.ToString(reader["Login"]),
						Foto = new Dominio.Foto()
						{
							Codigo = Convert.ToInt32(reader["CodFoto"]),
							Caminho = Convert.ToString(reader["Caminho"]),
							Legenda = Convert.ToString(reader["Legenda"])
						}
					};

					ListaColuna.Add(coluna);
				}

				return ListaColuna;
			}
			catch (MySqlException ex)
			{
				throw new ApplicationException("Ocorreu um erro ao acessar o banco de dados!", ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}
			}
		}

		#region Interface methods

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto, short qtde)
		{
			throw new NotImplementedException();
		}

		public void Alterar(Dominio.Coluna objeto)
		{
			throw new NotImplementedException();
		}

		public Dominio.Coluna Cadastrar(Dominio.Coluna objeto)
		{
			throw new NotImplementedException();
		}

		public Dominio.Coluna Pesquisar(int codigo)
		{
			throw new NotImplementedException();
		}

		public bool Excluir(int codigo)
		{
			throw new NotImplementedException();
		}

		public IList<Dominio.Coluna> Pesquisar(Dominio.Coluna objeto, int pagina = 0)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}