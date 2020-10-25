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

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            
            string dataInicial = txtDataInicial.Value;
            string dataFinal = txtDataFinal.Value;

            Session.Add("RelDataInicial",dataInicial);
            Session.Add("RelDataFinal",dataFinal);

        }
    }
}