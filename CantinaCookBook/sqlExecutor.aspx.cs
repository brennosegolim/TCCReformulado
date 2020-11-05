using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook
{
    public partial class sqlExecutor : System.Web.UI.Page
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

                msgEsconder();

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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable dt = null;

                string sql = txtSql.Value;

                dt = con.getSelect(sql);

                if(dt != null && dt.Rows.Count > 0)
                {
                    grdResultado.Visible = true;
                    grdResultado.DataSource = dt;
                    grdResultado.DataBind();

                } else
                {

                    throw new Exception("A consulta não trouxe retorno.");

                }

            }
            catch (Exception err)
            {
                string msgErro = err.ToString()
                                    .Replace("\n", "")
                                    .Replace("\r", "");

                msgAlerta(msgErro);

            }

        }

        protected void btnExecutar_Click(object sender, EventArgs e)
        {
            try 
            {

                string sql = txtSql.Value;
                int linhas = 0;

                linhas = con.executeSelect(sql);

                msgSucesso("Quantidade de linhas afetadas: " + linhas.ToString());

            } 
            catch(Exception err)
            {
                string msgErro = err.ToString()
                                    .Replace("\n", "")
                                    .Replace("\r", "");

                msgAlerta(msgErro);

            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtSql.Value = "";
            grdResultado.DataSource = null;
            grdResultado.DataBind();
            grdResultado.Visible = false;

            msgEsconder();
        }
    }
}