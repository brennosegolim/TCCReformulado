<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="AutenticarUsuario.aspx.cs" Inherits="CantinaCookBook.View.AutenticarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h3>Dados Princípais</h3>
        </div>
        <div class="row">
            <div class="col s6">
                <div class="row">
                    <div class="col 12">
                        <h6>Nome:</h6>
                    </div>
                </div>
                <h5 id="titleNome" runat="server"></h5>
            </div>
            <div class="col s6">
                <div class="row">
                    <div class="col 12">
                        <h6>CPF:</h6>
                    </div>
                </div>
                <h5 id="titleCpf" runat="server"></h5>
            </div>
        </div>
        <div class="row">
            <h3>Informações de contato</h3>
        </div>
        <div class="row">
            <div class="col s6">
                <div class="row">
                    <div class="col 12">
                        <h6>Email:</h6>
                    </div>
                </div>
                <h5 id="titleEmail" runat="server"></h5>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
