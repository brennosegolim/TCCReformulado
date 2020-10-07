using CantinaCookBook.Controller;
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
    public partial class AutenticarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CantinaCommons cc = new CantinaCommons();
            CantinaCon con = new CantinaCon();

            if (!IsPostBack)
            {

                int idCliente = 0;

                if (Session["IdAutenticar"] != null)
                {

                    DataTable dt = null;
                    ClienteCon clienteCon = new ClienteCon();

                    int.TryParse(Session["IdAutenticar"].ToString(), out idCliente);

                    string nome = "";
                    string cpf = "";
                    string email = "";

                    dt = clienteCon.SelectCliente(idCliente);

                    if(dt != null && dt.Rows.Count > 0)
                    {

                        nome = dt.Rows[0]["Nome"].ToString();
                        cpf = dt.Rows[0]["Cpf"].ToString();
                        email = dt.Rows[0]["Email"].ToString();

                        titleNome.InnerText = nome;
                        titleCpf.InnerText = cpf;
                        titleEmail.InnerText = email;

                    }

                } else
                {

                    Response.Redirect("UserHome.aspx");

                }

            }

        }

    }
}