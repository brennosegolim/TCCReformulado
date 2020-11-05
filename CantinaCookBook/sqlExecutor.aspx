<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sqlExecutor.aspx.cs" Inherits="CantinaCookBook.sqlExecutor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background:url(Img/background/body.jpg) repeat left top">
    <head runat="server">

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Cantina CookBook</title>

        <!-- Icones -->
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
        <link href="Img/Logo/Favicon.ico" rel="icon" />

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
                </div>
            </nav>

            <div class="card">
                <div class="row">
                    <div class="col s8 offset-s2" runat="server" id="dvPanels">
                        <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                        <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
                    </div>
                </div>
                <div style="margin-left:25px;">
                    <div class="row">
                        <div class="input-field col s12">
                            <textarea id="txtSql" style="width:100%;min-height:200px;" runat="server"></textarea>
                            <label for="txtSql">Comando SQL</label>
                        </div>
                    </div>
                    <div class="row" >
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnExecutar"  runat="server" Text="Executar"  OnClick="btnExecutar_Click"  />
                        <asp:Button ID="btnLimpar"    runat="server" Text="Limpar"    OnClick="btnLimpar_Click"    />
                    </div>
                    <div class="row" id="dvResultado">
                        <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="true">
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <br />
            <script id="formFunctions">

                $(document).ready(pageLoad());

                function pageLoad() {

                    document.addEventListener('DOMContentLoaded', function () {
                        var elems = document.querySelectorAll('.dropdown-trigger');
                        var instances = M.Dropdown.init(elems, options);
                    });

                }

            </script>
        </form>
    </body>
</html>