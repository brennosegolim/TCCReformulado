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
                                <div class="col s6 offset-s2 ">Dados Principais.</div>
                            </div>
                            <br />
                            <br />
                            <div class="col s12 offset-s2" runat="server">
                                <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                    <div class="input-field col s12" runat="server">
                                        <i class="material-icons prefix">account_circle</i>
                                        <input placeholder="Nome Completo" id="txtNome" type="text" class="validate" data-position="top" data-tooltip="Nome Completo" />
                                        <label for="txtNome" id="lblNome" runat="server">Nome</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 offset-s2" runat="server">
                                <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                    <div class="input-field col s12" runat="server">
                                        <i class="material-icons prefix">account_circle</i>
                                        <input placeholder="E-mail" id="txtEmail" type="text" class="validate" data-position="top" data-tooltip="E-mail" />
                                        <label for="txtEmail" id="lblEmail" runat="server">Email</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 offset-s2" runat="server">
                                <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                                    <div class="input-field col s12" runat="server">
                                        <i class="material-icons prefix">account_circle</i>
                                        <input placeholder="CPF" id="txtCPF" type="text" class="validate" data-position="top" data-tooltip="CPF" />
                                        <label for="txtCPF" id="lblCPF" runat="server">CPF</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 offset-s2" runat="server" style="margin-bottom:15px;">
                                <div class="col s6 offset-s2" runat="server">                            
                                    <asp:Button CssClass="btn waves-effect waves-light" ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click"/>
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