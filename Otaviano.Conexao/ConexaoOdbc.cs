using System.Data;
using System.Data.Odbc;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados Microsoft Odbc.
	/// </summary>
	public class ConexaoOdbc : Conexao
	{
		#region Atributos / Propriedades

		#endregion
				
		#region Métodos

		/// <summary>
		/// Construtor da Classe de ConexaoOdbc
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados</param>
		internal ConexaoOdbc(string stringConexao)
		{
			this.StringConexao = stringConexao;
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao Odbc</returns>
		internal override IDbConnection GetConexao()
		{
			return new OdbcConnection();			
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando Odbc</returns>
		internal override IDbCommand GetComando()
		{
			return new OdbcCommand();
		}
		/// <summary>
		/// Sobrescreve a Interfase de DataAdapter
		/// </summary>
		/// <returns>DataAdapter Odbc</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new OdbcDataAdapter();
		}
		/// <summary>
		/// Sobrescreve o metodo de Paramatro Retorno,adiciona o parametro de "@RETURN_VALUE".
		/// </summary>
		/// <returns>Retorna uma colecao de parametros com o parametro "@RETURN_VALUE" </returns>
		internal override IDataParameter GetParametroRetorno()
		{
			OdbcParameter p = new OdbcParameter("@RETURN_VALUE",OdbcType.Int);
			p.Direction = ParameterDirection.ReturnValue;
			
			return p;
		}

		#endregion
	}
}
