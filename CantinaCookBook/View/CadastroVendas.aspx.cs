using CantinaCookBook.Controller;
using CantinaCookBook.Models;
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
    public partial class CadastroVendas : System.Web.UI.Page
    {
        #region Objetos para produção
        CantinaCon con = new CantinaCon();
        CantinaCommons cc = new CantinaCommons();
        #endregion

        #region Load da página. 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                inicializar();

            }

        }

        #endregion

        #region Métodos produção.

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

        #region Métodos de limpeza
        private void limparCampos()
        {

            txtCliente.Value = "";
            txtCodigo.Value = "";
            txtProduto.Value = "";
            txtQuantidade.Value = "";

        }



        private void cancelarVenda(int idVenda)
        {

            VendaCon venda = new VendaCon();
            string sql = "";

            sql = @" DELETE FROM Produto_Venda WHERE IdVenda = " + idVenda.ToString();

            con.executeSelect(sql);

            venda.DeletarVenda(idVenda);

        }

        private void cancelarVenda(string idVenda)
        {

            VendaCon venda = new VendaCon();
            string sql = "";
            int codVenda = 0;

            int.TryParse(idVenda, out codVenda);

            sql = @" DELETE FROM Produto_Venda WHERE IdVenda = " + idVenda;

            con.executeSelect(sql);

            venda.DeletarVenda(codVenda);

        }

        private void limparCache()
        {

            Session.Remove("IdClienteVenda");
            Session.Remove("IdVendaAtual");

        }

        #endregion

        #region Método de inicialização
        private void inicializar()
        {

            msgEsconder();
            btnCancelar.Visible = false;
            dvCliente.Visible = false;
            dvDetalhamento.Visible = false;
            grdProduto.Visible = false;

        }
        #endregion

        #region Método para carregar informações de grids
        //Métodos para carregar as grids
        private void carregarClientes(string nome)
        {

            DataTable dt = null;

            string sql = "";

            if(nome != null)
            {

                try
                {

                    sql = @" SELECT TOP 10 CL.IdCliente,
                                    CL.Nome,
                                    CONVERT(VARCHAR(10),CL.DataNascimento,103) as DataNascimento,
                                    CL.CPF
                               FROM Cliente CL
                              INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente
                              WHERE AC.Nivel = 'U'
                                AND CL.Autenticado = 1
                                AND Nome LIKE '%" + nome + "%'";

                    dt = con.getSelect(sql);


                } catch(Exception err)
                {

                    msgAlerta("Não foi possível carregar a lista de clientes, tente novamente mais tarde.");

                }

                grdClientes.DataSource = dt;
                grdClientes.DataBind();

            }

        }

        #endregion

        #endregion

        #region Ações dos componentes em tela.
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {

            limparCampos();

            btnCancelar.Visible = true;
            btnAdicionar.Visible = false;
            dvCliente.Visible = true;
            btnConfirmarCliente.Visible = true;
            txtCliente.Disabled = false;

        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {

            string idVenda = Session["IdVendaAtual"].ToString();

            limparCampos();
            if( ! idVenda.Equals("")) cancelarVenda(idVenda);
            limparCache();

            btnCancelar.Visible = false;
            btnAdicionar.Visible = true;
            dvCliente.Visible = false;

        }

        protected void btnConfirmarCliente_Click(object sender, EventArgs e)
        {

            string nome = txtCliente.Value;

            carregarClientes(nome);

        }

        protected void btnSelecionar_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            ClienteCon cliente = new ClienteCon();
            VendaCon vendaCon = new VendaCon();
            Venda venda = new Venda();

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);
            bool sequencia = false;
            string sql = "";
            string idVenda = "";

            Session.Add("IdClienteVenda", idCliente);

            grdClientes.Visible = false;
            btnConfirmarCliente.Visible = false;

            dt = cliente.SelectCliente(idCliente);

            if(dt != null)
            {

                string nome = dt.Rows[0]["Nome"].ToString();

                txtCliente.Value = nome;
                txtCliente.Disabled = true;

            }

            dvDetalhamento.Visible = true;

            venda.Data = DateTime.Now.ToString();
            venda.ValorTotal = new decimal(0.0);
            venda.IdCliente = idCliente;
            sequencia = vendaCon.AdicionarVenda(venda);

            if (sequencia)
            {

                sql = @" SELECT TOP 1 IdVenda FROM Venda ORDER BY IdVenda DESC ";

                dt = null;
                dt = con.getSelect(sql);

                if(dt != null)
                {

                    idVenda = dt.Rows[0]["IdVenda"].ToString();

                    Session.Add("IdVendaAtual",idVenda);

                }

            } else
            {

                msgAlerta("Não foi possível adicionar a venda. Tente novamente mais tarde.");

            }

        }

        #endregion

        protected void btnPesquisarProduto_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            string sql = "";
            string codigo = "";
            string descricao = "";

            codigo = txtCodigo.Value;
            descricao = txtProduto.Value;

            sql = " SELECT TOP 10 * " 
                + "   FROM Produto  "
                + "  WHERE Descricao LIKE '%' + '"+ descricao + "' + '%' "
                + "     OR Codigo = '"+ codigo +"' ";

            dt = con.getSelect(sql);

            if(dt != null)
            {

                grdProduto.Visible = true;
                grdProduto.DataSource = dt;
                grdProduto.DataBind();

            }

        }

        protected void btnSelecionarProduto_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            VendaCon vendaCon = new VendaCon();
            ProdutoCon produtoCon = new ProdutoCon();

            int quantidade = 0;
            int idProduto = Convert.ToInt32((sender as LinkButton).CommandArgument);
            decimal precoProduto = new decimal(0.0);
            decimal valorTotal = new decimal(0.0);
            string idVenda = Session["IdVendaAtual"].ToString();
            string sql = "";

            int.TryParse(txtQuantidade.Value, out quantidade);

            if( ! cc.isEmpty(idVenda))
            {

                if (quantidade > 0)
                {

                    int id = 0;

                    int.TryParse(idVenda, out id);

                    dt = vendaCon.SelectVenda(id);

                    if (dt != null)
                    {

                        string valorTemp = dt.Rows[0]["ValorTotal"].ToString();

                        decimal.TryParse(valorTemp, out valorTotal);

                    }

                    dt = produtoCon.SelectProduto(idProduto);

                    if (dt != null)
                    {

                        string precoTemp = dt.Rows[0]["Preco"].ToString();

                        decimal.TryParse(precoTemp, out precoProduto);

                    }

                    decimal valorIntermed = precoProduto * quantidade;

                    sql = @"INSERT INTO Produto_Venda(IdProduto,IdVenda,Valor,Data,Quantidade)
                        VALUES (" + idProduto.ToString() + "," + idVenda + "," + valorIntermed.ToString("0.00", CultureInfo.InvariantCulture) + ",GETDATE()," + quantidade.ToString() + ")";

                    con.executeSelect(sql);

                    valorTotal += valorIntermed;

                    sql = @"UPDATE Venda SET ValorTotal = " + valorTotal.ToString("0.00", CultureInfo.InvariantCulture) + "WHERE IdVenda = " + idVenda;

                    //Lembra de verificar o saldo limite ao adicionar o total e produto venda.

                    con.executeSelect(sql);

                    txtCodigo.Value = "";
                    txtProduto.Value = "";
                    txtQuantidade.Value = "";

                    grdProduto.Visible = false;


                } else
                {

                    msgAlerta("Informe a quantidade.");

                }

            } else
            {

                msgAlerta("Não foi possível adicionar o produto à venda, tente novamente mais tarde !");

            }

        }
    }
}