using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class Cadastros : System.Web.UI.Page
    {
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

            }

        }

        protected void lnkCadastrarUsuario_Click(object sender, EventArgs e)
        {

            Response.Redirect("CadastroUsuario.aspx");

        }

        protected void lnkVisualizarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaUsuarios.aspx");
        }

        protected void lnkCadastrarProduto_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroProduto.aspx");
        }

        protected void lnkVisualizarProduto_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProdutos.aspx");
        }
    }
}