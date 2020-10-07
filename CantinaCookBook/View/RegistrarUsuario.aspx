<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="CantinaCookBook.View.RegistrarUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>

        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0"/>
        <title>.: Acesso ao Sistema :.</title>

        <!-- CSS  -->
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
        <link href="../CSS/CantinaEstilos.css" rel="stylesheet" />
        <link href="../CSS/materialize.css" rel="stylesheet" />
        <link href="../CSS/materialize.min.css" rel="stylesheet" />

        <!-- JS -->
        <script src="../JS/jquery-3.5.1.min.js"></script>
        <script src="../JS/materialize.js"></script>
        <script src="../JS/materialize.min.js"></script>
        <script src="../JS/jsFunctions.js"></script>
        <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>

    </head>
    <body onload="iniciar()">
        <form id="form" runat="server" class="col s12">
            <!-- Cabeçalho -->
            <nav runat="server" id="menu">
                <div class="nav-wrapper brown">
                    <a href="../Index.aspx" class="brand-logo" style="margin-left:5px;"><i class="material-icons dp48">home</i>Cantina Cook Book</a>
                </div>
            </nav>

            <!-- Conteúdo da página -->
            <main runat="server" id="main">
                <div class="container" runat="server">
                    <div class="row" runat="server" style="margin-top:10%">
                        <div class="card col s6 offset-s3">
                            <div id="dvCliente">
                                <div class="col s12 offset-s2" style="margin-top:15px;" runat="server">
                                    <div id="tituloCliente" class="col s6 offset-s2 ">Dados Principais.</div>
                                </div>
                                <br />
                                <br />
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">account_circle</i>
                                            <input maxlength="100" placeholder="Nome Completo" id="txtNome" type="text" class="validate" data-position="top" data-tooltip="Nome Completo" runat="server"/>
                                            <label for="txtNome" id="lblNome" runat="server">Nome</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">date_range</i>
                                            <input maxlength="20" placeholder="" id="txtDataNascimento" type="text" class="validate date" data-position="top" data-tooltip="Data de Nascimento" runat="server"/>
                                            <label for="txtDataNascimento" id="lblDataNascimento" runat="server">Data de Nascimento</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">credit_card</i>
                                            <input maxlength="11" placeholder="CPF" id="txtCPF" type="text" class="validate" data-position="top" data-tooltip="CPF" runat="server"/>
                                            <label for="txtCPF" id="lblCPF" runat="server">CPF (sem pontuação)</label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="col s12 offset-s2" style="margin-top:15px;" runat="server">
                                    <div id="tituloContato" class="col s6 offset-s2 ">Informações de Contato.</div>
                                </div>
                                <br />
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">email</i>
                                            <input maxlength="70" placeholder="E-mail" id="txtEmail" type="text" class="validate" data-position="top" data-tooltip="E-mail" onblur="verificaEmail()" runat="server"/>
                                            <label for="txtEmail" id="lblEmail" runat="server">Email</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">local_phone</i>
                                            <input maxlength="20" placeholder="Telefone" id="txtTelefone" type="text" class="validate phone_with_ddd" data-position="top" data-tooltip="Telefone" runat="server"/>
                                            <label for="txtTelefone" id="lblTelefone" runat="server">Telefone</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">smartphone</i>
                                            <input maxlength="20" placeholder="Celular" id="txtCelular" type="text" class="validate cellphone" data-position="top" data-tooltip="Celular" runat="server"/>
                                            <label for="txtCelular" id="lblCelular" runat="server">Celular</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="dvAcesso">
                                 <div class="col s12 offset-s2" style="margin-top:15px;" runat="server">
                                    <div id="tituloAcesso" class="col s6 offset-s2 ">Dados de Acesso.</div>
                                </div>
                                <br />
                                <br />
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">account_circle</i>
                                            <input maxlength="30" placeholder="Usuário" id="txtUsuario" type="text" class="validate" data-position="top" data-tooltip="Usuário" runat="server" onblur="verificaUsuario()" />
                                            <label for="txtUsuario" id="lblUsuario" runat="server">Usuário</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">security</i>
                                            <input maxlength="20" placeholder="Senha" id="txtSenha" type="password" class="validate" runat="server" onclick="verificarSenhas()" />
                                            <label for="txtSenha" id="lblSenha" runat="server">Senha</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" runat="server">
                                    <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                        <div class="input-field col s12" runat="server">
                                            <i class="material-icons prefix">security</i>
                                            <input maxlength="20" placeholder="Confirmar Senha" id="txtConfirmar" type="password" class="validate" runat="server" onblur="verificarSenhas()" />
                                            <label for="txtConfirmar" id="lblConfirmar" runat="server">Confirmar Senha</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 offset-s2" style="margin-bottom:15px; ">
                                    <div id="mensagemSenha" class="col s6 offset-s2 " style="font-size:10px;" >A senha deve conter no minimo 6 caracteres.</div>
                                </div>
                            </div>
                            <div id="dvConfirmar" class="col s12 offset-s2" runat="server" style="margin-bottom:15px;">
                                <div class="col s6 offset-s2" runat="server">                            
                                    <input style="margin-left: 7px;" class="btn waves-effect waves-light" id="btnRegistrar" type="button" value="Registrar" onclick="cadastrarUsuario()" />
                                    <input style="margin-left:7px;" class="btn waves-effect waves-light grey lighten-2" id="btnVoltar" type="button" value="Voltar" onclick="voltar()" />
                                </div>
                            </div>
                            <div id="dvProximo" class="col s12 offset-s2" runat="server" style="margin-bottom:15px;">
                                <div class="col s6 offset-s2" runat="server">                            
                                    <input class="btn waves-effect waves-light" id="btnProximo" type="button" value="Próximo" onclick="proximo()" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>


            <!-- Rodapé -->
            <footer class="page-footer brown" style="position:fixed;bottom:0;left:0;width:100%;" runat="server" id="footer">
                <div class="footer-copyright">
                    <div class="container">
                        © 2020 Copyright
                    </div>
                </div>
            </footer>
            <script type="text/javascript" id="jsActions">

                function iniciar(){

                    //Escondendo os campos referentes ao registro de acesso.
                    $("#dvAcesso").hide();
                    $("#dvConfirmar").hide();

                }

                function proximo() {

                    let confirmar = true;
                    let mensagem = "";
                    let nome = $("#txtNome").val();
                    let email = $("#txtEmail").val();
                    let cpf = $("#txtCPF").val();
                    let dataNascimento = $("#txtDataNascimento").val();
                    let telefone = $("#txtTelefone").val();
                    let celular = $("#txtCelular").val();
                    let patternData = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;

                    if (nome == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe seu nome completo.\n";

                    }

                    if (email == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe seu E-mail.\n";

                    }

                    if (cpf == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe seu CPF.";

                    }

                    if (dataNascimento == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe sua Data de Nascimento.";

                    }

                    if (telefone == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe seu Telefone.";

                    }

                    if (celular == "") {

                        confirmar = false;
                        mensagem += "Atenção ! por favor informe seu Celular.";

                    }

                    if (!patternData.test(dataNascimento)) {
                        alert("Digite a data no formato Dia/Mês/Ano");
                        $("#txtDataNascimento").val("");
                        return false;
                    }

                    if (confirmar) {

                        //Escondendo os campos que se referem ao usuário.
                        $("#dvCliente").hide();
                        $("#dvProximo").hide();

                        //Mostrando os campos referentes ao registro de acesso.
                        $("#dvAcesso").show();
                        $("#dvConfirmar").show();

                    } else {

                        alert(mensagem);

                    }

                }

                function verificarSenhas() {

                    let senha = $("#txtSenha").val();
                    let confirmacao = $("#txtConfirmar").val();
                    let verifica = true;

                    if (senha.length < 6) {

                        alert("Atenção !!! a senha precisa ter no mínimo 6 caracteres.");
                        verifica = false;

                    }


                    if (verifica) {

                        if (senha != "" && confirmacao != "") {

                            if (senha != confirmacao) {

                                alert("Atenção !!! as senhas não são idênticas.");
                                $("#txtConfirmar").val("");

                            }

                        }

                    }

                }

                function voltar(){

                    //Escondendo os campos de registro de acesso.
                    $("#dvAcesso").hide();
                    $("#dvConfirmar").hide();

                    //Mostrando os campos referentes ao usuário.
                    $("#dvCliente").show();
                    $("#dvProximo").show();

                }

                function cadastrarUsuario() {

                    let usuario = $("#txtUsuario").val();
                    let senha = $("#txtSenha").val();
                    let confirmacao = $("#txtConfirmar").val();
                    let mensagem = "";

                    if (usuario == "") mensagem += "Atenção !!! informe o campo do usuário.\n";
                    if (senha == "") mensagem += "Atenção !!! informe o campo da senha.\n";
                    if (confirmacao == "") mensagem += "Atenção !!! informe o campo de confirmação de senha.\n";
                    if (senha != confirmacao && mensagem == "") mensagem += "Atenção !!! as senhas não são idênticas.";

                    if (mensagem == "") {

                        $.ajax({

                            type: "POST",
                            url: "RegistrarUsuario.aspx/cadastrarUsuario",
                            data: "{   nome: '" + $("#txtNome").val() +
                                "', dataNascimento: '" + $("#txtDataNascimento").val() +
                                "', telefone: '" + $("#txtTelefone").val() +
                                "', celular: '" + $("#txtCelular").val() +
                                "', email: '" + $("#txtEmail").val() +
                                "',   cpf: '" + $("#txtCPF").val() +
                                "', usuario: '" + $("#txtUsuario").val() +
                                "', senha: '" + $("#txtSenha").val() +
                                "', confirmar: '" + $("#txtConfirmar").val() +
                                "'}",
                            contentType: "application/json; charset=utf8",
                            dataType: "json",
                            success: function (resposta) {

                                if (resposta.d != "") {

                                    alert(resposta.d);

                                } else {

                                    window.location = "../Index.aspx";
                                    alert("Usuário Cadastrado com sucesso ! Aguarde o administrador do sistema analisar seu usuário !");

                                }

                            }

                        })

                    } else {

                        alert(mensagem);

                    }

                }

                function verificaEmail(){

                    if ($("#txtEmail").val() != "") {

                        $.ajax({

                            type: "POST",
                            url: "RegistrarUsuario.aspx/VerificaEmail",
                            data: "{ email: '" + $("#txtEmail").val() + "'}",
                            contentType: "application/json; charset=utf8",
                            dataType: "json",
                            success: function (resposta) {

                                if (parseInt(resposta.d) > 0) {

                                    $("#txtEmail").val("");
                                    alert("Esse email já está cadastrado.");

                                }

                            }

                        })

                    }

                }

                function verificaUsuario() {

                    if ($("#txtUsuario").val() != "") {

                        $.ajax({

                            type: "POST",
                            url: "RegistrarUsuario.aspx/VerificarUsuario",
                            data: "{ login: '" + $("#txtUsuario").val() + "'}",
                            contentType: "application/json; charset=utf8",
                            dataType: "json",
                            success: function (resposta) {

                                if (parseInt(resposta.d) > 0) {

                                    $("#txtUsuario").val("");
                                    alert("Já existe um cliente cadastrado com esse usuário, por favor informe um difrente.");

                                }

                            }

                        })

                    }

                }

                $('.phone_with_ddd').mask('(00) 0000-0000');
                $('.cellphone').mask('(00) 00000-0000');
                $('.date').mask('00/00/0000');

            </script>
        </form>
    </body>
</html>