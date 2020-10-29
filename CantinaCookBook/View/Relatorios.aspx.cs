using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class Relatorios : System.Web.UI.Page
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

        protected void lnkRelPendencia_Click(object sender, EventArgs e)
        {

            Session.Add("RelName", "RelPendenciaPeriodo.aspx");

            Response.Redirect("~/Filtros/FiltroPeriodo.aspx");

        }
    }
}