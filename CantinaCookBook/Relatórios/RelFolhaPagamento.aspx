<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelFolhaPagamento.aspx.cs" Inherits="CantinaCookBook.Relatórios.RelFolhaPagamento" %>

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
                <h5 style="font-weight:bold;">PREZADOS PAIS E RESPONSÁVEIS</h5>
                <p id="corpoTexto" runat="server" style="text-align:justify;"></p>
                <br/>
                <br/>
                <div class="col s4 offset-s8">_______________________________________</div>
                <div class="col s4 offset-s8" style="text-align:center;">Tia Cris - (14) 99849-1500</div>
                <div class="col s4 offset-s8" style="text-align:center;" id="dataTexto" runat="server"></div>
                <div class="col s12">
                    <div class="col s4">
                        <img src="../Img/Logo/LogoBase.jpg" />
                    </div>
                    <div class="col s8" style="margin-top:100px;">
                        <div class="row" style="margin-bottom:0px;">
                            <b>MARIA CRISTINA DA COSTA FIGUEIRA - ME</b>
                        </div>
                        <div class="">CNPJ: 24.343.646.0001-30</div>
                        <div class="">Rua Tupinambaranas, 1085 - Centro - Tupã - SP - Cel: (14) 99849-1500</div>
                    </div>
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
