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

                if (Session["Nome"] == null)
                {

                    Session.RemoveAll();

                    Response.Redirect("~/Index.aspx");

                }
                else
                {

                    litNome.Text = "Bem Vindo " + (Session["Nome"].ToString());

                }

            }

        }

    }
}