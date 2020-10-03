<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="CantinaCookBook.View.UserHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main runat="server" id="main">
                <div class="container" runat="server">
                    <div id="dvSelectDependente" runat="server">
                        <div class="row">
                            <br/>
                            <div class="input-field col s6 offset-s3">
                                <asp:DropDownList ID="cbxDepentes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbxDepentes_SelectedIndexChanged" ></asp:DropDownList>
                                <label>Dependentes</label>
                            </div>
                        </div>
                    </div>
                    <div id="dvUsuario" runat="server">
                        <div class="row" runat="server" style="margin-top:5%">
                            <div class="col s12" style="text-align:center;">
                                <div class="row">
                                    <div class="col s12 header" id="dvNomeUsuario" runat="server" style="text-align:center;text-transform:uppercase;font-size:xx-large"></div>
                                 </div>
                                <div class="row">
                                    <div id="dvHistórico" class="card col s12" style="min-height:400px;max-height:500px; overflow:auto;" runat="server">
                                        <div class="row">
                                            <div class="col s12" style="text-align:center;text-transform:uppercase;font-size:xx-large">Histórico</div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <asp:Button ID="btnFiltroData" runat="server" CssClass="waves-effect waves-light btn-small" Text="Filtrar" OnClick="btnFiltroData_Click" />
                                            </div>
                                            <div class="row">
                                                <div class="col s2 offset-m5">
                                                    <input type="date" id="txtFiltroData" runat="server">
                                                </div>
                                                <div class="col s1">
                                                    <asp:Button ID="btnRealizarFiltro" runat="server" Text="Buscar" CssClass="waves-effect waves-light btn-small" OnClick="btnRealizarFiltro_Click"/>
                                                </div>
                                            </div>                                        
                                        </div>
                                        <div class="row">
                                            <div class="col s12" id="tableHistorico" runat="server"></div>
                                        </div>
                                    </div>
                                </div>
                                <br/>
                                <div class="row">
                                    <div id="dvLimiteCredito" class="card col s12" style="min-height:300px;max-height:300px;" runat="server">
                                        <div class="row">
                                            <div class="col s12" style="text-align:center;text-transform:uppercase;font-size:xx-large">Limitar Gastos</div>
                                        </div>
                                        <div class="row">
                                            <div class="input-field col s4 offset-s4">
                                                <i class="material-icons prefix">attach_money</i>
                                                <input placeholder="Preço" id="txtPrecoLimite" type="text" class="validate dinheiro" runat="server">
                                                <label for="txtPreco">Preço</label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:Button ID="btnConfirmarLimite" CssClass="waves-effect waves-light btn" runat="server" Text="Confirmar" OnClick="btnConfirmarLimite_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <br/>
                </div>
            </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $('select').formSelect();

        });

        $('.dinheiro').mask('#.##0,00', { reverse: true });

    </script>
</asp:Content>