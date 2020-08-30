using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System.Data;
using System.Data.SqlClient;

namespace CantinaTCC.Controller
{
    public class AcessoCon
    {

        CantinaCon connect;

        #region Atributos
        /// <summary>
        /// Atributo da classe Acesso referente a conexão.
        /// </summary>
        private SqlConnection _con;

        #endregion

        #region Métodos

        #region Métodos Construtores
        /// <summary>
        /// Método construtor da classe AcessoCon.
        /// Ao ser instanciado realiza conexão com o banco.
        /// </summary>
        public AcessoCon()
        {

            connect = new CantinaCon();

            this._con = connect.GetCon();

        }
        #endregion

        /// <summary>
        /// Método para adicionar registro a tabela de acessos.
        /// </summary>
        /// <param name="acesso">Objeto do tipo Acesso</param>
        /// <returns>Verdadeiro se algum registro for adicionado.</returns>
        public bool AdicionarAcesso(Acesso acesso)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("InsertAcesso", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", acesso.Login);
                cmd.Parameters.AddWithValue("@Senha", acesso.Senha);
                cmd.Parameters.AddWithValue("@Nivel", acesso.Nivel);
                cmd.Parameters.AddWithValue("@IdCliente", acesso.IdCliente);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Executando a procedure e recebendo quantidade de linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando conexão com o banco de dados.
            _con.Close();

            return (linhas > 0);

        }

        /// <summary>
        /// Método para atualizar a tabela de Acesso.
        /// </summary>
        /// <param name="acesso">Objeto do tipo acesso.</param>
        /// <returns>Verdairo se algum registro for atualizado.</returns>
        public bool AtualizarAcesso(Acesso acesso)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("UpdateAcesso", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAcesso", acesso.IdAcesso);
                cmd.Parameters.AddWithValue("@Login", acesso.Login);
                cmd.Parameters.AddWithValue("@Senha", acesso.Senha);
                cmd.Parameters.AddWithValue("@Nivel", acesso.Nivel);
                cmd.Parameters.AddWithValue("@IdCliente", acesso.IdCliente);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Executando a procedure e recebendo quantidade de linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando conexão com o banco de dados.
            _con.Close();

            return (linhas > 0);

        }

        /// <summary>
        /// Método para deletar registros da tabela acesso.
        /// </summary>
        /// <param name="acesso">Objeto do tipo Acesso.</param>
        /// <returns>Verdadeiro se deletar algum registro.</returns>
        public bool DeletarAcesso(Acesso acesso)
        {

            //Variável do tipo inteiro para receber a quantidade de linhas afetadas.
            int linhas;

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("DeleteAcesso", _con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAcesso", acesso.IdAcesso);

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Executando a procedure e recebendo quantidade de linhas afetadas.
                linhas = cmd.ExecuteNonQuery();

            }

            //Encerrando conexão com o banco de dados.
            _con.Close();

            return (linhas > 0);

        }

        /// <summary>
        /// Método para retornar um DataTable contendo os registros da tabela Acesso.
        /// </summary>
        /// <returns>Objeto do tipo DataTable.</returns>
        public DataTable SelectAcesso()
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectAcesso", _con))
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
        /// Método para retornar um DataTable contendo apenas um registro da tabela Acesso.
        /// </summary>
        /// <param name="idAcesso">Inteiro referente ao identificador da tabela Acesso.</param>
        /// <returns>Objeto do tipo DataTable.</returns>
        public DataTable SelectAcesso(int idAcesso)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();

            //Utilizando da classe SqlCommand para executar as procedures.
            //Nota: O uso do using se deve a sua garantia de liberação dos recursos após seu uso.
            using (SqlCommand cmd = new SqlCommand("SelectAcessoById", _con))
            {

                //Abrindo a conexão com o banco de dados.
                _con.Open();

                //Definindo o tipo de instrução a ser executada.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAcesso", idAcesso);
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
        /// Método para verificar a veracidade dos dados de login.
        /// </summary>
        /// <param name="usuario">String contendo o usuário que está tentando realizar o acesso.</param>
        /// <param name="senha">String cotendo a senha.</param>
        /// <returns>Retornar true se as informações forem idênticas.</returns>
        public bool loginAcesso(string usuario, string senha)
        {

            //Instanciando objeto do tipo Datatable para receber seleção de dados.
            DataTable dtbd = new DataTable();
            int retorno; //Variável a quantidade de registros que retornados.

            using (SqlCommand cmd = new SqlCommand("LoginAcesso", _con))
            {

                //Abrindo a conexão.
                _con.Open();

                //removendo os espaço nos textos.
                usuario = usuario.Trim();
                senha = senha.Trim();

                //Setando os parâmetros que serão enviados para a procedure no banco.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", usuario);
                cmd.Parameters.AddWithValue("@Senha", senha);

                //Adaptando os dados vindos do banco para preencher o DataTable.
                SqlDataAdapter adap = new SqlDataAdapter(cmd);

                //Preenchendo o DataTable.
                adap.Fill(dtbd);

            }

            //Encerrando a conexão com o banco.
            _con.Close();

            //setando o retorno na variável.
            retorno = int.Parse(dtbd.Rows[0]["Retorno"].ToString());

            //retornando os valores recuperados.
            return (retorno > 0);

        }

        #endregion

    }
}