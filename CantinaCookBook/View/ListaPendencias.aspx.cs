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
    public partial class ListaPendencias : System.Web.UI.Page
    {

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

                }
                else
                {

                    Session.RemoveAll();

                    Response.Redirect("~/Index.aspx");

                }

                msgEsconder();
                dvSelect.Visible = true;
                dvConsultaNome.Visible = false;
                dvConsultaData.Visible = false;
                dvGrid.Visible = false;

                limparCampos();

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            string nome = txtNome.Value;
            string data = txtData.Value;
            bool sucesso = true;
            DateTime dataT = new DateTime();

            if (!data.Equals("")) 
            {
                sucesso = (DateTime.TryParse(data, out dataT));
            }

            if (sucesso)
            {
                carregarGridPendencias(nome, data);

                dvGrid.Visible = true;
                txtNome.Disabled = true;
                txtData.Disabled = true;

                btnBuscarData.Visible = false;
                btnBuscarNome.Visible = false;
            }
            else
            {
                msgAlerta("Informe uma data válida.");
            }

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            
            int idVenda = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Session.Add("ListaIdVenda", idVenda);

            Response.Write("<script> window.open('ListaProdutoVenda.aspx'); </script>");

            msgSucesso("Visualização completa, se nada acontecer habilite os pop-ups para a página.");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            limparCampos();

            dvSelect.Visible = true;
            dvConsultaNome.Visible = false;
            dvConsultaData.Visible = false;
            dvGrid.Visible = false;
            txtNome.Disabled = false;
            txtData.Disabled = false;
            btnBuscarData.Visible = true;
            btnBuscarNome.Visible = true;

        }

        private void carregarGridPendencias(string nome,string data)
        {

            DataTable dt = null;

            string sql = "";

            if (nome.Equals("")) nome = "0";
            if (data.Equals("")) data = "0";

            sql = @"SELECT VE.IdVenda,
                           CONVERT(VARCHAR(10),VE.[Data],103) as [Data],
   	                       VE.ValorTotal,
   	                       CL.Nome
                      FROM Venda VE 
                     INNER JOIN Cliente CL ON CL.IdCliente = VE.IdCliente 
                     WHERE VE.ValorTotal > 0
                       AND ((CAST(Data as DATE) = '"+ data + "') OR ('0' = '" + data + "')) " + @"
                       AND ((CL.Nome LIKE '%" + nome + "%') OR ('0' = '" + nome + "'))" + @"
                     ORDER BY CAST(VE.[Data] as DATE) DESC,
                           CAST(VE.[Data] as TIME) DESC";

            dt = con.getSelect(sql);

            if(dt != null)
            {

                grdPendencia.DataSource = dt;
                grdPendencia.DataBind();

            }

        }

        private void limparCampos()
        {
            cbxSelect.SelectedValue = "0";
            txtNome.Value = "";
            txtData.Value = "";
        }

        #region Métodos de aviso.
        private void msgAlerta(string mensagem)
        {

            dvAlerta.InnerText = mensagem;
            dvPanels.Visible = true;
            dvAlerta.Visible = true;
            dvSucesso.Visible = false;

        }

        private void msgSucesso(string mensagem)
        {

            dvSucesso.InnerText = mensagem;
            dvPanels.Visible = true;
            dvSucesso.Visible = true;
            dvAlerta.Visible = false;

        }

        private void msgEsconder()
        {

            dvPanels.Visible = false;
            dvAlerta.Visible = false;
            dvSucesso.Visible = false;

            dvAlerta.InnerText = "";
            dvSucesso.InnerText = "";

        }

        #endregion

        protected void cbxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSelect.SelectedValue.Equals("1"))
            {
                dvSelect.Visible = false;
                dvConsultaNome.Visible = true;
            }

            if (cbxSelect.SelectedValue.Equals("2"))
            {
                dvSelect.Visible = false;
                dvConsultaData.Visible = true;

                DateTime dataAtual = DateTime.Now;

                txtData.Value = dataAtual.ToString("dd. MM. yyyy");

            }

        }

        protected void grdPendencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPendencia.PageIndex = e.NewPageIndex;
            carregarGridPendencias(txtNome.Value, txtData.Value);
        }

    }
}