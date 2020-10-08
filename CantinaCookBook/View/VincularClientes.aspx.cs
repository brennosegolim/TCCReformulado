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
    public partial class VincularClientes : System.Web.UI.Page
    {

        CantinaCon con = new CantinaCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {

        }

        private void atualizarGrid()
        {

            DataTable dt = null;

            string sql = "";
            string idCliente = "";

            if (Session["ClienteResponsavel"] != null) {

                idCliente = Session["ClienteResponsavel"].ToString();

                sql = " SELECT IdCliente,                                         " 
                    + "        Nome,                                              "
                    + "        DATEDIFF(YEAR, DataNascimento, GETDATE()) as Idade "
                    + "   FROM Cliente                                            "
                    + "  WHERE IdResponsavel = " + idCliente;

            }

            dt = con.getSelect(sql);

            if(dt != null)
            {

                grdUsuarios.DataSource = dt;
                grdUsuarios.DataBind();

            }

        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("ClienteResponsavel");
            Response.Redirect("ListaUsuarios.aspx");
        }
    }
}