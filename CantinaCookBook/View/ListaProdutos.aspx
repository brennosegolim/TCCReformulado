<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaProdutos.aspx.cs" Inherits="CantinaCookBook.View.ListaProdutos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col s8 offset-s2">
                <asp:GridView ID="grdProdutos" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDeletar" runat="server" Font-Size="18px" OnClick="btnDeletar_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="Excluir Produto" style="font-size:18px; color:#de2a2a;">delete</i></asp:LinkButton>
                                <asp:LinkButton ID="btnAlterar" runat="server" Font-Size="18px" OnClick="btnAlterar_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="Editar Produto" style="font-size:18px">create</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição"/>
                        <asp:BoundField DataField="Preco" HeaderText="Preço"/>
                        <asp:BoundField DataField="Observacao" HeaderText="Observação"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
