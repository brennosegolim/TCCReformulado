using CantinaCookBook.Controller;
using CantinaCookBook.Models;
using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CantinaCookBook.View
{
    public partial class CadastroUsuario : System.Web.UI.Page
    {

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

                if (Session["Metodo"] != null && Session["IdCliente"] != null)
                {

                    if (Session["Metodo"].ToString().Equals("alterar") && ! Session["IdCliente"].ToString().Equals(""))
                    {

                        DataTable dt = null;
                        CantinaCon con = new CantinaCon();
                        CantinaCommons commons = new CantinaCommons();

                        string sql = " SELECT CL.Nome AS NomeCliente, "
                                   + "        CONVERT(VARCHAR(10),DataNascimento,103) DataNascimento, "
                                   + "        CL.Telefone,"
                                   + "        CL.Celular,"
                                   + "        CL.Email, "
	                               + "        CL.CPF, "
	                               + "        AC.[Login], "
	                               + "        AC.Nivel "
                                   + "   FROM Cliente CL "
                                   + "  INNER JOIN Acesso AC ON AC.IdCliente = CL.IdCliente "
                                   + "  WHERE CL.IdCliente = " + Session["IdCliente"].ToString();

                        dt = con.getSelect(sql);

                        if(dt != null && dt.Rows.Count > 0)
                        {

                            string nome = dt.Rows[0]["NomeCliente"].ToString();
                            string email = dt.Rows[0]["Email"].ToString();
                            string cpf = dt.Rows[0]["CPF"].ToString();
                            string login = dt.Rows[0]["Login"].ToString();
                            string nivel = dt.Rows[0]["Nivel"].ToString();
                            string dataNascimento = dt.Rows[0]["DataNascimento"].ToString();
                            string telefone = dt.Rows[0]["Telefone"].ToString();
                            string celular = dt.Rows[0]["Celular"].ToString();

                            txtSenha.Disabled = true;
                            btnVerSenha.Disabled = true;
                            btnConfirmar.Value = "Alterar";
                            metodo.Value = "alterar";
                            idCliente.Value = Session["IdCliente"].ToString();

                            txtNome.Value = nome;
                            txtEmail.Value = email;
                            txtCpf.Value = cpf;
                            txtLogin.Value = login;
                            cbnNivelUsuario.Value = nivel;
                            txtDataNascimento.Value = dataNascimento;
                            txtTelefone.Value = telefone;
                            txtCelular.Value = celular;

                        }

                    }

                }

            }

        }

        [WebMethod]
        public static string cadastrarUsuario(string nome,string dataNascimento,string telefone,string celular, string email, string cpf, string usuario, string senha,string nivel,string metodo,string codCliente)
        {

            //Instanciando as Classes de conexão e modelos.
            CantinaCon con = new CantinaCon();
            CantinaCommons commons = new CantinaCommons();
            AcessoCon acessoCon = new AcessoCon();
            ClienteCon clienteCon = new ClienteCon();

            //DataTable para receber a consulta.
            DataTable dt = null;

            //Mensagem de erro.
            string msgErro = "";

            //String para receber consultas a parte.
            string sql = "";

            bool sucesso = true;

            if (!metodo.Equals("alterar"))
            {
                //Bloco protegido para inserir novo Cliente.
                try
                {

                    Cliente cliente = new Cliente();
                    cliente.Nome = nome;
                    cliente.Email = email;
                    cliente.CPF = cpf;
                    cliente.DataNascimento = dataNascimento;
                    cliente.Telefone = telefone;
                    cliente.Celular = celular;

                    sucesso = clienteCon.AdicionarCliente(cliente);

                }
                catch (Exception err)
                {

                    msgErro = err.ToString();

                    msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                }

                if (sucesso)
                {

                    int idCliente = 0;

                    sql = "SELECT TOP 1 IdCliente FROM Cliente ORDER BY IdCliente DESC";

                    dt = con.getSelect(sql);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        string cliente = dt.Rows[0]["IdCliente"].ToString();

                        int.TryParse(cliente, out idCliente);

                    }

                    try
                    {

                        string criptografia = commons.RetornarMD5(senha);

                        Acesso acesso = new Acesso();
                        acesso.IdCliente = idCliente;
                        acesso.Login = usuario;
                        acesso.Nivel = nivel;
                        acesso.Senha = criptografia;

                        acessoCon.AdicionarAcesso(acesso);

                    }
                    catch (Exception err)
                    {

                        msgErro = err.ToString();

                        msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                    }

                }

            } else
            {

                try
                {

                    int idCliente = 0;
                    int.TryParse(codCliente, out idCliente);

                    Cliente cliente = new Cliente();
                    cliente.IdCliente = idCliente;
                    cliente.Nome = nome;
                    cliente.Email = email;
                    cliente.CPF = cpf;
                    cliente.DataNascimento = dataNascimento;
                    cliente.Telefone = telefone;
                    cliente.Celular = celular;

                    clienteCon.AtualizarCliente(cliente);

                    sql = " UPDATE ACESSO  "
                        + "    SET [Login] = '" + usuario + "' ,"
                        + "        Nivel = '" + nivel + "'" 
                        + "  WHERE IdCliente = " + codCliente;

                    con.executeSelect(sql);

                } catch (Exception err)
                {

                    msgErro = err.ToString();

                    msgErro = msgErro.Replace("\n", "").Replace("\r", "");

                }

            }

            return msgErro;

        }

        [WebMethod]
        public static int VerificaEmail(string email)
        {

            //Instanciando as Classes de conexão e modelos.
            CantinaCon con = new CantinaCon();
            AcessoCon acessoCon = new AcessoCon();
            ClienteCon clienteCon = new ClienteCon();


            //String para receber consultas a parte.
            string sql = "";

            //Inteiro para receber total de registros da consulta.
            int quantidade = 0;


            //DataTable para receber a consulta.
            DataTable dt = null;

            sql = " SELECT COUNT(IdCliente) as Qtd "
               + "    FROM Cliente                 "
               + "   WHERE Email = '" + email + "' ";

            dt = con.getSelect(sql);

            if (dt != null && dt.Rows.Count > 0)
            {

                string qtd = dt.Rows[0]["Qtd"].ToString();

                int.TryParse(qtd, out quantidade);

            }

            return quantidade;

        }

        [WebMethod]
        public static int VerificarUsuario(string login)
        {

            //Instanciando as Classes de conexão e modelos.
            CantinaCon con = new CantinaCon();
            AcessoCon acessoCon = new AcessoCon();
            ClienteCon clienteCon = new ClienteCon();


            //String para receber consultas a parte.
            string sql = "";

            //Inteiro para receber total de registros da consulta.
            int quantidade = 0;


            //DataTable para receber a consulta.
            DataTable dt = null;

            sql = " SELECT COUNT(IdAcesso) as Qtd  "
               + "    FROM Acesso                  "
               + "   WHERE Login = '" + login + "' ";

            dt = con.getSelect(sql);

            if (dt != null && dt.Rows.Count > 0)
            {

                string teste = dt.Rows[0]["Qtd"].ToString();

                int.TryParse(teste, out quantidade);

            }

            return quantidade;

        }
    }
}