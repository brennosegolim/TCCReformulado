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
    public class ClienteLimiteCon
    {

        CantinaCon connect;

        #region Atributos
        /// <summary>
        /// Atributo da classe de conexão do cliente.
        /// </summary>
        private SqlConnection _con;
        #endregion


        #region Métodos

        /// <summary>
        /// Construtor da classe. Ao ser instanciado já realiza as configurações de conexão.
        /// </summary>
        public ClienteLimiteCon()
        {

            connect = new CantinaCon();

            this._con = connect.GetCon();

        }

        /// <summary>
        /// Método Responsável por realizar a inserção na tabela de clientesLimite.
        /// </summary>
        /// <param name="clienteLimite">Objeto do tipo ClienteLimite.</param>
        /// <returns>Verdadeiro se ocorrer a inserção na tabela.</returns>
        public bool AdicionarLimite(ClienteLimite clienteLimite)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("InsertClienteLimite", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", clienteLimite.IdCliente);
                cmd.Parameters.AddWithValue("@Valor", clienteLimite.Valor);
                cmd.Parameters.AddWithValue("@Data", clienteLimite.Data);

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
        /// Método para retornar a seleção de todos ClienteLimite.
        /// </summary>
        /// <returns>Objeto do tipo DataTable contendo os registros da tabela ClienteLimite.</returns>
        public DataTable SelectCliente()
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectClienteLimite", _con))
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
        /// Método para retornar um único ClienteLimite.
        /// </summary>
        /// <param name="idCliente">Inteiro contendo o identificador do Cliente.</param>
        /// <returns>DataTable contendo um único registro da tabela ClienteLimite.</returns>
        public DataTable SelectCliente(int idCliente)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectClienteLimiteById", _con))
            {

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Definindo o tipo de instrução a ser executada.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
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