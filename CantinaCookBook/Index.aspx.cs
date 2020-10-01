using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook
{
    public partial class Index : System.Web.UI.Page
    {

        CantinaCommons cc = new CantinaCommons();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (cc.isEmpty(verificaSessao()))
                {

                    acessoCliente.Visible = true;
                    clienteOption.Visible = false;

                } else
                {

                    acessoCliente.Visible = false;
                    clienteOption.Visible = true;
                    usuarioNome.Text += Session["usr"].ToString();

                }
            
            }
        
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void lnkCadastros_Click(object sender, EventArgs e)
        {
            Response.Redirect("View/Login.aspx");
        }

        [WebMethod]
        public string verificaSessao()
        {

            string retorno = "";

            if (Session["usr"] != null) retorno = Session["usr"].ToString();

            return retorno;

        }

    }
}