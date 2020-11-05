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
    public partial class ListaProdutoVenda : System.Web.UI.Page
    {

        CantinaCon con = new CantinaCon();

        protected void Page_Load(object sender, EventArgs e)
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

            if (Session["ListaIdVenda"] != null) {

                DataTable dt = null;

                string sql = @"SELECT PD.Codigo,
                                  PD.Descricao,
                             	  PV.Quantidade,
                             	  PV.Valor
                             FROM Produto_Venda PV
                            INNER JOIN Produto PD ON PD.IdProduto = PV.IdProduto 
                            WHERE PV.IdVenda = " + Session["ListaIdVenda"].ToString();

                dt = con.getSelect(sql);

                if(dt != null)
                {

                    titleCodigo.InnerText = "Código da Pendência: " + Session["ListaIdVenda"].ToString();

                    grdProdutoVenda.DataSource = dt;
                    grdProdutoVenda.DataBind();

                }

            }
        }
    }
}