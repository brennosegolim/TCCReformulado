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
    public class VendaCon
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
        public VendaCon()
        {

            connect = new CantinaCon();

            this._con = connect.GetCon();

        }

        /// <summary>
        /// Método Responsável por realizar a inserção na tabela de vendas.
        /// </summary>
        /// <param name="venda">Objeto do tipo Venda.</param>
        /// <returns>Verdadeiro se ocorrer a inserção na tabela.</returns>
        public bool AdicionarVenda(Venda venda)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("InsertVenda", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Data", venda.Data);
                cmd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                cmd.Parameters.AddWithValue("@IdCliente", venda.IdCliente);

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
        /// Método para atualizar informações na tabela de vendas.
        /// </summary>
        /// <param name="venda">Objeto do tipo Venda</param>
        /// <returns>Verdadeiro se ocorrer a atualização na tabela.</returns>
        public bool AtualizarVenda(Venda venda)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("UpdateVenda", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenda", venda.IdVenda);
                cmd.Parameters.AddWithValue("@Data", venda.Data);
                cmd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                cmd.Parameters.AddWithValue("@IdCliente", venda.IdCliente);

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
        /// Método para deletar registros da tabela venda.
        /// </summary>
        /// <param name="venda">Objeto do tipo Produto</param>
        /// <returns>Verdadeiro se o registro for deletado.</returns>
        public bool DeletarVenda(Venda venda)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteVenda", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenda", venda.IdVenda);

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
        /// Método para deletar registros da tabela venda.
        /// </summary>
        /// <param name="idVenda">Inteiro referente ao identificador da venda</param>
        /// <returns>Verdadeiro se o registro for deletado.</returns>
        public bool DeletarVenda(int idVenda)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteVenda", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);

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
        /// Método para retornar a seleção de todas as vendas.
        /// </summary>
        /// <returns>Objeto do tipo DataTable contendo os registros da tabela de vendas.</returns>
        public DataTable SelectVenda()
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectVenda", _con))
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
        /// Método para retornar uma única venda.
        /// </summary>
        /// <param name="idVenda">Inteiro contendo o identificador da venda.</param>
        /// <returns>DataTable contendo um único registro da tabela de venda.</returns>
        public DataTable SelectVenda(int idVenda)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectVendaById", _con))
            {

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Definindo o tipo de instrução a ser executada.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
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