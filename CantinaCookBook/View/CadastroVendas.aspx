<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastroVendas.aspx.cs" Inherits="CantinaCookBook.View.CadastroVendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col s8 offset-s2" runat="server" id="dvPanels">
                <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
            </div>
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
