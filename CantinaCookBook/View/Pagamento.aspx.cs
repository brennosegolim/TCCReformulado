using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class Pagamento : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
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

        protected void btnConfirmarCliente_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarCliente_Click(object sender, EventArgs e)
        {

        }
    }
}