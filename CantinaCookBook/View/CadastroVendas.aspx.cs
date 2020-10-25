using CantinaCookBook.Controller;
using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class CadastroVendas : System.Web.UI.Page
    {

        #region Objetos para produção
        CantinaCon con = new CantinaCon();
        CantinaCommons cc = new CantinaCommons();
        #endregion

        #region Load da página. 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                inicializar();

            }

        }

        #endregion

        #region Métodos produção.

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

        #region Métodos de limpeza
        private void limparCampos()
        {

            txtCliente.Value = "";
            txtCodigo.Value = "";
            txtProduto.Value = "";
            txtQuantidade.Value = "";
            titleLimiteDiario.InnerText = "";
            titleQuantidadeItens.InnerText = "";
            titleValorTotal.InnerText = "";

        }

        private void cancelarVenda(int idVenda)
        {

            VendaCon venda = new VendaCon();
            string sql = "";

            sql = @" DELETE FROM Produto_Venda WHERE IdVenda = " + idVenda.ToString();

            con.executeSelect(sql);

            venda.DeletarVenda(idVenda);

        }

        private void cancelarVenda(string idVenda)
        {

            VendaCon venda = new VendaCon();
            string sql = "";
            int codVenda = 0;

            int.TryParse(idVenda, out codVenda);

            sql = @" DELETE FROM Produto_Venda WHERE IdVenda = " + idVenda;

            con.executeSelect(sql);

            venda.DeletarVenda(codVenda);

        }

        private void limparCache()
        {

            Session.Remove("IdClienteVenda");
            Session.Remove("IdVendaAtual");

        }

        #endregion

        #region Método de inicialização
        private void inicializar()
        {

            msgEsconder();
            btnCancelar.Visible = false;
            btnEncerrarVenda.Visible = false;
            dvCliente.Visible = false;
            dvDetalhamento.Visible = false;
            grdProduto.Visible = false;
            dvTotalizadores.Visible = false;

        }
        #endregion

        #region Método para carregar informações de grids
        //Métodos para carregar as grids
        private void carregarClientes(string nome)
        {

            DataTable dt = null;

            string sql = "";

            if (nome == null) nome = "";

            if(nome != null)
            {

                try
                {

                    sql = @" SELECT TOP 10 CL.IdCliente,
                                    CL.Nome,
                                    CONVERT(VARCHAR(10),CL.DataNascimento,103) as DataNascimento,
                                    CL.CPF
                               FROM Cliente CL
                              INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente
                              WHERE AC.Nivel = 'U'
                                AND CL.Autenticado = 1
                                AND Nome LIKE '%" + nome + "%'";

                    dt = con.getSelect(sql);


                } catch(Exception err)
                {

                    msgAlerta("Não foi possível carregar a lista de clientes, tente novamente mais tarde.");

                }

                grdClientes.DataSource = dt;
                grdClientes.DataBind();

            }

        }

        private void carregarProdutoVenda(string idVenda)
        {

            DataTable dt = null;

            string sql = "";

            if (!cc.isEmpty(idVenda))
            {

                sql = @"SELECT PD.IdProduto,
                           PD.Codigo,
                           PD.Descricao,
                    	   PV.Quantidade,
                    	   PV.Valor
                      FROM Produto_Venda PV
                     INNER JOIN Produto PD ON PD.IdProduto = PV.IdProduto
                     WHERE IdVenda = " + idVenda;

                dt = con.getSelect(sql);

                if (dt != null)
                {

                    grdProdutoVenda.DataSource = dt;
                    grdProdutoVenda.DataBind();

                }

            }
            else
            {

                msgAlerta("Não foi possível carregar a lista de produtos desta venda, tente novamente mais tarde");

            }

        }

        #endregion

        #region Método para retornar e atualizar valores

        private decimal getValorLimite(int idCliente)
        {

            DataTable dt = null;
            ClienteLimiteCon limite = new ClienteLimiteCon();

            string sql = "";
            decimal valorLimite = new decimal(0.0);
            decimal gastoDiario = new decimal(0.0);

            sql = "EXEC getValorLimiteDiario @IdCliente = "+ idCliente.ToString() +", @Data = '"+ DateTime.Now.ToString("yyyy-MM-dd") + "'";

            dt = con.getSelect(sql);

            if (dt != null)
            {

                try
                {

                    decimal.TryParse(dt.Rows[0]["Limite"].ToString(), out valorLimite);

                    if (valorLimite > 0){ 

                        decimal.TryParse(dt.Rows[0]["Gasto"].ToString(), out gastoDiario);

                        valorLimite -= gastoDiario; 

                    }

                } catch
                {
                    //Nada.
                }

            }

            return valorLimite;

        }

        private void adicionarProdutoExistente(int idVenda,int idProduto ,int quantidade)
        {

            //Declarando a classe do tipo DataTable.
            DataTable dt = null;

            //Variáveis usadas no cálculo.
            int quantiadeAtual = 0;
            decimal valorAtual = new decimal(0.0);
            decimal valorProduto = new decimal(0.0);
            string sql = "";

            //Recuperando a quantidade e o valor da tabela produto_venda pois precisamos atualizar.
            sql = @"SELECT Quantidade,
                           Valor
                      FROM Produto_Venda
                     WHERE IdVenda = " + idVenda.ToString() + @"
                       AND IdProduto = " + idProduto.ToString();

            dt = con.getSelect(sql);

            //Verificando se o resultado trouxe algum valor.
            if (dt != null)
            {

                //Convertendo o valores recuperados do banco para decimal.
                decimal.TryParse(dt.Rows[0]["Valor"].ToString(), out valorAtual);
                int.TryParse(dt.Rows[0]["Quantidade"].ToString(),out quantiadeAtual);

                if (quantiadeAtual > 0 && valorAtual > 0)
                {

                    sql = "SELECT Preco FROM Produto WHERE IdProduto = " + idProduto;

                    dt = con.getSelect(sql);

                    if(dt != null)
                    {

                        decimal.TryParse(dt.Rows[0]["Preco"].ToString(),out valorProduto);

                        valorAtual += valorProduto * quantidade;
                        quantiadeAtual += quantidade;

                        sql = @"UPDATE Produto_Venda 
                                   SET Valor = " + valorAtual.ToString("0.00", CultureInfo.InvariantCulture) + "," +
                              "        Quantidade = " + quantiadeAtual.ToString() +
                              "  WHERE IdVenda = " + idVenda.ToString() +
                              "    AND IdProduto = " + idProduto.ToString();

                        con.executeSelect(sql);

                        sql = @"SELECT ValorTotal FROM Venda WHERE IdVenda = " + idVenda.ToString();

                        dt = con.getSelect(sql);

                        if(dt != null)
                        {

                            decimal.TryParse(dt.Rows[0]["ValorTotal"].ToString(),out valorAtual);

                            valorAtual += valorProduto * quantidade;

                            sql = "UPDATE Venda" +
                                "     SET ValorTotal = " + valorAtual.ToString("0.00", CultureInfo.InvariantCulture) +
                                "   WHERE IdVenda = " + idVenda.ToString();

                            con.executeSelect(sql);

                        }

                    }

                }

            }

        }

        private void ExcluirProduto(int IdProduto, int IdVenda)
        {

            DataTable dt = null;

            string sql = "";
            decimal valorProdutoVenda = new decimal(0.0);
            decimal valorVenda = new decimal(0.0);
            decimal novoValorVenda = new decimal(0.0);

            sql = @"SELECT PV.Valor as ValorProdutoVenda,
                           VE.ValorTotal as ValorVenda
                      FROM Produto_Venda PV
                     INNER JOIN Venda VE ON VE.IdVenda = PV.IdVenda
                     WHERE PV.IdVenda = " + IdVenda.ToString() +
                      " AND PV.IdProduto = " + IdProduto.ToString();

            dt = con.getSelect(sql);

            if(dt != null)
            {

                decimal.TryParse(dt.Rows[0]["ValorProdutoVenda"].ToString(),out valorProdutoVenda);
                decimal.TryParse(dt.Rows[0]["ValorVenda"].ToString(), out valorVenda);

                novoValorVenda = (valorVenda - valorProdutoVenda);

                sql = @"UPDATE Venda SET ValorTotal = " + novoValorVenda.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE IdVenda = " + IdVenda.ToString();

                con.executeSelect(sql);

                sql = "DELETE FROM Produto_Venda WHERE IdVenda = " + IdVenda.ToString() + " AND IdProduto = " + IdProduto.ToString();

                con.executeSelect(sql);

            }

        }

        private void carregarTotalizadores(int idVenda, int idCliente)
        {

            DataTable dt = null;
            VendaCon vendaCon = new VendaCon();

            //Variáveis que serão apresentadas.
            string textoLimiteGasto = "";
            string textoQuantidadeItens = "";
            string textoValorTotal = "";

            //Controle interno.
            string sql = "";
            int quantidadeItens = 0;
            decimal valorTotal = new decimal(0.0);

            //Primeiro vou recuperar o limite de gasto.
            textoLimiteGasto = getValorLimite(idCliente).ToString("0.00", CultureInfo.InvariantCulture);

            //Depois vou recuperar a quantidade de itens adicionada.
            sql = "SELECT SUM(Quantidade) as Qtd FROM Produto_Venda WHERE IdVenda = " + idVenda.ToString();

            dt = con.getSelect(sql);

            if(dt != null)
            {

                int.TryParse(dt.Rows[0]["Qtd"].ToString(),out quantidadeItens);

            }

            textoQuantidadeItens = quantidadeItens.ToString();

            //E por fim receber o valor total.

            dt = null; // Resetando a DataTable.

            dt = vendaCon.SelectVenda(idVenda);

            if(dt != null)
            {

                decimal.TryParse(dt.Rows[0]["ValorTotal"].ToString(),out valorTotal);

            }

            textoValorTotal = valorTotal.ToString("0.00", CultureInfo.InvariantCulture);

            //Atualizando os valores na tela.
            titleLimiteDiario.InnerText = textoLimiteGasto;
            titleQuantidadeItens.InnerText = textoQuantidadeItens;
            titleValorTotal.InnerText = textoValorTotal;

            dvTotalizadores.Visible = true;

        }

        #endregion

        #endregion

        #region Ações dos componentes em tela.

        //Método para inicar nova venda
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {

            limparCampos();
            msgEsconder();

            btnCancelar.Visible = true;
            btnAdicionar.Visible = false;
            dvCliente.Visible = true;
            btnConfirmarCliente.Visible = true;
            txtCliente.Disabled = false;

        }

        //Método para cancelar e excluir a venda iniciada.
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {

            string idVenda = "";

            if (Session["IdVendaAtual"] != null)
            {

                idVenda = Session["IdVendaAtual"].ToString();

            }

            limparCampos();
            if( ! idVenda.Equals("")) cancelarVenda(idVenda);
            limparCache();

            btnCancelar.Visible = false;
            btnAdicionar.Visible = true;
            btnEncerrarVenda.Visible = false;
            dvCliente.Visible = false;
            grdProdutoVenda.Visible = false;
            dvDetalhamento.Visible = false;
            dvTotalizadores.Visible = false;

            msgEsconder();

            msgSucesso("Pêndencia Cancelada com sucesso.");

        }

        //Método para pesquisar os clientes do sistema.
        protected void btnConfirmarCliente_Click(object sender, EventArgs e)
        {

            string nome = txtCliente.Value;

            carregarClientes(nome);

            grdClientes.Visible = true;

        }

        //Método para confirma o cliente e incluir definitivamente a venda.
        protected void btnSelecionar_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            ClienteCon cliente = new ClienteCon();
            VendaCon vendaCon = new VendaCon();
            Venda venda = new Venda();

            int idCliente = Convert.ToInt32((sender as LinkButton).CommandArgument);
            bool sequencia = false;
            string sql = "";
            string idVenda = "";

            Session.Add("IdClienteVenda", idCliente);

            grdClientes.Visible = false;
            btnConfirmarCliente.Visible = false;

            dt = cliente.SelectCliente(idCliente);

            if(dt != null)
            {

                string nome = dt.Rows[0]["Nome"].ToString();

                txtCliente.Value = nome;
                txtCliente.Disabled = true;

            }

            dvDetalhamento.Visible = true;

            venda.Data = DateTime.Now.ToString();
            venda.ValorTotal = new decimal(0.0);
            venda.IdCliente = idCliente;
            sequencia = vendaCon.AdicionarVenda(venda);

            if (sequencia)
            {

                sql = @" SELECT TOP 1 IdVenda FROM Venda ORDER BY IdVenda DESC ";

                dt = null;
                dt = con.getSelect(sql);

                if(dt != null)
                {

                    idVenda = dt.Rows[0]["IdVenda"].ToString();

                    Session.Add("IdVendaAtual",idVenda);

                    

                    dvDetalhamento.Visible = true;
                    btnEncerrarVenda.Visible = true;

                    carregarTotalizadores(int.Parse(idVenda), idCliente);

                }

            } else
            {

                msgAlerta("Não foi possível adicionar a pêndencia. Tente novamente mais tarde.");

            }

        }

        //Método para pesquisar os produtos.
        protected void btnPesquisarProduto_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            string sql = "";
            string codigo = "";
            string descricao = "";

            codigo = txtCodigo.Value;
            descricao = txtProduto.Value;

            if (codigo.Equals("")) codigo = descricao;
            if (descricao.Equals("")) descricao = codigo;

            sql = " SELECT TOP 10 * " 
                + "   FROM Produto  "
                + "  WHERE Descricao LIKE '%' + '"+ descricao + "' + '%' "
                + "     OR Codigo = '"+ codigo.Trim() +"' ";

            dt = con.getSelect(sql);

            if(dt != null)
            {

                grdProduto.Visible = true;
                grdProdutoVenda.Visible = false;
                grdProduto.DataSource = dt;
                grdProduto.DataBind();

            }

            btnCancelarPesquisa.Visible = true;

        }

        //Método para cancelar a pesquisa do produto.
        protected void btnCancelarPesquisa_Click(object sender, EventArgs e)
        {

            string idVenda = "";
            btnCancelarPesquisa.Visible = false;

            try {

                idVenda = Session["IdVendaAtual"].ToString();

            }
            catch {
            
                //Nada.

            }

            grdProduto.Visible = false;
            carregarProdutoVenda(idVenda);
            grdProdutoVenda.Visible = true;

        }

        //Método para adicionar o produto à venda.
        protected void btnSelecionarProduto_Click(object sender, EventArgs e)
        {

            //Instanciando classes necessárias.
            DataTable dt = null;
            VendaCon vendaCon = new VendaCon();
            ProdutoCon produtoCon = new ProdutoCon();

            //Variáveis de controle.
            int quantidade = 0;
            int idCliente = 0;
            int idProduto = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int produtoExistente = 0;
            decimal precoProduto = new decimal(0.0);
            decimal valorTotal = new decimal(0.0);
            decimal valorLimite = new decimal(0.0);
            string idVenda = Session["IdVendaAtual"].ToString();
            string sql = "";

            //Recebendo e convertendo valores.
            int.TryParse(txtQuantidade.Value, out quantidade);
            int.TryParse(Session["IdClienteVenda"].ToString(),out idCliente);

            //Verificando se o Id da venda está cadastrado.
            if( ! cc.isEmpty(idVenda))
            {

                //Se não for informada a quantidade ela será 1.
                if (quantidade == 0) quantidade = 1;
                int id = 0;

                int.TryParse(idVenda, out id);

                dt = vendaCon.SelectVenda(id);

                //Verificando o valor total contido na venda.
                if (dt != null)
                {

                    string valorTemp = dt.Rows[0]["ValorTotal"].ToString();

                    decimal.TryParse(valorTemp, out valorTotal);

                }

                //Recebendo o valor do produto.
                dt = produtoCon.SelectProduto(idProduto);

                if (dt != null)
                {

                    string precoTemp = dt.Rows[0]["Preco"].ToString();

                    decimal.TryParse(precoTemp, out precoProduto);

                }

                //Recebendo o valor total do produto multiplicado pela quantidade.
                decimal valorIntermed = precoProduto * quantidade;

                //Recebendo o valor máximo do gasto do aluno diáriamente.
                valorLimite = getValorLimite(idCliente);

                //Verificando se o valor execedeu o limite diário imposto pelo responsável(O sistema não irá travar, apenas avisar se o valor for maior).
                if (valorLimite > 0 && valorLimite < (valorIntermed + valorTotal)) msgAlerta("Atenção ! essa compra excedeu o limite de gastos do aluno.");

                //Verificando se o produto já existe na relação
                sql = @"SELECT COUNT(IdProduto) as Qtd
                          FROM Produto_Venda
                         WHERE IdVenda = " + idVenda +
                         " AND IdProduto = " + idProduto.ToString();

                dt = null;

                dt = con.getSelect(sql);

                if(dt != null)
                {

                    int.TryParse(dt.Rows[0]["Qtd"].ToString(),out produtoExistente);

                }

                if (produtoExistente <= 0)
                {

                    //comando para inserir os valores na tabela de produto_venda
                    sql = @"INSERT INTO Produto_Venda(IdProduto,IdVenda,Valor,Data,Quantidade)
                    VALUES (" + idProduto.ToString() + "," + idVenda + "," + valorIntermed.ToString("0.00", CultureInfo.InvariantCulture) + ",GETDATE()," + quantidade.ToString() + ")";

                    //Executando o comando.
                    con.executeSelect(sql);

                } else
                {

                    adicionarProdutoExistente(int.Parse(idVenda), idProduto, quantidade);

                }

                //adição do valor ao valor total da venda.
                valorTotal += valorIntermed;

                //Comando para atualizar o valor total da venda.
                sql = @"UPDATE Venda SET ValorTotal = " + valorTotal.ToString("0.00", CultureInfo.InvariantCulture) + "WHERE IdVenda = " + idVenda;

                //Executando atualização.
                con.executeSelect(sql);

                //Limpando os campos.
                txtCodigo.Value = "";
                txtProduto.Value = "";
                txtQuantidade.Value = "";

                //Escondendo componentes e mostrando os resultados da operação anterior.
                grdProduto.Visible = false;

                carregarProdutoVenda(idVenda);

                grdProdutoVenda.Visible = true;

                carregarTotalizadores(int.Parse(idVenda), idCliente);

                btnCancelarPesquisa.Visible = false;

            } else
            {

                msgAlerta("Não foi possível adicionar o produto à venda, tente novamente mais tarde !");

            }

        }
        
        //Método para excluir o produto da venda.
        protected void btnExcluirProduto_Click(object sender, EventArgs e)
        {

            int idProduto = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int idCliente = 0;
            int idVenda = 0;

            if (Session["IdVendaAtual"] != null)
            {

                int.TryParse(Session["IdVendaAtual"].ToString(), out idVenda);
                int.TryParse(Session["IdClienteVenda"].ToString(),out idCliente);

                try
                {

                    ExcluirProduto(idProduto, idVenda);

                    carregarProdutoVenda(idVenda.ToString());

                    carregarTotalizadores(idVenda, idCliente);

                }
                catch (Exception err)
                {

                    msgAlerta("Não foi possível remover o produto ! tente novamente mais tarde.");

                }

            }

        }
        
        //Método para encerrar a venda.
        protected void btnEncerrarVenda_Click(object sender, ImageClickEventArgs e)
        {

            limparCampos();
            limparCache();

            btnCancelar.Visible = false;
            btnAdicionar.Visible = true;
            dvCliente.Visible = false;
            grdProdutoVenda.Visible = false;
            dvDetalhamento.Visible = false;
            dvTotalizadores.Visible = false;
            btnEncerrarVenda.Visible = false;

            msgSucesso("Pêndencia encerrada e salva com sucesso !");

        }

        #endregion

    }
}