<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagamento.aspx.cs" Inherits="CantinaCookBook.View.Pagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col s8 offset-s2" runat="server" id="dvPanels">
                <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
            </div>
        </div>
        <div class="row">
            <div class="input-field col s6">
                <i class="material-icons prefix">person</i>
                <input placeholder="Cliente" id="txtCliente" type="text" class="validate" runat="server">
                <label for="txtCliente">Cliente</label>
            </div>
            <div class="input-field col s2">
                <asp:Button ID="btnConfirmarCliente" runat="server" Text="Buscar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;" OnClick="btnConfirmarCliente_Click"/>
            </div>
            <div class="input-field col s2">
                <asp:Button ID="btnCancelarCliente" runat="server" Text="Cancelar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;" OnClick="btnCancelarCliente_Click"/>
            </div>
        </div>
        <div class="row">
            <div class="card col s12" id="dvValorPendencia" runat="server">
                <div class="col s10 offset-s1" style="text-align:center;">
                    <asp:Label ID="lblReais" runat="server" Text="R$" Font-Size="XX-Large"></asp:Label>
                    <asp:Label ID="lblValor" runat="server" Text="0.00" Font-Size="XX-Large"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
