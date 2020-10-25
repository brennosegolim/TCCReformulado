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
    public partial class RelRankingProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                DataTable dt = null;
                CantinaCon con = new CantinaCon();

                string sql = "";
                int contador = 0;

                sql = @"SELECT ROW_NUMBER() OVER (ORDER BY SUM(Quantidade)  DESC) as Ranking,
                               PD.Descricao,
	                           SUM(Quantidade) as QuantidadeVendida
                          FROM Produto_Venda PV
                         INNER JOIN Produto PD ON PD.IdProduto = PV.IdProduto
                         GROUP BY PD.Descricao";

                //Recebendo o resultado em um datatable
                dt = con.getSelect(sql);

                if (dt != null)
                {

                    //Montando o cabeçalho.
                    string html = " <table class=\"responsive-table highlight\"> "
                                + "     <thead>                                  "
                                + "         <tr>                                 "
                                + "             <th>Ranking</th>                 "
                                + "             <th>Descrição</th>               "
                                + "             <th>Quantidade Vendida</th>      "
                                + "         </tr>                                "
                                + "     </thead>                                 "
                                + "     <tbody>                                  ";

                    //Percorrendo o DataTable e montando as linhas.
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string ranking = dt.Rows[i]["Ranking"].ToString();
                        string descricao = dt.Rows[i]["Descricao"].ToString();
                        string quantidade = dt.Rows[i]["QuantidadeVendida"].ToString();

                        html += " <tr>                            "
                              + "     <td>" + ranking + "º </td>  "
                              + "     <td>" + descricao + "</td>  "
                              + "     <td>" + quantidade + "</td> "
                              + " </tr>                           ";

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
        }
    }
}