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

                dvPesquisa.Visible = false;

                atualizarGrid();

            }

        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string sql = "";

            sql = " UPDATE Cliente              "
                + "    SET IdResponsavel = NULL "
                + "  WHERE IdCliente = " + idCliente.ToString();

            con.executeSelect(sql);

            atualizarGrid();

            atualizarPesquisa();

        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {

            string sql = "";
            string idResponsavel = "";
            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (Session["ClienteResponsavel"] != null)
            {

                idResponsavel = Session["ClienteResponsavel"].ToString();

                sql = " UPDATE Cliente "
                    + "    SET IdResponsavel = " + idResponsavel
                    + "  WHERE IdCliente = " + idCliente.ToString();

                con.executeSelect(sql);

                atualizarPesquisa();
                atualizarGrid();
            
            }
        
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("ClienteResponsavel");
            Response.Redirect("ListaUsuarios.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            dvPesquisa.Visible = true;

            atualizarPesquisa();

        }

        private void atualizarGrid()
        {

            DataTable dt = null;

            string sql = "";
            string idCliente = "";

            if (Session["ClienteResponsavel"] != null)
            {

                idCliente = Session["ClienteResponsavel"].ToString();

                sql = " SELECT IdCliente,                                         "
                    + "        Nome,                                              "
                    + "        DATEDIFF(YEAR, DataNascimento, GETDATE()) as Idade "
                    + "   FROM Cliente                                            "
                    + "  WHERE IdResponsavel = " + idCliente;

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    grdUsuarios.DataSource = dt;
                    grdUsuarios.DataBind();

                }

            }

        }

        private void atualizarPesquisa()
        {

            DataTable dt = null;

            string sql = "";
            string nome = "";

            nome = txtNome.Value;

            sql = @" SELECT TOP 5 CL.IdCliente,                                   
	                        CL.Nome,                                             
                            DATEDIFF(YEAR, CL.DataNascimento, GETDATE()) as Idade
                     FROM Cliente CL                                            
                          INNER JOIN Acesso AC 
                     	  ON AC.IdCliente = CL.IdCliente         
                     WHERE AC.Nivel = 'U'                                        
                       AND CL.IdCliente <> ISNULL(CL.IdResponsavel,0) 
                       AND NOT EXISTS( SELECT * 
                                       FROM Cliente CI 
                     				  WHERE CI.IdResponsavel = CL.IdCliente )
                       AND (CL.IdResponsavel IS NULL OR CL.IdResponsavel = 0)     
                       AND Nome LIKE '%' + '" + nome +"' + '%'  ";

            dt = con.getSelect(sql);

            if (dt != null)
            {

                grdPesquisa.DataSource = dt;
                grdPesquisa.DataBind();

            }

        }

    }
}