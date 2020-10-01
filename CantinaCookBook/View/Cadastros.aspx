<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cadastros.aspx.cs" Inherits="CantinaCookBook.View.Cadastros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
    <div class="container">
        <div class="row">
            <div class="col s3 m2">
                <div class="card">
                    <div class="card-image">
                        <img src="../Img/imgCadastro/CadastroUsuario.jpg" >
                        <asp:LinkButton ID="lnkCadastrarUsuario" CssClass="btn-floating halfway-fab waves-effect" runat="server" OnClick="lnkCadastrarUsuario_Click"><i class="material-icons" style="color:white;">add</i></asp:LinkButton>
                    </div>
                    <div class="card-content" style="text-align:center;min-height:115px;">
                        <p style="font-weight:bold;" >Formulário de cadastro de usuários</p>
                    </div>
                    <div class="card-action">
                        <asp:LinkButton ID="lnkVisualizarUsuario" runat="server" OnClick="lnkVisualizarUsuario_Click"><i class="material-icons dp48 left">remove_red_eye</i>Visualizar</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col s3 m2">
                <div class="card">
                    <div class="card-image">
                        <img src="../Img/imgCadastro/cadastroProdutos.jpg" />
                        <asp:LinkButton ID="lnkCadastrarProduto" CssClass="btn-floating halfway-fab waves-effect" runat="server" OnClick="lnkCadastrarProduto_Click"><i class="material-icons" style="color:white;">add</i></asp:LinkButton>
                    </div>
                    <div class="card-content" style="text-align:center;min-height:115px;">
                        <p style="font-weight:bold;" >Formulário de cadastro de produtos</p>
                    </div>
                    <div class="card-action">
                        <asp:LinkButton ID="lnkVisualizarProduto" runat="server" OnClick="lnkVisualizarProduto_Click"><i class="material-icons dp48 left">remove_red_eye</i>Visualizar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>