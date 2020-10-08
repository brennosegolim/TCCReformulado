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
    public partial class ListaUsuarios : System.Web.UI.Page
    {

        CantinaCon con = new CantinaCon();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                atualizarGrid();

                Session.Remove("IdCliente");
                Session.Remove("Metodo");
            
            }

        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Session.Add("IdCliente",idCliente);
            Session.Add("Metodo","alterar");

            Response.Redirect("CadastroUsuario.aspx");

        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {

            try
            {

                AcessoCon ac = new AcessoCon();
                ClienteCon cc = new ClienteCon();
                DataTable dt = null;

                int verifica = 0;
                int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);
                string sql = "";

                sql = " SELECT COUNT(IdVenda) as Qtd FROM Venda WHERE IdCliente = " + idCliente.ToString();

                 dt = con.getSelect(sql);

                if (dt != null)
                {

                    int.TryParse(dt.Rows[0]["Qtd"].ToString(), out verifica);

                }

                if (verifica <= 0)
                {

                    sql = " DELETE FROM Acesso WHERE IdCliente = " + idCliente.ToString();

                    con.executeSelect(sql);
                    
                    cc.DeletarCliente(idCliente);

                    atualizarGrid();

                } else
                {

                    Response.Write("<script>alert('Não foi possível deletar esse usuário. foram encontradas vendas registradas para o mesmo.!');</script>");

                }

            } catch (Exception err)
            {

                string msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                Response.Write("<script>alert('Não foi possível deletar esse usuário. Tente novamente mais tarde!');</script>");
                Response.Write("<script>Console.Log('" + msgErro + "');</script>");

            }

        }

        private void atualizarGrid()
        {

            DataTable dt = null;

            string sql = " SELECT CL.IdCliente,                                        "
                       + "        CL.Nome,                                             "
                       + "        CL.Email,                                            "
                       + "        AC.[Login],                                          "
                       + "        DATEDIFF(YEAR,CL.DataNascimento,GETDATE()) as Idade, "
                       + "        CL.Celular,                                          "
                       + "        CL.Telefone,                                         "
                       + "        CASE WHEN AC.Nivel = 'A' THEN 'Administrador'        "
                       + "                                 ELSE 'Usuário'              "
                       + "                                  END as Nivel               "
                       + "   FROM Cliente CL                                           "
                       + "  INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente        "
                       + "  ORDER BY Nome                                              ";

            dt = con.getSelect(sql);

            if (dt != null)
            {

                grdUsuarios.DataSource = dt;
                grdUsuarios.DataBind();

            }

        }

        protected void btnVincular_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int idade = 0;
            string sql = "";

            sql = " SELECT DATEDIFF(YEAR,DataNascimento,GETDATE()) as Idade "
                + "   FROM Cliente                                          "
                + "  WHERE IdCliente = " + idCliente.ToString();

            dt = con.getSelect(sql);

            if (dt != null)
            {

                int.TryParse(dt.Rows[0]["Idade"].ToString(), out idade);

            }

            if(idade < 18)
            {
            
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Atenção, esse cliente é menor de idade, porntanto não poderá possuir dependentes.')", true);
            
            } else
            {

                Session.Add("ClienteResponsavel",idCliente);
                Response.Redirect("VincularClientes.aspx");

            }

        }

    }
}