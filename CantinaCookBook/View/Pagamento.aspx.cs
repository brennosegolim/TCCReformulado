using CantinaCookBook.Controller;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class Pagamento : System.Web.UI.Page
    {

        CantinaCon con = new CantinaCon();
        CantinaCommons cc = new CantinaCommons();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Nivel"] != null)
                {

                    if (!Session["Nivel"].ToString().Equals("A"))
                    {

                        Response.Redirect("UserHome.aspx");

                    }

                }
                else
                {

                    Session.RemoveAll();

                    Response.Redirect("~/Index.aspx");

                }

                initialize();
            }
        }

        #region Métodos de aviso.
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

        #endregion

        #region Métodos do inicialização
        private void initialize()
        {

            msgEsconder();
            limparCampos();
            toogleFinalizar();
            dvValores.Visible = false;
            btnCancelarCliente.Visible = false;
            dvResultadoPagamento.Visible = false;
            dvHistóricoPagamento.Visible = false;

            txtCliente.Focus();

        }
        #endregion

        #region Método para limpeza
        private void limparCampos()
        {
            txtCliente.Value = "";
        }

        private void resetHistorico()
        {

            string html = " <div class=\"row\"> "
                        + "     <h3 style=\"text-align:center;\">HISTÓRICO</h3> "
                        + " </div> ";

            dvHistóricoPagamento.InnerHtml = html;

        }

        #endregion

        #region Método para finalizar o pagamento
        private void toogleFinalizar()
        {

            if (dvFundo.Visible)
            {

                dvFundo.Visible = false;
                dvFinalizar.Visible = false;

            }
            else
            {

                dvFundo.Visible = true;
                dvFinalizar.Visible = true;

            }

        }
        #endregion

        #region Métodos para carregar as informações na tela.
        private void carregarGridUsuarios(string nome)
        {

            DataTable dt = null;

            string sql = "";

            if (nome == null) nome = "";

            if (nome != null)
            {

                try
                {

                    sql = @" SELECT TOP 10 CL.IdCliente,
                                    CL.Nome,
                                    CONVERT(VARCHAR(10),CL.DataNascimento,103) as DataNascimento,
                                    CL.CPF,
                                    CL.Celular
                               FROM Cliente CL
                              INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente
                              WHERE AC.Nivel = 'U'
                                AND CL.Autenticado = 1
                                AND Nome LIKE '%" + nome + "%'";

                    dt = con.getSelect(sql);


                }
                catch (Exception err)
                {

                    msgAlerta("Não foi possível carregar a lista de clientes, tente novamente mais tarde.");

                }

                grdUsuarios.DataSource = dt;
                grdUsuarios.DataBind();

            }

        }

        private void carregarValor(string idCliente)
        {

            try
            {

                DataTable dt = null;

                decimal diferenca = new decimal(0.0);
                string cor = "";
                string sql = "";

                sql = "EXEC getSaldo @IdCliente = " + idCliente.ToString();

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    decimal.TryParse(dt.Rows[0]["ValorDiferenca"].ToString(), out diferenca);

                }

                if (diferenca > 0) { cor = "red"; txtEhCredito.Value = "N"; } else { cor = "green"; diferenca = diferenca * -1; txtEhCredito.Value = "S"; }

                lblReais.Style.Add("color", cor);
                lblValor.Style.Add("color", cor);

                lblValor.Text = diferenca.ToString("0.00", CultureInfo.InvariantCulture);

                dvPesquisa.Visible = false;
                dvUsuarios.Visible = false;
                dvValores.Visible = true;

            }
            catch
            {

                msgAlerta("Não foi possível carregar os valores ! Tente novamente mais tarde ou informe ao suporte do Sistema.");

            }

        }

        private void carregarHistórico(string idCliente)
        {

            DataTable dt = null;

            string sql = "";
            string html = "";

            sql = @"SELECT IdPagamento,
                           IdCliente,
	                       Valor as ValorPagamento,
	                       ValorAnterior as Saldo,
	                       CONVERT(VARCHAR(20),[Data],113) as [Data],
	                       (CASE WHEN Tipo = 'PAG'    THEN 'Pagamento de pêndencia'
	                            WHEN Tipo = 'CRED'    THEN 'Adição de crédito.'
	                    		WHEN Tipo = 'PAGCR'   THEN 'Pagamento de pêndencia e adição de troco ao crédito'
			                     END) as Tipo
                      FROM Pagamento
                     WHERE IdCliente = " + idCliente
                 + " ORDER BY Data DESC";

            try
            {

                dt = con.getSelect(sql);
                resetHistorico();

            } catch
            {

                msgAlerta("Não foi possível carregar o histórico de pagamentos.");

            }

            html = "<div class=\"col s12\">";

            if(dt != null)
            {

                html += " <table class=\"responsive-table highlight\"> "
                      + "     <thead>                                  "
                      + "         <tr>                                 "
                      + "             <th>Valor Pendente/Crédito</th>  "
                      + "             <th>Valor Pagamento</th>         "
                      + "             <th>Valor Resultante</th>        "
                      + "             <th>Data</th>                    "
                      + "             <th>Tipo de Transação</th>       "
                      + "         </tr>                                "
                      + "     </thead>                                 "
                      + "     <tbody>                                  ";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    decimal valorPendente = new decimal(0.0);
                    decimal valorPagamento = new decimal(0.0);
                    decimal resultado = new decimal(0.0);
                    string data = dt.Rows[i]["Data"].ToString();
                    string tipo = dt.Rows[i]["Tipo"].ToString();
                    string cor = "";
                    string aux = "";
                    string auxRes = "";

                    decimal.TryParse(dt.Rows[i]["Saldo"].ToString(),out valorPendente);
                    decimal.TryParse(dt.Rows[i]["ValorPagamento"].ToString(), out valorPagamento);

                    resultado = (valorPendente - valorPagamento);

                    if (valorPendente <= 0)
                    {

                        cor = "#c8e6c9";
                        valorPendente = (valorPendente * -1);
                        aux = " de crédito.";

                    }
                    else
                    {
                     
                        cor = "#ffcdd2";
                        aux = " pendentes.";

                    }

                    if (resultado > 0) auxRes = " - pendentes.";
                    if (resultado < 0) { auxRes = " - de crédito."; resultado = (resultado * -1); };

                    html += " <tr style=\"background-color:"+ cor +";\" >           "
                          + "     <td> R$ " + valorPendente  + " - " + aux + "</td> "
                          + "     <td> R$ " + valorPagamento + "</td>               "
                          + "     <td> R$ " + resultado + auxRes +"</td>            "
                          + "     <td>" + data + "</td>                             "
                          + "     <td>" + tipo + "</td>                             "
                          + " </tr>                                                 ";

                }

                html += "</tbody>"
                      + "</table>"
                      + "</div>";

                dvHistóricoPagamento.InnerHtml += html;

                dvHistóricoPagamento.Visible = true;

            }

        }

        #endregion

        #region Evento dos componentes

        //Método para realizar a busca de clientes.
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {

            string nome = txtCliente.Value;

            carregarGridUsuarios(nome);

            txtCliente.Disabled = true;
            grdUsuarios.Visible = true;
            btnBuscarCliente.Visible = false;
            btnCancelarCliente.Visible = true;

        }

        //Método para cancelar a pesquisa de clientes.
        protected void btnCancelarCliente_Click(object sender, EventArgs e)
        {

            limparCampos();

            txtCliente.Disabled = false;
            grdUsuarios.Visible = false;
            btnBuscarCliente.Visible = true;
            btnCancelarCliente.Visible = false;

        }

        //Método para confirmar o cliente que será analisado.
        protected void btnConfirmarUsuario_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            ClienteCon clienteCon = new ClienteCon();

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);

            try
            {

                dt = clienteCon.SelectCliente(idCliente);

            }
            catch
            {
                //Nada
            }

            if(dt != null)
            {

                lblNomeCliente.Text = dt.Rows[0]["Nome"].ToString();

            }

            txtIdCliente.Value = idCliente.ToString();

            carregarValor(idCliente.ToString());

            carregarHistórico(idCliente.ToString());

        }

        //Método para realizar o pagamento.
        protected void btnRealizarPagamento_Click(object sender, EventArgs e)
        {

            bool ehCredito = txtEhCredito.Value.Equals("S");

            toogleFinalizar();

            if (ehCredito)
            {

                txtValorPendente.Value = "0.00";

            }
            else
            {

                txtValorPendente.Value = lblValor.Text;

            }

            txtValorPendente.Disabled = true;
            txtValorRecebido.Focus();

        }

        //Método para cancelar o pagamento
        protected void lnkCancelarPagamento_Click(object sender, EventArgs e)
        {

            txtValorRecebido.Value = "";

            toogleFinalizar();

        }

        //Método de impressão.
        protected void btnImprimirFolhaPagamento_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            
            bool ehCredito = txtEhCredito.Value.Equals("S");
            string idCliente = txtIdCliente.Value;
            string nome = "";
            string dataInicio = "";
            string dataFinal = "";
            string sql = "";

            if (!idCliente.Equals(""))
            {

                sql = " EXEC stp_relCartaCobranca @IdCliente = " + idCliente;

                dt = con.getSelect(sql);

                if(dt != null)
                {

                    nome = dt.Rows[0]["Nome"].ToString();
                    dataInicio = dt.Rows[0]["DataInicial"].ToString();
                    dataFinal = dt.Rows[0]["DataFinal"].ToString();

                }

                if (!ehCredito)
                {

                    Session.Add("RelNome", nome);
                    Session.Add("RelDataInicio",dataInicio);
                    Session.Add("RelDataFinal",dataFinal);
                    Session.Add("RelValor", lblValor.Text);

                    Response.Write("<script> window.open('../Relatórios/RelFolhaPagamento.aspx','_blank'); </script>");

                }
                else
                {

                    msgAlerta("Atenção ! O cliente não possue pagamentos pendentes. Por este motivo a carta de cobrança não será emitida.");

                }

            } else
            {

                msgAlerta("Não foi possível identificar o cliente, tente novamente mais tarde!");

            }

        }
        
        //Confirmar Pagamento.
        protected void btnConfirmarPagamento_Click(object sender, EventArgs e)
        {

            DataTable dt = null;


            int idCliente = int.Parse(txtIdCliente.Value);
            decimal diferenca = new decimal(0.0);
            decimal valorPago = new decimal(0.0);
            decimal resultado = new decimal(0.0);
            string sql = "";
            bool ehCredito = txtEhCredito.Value.Equals("S");

            if (!ehCredito)
            {

                sql = "EXEC getSaldo @IdCliente = " + idCliente.ToString();

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    decimal.TryParse(dt.Rows[0]["ValorDiferenca"].ToString(), out diferenca);
                    decimal.TryParse(txtValorRecebido.Value, out valorPago);

                }

                resultado = (diferenca - valorPago);

                if (resultado >= 0)
                {

                    sql = @"INSERT INTO Pagamento (IdCliente,Valor,ValorAnterior,Tipo,[Data])
                            VALUES (" + idCliente.ToString() + "," + valorPago.ToString("0.00",CultureInfo.InvariantCulture) + "," + diferenca.ToString("0.00", CultureInfo.InvariantCulture) + ",'PAG','" + DateTime.Now.ToString() + "')";

                    try
                    {

                        con.executeSelect(sql);

                        toogleFinalizar();

                        msgSucesso("Pagamento Realizado com sucesso.");

                    }
                    catch (Exception err)
                    {

                        toogleFinalizar();

                        msgAlerta("Não foi possível realizar o pagamento ! Tente novamente mais tarde.");

                    }
                    finally
                    {
                        carregarValor(idCliente.ToString());
                        carregarHistórico(idCliente.ToString());
                    }

                }
                else
                {

                    Session.Add("ValorPago",resultado);
                    Session.Add("Diferenca",diferenca);

                    dvValorResultado.InnerText = "R$ " + (resultado * -1).ToString();

                    dvPagamentoFinalizar.Visible = false;
                    dvResultadoPagamento.Visible = true;

                }

            }
            else
            {

                sql = "EXEC getSaldo @IdCliente = " + idCliente.ToString();

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    decimal.TryParse(dt.Rows[0]["ValorDiferenca"].ToString(), out diferenca);
                    decimal.TryParse(txtValorRecebido.Value, out valorPago);

                }

                sql = @"INSERT INTO Pagamento (IdCliente,Valor,ValorAnterior,Tipo,[Data])
                        VALUES (" + idCliente.ToString() + "," + valorPago.ToString("0.00", CultureInfo.InvariantCulture) + "," + diferenca.ToString("0.00",CultureInfo.InvariantCulture) + ",'CRED','" + DateTime.Now.ToString() + "')";

                try
                {

                    con.executeSelect(sql);

                    toogleFinalizar();

                    msgSucesso("Crédito Adicionado com sucesso.");
                
                } 
                catch
                {

                    toogleFinalizar();

                    msgAlerta("Não foi possível adicionar crédito ! Tente novamente mais tarde.");

                } finally
                {

                    carregarValor(idCliente.ToString());
                    carregarHistórico(idCliente.ToString());

                }

            }

            txtValorRecebido.Value = "";

        }

        //Confirmar Pagamento e adicionar crédito.
        protected void btnAdicionarCredito_Click(object sender, EventArgs e)
        {

            if (Session["ValorPago"] != null && Session["Diferenca"] != null)
            {

                string sql = "";
                string idCliente = txtIdCliente.Value;
                decimal valorPago = new decimal(0.0);
                decimal diferenca = new decimal(0.0);
                decimal resultado = new decimal(0.0);

                decimal.TryParse(Session["ValorPago"].ToString(), out valorPago);
                decimal.TryParse(Session["Diferenca"].ToString(), out diferenca);

                resultado = (diferenca - valorPago);

                sql = @"INSERT INTO Pagamento (IdCliente,Valor,ValorAnterior,Tipo,[Data])
                        VALUES (" + idCliente.ToString() + "," + resultado.ToString("0.00", CultureInfo.InvariantCulture) + "," + diferenca.ToString("0.00", CultureInfo.InvariantCulture) + ",'PAGCR','" + DateTime.Now.ToString() + "')";

                try
                {

                    con.executeSelect(sql);

                    toogleFinalizar();

                    msgSucesso("Pagamento realizado e crédito adicionado com sucesso !");

                }
                catch
                {

                    toogleFinalizar();

                    msgAlerta("Não foi possível adicionar o pagamento e o crédito ! tente novamente mais tarde.");

                }
                finally
                {

                    carregarValor(idCliente);
                    carregarHistórico(idCliente);

                    Session.Remove("ValorPago");
                    Session.Remove("Diferenca");

                    dvValorResultado.InnerText = "";

                    dvPagamentoFinalizar.Visible = true;
                    dvResultadoPagamento.Visible = false;

                }

            }
            else
            {

                toogleFinalizar();

                msgAlerta("Ocorreu um problema inesperado, informe ao admnistrador do sistema.");

            }

        }

        //Confirmar Pagamento e devolver o troco.
        protected void btnTroco_Click(object sender, EventArgs e)
        {

            if (Session["Diferenca"] != null)
            {

                string sql = "";
                string idCliente = txtIdCliente.Value;
                decimal diferenca = new decimal(0.0);

                decimal.TryParse(Session["Diferenca"].ToString(), out diferenca);

                sql = @"INSERT INTO Pagamento (IdCliente,Valor,ValorAnterior,Tipo,[Data])
                        VALUES (" + idCliente.ToString() + "," + diferenca.ToString("0.00", CultureInfo.InvariantCulture) + "," + diferenca.ToString("0.00", CultureInfo.InvariantCulture) + ",'PAG','" + DateTime.Now.ToString() + "')";

                try
                {

                    con.executeSelect(sql);

                    toogleFinalizar();

                    msgSucesso("Pagamento realizado com sucesso !");

                }
                catch
                {

                    toogleFinalizar();

                    msgAlerta("Não foi possível adicionar o pagamento ! tente novamente mais tarde.");

                }
                finally
                {

                    carregarValor(idCliente);
                    carregarHistórico(idCliente);


                    Session.Remove("ValorPago");
                    Session.Remove("Diferenca");

                    dvValorResultado.InnerText = "";

                    dvPagamentoFinalizar.Visible = true;
                    dvResultadoPagamento.Visible = false;

                }

            }
            else
            {

                toogleFinalizar();

                msgAlerta("Ocorreu um problema inesperado, informe ao admnistrador do sistema.");

            }

        }

        //Método para trocar cliente.
        protected void lnkTrocarCliente_Click(object sender, EventArgs e)
        {

            Response.Redirect("Pagamento.aspx");

        }

        #endregion

    }

}