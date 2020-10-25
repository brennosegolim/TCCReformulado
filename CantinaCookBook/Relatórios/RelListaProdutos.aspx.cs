using CantinaCookBook.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.Relatórios
{
    public partial class RelListaProdutos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ProdutoCon produto = new ProdutoCon();

                DataTable dt = null;

                int contador = 0;

                //Recebendo o resultado em um datatable
                dt = produto.SelectProduto();

                if (dt != null)
                {

                    //Montando o cabeçalho.
                    string html = " <table class=\"responsive-table highlight\"> "
                                + "     <thead>                                  "
                                + "         <tr>                                 "
                                + "             <th>Código</th>                  "
                                + "             <th>Descrição</th>               "
                                + "             <th>Preço</th>                   "
                                + "         </tr>                                "
                                + "     </thead>                                 "
                                + "     <tbody>                                  ";

                    //Percorrendo o DataTable e montando as linhas.
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string codigo = dt.Rows[i]["Codigo"].ToString();
                        string descricao = dt.Rows[i]["Descricao"].ToString();
                        string preco = dt.Rows[i]["Preco"].ToString();

                        html += " <tr>                                            "
                              + "     <td>" + codigo + "</td>                     "
                              + "     <td>" + descricao + "</td>                  "
                              + "     <td>" + preco + "</td>                      "
                              + " </tr>                                           ";

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