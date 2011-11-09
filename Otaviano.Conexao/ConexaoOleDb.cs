using System.Data;
using System.Data.OleDb;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados OleDb.
	/// </summary>
	public class ConexaoOleDb : Conexao
	{
		#region Atributos / Propriedades

		#endregion
		
		#region Métodos
		
		/// <summary>
		/// Construtor da Classe de ConexaoOleDb
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados </param>
		public ConexaoOleDb(string stringConexao)
		{
			this.StringConexao = stringConexao;
		}
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao OleDb</returns>
		internal override IDbConnection GetConexao()
		{
			return new OleDbConnection();			
		}
		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando OleDb</returns>
		internal override IDbCommand GetComando()
		{
			return new OleDbCommand();
		}
		/// <summary>
		/// Sobrescreve a Interfase de DataAdapter
		/// </summary>
		/// <returns>DataAdapter OleDb</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new OleDbDataAdapter();
		}
		/// <summary>
		/// Sobrescreve o metodo de Parametro de retorno, adiciona o parametro de "@RETURN_VALUE"
		/// </summary>
		/// <returns>Retorna uma colecao de parametros com o parametro "@RETURN_VALUE"</returns>
		internal override IDataParameter GetParametroRetorno()
		{
			OleDbParameter p = new OleDbParameter("@RETURN_VALUE",OleDbType.Integer);
			p.Direction = ParameterDirection.ReturnValue;
			
			return p;
		}

		#endregion
	}
}
