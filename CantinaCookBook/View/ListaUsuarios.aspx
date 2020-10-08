<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="CantinaCookBook.View.ListaUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col s12">
                <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDeletar"  runat="server" Font-Size="18px" OnClick="btnDeletar_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Excluir Usuário" style="font-size:18px; color:#de2a2a;">delete</i></asp:LinkButton>
                                <asp:LinkButton ID="btnAlterar"  runat="server" Font-Size="18px" OnClick="btnAlterar_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Editar Usuário" style="font-size:18px">create</i></asp:LinkButton>  
                                <asp:LinkButton ID="btnVincular" runat="server" Font-Size="18px" OnClick="btnVincular_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Vincular Dependentes" style="font-size:18px; color:green">group_add</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                        <asp:BoundField DataField="Idade" HeaderText="Idade"/>
                        <asp:BoundField DataField="Celular" HeaderText="Celular"/>
                        <asp:BoundField DataField="Telefone" HeaderText="Telefone"/>
                        <asp:BoundField DataField="Email" HeaderText="Email"/>
                        <asp:BoundField DataField="Login" HeaderText="Login"/>
                        <asp:BoundField DataField="Nivel" HeaderText="Nivel"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
