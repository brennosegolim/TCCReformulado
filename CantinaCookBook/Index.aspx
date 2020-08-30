<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CantinaCookBook.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Cantina CookBook</title>

        <!-- Icones -->
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>

        <!-- Includes CSS -->
        <link href="CSS/CantinaEstilos.css" rel="stylesheet" />
        <link href="CSS/materialize.css" rel="stylesheet" />
        <link href="CSS/materialize.min.css" rel="stylesheet" />

        <!-- Inscludes JavaScript -->
        <script src="JS/jsFunctions.js"></script>
        <script src="JS/materialize.js"></script>
        <script src="JS/materialize.min.js"></script>

        <!-- Jquery -->
        <script src="JS/jquery-3.5.1.min.js"></script>

    </head>
    <body>
        <form id="form" runat="server">
             <!-- Cabeçalho -->
            <nav runat="server" id="menu">
                <div class="nav-wrapper brown">
                    <asp:LinkButton OnClick="lnkHome_Click" ID="lnkHome" CssClass="brand-logo" style="margin-left:5px;" runat="server"><i class="material-icons dp48">home</i>Cantina Cook Book</asp:LinkButton>
                    <ul id="nav-mobile" class="right hide-on-med-and-down">
                        <li><a href="sass.html"></a></li>
                        <li><asp:LinkButton ID="lnkCadastros" runat="server" Text="Área do Cliente" OnClick="lnkCadastros_Click"></asp:LinkButton></li>
                    </ul>
                </div>
            </nav>
            <div id="spaceTop" ></div>
            <div id="index-banner" class="parallax-container">
                <div class="section no-pad-bot">
                    <div class="container">
                        <h1 class="header center " style="color:white;font-weight:bold;font-family:sans-serif;">Bem-Vindo</h1>
                        <div class="row center">
                            <h5 class="header col s12 light" style="color:white;font-weight:bold;">Um lugar feliz para gente feliz 😊</h5>
                        </div>
                    </div>
                </div>
                <div class="parallax">
                    <img src="Img/banners/banner.jpg" alt="Unsplashed background img 1" style="transform: translate3d(-50%, 235.39px, 0px); opacity: 1; filter:blur(4px);"/>
                </div>
            </div>
            <div class="container">
                <div class="section">
                    <div class="row">
                        <div class="col s12 m4">
                            <div class="icon-block">
                                <h2 class="center brown-text">
                                    <i class="material-icons">business</i>
                                </h2>
                                <h5 class="center">Quem somos</h5>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                            </div>
                        </div>
                        <div class="col s12 m4">
                             <div class="icon-block">
                                <h2 class="center brown-text">
                                    <i class="material-icons">business</i>
                                </h2>
                                <h5 class="center">Quem somos</h5>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                            </div>
                        </div>
                        <div class="col s12 m4">
                             <div class="icon-block">
                                <h2 class="center brown-text">
                                    <i class="material-icons">business</i>
                                </h2>
                                <h5 class="center">Quem somos</h5>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                                <p class="light">Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina Cantina</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <!-- Rodapé -->
            <footer class="page-footer brown" style="bottom:0;left:0;width:100%;z-index:1001;" runat="server" id="footer">
                <div class="footer-copyright">
                    <div class="container">
                        © 2020 Todos Direitos Reservados 
                    </div>
                </div>
            </footer>
        </form>
    </body>
</html>
