using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System.Data;
using System.Data.SqlClient;

namespace CantinaTCC.Controller
{
    public class ClienteCon
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
        public ClienteCon()
        {

            connect = new CantinaCon();

            this._con = connect.GetCon();

        }

        /// <summary>
        /// Método Responsável por realizar a inserção na tabela de clientes.
        /// </summary>
        /// <param name="cliente">Objeto do tipo Cliente.</param>
        /// <returns>Verdadeiro se ocorrer a inserção na tabela.</returns>
        public bool AdicionarCliente(Cliente cliente)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("InsertCliente", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                cmd.Parameters.AddWithValue("@IdResponsavel", cliente.IdResponsavel);

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
        /// Método para atualizar informações na tabela de clientes.
        /// </summary>
        /// <param name="cliente">Objeto do tipo Cliente</param>
        /// <returns>Verdadeiro se ocorrer a atualização na tabela.</returns>
        public bool AtualizarCliente(Cliente cliente)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("UpdateCliente", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                cmd.Parameters.AddWithValue("@IdResponsavel", cliente.IdResponsavel);

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
        /// Método para deletar registros da tabela cliente.
        /// </summary>
        /// <param name="cliente">Objeto do tipo Cliente</param>
        /// <returns>Verdadeiro se o registro for deletado.</returns>
        public bool DeletarCliente(Cliente cliente)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteCliente", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

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
        /// Método para retornar a seleção de todos clientes.
        /// </summary>
        /// <returns>Objeto do tipo DataTable contendo os registros da tabela Cliente.</returns>
        public DataTable SelectCliente()
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectCliente", _con))
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
        /// Método para retornar um único cliente.
        /// </summary>
        /// <param name="idCliente">Inteiro contendo o identificador do cliente.</param>
        /// <returns>DataTable contendo um único registro da tabela Cliente.</returns>
        public DataTable SelectCliente(int idCliente)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectClienteById", _con))
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