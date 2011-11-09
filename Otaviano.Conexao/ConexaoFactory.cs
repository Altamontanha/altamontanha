using System;

namespace Otaviano.Conexao
{
	/// <summary>
	/// Objeto que cria uma inst�ncia de um provider
	/// definido no arquivo de configura��o ou no tipo de provider
	/// </summary>
	public sealed class ConexaoFactory
	{
		#region Atributos / Propriedades

		#endregion
				
		#region M�todos
		
		/// <summary>
		/// Captura o tipo de provedor de dados,
		/// retorna uma instancia do objeto de Conexao conforme o 
		/// provider definido no arquivo de configura��o da aplica��o.
		/// </summary>
		/// <returns>Objeto de Conexao do provider informado</returns>
		public static Conexao GetConexao()
		{
			// Verifica se as tags do arquivo de configura��o s�o validas, diferente de nulas.
			if (System.Configuration.ConfigurationManager.AppSettings.Get("TipoProvedorDados") == null 
				||  System.Configuration.ConfigurationManager.ConnectionStrings["StringConexao"] == null)
				throw new ArgumentNullException("Informe o Banco de Dados e a String de Conexao no arquivo de configuracao da aplicacao.");
			
			TipoProvedorDados tipoProvedorDados;
			
			try
			{
				tipoProvedorDados = (TipoProvedorDados) System.Enum.Parse(typeof(TipoProvedorDados), System.Configuration.ConfigurationManager.AppSettings.Get("TipoProvedorDados"), true);
			}
			catch
			{
				throw new ArgumentException("Tipo de Banco de Dados informado � invalido.");
			}

			// Retorna o provider escolhido
			return GetConexao(tipoProvedorDados, System.Configuration.ConfigurationManager.ConnectionStrings["StringConexao"].ToString());
		}
		/// <summary>
		/// Instancia um objeto de Acesso de Dados conforme o tipo de provedor informando
		/// </summary>
		/// <param name="tipoProvedorDados">Tipo de Provedor</param>
		/// <param name="stringConexao">String de Conex�o Criptografada</param>
		public static Conexao GetConexao(TipoProvedorDados tipoProvedorDados, string stringConexao)
		{
			switch (tipoProvedorDados)
			{
				case TipoProvedorDados.MSSQL:
					return new ConexaoMSSQL(stringConexao);
				case TipoProvedorDados.MySQL:
					return new ConexaoMySQL(stringConexao);
				case TipoProvedorDados.Oracle:
					return new ConexaoOracle(stringConexao);
				case TipoProvedorDados.Odbc:
					return new ConexaoOdbc(stringConexao);
				case TipoProvedorDados.OleDB:
					return new ConexaoOleDb(stringConexao);
				case TipoProvedorDados.Sybase:
					return new ConexaoSybase(stringConexao);
				case TipoProvedorDados.Postgre:
					return new ConexaoPostgre(stringConexao);
				default:
					throw new ArgumentException("Tipo de Banco de Dados informado n�o � suportado.");
			}
		}
				
		#endregion
	}
}
