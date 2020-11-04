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
    public partial class RelPendenciaPeriodo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dt = null;
                CantinaCon con = new CantinaCon();

                string dataInicio = "";
                string datafim = "";

                if(Session["RelDataInicial"] != null && Session["RelDataFinal"] != null)
                {

                    dataInicio = Session["RelDataInicial"].ToString();
                    datafim = Session["RelDataFinal"].ToString();

                    dataInicio = dataInicio.Substring(6, 4) + "-" + dataInicio.Substring(0, 2) + "-" + dataInicio.Substring(3, 2);
                    datafim = datafim.Substring(6, 4) + "-" + datafim.Substring(0, 2) + "-" + datafim.Substring(3, 2);

                } else
                {

                    Response.Redirect("~/View/Relatorios.aspx");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('Não foi possível imprimir o relatório! Tente novamente mais tarde.')", true);

                }

                string sql = "";
                int contador = 0;

                sql = @" SELECT DISTINCT CL.IdCliente,
                                CL.Nome
                           FROM Cliente CL
                          INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente
                          INNER JOIN Venda  VE ON VE.IdCliente = CL.IdCliente
                          WHERE Nivel = 'U'
                            AND Autenticado = 1
                            AND VE.[Data] BETWEEN '"+ dataInicio +"' AND '" + datafim +"'"
                       +" ORDER BY CL.Nome";

                //Recebendo o resultado em um datatable
                dt = con.getSelect(sql);

                if (dt != null)
                {

                    //Montando o cabeçalho.
                    string html = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        html += " <table class=\"responsive-table highlight\">           "
                              + " <thead>                                                "
                              + " <tr>                                                   "
                              + "     <th>Nome: "+ dt.Rows[i]["Nome"].ToString() +"</th> "
                              + " </tr>                                                  "
                              + " </thead>                                               ";

                        sql = @" SELECT DISTINCT VE.IdVenda,
                                        VE.[Data]
                                   FROM Venda VE
                                  INNER JOIN Produto_Venda PV ON PV.IdVenda = VE.IdVenda
                                  WHERE IdCliente = " + dt.Rows[i]["IdCliente"].ToString() + @"          
                                    AND VE.[Data] BETWEEN '" + dataInicio + "' AND '" + datafim + "'" +
                               @" ORDER BY VE.[Data] DESC,
                                        VE.IdVenda";

                        DataTable dtV = con.getSelect(sql);

                        if (dtV != null && dtV.Rows.Count > 0)
                        {

                            for (int k = 0; k < dtV.Rows.Count; k++)
                            {

                                string dataPend = dtV.Rows[k]["Data"].ToString();

                                dataPend = dataPend.Substring(0, 2) + "/" + dataPend.Substring(3, 2) + "/" +dataPend.Substring(6, 4);

                                html += "<tbody>" 
                                      + "<table>"
                                      + " <thead>"
                                      + " <tr>                                                                        "
                                      + "     <th>Código Pendência: " + dtV.Rows[k]["IdVenda"].ToString() + "</th>    "
                                      + "     <th>Data da Pendência: " + dataPend + "</th> "
                                      + " </tr> "
                                      + " </thead>";

                                sql = @" SELECT *
                                           FROM Produto_Venda PV
                                          INNER JOIN Produto PD ON PD.IdProduto = PV.IdProduto
                                          WHERE IdVenda = " + dtV.Rows[k]["IdVenda"].ToString();

                                DataTable dtP = con.getSelect(sql);

                                if (dtP != null && dtP.Rows.Count > 0)
                                {

                                    html += "<tbody> " +
                                            "   <table> " +
                                            "   <thead> " +
                                            "       <tr>" +
                                            "           <th>Código</th>" +
                                            "           <th>Descrição</th>" +
                                            "           <th>Quantidade</th>" +
                                            "           <th>ValorTotal</th>" +
                                            "       <tr>" +
                                            "   </thead>" +
                                            "   <tbody>";

                                    decimal valorTotal = new decimal(0.0);
                                    int contadorItens = 0;

                                    for (int j = 0; j < dtP.Rows.Count; j++)
                                    {

                                        valorTotal += decimal.Parse(dtP.Rows[j]["Valor"].ToString());

                                        html += "<tr>" +
                                                "   <td>" + dtP.Rows[j]["Codigo"].ToString()     + "</td>" +
                                                "   <td>" + dtP.Rows[j]["Descricao"].ToString()  + "</td>" +
                                                "   <td>" + dtP.Rows[j]["Quantidade"].ToString() + "</td>" +
                                                "   <td>" + dtP.Rows[j]["Valor"].ToString()      + "</td>" +
                                                "</tr>";

                                        contadorItens++;

                                    }

                                    html += " </tbody>" +
                                           "   <tfoot>                                                  " +
                                           "       <tr>                                                  " +
                                           "           <td>Registros: (" + contadorItens.ToString() + ")</td> " +
                                           "       </tr>                                                 " +
                                           "   </tfoot>" +
                                           "   </table>" +
                                           "</tbody>";

                                }

                                html += "</table>" +
                                        "</tbody>";

                            }

                        }

                        contador++;

                    }

                    html += "<tfoot>                                                  "
                          + "   <tr>                                                  "
                          + "       <td>Total de Clientes: (" + contador.ToString() + ")</td> "
                          + "   </tr>                                                 "
                          + "</tfoot>                                                 "
                          + "</table>                                                 ";

                    //Inserindo a variável no card.
                    dvListagem.InnerHtml = html;

                }
            }
        }
    }
}