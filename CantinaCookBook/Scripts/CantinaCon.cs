using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CantinaCookBook.Scripts
{
    public class CantinaCon
    {

        #region Atributos

        private SqlConnection _con;

        #endregion

        #region Métodos

        #region Métodos Construtores
        public CantinaCon()
        {

            this.Connection();

        }
        #endregion

        private void Connection()
        {

            string conString = ConfigurationManager.ConnectionStrings["TCCantinaConnectionString"].ToString();
            _con = new SqlConnection(conString);

        }

        public SqlConnection GetCon()
        {

            return this._con;

        }

        /// <summary>
        /// Realiza a consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">Texto contendo o comando para a consulta.</param>
        /// <returns>Objeto do tipo DataTable contendo a consulta.</returns>
        public DataTable getSelect(string consulta)
        {

            DataTable dt = null;

            using (SqlCommand cmd = new SqlCommand(consulta, this._con))
            {

                this._con.Open();

                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);

            }

            this._con.Close();

            return dt;

        }

        /// <summary>
        /// Realiza a consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">Texto contendo o comando para a consulta.</param>
        /// <returns>Objeto do tipo Inteiro contendo o número de linhas afetadas após a consulta.</returns>
        public int executeSelect(string consulta)
        {

            int linhasAfetadas = 0;

            using (SqlCommand cmd = new SqlCommand(consulta, this._con))
            {

                this._con.Open();

                linhasAfetadas = cmd.ExecuteNonQuery();

            }

            this._con.Close();

            return linhasAfetadas;

        }

        #endregion

    }
}