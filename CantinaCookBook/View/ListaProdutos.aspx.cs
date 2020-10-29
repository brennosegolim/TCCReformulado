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
    public partial class ListaProdutos : System.Web.UI.Page
    {

        CantinaCon con = new CantinaCon();
        ProdutoCon pc = new ProdutoCon();

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

                atualizarGrid();

                Session.Remove("IdProduto");
                Session.Remove("Metodo");
            
            }

        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {

            try
            {

                int idProduto = Convert.ToInt32((sender as LinkButton).CommandArgument);

                pc.DeletarProduto(idProduto);

                atualizarGrid();

            } catch(Exception err)
            {

                Response.Write("<script>alert('Não foi possível deletar esse produto. foram encontradas vendas registradas para o mesmo.!');</script>");

            }

        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {

            int idProduto = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Session.Add("IdProduto",idProduto);
            Session.Add("Metodo","alterar");

            Response.Redirect("CadastroProduto.aspx");

        }

        private void atualizarGrid()
        {

            grdProdutos.DataSource = pc.SelectProduto();
            grdProdutos.DataBind();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            string descricao = txtDescricao.Value;
            string sql = "";

            sql = " SELECT *        "
                + "   FROM Produto  "
                + "  WHERE Descricao LIKE '%' + '" + descricao + "' +'%' ";

            dt = con.getSelect(sql);

            if(dt != null)
            {

                grdProdutos.DataSource = dt;
                grdProdutos.DataBind();

            }

        }
    }
}