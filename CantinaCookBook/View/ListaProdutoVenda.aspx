<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaProdutoVenda.aspx.cs" Inherits="CantinaCookBook.View.ListaProdutoVenda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card" style="padding:20px;">
            <br />
            <div class="row">
                <h3 id="titleCodigo" runat="server"></h3>
                <br />
                <h5>Lista de produtos</h5>
                <br />
                <div class="col s12">
                    <asp:GridView ID="grdProdutoVenda" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Código"/>
                            <asp:BoundField DataField="Descricao" HeaderText="Descrição"/>
                            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade"/>
                            <asp:BoundField DataField="Valor" HeaderText="Valor"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <br/>
    <br/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
