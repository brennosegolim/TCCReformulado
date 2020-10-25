using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.Relatórios
{
    public partial class RelFolhaPagamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string nomeAluno = Session["RelNome"].ToString();
                string valorPendente = Session["RelValor"].ToString();
                string dataInicial = "01/07/2020";
                string dataFinal = "05/12/2020";
                string dia = "21";
                string mes = "Outubro";
                string ano = "2020";

                string texto = "Reiteramos nosso desejo de sempre oferecer os melhores produtos e serviços aos alunos e clientes, os quais é sempre uma honra e prazer atendê-los. Para tanto, " 
                             + " necessitamos que os débitos do período sejam quitados e gostáriamos de informar que existe débito de consumo do(a) aluno(a): <b>" + nomeAluno + "</b> , no valor de <b> R$ "
                             + valorPendente + "</b>, referentes ao período de <b>" + dataInicial + "</b> a <b>" + dataFinal + "</b>, o qual solicitamos a imediata quitação.</p> <p style=\"text-align:justify;\">"
                             + "Agradecemos sua compreensão e estamos disponíveis para quaisquer esclarecimentos que se fizerem necessário.";

                string data = "Tupã," + dia + " de " + mes + " de " + ano;

                corpoTexto.InnerHtml = texto;
                dataTexto.InnerHtml = data;

            }

        }
    }
}