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
        </ul>
        <br/>
        <ul class="collection with-header">
            <li class="collection-header"><h4>Listagem com agrupamento</h4></li>
            <li class="collection-item"><div>Listagem de dependentes por resposável<a href="../Relatórios/RelResponsavelCliente.aspx" class="secondary-content"><i class="material-icons">local_printshop</i></a></div></li>
            <li class="collection-item"><div>Lista de Pendências<asp:LinkButton ID="LinkButton1" runat="server" CssClass="secondary-content" OnClick="lnkRelPendencia_Click"><i class="material-icons">local_printshop</i></asp:LinkButton></div></li>
        </ul>
        <br/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
