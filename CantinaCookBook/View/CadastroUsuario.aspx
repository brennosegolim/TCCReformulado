<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="CantinaCookBook.View.CadastroUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row card" style="padding:25px;">
            <div class="row">
                <div class="col s8 offset-s2" runat="server" id="dvPanels">
                    <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                    <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
                </div>
            </div>
            <h4 class="header">CADASTRO DE USUÁRIOS</h4>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">account_circle</i>
                    <input id="txtNome" type="text" class="validate" placeholder="Nome Completo" maxlength="100" runat="server">
                </div>
            </div>
            <div class="row">
                <div class="input-field col s2">
                    <i class="material-icons prefix">date_range</i>
                    <input id="txtDataNascimento" type="text" class="validate date" placeholder="Data de Nascimento" maxlength="10" runat="server">
                </div>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">featured_play_list</i>
                    <input id="txtCpf" type="text" class="validate" placeholder="CPF" maxlength="11" runat="server">
                </div>
            </div>
            <div class="row">
                <h4 class="header">INFORMAÇÕES DE CONTATO</h4>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">email</i>
                    <input id="txtEmail" type="text" class="validate" placeholder="Email" maxlength="70" runat="server" onblur="verificaEmail()">
                </div>
            </div>
            <div class="row">
                <div class="input-field col s3">
                    <i class="material-icons prefix">local_phone</i>
                    <input id="txtTelefone" type="text" class="validate phone_with_ddd" placeholder="Telefone" maxlength="20" runat="server">
                </div>
            </div>
            <div class="row">
                <div class="input-field col s3">
                    <i class="material-icons prefix">smartphone</i>
                    <input id="txtCelular" type="text" class="validate cellphone" placeholder="Celular" maxlength="20" runat="server">
                </div>
            </div>
            <div class="row">
                <h4 class="header">INFORMAÇÕES DE CONTATO</h4>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">person</i>
                    <input id="txtLogin" type="text" class="validate" placeholder="Login" maxlength="30" runat="server" onblur="verificaUsuario()">
                </div>
            </div>
            <div class="row">
                <div class="input-field col s6" style="display:flex;">
                    <i class="material-icons prefix">security</i>
                    <input id="txtSenha" type="password" class="validate" placeholder="Senha" maxlength="30" runat="server">
                    <input style="margin-left: 7px;" class="btn waves-effect waves-light waves-light btn" id="btnVerSenha" value="ver" type="button" onclick="verSenha()" runat="server"/>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">security</i>
                    <select id="cbnNivelUsuario" runat="server">
                        <option value="A">Administrador</option>
                        <option value="U">Usuário</option>
                    </select>
                    <label>Nível de Usuário</label>
                </div>
            </div>
            <div class="row">
                <input id="btnConfirmar" class="waves-effect waves-light btn" type="button" onclick="cadastrarUsuario()" value="Confirmar" runat="server">
                <input id="btnCancelar" class="waves-effect waves-light btn" type="button" onclick="cancelar()" value="Cancelar">
            </div>
            <div>
                <input id="metodo" style="display:none;" runat="server" />
                <input id="idCliente" style="display:none;" runat="server" />
            </div>
            <br/>
            <br/>
            <br/>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            $('select').formSelect();
        });

        $(document).ready(function () {

            $("#ContentPlaceHolder1_dvSucesso").hide();
            $("#ContentPlaceHolder1_dvAlerta").hide();

        });

        function verSenha() {

            $("#ContentPlaceHolder1_txtSenha").attr("type", "text");

            setInterval(function () {

                $("#ContentPlaceHolder1_txtSenha").attr("type", "password");

            }, 5000);

        }

        function cadastrarUsuario() {

            let nome = $("#ContentPlaceHolder1_txtNome").val();
            let email = $("#ContentPlaceHolder1_txtEmail").val();
            let cpf = $("#ContentPlaceHolder1_txtCpf").val();
            let usuario = $("#ContentPlaceHolder1_txtLogin").val();
            let senha = $("#ContentPlaceHolder1_txtSenha").val();
            let nivel = $("#ContentPlaceHolder1_cbnNivelUsuario").val();
            let metodo = $("#ContentPlaceHolder1_metodo").val();
            let idCliente = $("#ContentPlaceHolder1_idCliente").val();
            let dataNascimento = $("#ContentPlaceHolder1_txtDataNascimento").val();
            let telefone = $("#ContentPlaceHolder1_txtTelefone").val();
            let celular = $("#ContentPlaceHolder1_txtCelular").val();
            let mensagem = "";


            if (nome == "") mensagem += "Atenção !!! informe o campo Nome Completo.\n";
            if (dataNascimento == "") mensagem = "Atenção !!! informe o campo Data de Nascimento";
            if (cpf == "") mensagem += "Atenção !!! informe o campo do CPF.\n";
            if (email == "") mensagem += "Atenção !!! informe o campo do Email.\n";
            if (telefone == "") mensagem += "Atenção !!! informe o campo Telefone.\n";
            if (celular == "") mensagem += "Atenção !!! informe o campo do Celular.\n";
            if (usuario == "") mensagem += "Atenção !!! informe o campo do Usuário.\n";
            if (senha == "" && metodo != "alterar") mensagem += "Atenção !!! informe o campo Senha.\n";

            if (mensagem == "") {

                $.ajax({

                    type: "POST",
                    url: "CadastroUsuario.aspx/cadastrarUsuario",
                    data: "{   nome: '" + nome +
                          "', dataNascimento: '" + dataNascimento +
                          "', telefone: '" + telefone +
                          "', celular: '" + celular +
                          "', email: '" + email +
                          "',   cpf: '" + cpf +
                          "', usuario: '" + usuario +
                          "', senha: '" + senha +
                          "', nivel: '" + nivel +
                          "', metodo: '" + metodo +
                          "', codCliente: '" + idCliente +
                          "'}",
                    contentType: "application/json; charset=utf8",
                    dataType: "json",
                    success: function (resposta) {

                        if (resposta.d != "") {

                            alert(resposta.d);

                        } else {

                            if (metodo != "alterar") {

                                $("#ContentPlaceHolder1_dvSucesso").text("Usuário cadastrado com sucesso.");
                                $("#ContentPlaceHolder1_dvSucesso").show();

                            } else {

                                sessionStorage.removeItem('IdCliente');
                                sessionStorage.removeItem('Metodo');
                                window.history.back();
                                alert("Usuário alterado com sucesso.");

                            }

                            limparCampos();

                            setInterval(function () {

                                $("#ContentPlaceHolder1_dvSucesso").text("");
                                $("#ContentPlaceHolder1_dvSucesso").hide();

                            }, 10000);

                        }

                    }

                })

            } else {

                alert(mensagem);

            }

        }

        function verificaEmail() {

            if ($("#ContentPlaceHolder1_txtEmail").val() != "" && $("#ContentPlaceHolder1_metodo").val() != "alterar") {

                $.ajax({

                    type: "POST",
                    url: "CadastroUsuario.aspx/VerificaEmail",
                    data: "{ email: '" + $("#ContentPlaceHolder1_txtEmail").val() + "'}",
                    contentType: "application/json; charset=utf8",
                    dataType: "json",
                    success: function (resposta) {

                        if (parseInt(resposta.d) > 0) {

                            $("#ContentPlaceHolder1_txtEmail").val("");
                            alert("Esse email já está cadastrado.");

                        }

                    }

                })

            }

        }

        function verificaUsuario() {

            if ($("#ContentPlaceHolder1_txtLogin").val() != "" && $("#ContentPlaceHolder1_metodo").val() != "alterar") {

                $.ajax({

                    type: "POST",
                    url: "CadastroUsuario.aspx/VerificarUsuario",
                    data: "{ login: '" + $("#ContentPlaceHolder1_txtLogin").val() + "'}",
                    contentType: "application/json; charset=utf8",
                    dataType: "json",
                    success: function (resposta) {

                        if (parseInt(resposta.d) > 0) {

                            $("#ContentPlaceHolder1_txtLogin").val("");
                            alert("Já existe um cliente cadastrado com esse usuário, por favor informe um difrente.");

                        }

                    }

                })

            }

        }

        function cancelar() {

            window.history.back();
            
        }

        function limparCampos() {

            $("#ContentPlaceHolder1_txtNome").val("");
            $("#ContentPlaceHolder1_txtEmail").val("");
            $("#ContentPlaceHolder1_txtCpf").val("");
            $("#ContentPlaceHolder1_txtLogin").val("");
            $("#ContentPlaceHolder1_txtSenha").val("");
            $("#ContentPlaceHolder1_txtDataNascimento").val("");
            $("#ContentPlaceHolder1_txtTelefone").val("");
            $("#ContentPlaceHolder1_txtCelular").val("");

        }

        $('.phone_with_ddd').mask('(00) 0000-0000');
        $('.cellphone').mask('(00) 00000-0000');
        $('.date').mask('00/00/0000');

    </script>

</asp:Content>
