﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaProdutos.aspx.cs" Inherits="CantinaCookBook.View.ListaProdutos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card" style="padding:20px;">
            <br />
            <div class="row">
                <div class="input-field col s3 offset-m4">
                    <i class="material-icons prefix">search</i>
                    <input id="txtDescricao" type="text" class="validate" placeholder="Descrição do produto" maxlength="150" runat="server">
                </div>
                <div class="col s1" style="margin-top:2%;">
                    <asp:Button ID="btnBuscar" CssClass="waves-effect waves-light btn-small" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col s10 offset-s1">
                    <div class="col s2" style="padding:0px;">
                        <asp:Button ID="btnAdicionarNovo" runat="server" Text="+" style="border: 0px;font-size:larger;font-weight:bold; background-color: #715348;color: white;height: 2em;width: 3em; cursor:pointer;" OnClick="btnAdicionarNovo_Click"/>
                    </div>
                    <asp:GridView ID="grdProdutos" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDeletar" runat="server" Font-Size="18px" OnClick="btnDeletar_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="Excluir Produto" style="font-size:18px; color:#de2a2a;">delete</i></asp:LinkButton>
                                    <asp:LinkButton ID="btnAlterar" runat="server" Font-Size="18px" OnClick="btnAlterar_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="Editar Produto" style="font-size:18px">create</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Código"/>
                            <asp:BoundField DataField="Descricao" HeaderText="Descrição"/>
                            <asp:BoundField DataField="Preco" HeaderText="Preço"/>
                            <asp:BoundField DataField="Observacao" HeaderText="Observação"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <br/>
        <br/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
