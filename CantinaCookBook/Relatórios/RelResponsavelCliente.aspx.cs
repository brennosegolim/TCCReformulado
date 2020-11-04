using CantinaCookBook.Controller;
using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.Relatórios
{
    public partial class RelResponsavelCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Nivel"] != null)
                {

                    if (!Session["Nivel"].ToString().Equals("A")) throw new Exception("Acesso negado");

                    DataTable dt = null;
                    CantinaCon con = new CantinaCon();
                    ClienteCon cliente = new ClienteCon();

                    string sql = "";
                    int contador = 0;

                    sql = @"SELECT *
                              FROM Cliente CL
                             WHERE EXISTS (SELECT IdCliente 
                                             FROM Cliente
				                            WHERE IdResponsavel = CL.IdCliente)";

                    //Recebendo o resultado em um datatable
                    dt = con.getSelect(sql);

                    if (dt != null)
                    {

                        //Montando o cabeçalho.
                        string html = "<table class=\"responsive-table highlight\">" +
                                      "    <tbody>                                 ";

                        //Percorrendo o DataTable e montando as linhas.
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string idCliente = dt.Rows[i]["IdCliente"].ToString();
                            string nomeResponsavel = dt.Rows[i]["Nome"].ToString();

                            //Montando o cabeçalho.
                            html += " <table>                                                          "
                                  + "     <thead>                                                      "
                                  + "         <tr>                                                     "
                                  + "             <th>Nome do Responsável: "+ nomeResponsavel +" </th> "
                                  + "         </tr>                                                    "
                                  + "     </thead>                                                     "
                                  + "     <tbody>                                                      ";

                            sql = @" SELECT Nome,
                                      	    CPF,
                                            DATEDIFF(YEAR,DataNascimento,GETDATE()) AS Idade
                                       FROM Cliente
                                      WHERE IdResponsavel = " + idCliente;

                            DataTable dtC = con.getSelect(sql);

                            if(dtC != null && dtC.Rows.Count > 0)
                            {

                                int contadorDepen = 0;

                                html += " <table>                               "
                                      + "   <thead>                             "
                                      + "       <tr>                            "
                                      + "           <th>Nome do dependente</th> "
                                      + "           <th>CPF</th>                "
                                      + "           <th>Idade</th>              "
                                      + "       </tr>                           "
                                      + "   </thead>                            "
                                      + "   <tbody>                             ";

                                for (int j = 0; j < dtC.Rows.Count; j++)
                                {

                                    string nomeDependente = dtC.Rows[j]["Nome"].ToString();
                                    string cpf = dtC.Rows[j]["CPF"].ToString();
                                    string idade = dtC.Rows[j]["Idade"].ToString();

                                    html += " <tr>                                 "
                                          + "     <td>" + nomeDependente + " </td> "
                                          + "     <td>" + cpf + "</td>             "
                                          + "     <td>" + idade + "</td>           "
                                          + " </tr>                                ";

                                    contadorDepen ++;

                                }

                                html += "</tbody>                                                                       "
                                      + "   <tfoot>                                                                     "
                                      + "       <tr>                                                                    "
                                      + "           <td>Numero de dependentes: (" + contadorDepen.ToString() + ")</td> "
                                      + "       </tr>                                                                   "
                                      + "   </tfoot>                                                                    "
                                      + "</table>                                                                       ";

                            }

                            html += "     </tbody> "
                                  + " </table>     ";

                            contador++;

                        }

                        html += "</tbody>                                                                  "
                              + "   <tfoot>                                                                "
                              + "       <tr>                                                               "
                              + "           <td>Numero de responsáveis: (" + contador.ToString() + ")</td> "
                              + "       </tr>                                                              "
                              + "   </tfoot>                                                               "
                              + "</table>                                                                  ";

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