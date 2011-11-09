using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Otaviano.Conexao;
using System.Configuration;

namespace AltaMontanha.Models.Persistencia.MySQL
{
	public class ConteudoMySQL //: Abstracao.IConteudo
	{
		private Conexao conexao = ConexaoFactory.GetConexao(TipoProvedorDados.MySQL, ConfigurationManager.ConnectionStrings["StringConexao"].ConnectionString);
		// TODO : testar em artigo
		public Dominio.Conteudo VincularFotos(Dominio.Conteudo conteudo)
		{
			string sql = string.Empty;

			try
			{
				sql = "INSERT INTO tb_conteudofoto (CodConteudo, CodFoto)" +
					   "VALUES(@CodConteudo, @CodFoto);" +
					   "SELECT LAST_INSERT_ID();";

				foreach (var item in conteudo.ListaFotos)
				{

					IDataParameter[] parametros = new IDataParameter[]
					{
						new MySqlParameter("@CodConteudo", conteudo.Codigo),
						new MySqlParameter("@CodFoto", item.Codigo)
					};

					item.Codigo = Convert.ToInt32(this.conexao.ExecutarEscalar(sql, CommandType.Text, parametros));
				}

				return conteudo;
			}
			catch (MySqlException ex)
			{
				throw new ApplicationException("Ocorreu um erro ao acessar o banco de dados!", ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

}
