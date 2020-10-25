using CantinaCookBook.Controller;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.Relatórios
{
    public partial class RelListaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string nivel = "";

                if (Session["Nivel"] != null)
                {

                    if( ! Session["Nivel"].ToString().Equals("A")) throw new Exception("Acesso negado");

                    DataTable dt = null;
                    CantinaCon con = new CantinaCon();

                    string sql = "";
                    int contador = 0;

                    sql = @" SELECT CL.*
                           FROM Cliente CL
                          INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente
                          WHERE AC.Nivel = 'U'";

                    //Recebendo o resultado em um datatable
                    dt = con.getSelect(sql);

                    if (dt != null)
                    {

                        //Montando o cabeçalho.
                        string html = " <table class=\"responsive-table highlight\"> "
                                    + "     <thead>                                  "
                                    + "         <tr>                                 "
                                    + "             <th>Nome</th>                    "
                                    + "             <th>Data de Nascimento</th>      "
                                    + "             <th>Celular</th>                 "
                                    + "             <th>Email</th>                   "
                                    + "         </tr>                                "
                                    + "     </thead>                                 "
                                    + "     <tbody>                                  ";

                        //Percorrendo o DataTable e montando as linhas.
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string nome = dt.Rows[i]["Nome"].ToString();
                            string dataNascimento = dt.Rows[i]["DataNascimento"].ToString();
                            string celular = dt.Rows[i]["Celular"].ToString();
                            string email = dt.Rows[i]["Email"].ToString();

                            html += " <tr>                                                "
                                  + "     <td>" + nome + "</td>                           "
                                  + "     <td>" + dataNascimento.Substring(0, 10) + "</td> "
                                  + "     <td>" + celular + "</td>                        "
                                  + "     <td>" + email + "</td>                          "
                                  + " </tr>                                               ";

                            contador++;

                        }

                        html += "</tbody>                                                 "
                              + "<tfoot>                                                  "
                              + "   <tr>                                                  "
                              + "       <td>Registros: (" + contador.ToString() + ")</td> "
                              + "   </tr>                                                 "
                              + "</tfoot>                                                 "
                              + "</table>                                                 ";

                        //Inserindo a variável no card.
                        dvListagem.InnerHtml = html;

                    }

                }
                else
                {

                    throw new Exception("Acesso Negado");

                }

            }
        }
    }
}