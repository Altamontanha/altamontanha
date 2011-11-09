using System.Data;
using Npgsql;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados Postgre.
	/// </summary>
	public class ConexaoPostgre	: Conexao
	{
		#region Atributos / Propriedades

		#endregion
		
		#region Métodos

		/// <summary>
		/// Construtor da Classe de ConexaoPostgre
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados </param>
		internal ConexaoPostgre(string stringConexao)
		{
			this.StringConexao = stringConexao;	
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao Oracle</returns>
		internal override IDbConnection GetConexao()
		{
			return new  NpgsqlConnection();			
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando Oracle</returns>
		internal override IDbCommand GetComando()
		{
			return new   NpgsqlCommand();
		}
		/// <summary>
		/// Sobrescreve a Interfase de DataAdapter
		/// </summary>
		/// <returns>DataAdapter Oracle</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new   NpgsqlDataAdapter();
		}
		/// <summary>
		/// Sobrescreve o metodo de GetParametroRetorno, adiciona o parametro de "@RETURN_VALUE".
		/// </summary>
		/// <returns>Retorna uma colecao de parametros com o parametro "@RETURN_VALUE" </returns>
		internal override IDataParameter GetParametroRetorno()
		{

			NpgsqlParameter  p = new NpgsqlParameter("@RETURN_VALUE",DbType.Int32);
			p.Direction = ParameterDirection.ReturnValue;

			
			return p;
		}

		#endregion
	}
}
