using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Otaviano.Conexao;
using System.Configuration;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Persistencia.MySQL
{
    public class ConteudoMySQL
    {
        private Conexao conexao = ConexaoFactory.GetConexao(TipoProvedorDados.MySQL, ConfigurationManager.ConnectionStrings["StringConexao"].ConnectionString);

        public Dominio.Conteudo VincularFotos(Dominio.Conteudo conteudo)
        {
            string sql = string.Empty;

            sql = "delete from tb_conteudofoto where CodConteudo = " + conteudo.Codigo;

            this.conexao.ExecutarNonQuery(sql);

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

                    this.conexao.ExecutarEscalar(sql, CommandType.Text, parametros);
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

        public Dominio.Conteudo VincularPalavraChave(Dominio.Conteudo conteudo)
        {
            string sql = string.Empty;

            try
            {
                sql = "INSERT INTO tb_conteudopalavrachave (CodConteudo, CodPalavraChave)" +
                       "VALUES(@CodConteudo, @CodPalavraChave);" +
                       "SELECT LAST_INSERT_ID();";

                foreach (var item in conteudo.ListaPalavrasChave)
                {

                    IDataParameter[] parametros = new IDataParameter[]
					{
						new MySqlParameter("@CodConteudo", conteudo.Codigo),
						new MySqlParameter("@CodPalavraChave", item.Codigo)
					};

                    //item.Codigo = Convert.ToInt32(this.conexao.ExecutarEscalar(sql, CommandType.Text, parametros));
                    this.conexao.ExecutarEscalar(sql, CommandType.Text, parametros);
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

        public Dominio.Conteudo DesvincularFotos(Dominio.Conteudo conteudo)
        {
            string sql = string.Empty;

            sql = "delete from tb_conteudofoto where CodConteudo = " + conteudo.Codigo;

            this.conexao.ExecutarNonQuery(sql);

            return conteudo;
        }

        public void VincularFotoConteudo(int CodConteudo, int CodFoto)
        {
            string sql = string.Empty;

            sql = "insert into tb_conteudofoto (CodConteudo, CodFoto) values (" + CodConteudo + ", " + CodFoto + ")";

            this.conexao.ExecutarNonQuery(sql);
        }

        public int InserirFoto(Foto foto)
        {
            string sql = string.Empty;

            sql = "insert into tb_foto (Caminho, Fonte, Autor, Legenda, Galeria) values (@Caminho, @Fonte, @Autor, @Legenda, @Galeria);SELECT LAST_INSERT_ID();";
            
            IDataParameter[] parametros = new IDataParameter[]
			{
				new MySqlParameter("@Caminho", foto.Caminho),
				new MySqlParameter("@Fonte", foto.Fonte),
				new MySqlParameter("@Autor", foto.Autor),
				new MySqlParameter("@Legenda", foto.Legenda),
				new MySqlParameter("@Galeria", (foto.Galeria ? "1" : "0")),
				new MySqlParameter("@Fonte", foto.Fonte)
			};

            return Convert.ToInt32(this.conexao.ExecutarEscalar(sql, CommandType.Text, parametros));
        }
    }
}