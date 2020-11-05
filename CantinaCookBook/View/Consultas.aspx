<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="CantinaCookBook.View.Consultas" %>
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
                        <img src="../Img/imgCadastro/ListaPendencias.png" >
                        <asp:LinkButton ID="lnkConsultarPendencia" CssClass="btn-floating halfway-fab waves-effect" runat="server" OnClick="lnkConsultarPendencia_Click"><i class="material-icons" style="color:white;">remove_red_eye</i></asp:LinkButton>
                    </div>
                    <div class="card-content" style="text-align:center;min-height:115px;">
                        <p style="font-weight:bold;" >Consultar Pendências</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
