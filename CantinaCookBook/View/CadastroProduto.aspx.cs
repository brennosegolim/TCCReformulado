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
    public partial class CadastroProduto : System.Web.UI.Page
    {

        CantinaCommons cc = new CantinaCommons();
        CantinaCon con = new CantinaCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                msgEsconder();

                if (Session["IdProduto"] != null && Session["Metodo"] != null)
                {

                    DataTable dt = null;
                    ProdutoCon pc = new ProdutoCon();

                    int idProduto = 0;

                    int.TryParse(Session["IdProduto"].ToString(), out idProduto);
                    dt = pc.SelectProduto(idProduto);

                    txtCodigo.Value = dt.Rows[0]["Codigo"].ToString();
                    txtDescricao.Value = dt.Rows[0]["Descricao"].ToString();
                    txtPreco.Value = dt.Rows[0]["Preco"].ToString();
                    txtObservacao.Value = dt.Rows[0]["Observacao"].ToString();

                } else
                {

                    txtCodigo.Value = gerarCodigo();

                }

            }
        }

        private void msgAlerta(string mensagem)
        {

            dvAlerta.InnerText = mensagem;
            dvPanels.Visible  = true;
            dvAlerta.Visible  = true;
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

        private void limparCampos()
        {

            txtCodigo.Value = "";
            txtDescricao.Value = "";
            txtPreco.Value = "";
            txtObservacao.Value = "";

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            string metodo = "";
            string idProduto = "";

            string codigo = txtCodigo.Value;
            string descricao = txtDescricao.Value;
            decimal preco = new decimal(0.0);
            decimal.TryParse(txtPreco.Value, out preco);
            string observacao = txtObservacao.Value;
            string msg = "";

            if (codigo.Equals("")) msg += "Informe o código.\n";
            if (descricao.Equals("")) msg += "Informe a Descrição !\n";
            if (txtPreco.Value.Equals("")) msg += "Informe o preço !\n";

            if (msg.Equals(""))
            {

                try
                {

                    //Classes
                    Produto produto = new Produto();
                    ProdutoCon pc = new ProdutoCon();

                    //Variáveis
                    bool sucesso = false;

                    produto.Codigo = codigo;
                    produto.Descricao = descricao;
                    produto.Preco = preco;
                    produto.Observacao = observacao;

                    if (Session["IdProduto"] != null && Session["Metodo"] != null)
                    {

                        produto.IdProduto = int.Parse(Session["IdProduto"].ToString());

                        pc.AtualizarProduto(produto);

                        Response.Redirect("ListaProdutos.aspx");

                    }
                    else
                    {

                        sucesso = pc.AdicionarProduto(produto);

                        if (sucesso)
                        {

                            msgSucesso("Produto Inserido com sucesso.");
                            limparCampos();
                            txtCodigo.Value = gerarCodigo();

                        }
                        else
                        {

                            msgAlerta("Ocorreu um problema ao inserir o produto tente novamente mais tarde");

                        }

                    }

                }
                catch (Exception err)
                {

                    msgAlerta("Ocorreu um problema ao inserir o produto tente novamente mais tarde");

                }

            }
            else
            {
                
                msgAlerta(msg);

            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("IdProduto");
            Session.Remove("Metodo");
            Response.Redirect("Cadastros.aspx");
        }

        private string gerarCodigo()
        {

            DataTable dt = null;

            string retorno = "";
            string codigo = "";
            string sql = "";

            try
            {

                sql = @" SELECT TOP 1 Codigo
                           FROM Produto
                          ORDER BY IdProduto DESC ";

                dt = con.getSelect(sql);

                if(dt != null)
                {

                    codigo = dt.Rows[0]["Codigo"].ToString();

                }

                if (cc.isInterger(codigo))
                {

                    int valor = int.Parse(codigo);

                    valor++;

                    retorno = valor.ToString();

                } else
                {

                    int valor = 0;

                    Random rd = new Random();

                    valor = rd.Next(999999);

                    retorno = valor.ToString();

                }

            } catch(Exception err)
            {

                retorno = "codigo";
                
            }

            return retorno;

        }

    }
}