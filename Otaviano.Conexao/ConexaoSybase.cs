using System.Data;
using Sybase.Data.AseClient;


namespace Otaviano.Conexao
{
	/// <summary>
	/// Classe que implementa os metodos da classe abstrata Conexao,
	/// para o provedor de dados Sybase.
	/// </summary>
	public class ConexaoSybase	: Conexao
	{
		#region Atributos(variavéis)
		#endregion

		#region Propriedades(get/set)
		#endregion

		#region Construtores
		/// <summary>
		/// Construtor da Classe de ConexaoSybase
		/// </summary>
		/// <param name="stringConexao">String de conexao criptografada de acesso a base de dados </param>
		internal ConexaoSybase(string stringConexao)
		{
			this.StringConexao = stringConexao;	
		}
		#endregion
		
		#region Eventos
		#endregion

		#region Métodos
		/// <summary>
		/// Recupera o objeto de conexao.
		/// </summary>
		/// <returns>Conexao Oracle</returns>
		internal override IDbConnection GetConexao()
		{
			return new  AseConnection();			
		}

		/// <summary>
		/// Recupera o objeto de Comando.
		/// </summary>
		/// <returns>Comando Oracle</returns>
		internal override IDbCommand GetComando()
		{
			return new  AseCommand();
		}

		/// <summary>
		/// Sobrescreve a Interfase de DataAdapter
		/// </summary>
		/// <returns>DataAdapter Oracle</returns>
		internal override IDbDataAdapter GetDataAdapter()
		{
			return new  AseDataAdapter();
		}

		/// <summary>
		/// Sobrescreve o metodo de GetParametroRetorno, adiciona o parametro de "@RETURN_VALUE".
		/// </summary>
		/// <returns>Retorna uma colecao de parametros com o parametro "@RETURN_VALUE" </returns>

		internal override IDataParameter GetParametroRetorno()
		{

			AseParameter p = new AseParameter("@retValue",AseDbType.Integer);
			p.Direction = ParameterDirection.ReturnValue;
			
			return p;
		}
		#endregion
	}
}
