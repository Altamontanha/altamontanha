using System.Data;
using MySql.Data.MySqlClient;

namespace Otaviano.Conexao
{
	public class ConexaoMySQL : Conexao
	{
		#region Métodos

		/// <summary>
		/// Construtor da Classe de ConexaoSql.
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados</param>
		internal ConexaoMySQL(string stringConexao)
		{
			this.StringConexao = stringConexao;
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao SQL Server</returns>
		internal override System.Data.IDbConnection GetConexao()
		{
			return new MySqlConnection();	
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando SQL Server</returns>
		internal override System.Data.IDbCommand GetComando()
		{
			return new MySqlCommand();
		}
		/// <summary>
		/// Recupera o objeto IDbDataAdapter.
		/// </summary>
		/// <returns>DataAdapter SQL Server</returns>
		internal override System.Data.IDbDataAdapter GetDataAdapter()
		{
			return new MySqlDataAdapter();
		}
		/// <summary>
		/// Recupera um o parametro de retorno(@RETURN_VALUE).
		/// </summary>
		/// <returns>Parametro de retorno</returns>
		internal override System.Data.IDataParameter GetParametroRetorno()
		{
			MySqlParameter parametro = new MySqlParameter("@RETURN_VALUE", MySqlDbType.Int32);
			parametro.Direction = ParameterDirection.ReturnValue;

			return parametro;
		}

		#endregion
	}
}
