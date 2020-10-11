<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="VincularClientes.aspx.cs" Inherits="CantinaCookBook.View.VincularClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br/>
        <div class="col s2">
            <div style="padding:0px; float:right;">
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="waves-effect waves-light btn-small grey lighten-1" OnClick="btnVoltar_Click"/>
            </div>
        </div>
        <div class="row">
            <h4>Vinculo de Dependentes</h4>
        </div>
        <div class="row">
            <div class="input-field col s3 offset-m4">
                <i class="material-icons prefix">account_circle</i>
                <input id="txtNome" type="text" class="validate" placeholder="Nome Completo" maxlength="100" runat="server">
                <input id="txtIdCliente" type="hidden" runat="server"/>
            </div>
            <div class="col s1" style="margin-top:2%;">
                <asp:Button ID="btnBuscar" CssClass="waves-effect waves-light btn-small" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>
        <div class="row" id="dvPesquisa" runat="server">
            <div class="col s12">
                <asp:GridView ID="grdPesquisa" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAdicionar" runat="server" Font-Size="18px" OnClick="btnAdicionar_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Desvincular Usuário" style="font-size:18px; color:green;">add</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                        <asp:BoundField DataField="Idade" HeaderText="Idade"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <h4>Dependentes</h4>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnRemover"  runat="server" Font-Size="18px" OnClick="btnRemover_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Desvincular Usuário" style="font-size:18px; color:#de2a2a;">clear</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                        <asp:BoundField DataField="Idade" HeaderText="Idade"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
