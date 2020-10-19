using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["usr"] == null)
                {

                    Session.RemoveAll();

                    Response.Redirect("~/Index.aspx");

                }
                else
                {

                    litNome.Text = (Session["Nome"].ToString());

                    if (Session["Nivel"] != null)
                    {

                        if (Session["Nivel"].ToString().Equals("U"))
                        {

                            lnkCadastros.Visible = false;
                            lnkVendas.Visible = false;
                            lnkPagamento.Visible = false;

                        }

                    }

                }

            }

        }

        protected void lnkCadastros_Click(object sender, EventArgs e)
        {

            if (Session["Nivel"] != null)
            {

                if (Session["Nivel"].ToString().Equals("A"))
                {

                    Response.Redirect("Cadastros.aspx");

                }

            }

        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {

            Session.RemoveAll();

            Response.Redirect("~/Index.aspx");

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {

            Response.Redirect("UserHome.aspx");

        }

        protected void lnkVendas_Click(object sender, EventArgs e)
        {

            if (Session["Nivel"] != null)
            {

                if (Session["Nivel"].ToString().Equals("A"))
                {

                    Response.Redirect("CadastroVendas.aspx");

                }

            }

        }

        protected void lnkPagamento_Click(object sender, EventArgs e)
        {

            if (Session["Nivel"] != null)
            {

                if (Session["Nivel"].ToString().Equals("A"))
                {

                    Response.Redirect("Pagamento.aspx");

                }

            }

        }
    }
}