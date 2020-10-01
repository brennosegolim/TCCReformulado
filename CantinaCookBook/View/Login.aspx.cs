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
    public partial class Login : System.Web.UI.Page
    {

        CantinaCommons cc = new CantinaCommons();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                dvPanels.Visible = false;

                try
                {

                    if ( Session["usr"] != null )
                    {

                        if ( ! cc.isEmpty(Session["usr"].ToString()))
                        {

                            Response.Redirect("UserHome.aspx");

                        }

                    }
                
                } 
                catch (Exception err)
                {

                    string msgErro = err.ToString();

                    msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                    alerta("Ocorreu um problema :" + msgErro);

                }

            }

        }


        protected void btnAcesso_Click(object sender, EventArgs e)
        {

            CantinaCon con = new CantinaCon();
            AcessoCon acesso = new AcessoCon();
            DataTable dt = new DataTable();

            string login = txtLogin.Value;
            string senha = txtSenha.Value;
            string sql = "";

            if(! cc.isEmpty(login) && !cc.isEmpty(senha))
            {

                bool sucesso = true;

                senha = cc.RetornarMD5(senha);

                sucesso = acesso.loginAcesso(login, senha);

                if (sucesso)
                {

                    sql = " SELECT TOP 1 AC.IdAcesso,                             "
                        + "        CL.IdCliente,                                  "
                        + "        AC.[Login],                                    "
                        + "        CL.Nome,                                       "
                        + "        CL.Autenticado,                                "
                        + "        AC.Nivel                                       " 
                        + "   FROM Acesso AC                                      "
                        + "  INNER JOIN Cliente CL ON CL.IdCliente = AC.IdCliente "
                        + "  WHERE Login = '" + login + "'                        ";

                    dt = con.getSelect(sql);

                    if ( dt != null && dt.Rows.Count > 0)
                    {

                        string idAcesso = "";
                        string user = "";
                        string nome = "";
                        string nivel = "";
                        string idCliente = "";
                        bool autenticado = false;

                        idAcesso = dt.Rows[0]["IdAcesso"].ToString();
                        user = dt.Rows[0]["Login"].ToString();
                        nome = dt.Rows[0]["Nome"].ToString();
                        autenticado = bool.Parse(dt.Rows[0]["Autenticado"].ToString());
                        nivel = dt.Rows[0]["Nivel"].ToString();
                        idCliente = dt.Rows[0]["IdCliente"].ToString();

                        if (autenticado)
                        {

                            Session.Add("IdAcesso", idAcesso);
                            Session.Add("usr", user);
                            Session.Add("Nome", nome);
                            Session.Add("Nivel", nivel);
                            Session.Add("IdCliente", idCliente);

                            Session.Timeout = 30;

                            Response.Redirect("UserHome.aspx");

                        } else
                        {

                            alerta("Atenção ! Seu usuário ainda não foi autenticado,tente novamente mais tarde ou entre em contato com o suporte.");

                        }

                    }
                    else
                    {

                        alerta("Ocorreu um problema ao realizar o login, tente novamente mais tarde.");

                    }

                } else
                {

                    alerta("Usuário ou senha estão incorretos !");

                }

            } else
            {

                alerta("Prrencha todos os campos.");

            }

        }

        protected void lnkCadastrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["usr"] == null)
                {

                    Response.Redirect("RegistrarUsuario.aspx");

                }

            }
            catch (Exception err)
            {

                string msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                alerta(msgErro);

            }
        }

        protected void lnkRecuperarSenha_Click(object sender, EventArgs e)
        {

        }

        protected void alerta(string mensagem)
        {

            dvAlerta.InnerText = mensagem;
            dvPanels.Visible = true;
            dvAlerta.Visible = true;
            dvSucesso.Visible = false;

        }

    }
}