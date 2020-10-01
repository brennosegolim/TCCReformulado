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

namespace CantinaCookBook.View
{
    public partial class UserHome : System.Web.UI.Page
    {

        CantinaCommons cc = new CantinaCommons();
        CantinaCon con = new CantinaCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                if (Session["Nome"] != null )
                {

                    string nome = Session["Nome"].ToString();
                    string nivel = Session["Nivel"].ToString();
                    string idCliente = Session["IdCliente"].ToString();

                    dvNomeUsuario.InnerText = Session["Nome"].ToString();

                    if (numeroDependentes(idCliente) > 0)
                    {

                        DataTable dt = null;

                        string html = "";

                        dvUsuario.Visible = false;
                        dvSelectDependente.Visible = true;

                        dt = retornaDependentes(idCliente);

                        if(dt != null && dt.Rows.Count > 0)
                        {

                            cbxDepentes.Items.Add(new ListItem("Selecione o Dependente.", ""));

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                string teste = dt.Rows[i]["IdCliente"].ToString();
                                string teste2 = dt.Rows[i]["Nome"].ToString();

                                cbxDepentes.Items.Add(new ListItem(dt.Rows[i]["Nome"].ToString(),dt.Rows[i]["IdCliente"].ToString()));

                            }

                        }

                    }
                    else
                    {

                        dvUsuario.Visible = true;
                        dvSelectDependente.Visible = false;
                        gerarHistorico(idCliente, "0");

                    }

                }

            }
        }

        private void gerarHistorico(string idCliente,string data)
        {

            DataTable dt = null;
            string sql = "";

            sql = " SELECT IdProduto_Venda,                               "
                + "        PR.Descricao as NomeProduto,                   "
                + "        PR.Preco as Valor,                             "
                + "        PV.Quantidade,                                 "
                + "        CONVERT(VARCHAR(10),VE.[Data], 103) as [Data], "
                + "        CONVERT(VARCHAR(5), PV.[Data], 108) AS Hora    "
                + "   FROM Produto_Venda PV                               "
                + "  INNER JOIN Produto PR ON PR.IdProduto = PV.IdProduto "
                + "  INNER JOIN Venda VE ON VE.IdVenda = PV.IdVenda       "
                + "  WHERE IdCliente = " + idCliente + "                  "
                + "    AND (('0' = '" + data + "') OR (PV.[Data] = '" + data + "')) ";

            dt = con.getSelect(sql);

            if (dt != null && dt.Rows.Count > 0)
            {

                string html = " <table class=\"responsive-table highlight\"> "
                            + "     <thead>                                  "
                            + "         <tr>                                 "
                            + "             <th>Produto</th>                 "
                            + "             <th>Valor Unitário</th>          "
                            + "             <th>Quantidade</th>              "
                            + "             <th>Data</th>                    "
                            + "             <th>Hora</th>                    "
                            + "         </tr>                                "
                            + "     </thead>                                 "
                            + "     <tbody>                                  ";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string nomeProduto = dt.Rows[i]["NomeProduto"].ToString();
                    string valor = dt.Rows[i]["Valor"].ToString();
                    string quantidade = dt.Rows[i]["Quantidade"].ToString();
                    string dataSql = dt.Rows[i]["Data"].ToString();
                    string hora = dt.Rows[i]["Hora"].ToString();

                    html += " <tr>                                                         "
                          + "     <td>" + nomeProduto + "</td>                             "
                          + "     <td>" + valor + "</td>      "
                          + "     <td>" + quantidade + "</td> "
                          + "     <td>" + dataSql + "</td>                                 "
                          + "     <td>" + hora + "</td>                                    "
                          + " </tr>                                                        ";

                }

                html += "</tbody>"
                      + "</table>";

                tableHistorico.InnerHtml = html;

            }

        }

        private int numeroDependentes(string idCliente)
        {

            DataTable dt = null;

            int quantidade = 0;
            string sql = "";

            sql = " SELECT COUNT(IdResponsavel) as Qtd "
                + "   FROM Cliente                     "
                + "  WHERE IdResponsavel = " + idCliente;

            try
            {

                dt = con.getSelect(sql);

                if (dt != null && dt.Rows.Count > 0)
                {

                    quantidade = int.Parse(dt.Rows[0]["Qtd"].ToString());

                }

            } catch(Exception err)
            {

                //Nada acontece aqui.

            }

            return quantidade;

        }

        private DataTable retornaDependentes(string idCliente)
        {

            DataTable dt = null;

            string sql = "";

            try
            {

                sql = " SELECT IdCliente,      "
                    + "        Nome            "
                    + "   FROM Cliente         "
                    + "  WHERE IdResponsavel = " + idCliente;

                dt = con.getSelect(sql);

            } catch(Exception err)
            {

                //Nada Acontece aqui. tchau e bença.

            }

            return dt;

        }

        protected void cbxDepentes_SelectedIndexChanged(object sender, EventArgs e)
        {

            string idCliente = cbxDepentes.SelectedValue;

            dvUsuario.Visible = true;
            dvSelectDependente.Visible = false;
            gerarHistorico(idCliente, "0");

        }

        protected void btnConfirmarLimite_Click(object sender, EventArgs e)
        {

            try
            {

                int idCliente = int.Parse(cbxDepentes.SelectedValue);
                decimal valorLimite = new decimal(0.0);
                Decimal.TryParse(txtPrecoLimite.Value, out valorLimite);
                string dataAtual = DateTime.Now.ToString();

                ClienteLimiteCon clienteLimiteCon = new ClienteLimiteCon();
                ClienteLimite clienteLimite = new ClienteLimite();
                
                clienteLimite.IdCliente = idCliente;
                clienteLimite.Valor = valorLimite;
                clienteLimite.Data = dataAtual;

                clienteLimiteCon.AdicionarLimite(clienteLimite);

            }
            catch (Exception err)
            {

                string msgErro = "";

                msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                Response.Write("<script type=\"text/javascriot\">alert('Não foi possível adicionar um limite, entre em contato com o suporte.');Console.log('Erro: "+ msgErro +"')<script>");

            }

        }
    }
}