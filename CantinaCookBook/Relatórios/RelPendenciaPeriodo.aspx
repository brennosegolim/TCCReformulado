<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelPendenciaPeriodo.aspx.cs" Inherits="CantinaCookBook.Relatórios.RelPendenciaPeriodo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>.: <asp:Literal ID="litNome" runat="server"></asp:Literal> :.</title>

        <!-- Icones -->
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
        <link href="../Img/Logo/Favicon.ico" rel="icon"/>

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

    </head>
    <body>
        <form id="frmRel" runat="server">
            <div class="row" id="dvOpcoes">
                <div class="col s2">
                    <input id="btnVoltar" type="button" value="Voltar" style="border: 0px; background-color: #715348; color: white; height: 3em; width: 8em; cursor: pointer;" onclick="window.history.back();" />
                </div>
                <div class="col s2">
                    <input id="btnPrint" type="button" value="Imprimir" style="border: 0px; background-color: #715348; color: white; height: 3em; width: 8em; cursor: pointer;" onclick="imprimir();" />
                </div>
            </div>
            <div class="row" style="text-align:center; font-family:'Times New Roman';">
                <h5 style="font-weight:bold;">Listagem de Clientes</h5>
                <div class="row" id="dvListagem" runat="server">
                    
                </div>
            </div>
        </form>
        <script type="text/javascript">

            function imprimir() {

                $("#dvOpcoes").hide();

                print();

                $("#dvOpcoes").show();

            }

            imprimir();

        </script>
    </body>
</html>
