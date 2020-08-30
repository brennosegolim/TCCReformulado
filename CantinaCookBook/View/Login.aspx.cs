using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class Login : System.Web.UI.Page
    {

        CantinaCommons cc = new CantinaCommons();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {

                    if ( Session["user"] != null )
                    {

                        if (cc.isEmpty(Session["user"].ToString()))
                        {

                            Response.Redirect("UserHome.aspx");

                        }

                    }
                
                } 
                catch (Exception err)
                {

                    string msgErro = err.ToString();

                    msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                    Page.Response.Write("<script> console.log('Problemas ao verificar Sessão: " + msgErro + "');</script>");

                }

            }

        }

        protected void btnAcesso_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCadastrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["user"] == null)
                {

                    Response.Redirect("RegistrarUsuario.aspx");

                }

            }
            catch (Exception err)
            {

                string msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                Page.Response.Write("<script> console.log('Problemas ao tentar cadastrar novo usuário - Erro: " + msgErro + "');</script>");

            }
        }

        protected void lnkRecuperarSenha_Click(object sender, EventArgs e)
        {

        }

    }
}