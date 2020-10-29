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

        CantinaCommons cc = new CantinaCommons();
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

                } else
                {

                    Session.RemoveAll();

                    Response.Redirect("~/Index.aspx");

                }

                int idCliente = 0;

                if (Session["IdAutenticar"] != null)
                {

                    DataTable dt = null;
                    ClienteCon clienteCon = new ClienteCon();

                    int.TryParse(Session["IdAutenticar"].ToString(), out idCliente);

                    string nome = "";
                    string dataNascimento = "";
                    string cpf = "";
                    string telefone = "";
                    string celular = "";
                    string email = "";

                    dt = clienteCon.SelectCliente(idCliente);

                    if(dt != null && dt.Rows.Count > 0)
                    {

                        nome = dt.Rows[0]["Nome"].ToString();
                        dataNascimento = dt.Rows[0]["DataNascimento"].ToString();
                        cpf = dt.Rows[0]["Cpf"].ToString();
                        telefone = dt.Rows[0]["Telefone"].ToString();
                        celular = dt.Rows[0]["Celular"].ToString();
                        email = dt.Rows[0]["Email"].ToString();

                        dataNascimento = dataNascimento.Substring(0, 10);

                        titleNome.InnerText = nome;
                        titleDataNascimento.InnerText = dataNascimento;
                        titleCpf.InnerText = cpf;
                        titleTelefone.InnerText = telefone;
                        titleCelular.InnerText = celular;
                        titleEmail.InnerText = email;

                    }

                } else
                {

                    Response.Redirect("UserHome.aspx");

                }

            }

        }

        protected void btnAutenticar_Click(object sender, EventArgs e)
        {

            int idAutenticar = 0;
            string sql = "";
            int linhas = 0;

            if (Session["IdAutenticar"] != null)
            {

                int.TryParse(Session["IdAutenticar"].ToString(), out idAutenticar);

                sql = " UPDATE Cliente        " 
                    + "    SET Autenticado = 1"
                    + "  WHERE IdCliente = " + idAutenticar.ToString();

                linhas = con.executeSelect(sql);

                if (linhas > 0)
                {

                    Response.Redirect("UserHome.aspx");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cliente Autenticado com sucesso.')", true);

                } else
                {

                    Response.Redirect("UserHome.aspx");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Atenção !!! Não foi possível autenticar o usuário, tente novamente mais tarde.')", true);

                }

            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

            Response.Redirect("UserHome.aspx");
        
        }
    }
}