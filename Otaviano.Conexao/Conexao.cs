using System;
using System.Collections;
using System.Data;

namespace Otaviano.Conexao
{
    /// <summary>
    /// Classe Abstrata que implementa os metodos de acesso a base de dados.
    /// </summary>
    public abstract class Conexao
    {
        #region Atributos / Propriedades

        private string stringConexao;
        /// <summary>
        /// String de conexao de acesso a base de dados.
        /// </summary>
        internal string StringConexao
        {
            get
            {
                if (stringConexao == string.Empty || stringConexao.Length == 0)
                    throw new ArgumentException("String de conexao invalida.");

                return stringConexao;
            }
            set
            {
                stringConexao = value;
            }
        }

        private IDbConnection conexao;
        private IDbCommand comando;
        private IDbTransaction transacao;
        private IDbDataAdapter dataAdapter;
        private static Hashtable parametros = Hashtable.Synchronized(new Hashtable());
        private IDataParameterCollection parametrosSaida;
        private int CommandTimeout;

        #endregion

        #region Métodos
		
        #region Abstratos

        internal abstract IDbConnection GetConexao();
        internal abstract IDbCommand GetComando();
        internal abstract IDbDataAdapter GetDataAdapter();
        internal abstract IDataParameter GetParametroRetorno();

        #endregion

        #region Transacao

