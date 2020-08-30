<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CantinaCookBook.View.Login" %>

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
        <script src="../JS/jsFunctions.js"></script>
        <script src="../JS/materialize.js"></script>
        <script src="../JS/materialize.min.js"></script>

    </head>
    <body onload="">
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
                            <div class="col s12 offset-s2" style="margin-top:15px;" runat="server">
                                <div class="col s6 offset-s2">Acesso ao sistema.</div>
                            </div>
                            <div class="col s12 offset-s2" runat="server">
                                <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                    <div class="input-field col s12" runat="server">
                                        <i class="material-icons prefix">account_circle</i>
                                        <input id="txtLogin" type="text" class="validate" autocomplete="off" runat="server"/>
                                        <label for="txtLogin" id="lblLogin" runat="server">Login</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 offset-s2">
                                <div class="col s12 m8 offset-m1 xl7 offset-xl1">
                                    <div class="input-field col s12">
                                        <i class="material-icons prefix">security</i>
                                        <input id="txtSenha" type="password" autocomplete="off" class="validate" runat="server"/>
                                        <label for="txtSenha" id="lblSenha" runat="server">Senha</label>
                                        <div runat="server" id="msgErro">
                                            <span runat="server" id="spnErro" class="red-text text-darken-2"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 offset-s2" runat="server" style="margin-bottom:15px;">
                                <div class="col s6 offset-s2" runat="server">                            
                                    <asp:Button CssClass="btn waves-effect waves-light" ID="btnAcesso" runat="server" Text="Acessar" OnClick="btnAcesso_Click"/>
                                </div>
                            </div>
                            <div class="col s12">
                                <div class="col s8 offset-s3">
                                    <div class="col s12">
                                        Não tem uma conta ?
                                        <asp:LinkButton CssClass="orange-text" ID="lnkCadastrarUsuario" OnClick="lnkCadastrarUsuario_Click" runat="server">
                                            Registrar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col s8 offset-s3" style="margin-bottom:15px;">
                                    <div class="col s12">
                                        <asp:LinkButton CssClass="orange-text" ID="lnkRecuperarSenha" OnClick="lnkRecuperarSenha_Click" runat="server">
                                            Esqueci minha senha
                                        </asp:LinkButton>
                                    </div>
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
        </form>
    </body>
</html>