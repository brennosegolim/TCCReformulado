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

                msgEsconder();
                txtFiltroData.Visible = false;
                btnRealizarFiltro.Visible = false;
                dvSelecionarOutro.Visible = false;

                if (Session["Nome"] != null )
                {

                    string nome = Session["Nome"].ToString();
                    string nivel = Session["Nivel"].ToString();
                    string idCliente = Session["IdCliente"].ToString();

                    if (nivel.Equals("U"))
                    {

                        dvAdministrador.Visible = false;
                        dvNomeUsuario.InnerText = Session["Nome"].ToString();

                        if (numeroDependentes(idCliente) > 0)
                        {

                            DataTable dt = null;

                            string html = "";

                            dvUsuario.Visible = false;
                            dvSelectDependente.Visible = true;

                            dt = retornaDependentes(idCliente);

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                cbxDepentes.Items.Add(new ListItem("Selecione o Dependente.", ""));
                                cbxDepentes.Items.Add(new ListItem("Eu", idCliente));

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    string teste = dt.Rows[i]["IdCliente"].ToString();
                                    string teste2 = dt.Rows[i]["Nome"].ToString();

                                    cbxDepentes.Items.Add(new ListItem(dt.Rows[i]["Nome"].ToString(), dt.Rows[i]["IdCliente"].ToString()));

                                }

                            }

                        }
                        else
                        {

                            dvUsuario.Visible = true;
                            dvSelectDependente.Visible = false;
                            gerarHistorico(idCliente, "0");

                            if (temResponsavel(idCliente))
                            {

                                setLimiteGasto(int.Parse(idCliente));
                                txtPrecoLimite.Disabled = true;
                                titleLimiteGastos.InnerText = "SEU LIMITE DE GASTOS";
                                btnConfirmarLimite.Visible = false;

                            }
                            else
                            {

                                setLimiteGasto(int.Parse(idCliente));
                                txtPrecoLimite.Disabled = false;
                                btnConfirmarLimite.Visible = true;

                            }

                        }

                    } else
                    {

                        dvUsuario.Visible = false;
                        dvSelectDependente.Visible = false;
                        dvAdministrador.Visible = true;
                        carregarGridAdministrasdor();

                    }

                }

            }

        }

        //Método para gerar a tabela de histórico de gastos.
        private void gerarHistorico(string idCliente,string data)
        {

            DataTable dt = null;
            string sql = "";

            //Select que irá retornar os histórico de gastos.
            sql = " SELECT TOP 50 IdProduto_Venda,                        "
                + "        PR.Descricao as NomeProduto,                   "
                + "        PR.Preco as Valor,                             "
                + "        PV.Quantidade,                                 "
                + "        CONVERT(VARCHAR(10),VE.[Data], 103) as [Data], "
                + "        CONVERT(VARCHAR(5), PV.[Data], 108) AS Hora    "
                + "   FROM Produto_Venda PV                               "
                + "  INNER JOIN Produto PR ON PR.IdProduto = PV.IdProduto "
                + "  INNER JOIN Venda VE ON VE.IdVenda = PV.IdVenda       "
                + "  WHERE IdCliente = " + idCliente + "                  "
                + "    AND (('0' = '" + data + "') OR ( CAST(PV.[Data] as Date) = '" + data + "')) "
                + "  ORDER BY PV.Data DESC";

            //Recebendo o resultado em um datatable
            dt = con.getSelect(sql);

            if (dt != null)
            {

                //Montando o cabeçalho.
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

                //Percorrendo o DataTable e montando as linhas.
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

                //Inserindo a variável no card.
                tableHistorico.InnerHtml = html;

            }

        }

        //Método que retorna o número de dependentes.
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

        //Método para retornar a consulta dos dependentes.
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

        //Ação do botão para mostrar e esconder o calendário
        private void toggleFiltroData()
        {

            string idCliente = cbxDepentes.SelectedValue;

            if (idCliente == null || idCliente.Equals(""))
            {

                if (Session["IdCliente"] != null)
                {
                
                    idCliente = Session["IdCliente"].ToString();
                
                } else
                {

                    idCliente = "0";

                }
            
            }

            if (txtFiltroData.Visible) {

                btnRealizarFiltro.Visible = false;
                txtFiltroData.Visible = false;
                txtFiltroData.Value = "";
                btnFiltroData.Text = "Filtrar";
                gerarHistorico(idCliente, "0");

            } else
            {

                txtFiltroData.Value = DateTime.Now.ToString("yyyy-MM-dd");
                btnRealizarFiltro.Visible = true;
                txtFiltroData.Visible = true;
                btnFiltroData.Text = "Esconder";

            }

        }

        private bool temResponsavel(string idCliente)
        {

            DataTable dt = null;

            string sql = "";
            int quantidade = 0;

            sql = " SELECT COUNT(IdResponsavel) as Qtd "
                + "   FROM Cliente                     "
                + "  WHERE IdResponsavel <> 0          "
                + "    AND IdCliente = " + idCliente;

            dt = con.getSelect(sql);

            if (dt != null)
            {

                int.TryParse(dt.Rows[0]["Qtd"].ToString(),out quantidade);

            }

            return quantidade > 0;

        }

        private void setLimiteGasto()
        {

            DataTable dt = null;
            ClienteLimiteCon clienteLimiteCon = new ClienteLimiteCon();

            int idCliente = int.Parse(cbxDepentes.SelectedValue);
            decimal valor = new decimal(0.0);

            dt = clienteLimiteCon.SelectCliente(idCliente);

            if(dt != null && dt.Rows.Count > 0)
            {

                decimal.TryParse(dt.Rows[0]["Valor"].ToString(), out valor);

            }

            txtPrecoLimite.Value = valor.ToString();

        }

        private void setLimiteGasto(int idCliente)
        {

            DataTable dt = null;
            ClienteLimiteCon clienteLimiteCon = new ClienteLimiteCon();

            decimal valor = new decimal(0.0);

            dt = clienteLimiteCon.SelectCliente(idCliente);

            if (dt != null && dt.Rows.Count > 0)
            {

                decimal.TryParse(dt.Rows[0]["Valor"].ToString(), out valor);

            }

            txtPrecoLimite.Value = valor.ToString();

        }

        protected void cbxDepentes_SelectedIndexChanged(object sender, EventArgs e)
        {

            string idCliente = cbxDepentes.SelectedValue;

            if (idCliente != null && !idCliente.Equals(""))
            {

                dvUsuario.Visible = true;
                dvSelectDependente.Visible = false;
                dvSelecionarOutro.Visible = true;
                gerarHistorico(idCliente, "0");
                setLimiteGasto(int.Parse(idCliente));

            }

        }

        protected void btnConfirmarLimite_Click(object sender, EventArgs e)
        {

            try
            {

                string idCliente = cbxDepentes.SelectedValue;

                if (idCliente == null || idCliente.Equals(""))
                {

                    if (Session["IdCliente"] != null)
                    {

                        idCliente = Session["IdCliente"].ToString();

                    }
                    else
                    {

                        idCliente = "0";

                    }

                }

                decimal valorLimite = new decimal(0.0);
                Decimal.TryParse(txtPrecoLimite.Value, out valorLimite);
                string dataAtual = DateTime.Now.ToString();

                ClienteLimiteCon clienteLimiteCon = new ClienteLimiteCon();
                ClienteLimite clienteLimite = new ClienteLimite();

                DataTable dt = clienteLimiteCon.SelectCliente(int.Parse(idCliente));

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (decimal.Parse(dt.Rows[0]["Valor"].ToString()) != valorLimite)
                    {

                        clienteLimite.IdCliente = int.Parse(idCliente);
                        clienteLimite.Valor = valorLimite;
                        clienteLimite.Data = dataAtual;

                        clienteLimiteCon.AdicionarLimite(clienteLimite);

                        msgSucesso("Limite de gasto alterado com sucesso.");
                    
                    } else {

                        msgEsconder();

                    }

                } else
                {

                    clienteLimite.IdCliente = int.Parse(idCliente);
                    clienteLimite.Valor = valorLimite;
                    clienteLimite.Data = dataAtual;

                    clienteLimiteCon.AdicionarLimite(clienteLimite);

                    msgSucesso("Limite de gasto alterado com sucesso.");

                }

            }
            catch (Exception err)
            {

                string msgErro = "";

                msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                System.Diagnostics.Debug.WriteLine(msgErro);

                msgAlerta("Não possível alterar o limite de gasto.");

            }

        }

        protected void btnFiltroData_Click(object sender, EventArgs e)
        {

            toggleFiltroData();

        }

        protected void btnRealizarFiltro_Click(object sender, EventArgs e)
        {

            string idCliente = cbxDepentes.SelectedValue;

            if (idCliente == null || idCliente.Equals(""))
            {

                if (Session["IdCliente"] != null)
                {

                    idCliente = Session["IdCliente"].ToString();

                }
                else
                {

                    idCliente = "0";

                }

            }

            string data = txtFiltroData.Value;

            if (data.Length == 10)
            {

                data = data.Substring(8, 2) + "/" + data.Substring(5, 2) + "/" + data.Substring(0, 4);

                gerarHistorico(idCliente, data);

            }

        }

        private void msgAlerta(string mensagem)
        {

            dvAlerta.InnerText = mensagem;
            dvPanels.Visible = true;
            dvAlerta.Visible = true;
            dvSucesso.Visible = false;

        }

        private void msgSucesso(string mensagem)
        {

            dvSucesso.InnerText = mensagem;
            dvPanels.Visible = true;
            dvSucesso.Visible = true;
            dvAlerta.Visible = false;

        }

        private void msgEsconder()
        {

            dvPanels.Visible = false;
            dvAlerta.Visible = false;
            dvSucesso.Visible = false;

            dvAlerta.InnerText = "";
            dvSucesso.InnerText = "";

        }

        private void carregarGridAdministrasdor()
        {

            DataTable dt = null;

            string sql = " SELECT *               "
                       + "   FROM Cliente         "
                       + "  WHERE Autenticado = 0 ";

            dt = con.getSelect(sql);

            if (dt != null)
            {

                grdClientes.DataSource = dt;
                grdClientes.DataBind();

            }

        }

        protected void btnAutenticar_Click(object sender, EventArgs e)
        {

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Session.Add("IdAutenticar",idCliente);

            Response.Redirect("AutenticarUsuario.aspx");

        }

        protected void btnEscolherDependente_Click(object sender, EventArgs e)
        {
            dvUsuario.Visible = false;
            dvSelectDependente.Visible = true;
            dvSelecionarOutro.Visible = false;
            txtFiltroData.Value = "";

        }
    }
}