using CantinaCookBook.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CantinaCookBook.Controller;
using CantinaCookBook.Models;
using System.Data;
using System.Web.Services;

namespace CantinaCookBook.View
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string cadastrarUsuario(string nome,string email,string cpf,string usuario,string senha,string confirmar)
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

            //Bloco protegido para inserir novo Cliente.
            try
            {

                Cliente cliente = new Cliente();
                cliente.Nome = nome;
                cliente.Email = email;
                cliente.CPF = cpf;

                sucesso = clienteCon.AdicionarCliente(cliente);

            }
            catch (Exception err)
            {

                msgErro = err.ToString();

                msgErro = msgErro.Replace("\n", "").Replace("\r", "");

            }

            if (sucesso)
            {

                int idCliente  = 0;

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
                    acesso.Nivel = "U";
                    acesso.Senha = criptografia;

                    acessoCon.AdicionarAcesso(acesso);
                
                } catch(Exception err)
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
               + "   FROM Cliente                 "
               + "  WHERE Email = '" + email + "' ";

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

            sql = " SELECT COUNT(IdAcesso) as Qtd "
               + "   FROM Acesso                  "
               + "  WHERE Login = '" + login + "' ";

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