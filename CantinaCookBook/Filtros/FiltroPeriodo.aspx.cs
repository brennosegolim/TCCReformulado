using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.Filtros
{
    public partial class FiltroPeriodo : System.Web.UI.Page
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

                            Response.Redirect("~/View/UserHome.aspx");

                        } else
                        {

                            DateTime dataAtual = DateTime.Now;

                            string dataInicial = "01" + DateTime.Now.ToString("/MM/yyyy");
                            string dataFinal = dataAtual.ToString("dd/MM/yyyy");

                            txtDataInicial.Value = dataInicial;
                            txtDataFinal.Value = dataFinal;

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

        protected void lnkRelatorios_Click(object sender, EventArgs e)
        {
            if (Session["Nivel"] != null)
            {

                if (Session["Nivel"].ToString().Equals("A"))
                {

                    Response.Redirect("Relatorios.aspx");

                }

            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

            if (Session["RelName"] != null)
            {

                string dataInicial = txtDataInicial.Value;
                string dataFinal = txtDataFinal.Value;

                Session.Add("RelDataInicial", dataInicial);
                Session.Add("RelDataFinal", dataFinal);

                Response.Redirect("~/Relatórios/" + Session["RelName"].ToString());

            } else
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('Não foi possível imprimir o relatório! Tente novamente mais tarde.')", true);

            }

        }
    }
}