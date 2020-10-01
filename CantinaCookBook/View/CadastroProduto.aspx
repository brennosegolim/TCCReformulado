<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastroProduto.aspx.cs" Inherits="CantinaCookBook.View.CadastroProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="row">
                <div class="col s8 offset-s2" runat="server" id="dvPanels">
                    <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                    <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
                </div>
            </div>
            <h4 class="header">CADASTRO DE PRODUTOS</h4>
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">sms</i>
                    <input placeholder="Descrição do produto" id="txtDescricao" type="text" class="validate" runat="server">
                    <label for="txtDescricao">Descrição do produto</label>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s4">
                    <i class="material-icons prefix">attach_money</i>
                    <input placeholder="Preço" id="txtPreco" type="text" class="validate dinheiro" runat="server">
                    <label for="txtPreco">Preço</label>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s12">
                    <i class="material-icons prefix">mode_edit</i>
                    <textarea id="txtObservacao" class="materialize-textarea" runat="server"></textarea>
                    <label for="txtObservacao">Observação</label>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s1" style="margin-left:3%;">
                    <asp:Button runat="server" Text="Confirmar" id="btnConfirmar" CssClass="waves-effect waves-light btn" OnClick="btnConfirmar_Click"/>
                </div>
                <div class="input-field col s1" style="margin-left:2%;">
                    <asp:Button  runat="server" Text="Cancelar" id="btnCancelar" CssClass="waves-effect waves-light btn" OnClick="btnCancelar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        $('.dinheiro').mask('#.##0,00', { reverse: true });

    </script>
</asp:Content>
