using System.Data;
using Oracle.DataAccess.Client;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados Oracle.
	/// </summary>
	public class ConexaoOracle : Conexao
	{
		#region Métodos

		/// <summary>
		/// Construtor da Classe de ConexaoOracle
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados </param>
		internal ConexaoOracle(string stringConexao)
		{
			this.StringConexao = stringConexao;
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao Oracle</returns>
		internal override IDbConnection GetConexao()
		{
			return new OracleConnection();
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando Oracle</returns>
		internal override IDbCommand GetComando()
		{
			return new OracleCommand();
		}
		/// <summary>
		/// Sobrescreve a Interfase de DataAdapter
		/// </summary>
		/// <returns>DataAdapter Oracle</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new OracleDataAdapter();
		}
		/// <summary>
		/// Sobrescreve o metodo de GetParametroRetorno, adiciona o parametro de "@RETURN_VALUE".
		/// </summary>
		/// <returns>Retorna uma colecao de parametros com o parametro "@RETURN_VALUE" </returns>
		internal override IDataParameter GetParametroRetorno()
		{
			OracleParameter p = new OracleParameter("@RETURN_VALUE", OracleDbType.Int32);
			p.Direction = ParameterDirection.ReturnValue;
			
			return p;
		}

		#endregion
	}
}
