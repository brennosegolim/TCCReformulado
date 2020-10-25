<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FiltroPeriodo.aspx.cs" Inherits="CantinaCookBook.Filtros.FiltroPeriodo" %>

<!DOCTYPE html>

<html>
    <head runat="server">

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>.: <asp:Literal ID="litNome" runat="server"></asp:Literal> :.</title>

        <!-- Icones -->
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>

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
    <body>
        <form id="form" runat="server" method="post" class="col s12">
            <!-- Cabeçalho -->
            <nav runat="server" id="menu">
                <div class="nav-wrapper brown">
                    <asp:LinkButton OnClick="lnkHome_Click" ID="lnkHome" CssClass="brand-logo" style="margin-left:5px;" runat="server"><i class="material-icons dp48">home</i>Cantina Cook Book</asp:LinkButton>
                    <ul id="nav-mobile" class="right hide-on-med-and-down">
                        <li><a href="sass.html"></a></li>
                        <li><asp:LinkButton ID="lnkVendas" runat="server" Text="Pendência" OnClick="lnkVendas_Click" AccessKey="1"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnkPagamento" runat="server" Text="Pagamentos" OnClick="lnkPagamento_Click" AccessKey="2"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnkCadastros" runat="server" Text="Cadastros" OnClick="lnkCadastros_Click" AccessKey="3"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnkRelatorios" runat="server" Text="Relatórios" OnClick="lnkRelatorios_Click" AccessKey="4"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnkSair" runat="server" OnClick="lnkSair_Click" Text="Logout"></asp:LinkButton></li>
                    </ul>
                </div>
            </nav>
            <div id="dvFundo" runat="server" style="position:fixed; width:100%; height:100%; z-index:1001; background-color:black; opacity:40%;">
            </div>
            <div style="background-color:white; margin:auto; width:50%; padding:0px; min-height:30%; margin-top:10%; margin-left:25%; position:fixed; z-index:1100;" id="dvBackground" runat="server">
                <div class="row">
                    <div class="row">
                        <div class="col s4 offset-s4" style="text-align:center; font-size:32px;">    
                        PERÍODO
                        </div>
                    </div>
                    <div class="row" id="dvDatas" runat="server">
                        <div class="col s12 m8 offset-m1 xl7 offset-xl1" runat="server">
                            <div class="input-field col s5" runat="server">
                                <i class="material-icons prefix">date_range</i>
                                <input maxlength="10" placeholder="" id="txtDataInicial" type="text" class="validate date" data-position="top" data-tooltip="Data Inicial" runat="server"/>
                                <label for="txtDataInicial" id="lblDataInicial" runat="server">Data Inicial</label>
                            </div>
                            <div class="input-field col s5 offset-s1" runat="server">
                                <i class="material-icons prefix">date_range</i>
                                <input maxlength="10" placeholder="" id="txtDataFinal" type="text" class="validate date" data-position="top" data-tooltip="Data Final" runat="server"/>
                                <label for="txtDataFinal" id="lblDataFinal" runat="server">Data Final</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col s2 offset-s1" style="margin-top:2%;">
                                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em; cursor:pointer;" OnClick="btnImprimir_Click"/>
                            </div>
                            <div class="col s2">
                                <input id="btnCancelar" type="button" value="Cancelar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em; cursor:pointer;" onclick="cancelar();"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript" id="formJS">

                $(document).ready(function () {

                    $('.materialboxed').materialbox();
                    $('.date').mask('00/00/0000');

                });

                function cancelar() {

                    window.history.back();

                }

            </script>
        </form>
    </body>
</html>
