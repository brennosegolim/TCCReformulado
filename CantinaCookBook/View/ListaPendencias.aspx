<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaPendencias.aspx.cs" Inherits="CantinaCookBook.View.ListaPendencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card" style="padding:20px;">
            <br />
            <div class="row">
                <div class="col s8 offset-s2" runat="server" id="dvPanels">
                    <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                    <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
                </div>
            </div>
            <div class="row" id="dvSelect" runat="server">
                <div class="input-field col s12">
                    <asp:DropDownList ID="cbxSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbxSelect_SelectedIndexChanged">
                        <asp:ListItem Enabled="true" Text="Selecione o filtro" Selected="True" Value="0" />
                        <asp:ListItem Enabled="true" Text="Nome" Selected="False" Value="1" />
                        <asp:ListItem Enabled="true" Text="Data" Selected="False" Value="2" />
                    </asp:DropDownList>
                    <label>Selecione o filtro</label>
                </div>
            </div>
            <div class="row" id="dvConsultaNome" runat="server">
                <div class="input-field col s4 offset-m4">
                    <i class="material-icons prefix">person</i>
                    <input id="txtNome" type="text" class="validate" placeholder="Nome" maxlength="150" runat="server">
                </div>
                <div class="col s1" style="margin-top:2%;">
                    <asp:Button ID="btnBuscarNome" CssClass="waves-effect waves-light btn-small" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
                <div class="col s2" style="margin-top:2%;">
                    <asp:Button ID="btnCancelarNome" CssClass="waves-effect waves-light btn-small" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
            </div>
            <div class="row" id="dvConsultaData" runat="server">
                <div class="input-field col s3 offset-m4">
                    <i class="material-icons prefix">search</i>
                    <input id="txtData" type="date" class="validate" placeholder="Data" maxlength="10" runat="server">
                </div>
                <div class="col s1" style="margin-top:2%;">
                    <asp:Button ID="btnBuscarData" CssClass="waves-effect waves-light btn-small" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
                <div class="col s2" style="margin-top:2%;">
                    <asp:Button ID="btnCancelarData" CssClass="waves-effect waves-light btn-small" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
            </div>
            <div class="row" id="dvGrid" runat="server">
                <div class="col s12 ">
                    <asp:GridView ID="grdPendencia" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPendencia_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnConsultar" runat="server" Font-Size="18px" OnClick="btnConsultar_Click" CommandArgument='<%# Eval("IdVenda") %>'><i class="large material-icons" title="Ver Produtos da pendência" style="font-size:18px; color:dodgerblue;">remove_red_eye</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdVenda" HeaderText="Código da Pendência"/>
                            <asp:BoundField DataField="Nome" HeaderText="Nome do Cliente"/>
                            <asp:BoundField DataField="Data" HeaderText="Data"/>
                            <asp:BoundField DataField="ValorTotal" HeaderText="Valor"/>
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
    <script type="text/javascript">

        $(document).ready(function () {

            $('select').formSelect();

            setTimeout(function () { $("#ContentPlaceHolder1_dvPanels").hide(); }, 5000);

        });

    </script>
</asp:Content>