        /// <summary>
        /// Seta o timeOut de uma transacao na base de dados. 
        /// O tempo máximo permitido é 60 segundos
        /// </summary>
        public void SetTimeOut(int timeOut)
        {
            if (timeOut > 60)
                timeOut = 60;

            CommandTimeout = timeOut;
        }
        /// <summary>
        /// Inicia uma transacao na base de dados.
        /// </summary>
        public void IniciarTransacao()
        {
            try
            {
                conexao = GetConexao();
                conexao.ConnectionString = this.StringConexao;
                conexao.Open();
                transacao = conexao.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch
            {
                conexao.Close();
                throw;
            }
        }
        /// <summary>
        /// Finaliza uma transacao na base de dados.
        /// </summary>
        public void FinalizarTransacao()
        {
            if (transacao == null)
                return;

            try
            {
                transacao.Commit();
            }
            catch
            {
                CancelarTransacao();
                throw;
            }
            finally
            {
                conexao.Close();
                transacao = null;
            }
        }
        /// <summary>
        /// Cancela uma transacao na base de dados.
        /// </summary>
        public void CancelarTransacao()
        {
            if (transacao == null)
                return;

            try
            {
                transacao.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                conexao.Close();
                transacao = null;
            }
        }

        #endregion

        #region DataReader

        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa, como retorno utiliza uma interface IDataReader.
        /// A conexao so sera fechada quando o objeto retornado for fechado.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecutarDataReader(string cmdTexto)
        {
            return this.ExecutarDataReader(cmdTexto, CommandType.Text, null);
        }
        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa, como retorno utiliza uma interface IDataReader.
        /// A conexao so sera fechada quando o objeto retornado for fechado.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecutarDataReader(string cmdTexto, CommandType tipoComando)
        {
            return this.ExecutarDataReader(cmdTexto, tipoComando, null);
        }
        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa, como retorno utiliza uma interface IDataReader.
        /// A conexao so sera fechada quando o objeto retornado for fechado.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <param name="cmdParametros">Colecao de Parametros</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecutarDataReader(string cmdTexto, CommandType tipoComando, IDataParameter[] cmdParametros)
        {
            try
            {
                PrepararComando(tipoComando, cmdTexto, cmdParametros);

                IDataReader dr;

                if (transacao == null)
                    dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    dr = comando.ExecuteReader();

                return dr;
            }
            catch (System.Exception ex)
            {
                if (transacao == null)
                {
                    if (conexao != null)
                    {
                        conexao.Close();
                        conexao.Dispose();
                    }
                }
                else
                    CancelarTransacao();

                throw ex;
            }
        }

        #endregion

        #region ExecutarNonQuery
        
		/// <summary>
        /// Metodo responsavel pela execucao de comandos que nao retorna  um conjunto de resultados, como comandos de atualizacao, insercao, exclusao.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <returns>
        /// Retorna a quantidade de linhas afetadas pelo comando ou 
        /// um parametro de saida do tipo inteiro.
        /// </returns>
        public int ExecutarNonQuery(string cmdTexto)
        {
            return this.ExecutarNonQuery(cmdTexto, CommandType.Text, null);
        }
		/// <summary>
        ///Metodo responsavel pela execucao de comandos que nao retorna um conjunto de resultados, como comandos de atualizacao, insercao, exclusao.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <returns>
        /// Retorna a quantidade de linhas afetadas pelo comando ou 
        /// um parametro de saida do tipo inteiro.
        /// </returns>
        public int ExecutarNonQuery(string cmdTexto, CommandType tipoComando)
        {
            return this.ExecutarNonQuery(cmdTexto, tipoComando, null);
        }
		/// <summary>
				/// Metodo responsavel pela execucao de comandos que nao sao de pesquisa(Insert, Update, Delete).
				/// </summary>
				/// <param name="cmdTexto">Texto do Comando</param>
				/// <param name="tipoComando">Tipo do Comando</param>
				/// <param name="cmdParametros">Colecao de Parametros</param>
				/// <returns>
				/// Retorna a quantidade de linhas afetadas pelo comando ou 
				/// um parametro de saida do tipo inteiro.
				/// </returns>
        public int ExecutarNonQuery(string cmdTexto, CommandType tipoComando, IDataParameter[] cmdParametros)
        {
            try
            {
                PrepararComando(tipoComando, cmdTexto, cmdParametros);

                int linhas = comando.ExecuteNonQuery();

                parametrosSaida = comando.Parameters;

                if (comando.Parameters.Contains("@RETURN_VALUE"))
                {
                    try
                    {
                        object valor = ((IDataParameter)comando.Parameters["@RETURN_VALUE"]).Value;
                        return Convert.ToInt32(valor);
                    }
                    catch //alguns drivers como o ASAClient não suporta @REturn_Value
                    {
                        return linhas;
                    }

                }
                else
                    return linhas;
            }
            catch (System.Exception ex)
            {
                if (transacao != null)
                    CancelarTransacao();

                throw ex;
            }
            finally
            {
                if (transacao == null)
                {
                    conexao.Close();
                    comando.Dispose();
                }
            }
        }
        
		#endregion

        #region ExecutarEscalar

        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa que retorna a primeira 
        /// coluna da primeira linha do conjunto de resultados, todas as demais colunas são ignoradas.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <returns>Retorna a primeira coluna da primeira linha do conjunto de resultados</returns>
        public object ExecutarEscalar(string cmdTexto)
        {
            return this.ExecutarEscalar(cmdTexto, CommandType.Text, null);
        }
        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa que retorna a primeira 
        /// coluna da primeira linha do conjunto de resultados, todas as demais colunas são ignoradas.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <returns>Retorna a primeira coluna da primeira linha do conjunto de resultados</returns>
        public object ExecutarEscalar(string cmdTexto, CommandType tipoComando)
        {
            return this.ExecutarEscalar(cmdTexto, tipoComando, null);
        }
        /// <summary>
        /// Metodo responsavel pela execucao de comandos de pesquisa que retorna a primeira 
        /// coluna da primeira linha do conjunto de resultados, todas as demais colunas são ignoradas.
        /// </summary>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <param name="cmdParametros">Colecao de Parametros</param>
        /// <returns>Retorna a primeira coluna da primeira linha do conjunto de resultados</returns>
        public object ExecutarEscalar(string cmdTexto, CommandType tipoComando, IDataParameter[] cmdParametros)
        {
            try
            {
                PrepararComando(tipoComando, cmdTexto, cmdParametros);

                object objeto = comando.ExecuteScalar();

                if (objeto != DBNull.Value)
                    return objeto;
                else
                    return null;
            }
            catch
            {
                if (transacao != null)
                    CancelarTransacao();

                throw;
            }
            finally
            {
                if (transacao == null)
                {
                    conexao.Close();
                    comando.Dispose();
                }
            }
        }

        #endregion

        #region Parametros de Saida

        /// <summary>
        /// Metodo responsavel por recuperar parametros de saida da stored procedure.
        /// </summary>
        /// <param name="Nome">Nome do parametro de saida da ultima stored procedure executada.</param>
        /// <returns>Retorna um objeto com o valor do parametro de saida.</returns>
        public object GetParametroSaida(string Nome)
        {
            return ((IDataParameter)parametrosSaida[Nome]).Value;
        }

        #endregion

        #region Parametros Cache

        /// <summary>
        /// Metodo responsavel por armazenar a colecao de parametros em cache.
        /// </summary>
        /// <param name="nomeCache">Nome da Stored Procedure que utiliza os parametros</param>
        /// <param name="cmdParametros">Colecao de Parametros</param>
        public static void SetParametrosCache(string nomeCache, params IDataParameter[] cmdParametros)
        {
            parametros[nomeCache] = cmdParametros;
        }
        /// <summary>
        /// Metodo responsavel por recuperar a colecao de parametros do cache 
        /// de uma determinada stored procedure.
        /// </summary>
        /// <param name="nomeCache">Nome da Stored Procedure</param>
        /// <returns>Retorna uma colecao de parametros, caso não localize no cache retorna null</returns>
        public static IDataParameter[] GetParametrosCache(string nomeCache)
        {
            IDataParameter[] parametrosCache = (IDataParameter[])parametros[nomeCache];

            if (parametrosCache == null)
                return null;

            IDataParameter[] parametrosCopia = new IDataParameter[parametrosCache.Length];

            for (int i = 0; i < parametrosCache.Length; i++)
                parametrosCopia[i] = (IDataParameter)((ICloneable)parametrosCache[i]).Clone();

            return parametrosCopia;
        }

        #endregion

        #region Privados

        /// <summary>
        /// Prepara o objeto DataAdapter para a execucao de comandos. 
        /// </summary>
        /// <param name="tipoComando">Tipo de Comando</param>
        /// <param name="cmdTexto">Texto do Comando </param>
        /// <param name="cmdParametros">Colecao de Parametros</param>
        private void PrepararDataAdapter(CommandType tipoComando, string cmdTexto, IDataParameter[] cmdParametros)
        {
            dataAdapter = GetDataAdapter();
            dataAdapter.SelectCommand = GetComando();
            dataAdapter.SelectCommand.Connection = GetConexao();
            dataAdapter.SelectCommand.Connection.ConnectionString = StringConexao;
            dataAdapter.SelectCommand.CommandText = cmdTexto;
            dataAdapter.SelectCommand.CommandType = tipoComando;
            if (cmdParametros != null)
            {
                foreach (IDataParameter parametros in cmdParametros)
                    dataAdapter.SelectCommand.Parameters.Add(parametros);
            }
        }
        /// <summary>
        /// Prepara o objeto para a execucao de comandos. 
        /// </summary>
        /// <param name="tipoComando">Tipo do Comando</param>
        /// <param name="cmdTexto">Texto do Comando</param>
        /// <param name="cmdParametros">Colecao de Parametros</param>
        private void PrepararComando(CommandType tipoComando, string cmdTexto, IDataParameter[] cmdParametros)
        {
            if (conexao == null)
            {
                conexao = GetConexao();
                conexao.ConnectionString = this.StringConexao;
            }

            if (conexao.State != ConnectionState.Open)
                conexao.Open();

            if (comando == null)
                comando = GetComando();

            //Não pode ser 0 que é igual a sem limite de tempo
            if (CommandTimeout > 0)
                comando.CommandTimeout = CommandTimeout;

            comando.Connection = conexao;
            comando.CommandText = cmdTexto;
            comando.CommandType = tipoComando;

            if (transacao != null)
                comando.Transaction = transacao;

            comando.Parameters.Clear();

            if (cmdParametros != null)
            {
                comando.Parameters.Add(this.GetParametroRetorno());
                foreach (IDataParameter parametros in cmdParametros)
                    comando.Parameters.Add(parametros);
            }
        }
        /// <summary>
        /// Pesquisa o atributo CmdParametro nas propriedade do objeto de negocio.
        /// </summary>
        /// <param name="tipoObjeto">Tipo do Objeto de Negocio</param>
        /// <param name="nomeAtributo">Nome do parametro da Stored Procedure</param>
        /// <returns>PropertyInfo</returns>
        
        #endregion

        #endregion
    }
}
