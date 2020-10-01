﻿using CantinaCookBook.Controller;
using CantinaCookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class ListaProdutos : System.Web.UI.Page
    {

        ProdutoCon pc = new ProdutoCon();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

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
    }
}