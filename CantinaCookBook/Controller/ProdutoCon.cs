using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CantinaCookBook.Controller
{
    public class ProdutoCon
    {

        CantinaCon connect;

        #region Atributos
        /// <summary>
        /// Atributo da classe de conexão do produto.
        /// </summary>
        private SqlConnection _con;
        #endregion


        #region Métodos

        /// <summary>
        /// Construtor da classe. Ao ser instanciado já realiza as configurações de conexão.
        /// </summary>
        public ProdutoCon()
        {

            connect = new CantinaCon();

            this._con = connect.GetCon();

        }

        /// <summary>
        /// Método Responsável por realizar a inserção na tabela de produtos.
        /// </summary>
        /// <param name="produto">Objeto do tipo Produto.</param>
        /// <returns>Verdadeiro se ocorrer a inserção na tabela.</returns>
        public bool AdicionarProduto(Produto produto)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("InsertProduto", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo",produto.Codigo);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Observacao", produto.Observacao);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando a conexão ao banco de dados.
            _con.Close();

            //Retornando verdadeiro se o algum registro foi afetado.
            return (linhas > 0);

        }

        /// <summary>
        /// Método para atualizar informações na tabela de produtos.
        /// </summary>
        /// <param name="produto">Objeto do tipo Produto</param>
        /// <returns>Verdadeiro se ocorrer a atualização na tabela.</returns>
        public bool AtualizarProduto(Produto produto)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("UpdateProduto", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);
                cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Observacao", produto.Observacao);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Executando o comando e retornando as linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando a conexão ao banco de dados.
            _con.Close();

            //Retornando verdadeiro se o algum registro foi afetado.
            return (linhas > 0);

        }

        /// <summary>
        /// Método para deletar registros da tabela produtos.
        /// </summary>
        /// <param name="produto">Objeto do tipo Produto</param>
        /// <returns>Verdadeiro se o registro for deletado.</returns>
        public bool DeletarProduto(Produto produto)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteProduto", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //executando o comando e retornando a quantidade de linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando a conexão com o banco de dados.
            _con.Close();

            //Retornando verdadeiro se o algum registro foi afetado.
            return (linhas > 0);

        }

        /// <summary>
        /// Método para deletar registros da tabela produto.
        /// </summary>
        /// <param name="idProduto">Inteiro referente ao identificador do Produto</param>
        /// <returns>Verdadeiro se o registro for deletado.</returns>
        public bool DeletarProduto(int idProduto)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteProduto", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //executando o comando e retornando a quantidade de linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando a conexão com o banco de dados.
            _con.Close();

            //Retornando verdadeiro se o algum registro foi afetado.
            return (linhas > 0);

        }

        /// <summary>
        /// Método para retornar a seleção de todos produtos.
        /// </summary>
        /// <returns>Objeto do tipo DataTable contendo os registros da tabela de Produtos.</returns>
        public DataTable SelectProduto()
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectProduto", _con))
            {

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Definindo o tipo de instrução a ser executada.
                cmd.CommandType = CommandType.StoredProcedure;
                //Adaptando os dados vindos do banco para preencher o DataTable.
                SqlDataAdapter adap = new SqlDataAdapter(cmd);

                //Preenchendo o Datatable.
                adap.Fill(dtbd);

            }

            //Encerrando a conexão com banco.
            _con.Close();

            //Retornando o Datatable.
            return dtbd;

        }

        /// <summary>
        /// Método para retornar um único produto.
        /// </summary>
        /// <param name="idProduto">Inteiro contendo o identificador do produto.</param>
        /// <returns>DataTable contendo um único registro da tabela Produto.</returns>
        public DataTable SelectProduto(int idProduto)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectProdutoById", _con))
            {

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Definindo o tipo de instrução a ser executada.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                //Adaptando os dados vindos do banco para preencher o DataTable.
                SqlDataAdapter adap = new SqlDataAdapter(cmd);

                //Preenchendo o Datatable.
                adap.Fill(dtbd);

            }

            //Encerrando a conexão com banco.
            _con.Close();

            //Retornando o Datatable.
            return dtbd;

        }

        #endregion

    }
}