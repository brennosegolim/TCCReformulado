using System;
using CantinaCookBook.Scripts;

namespace CantinaTCC
{
    public partial class paginaTeste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTestar_Click(object sender, EventArgs e)
        {

            string texto = txtTexto.Value;

            CantinaCommons cc = new CantinaCommons();

            lblResultado.Text = cc.RetornarMD5(texto);

        }

        protected void btnComparar_Click(object sender, EventArgs e)
        {

            string texto1 = txtTexto.Value;
            string texto2 = txtTexto2.Value;

            CantinaCommons cc = new CantinaCommons();

            texto1 = cc.RetornarMD5(texto1);
            texto2 = cc.RetornarMD5(texto2);

            lblResultado.Text = texto1.Equals(texto2) ? "Senhas iguais" : "Senhas diferentes";

        }

    }
}