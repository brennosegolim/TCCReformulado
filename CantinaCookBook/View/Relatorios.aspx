<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Relatorios.aspx.cs" Inherits="CantinaCookBook.View.Relatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br/>
        <h3>Relatórios</h3>
        <br/>
        <ul class="collection with-header">
            <li class="collection-header"><h4>Listagem Simples</h4></li>
            <li class="collection-item"><div>Listagem de clientes<a href="../Relatórios/RelListaClientes.aspx" class="secondary-content"><i class="material-icons">local_printshop</i></a></div></li>
            <li class="collection-item"><div>Listagem de produtos<a href="../Relatórios/RelListaProdutos.aspx" class="secondary-content"><i class="material-icons">local_printshop</i></a></div></li>
            <li class="collection-item"><div>Ranking dos produtos mais vendidos<a href="../Relatórios/RelRankingProduto.aspx" class="secondary-content"><i class="material-icons">local_printshop</i></a></div></li>
            <li class="collection-item"><div>Ranking dos clientes<a href="#!" class="secondary-content"><i class="material-icons">local_printshop</i></a></div></li>
        </ul>
        <br/>
        <br/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
