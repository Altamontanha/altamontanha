using System.Data;
using System.Data.SqlClient;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados Microsoft SQL Server.
	/// </summary>
	public class ConexaoMSSQL : Conexao
	{
		#region Atributos/Propriedades

		#endregion

		#region Métodos
		
		/// <summary>
		/// Construtor da Classe de ConexaoSql.
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados</param>
		internal ConexaoMSSQL(string stringConexao)
		{
			this.StringConexao = stringConexao;
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao SQL Server</returns>
		internal override IDbConnection GetConexao()
		{
			return new SqlConnection();			
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando SQL Server</returns>
		internal override IDbCommand GetComando()
		{
			return new SqlCommand();
		}
		/// <summary>
		/// Recupera o objeto IDbDataAdapter.
		/// </summary>
		/// <returns>DataAdapter SQL Server</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new SqlDataAdapter();
		}
		/// <summary>
		/// Recupera um o parametro de retorno(@RETURN_VALUE).
		/// </summary>
		/// <returns>Parametro de retorno</returns>
		internal override IDataParameter GetParametroRetorno()
		{
			SqlParameter p = new SqlParameter("@RETURN_VALUE",SqlDbType.Int);
			p.Direction = ParameterDirection.ReturnValue;
			
			return p;
		}
		
		#endregion
	}
}
