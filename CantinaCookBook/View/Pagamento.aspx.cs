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

        }
        #endregion

        #region Método para limpeza
        private void limparCampos()
        {
            txtCliente.Value = "";
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

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);

            txtIdCliente.Value = idCliente.ToString();

            carregarValor(idCliente.ToString());
        
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

        //Método de impressão.
        protected void btnImprimirFolhaPagamento_Click(object sender, EventArgs e)
        {

            bool ehCredito = txtEhCredito.Value.Equals("S");

            if ( ! ehCredito ) 
            {

                Response.Write("<script> window.open('../Relatórios/RelFolhaPagamento.aspx','_blank'); </script>");

            } else
            {

                msgAlerta("Atenção ! O cliente não possue pagamentos pendentes. Por este motivo a folha de pagamento não será emitida.");

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
                    }

                }
                else
                {

                    dvPagamentoFinalizar.Visible = false;

                    dvResultadoPagamento.InnerText = (resultado * -1).ToString();

                }

            }
            else
            {

                sql = "EXEC getSaldo @IdCliente = " + idCliente.ToString();

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    decimal.TryParse(dt.Rows[0]["ValorDiferenca"].ToString(), out diferenca);

                }

                sql = @"INSERT INTO Pagamento (IdCliente,Valor,ValorAnterior,Tipo,[Data])
                        VALUES (" + idCliente.ToString() + "," + valorPago.ToString("0.00", CultureInfo.InvariantCulture) + "," + diferenca.ToString() + ",'CRED','" + DateTime.Now.ToString() + "')";


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

                }

            }

        }

        #endregion

        protected void btnAdicionarCredito_Click(object sender, EventArgs e)
        {
            toogleFinalizar();
        }

        protected void btnTroco_Click(object sender, EventArgs e)
        {
            toogleFinalizar();
        }
    }

}