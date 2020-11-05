<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="CantinaCookBook.View.UserHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main runat="server" id="main">
                <div class="container" runat="server">
                    <div class="row">
                        <div class="col s8 offset-s2" runat="server" id="dvPanels">
                            <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                            <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
                        </div>
                    </div>
                    <div id="dvSelecionarOutro" runat="server">
                        <div class="row">
                            <div style="padding:0px; float:right;">
                                <asp:Button ID="btnEscolherDependente" runat="server" Text="Escolher Dependente" CssClass="waves-effect waves-light btn-small grey lighten-1" OnClick="btnEscolherDependente_Click"/>
                            </div>
                        </div>
                    </div>
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
                                    <div class="col s12 header card" id="dvNomeUsuario" runat="server" style="text-align:center;text-transform:uppercase;font-size:xx-large"></div>
                                </div>
                                <div class="row">
                                    <div id="dvPagamento" class="card col s12">
                                        <h4 style="margin-top: 5px;">VALOR PENDENTE/CRÉDITO</h4>
                                        <br/>
                                        <div class="col s10" style="text-align:center;">
                                            <div class="card col s3" style="background-color: #a9a9a900;margin-left: 47%;">
                                                <asp:Label ID="lblReais" runat="server" Text="R$" Font-Size="72px" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblValor" runat="server" Text="0.00" Font-Size="72px" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col s2" style="margin-top:15px;text-align:left;">
                                            <div class="row">
                                                <div class="col s3">
                                                    <div style="background-color:red; width:30px; height:30px; border: solid 1px;">
                                                    </div>
                                                </div>
                                                <div class="col s9">
                                                    <div style="margin-top:3px;">Valor pendente.</div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col s3">
                                                    <div style="background-color:green; width:30px; height:30px; border: solid 1px;">
                                                    </div>
                                                </div>
                                                <div class="col s9">
                                                    <div style="margin-top:3px;">Crédito.</div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="dvHistórico" class="card col s12" style="min-height:500px;max-height:600px; overflow:auto;" runat="server">
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
                                            <div id="titleLimiteGastos" runat="server" class="col s6 offset-s3" style="text-align:center;text-transform:uppercase;font-size:xx-large">Limitar Gastos Diários</div>
                                            <div id="observacaoLimite" runat="server" class="col s2 offset-s1 red-text">Valor de R$ 0,00 será considerado sem limite de gasto !</div>
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
                    <div id="dvAdministrador" runat="server">
                        <div class="row card" style="padding:25px;min-height:500px">
                            <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAutenticar" runat="server" Font-Size="18px" OnClick="btnAutenticar_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Autenticar" style="font-size:18px">lock_open</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                                    <asp:BoundField DataField="CPF" HeaderText="CPF"/>
                                    <asp:BoundField DataField="Email" HeaderText="Email"/>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnSql" runat="server" Text="🛠" Width="25px" Height="25px" OnClick="btnSql_Click" style="float:left;font-size:15px;color:white;background-color:#715348;bottom:0;left:0;"/>
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

            setTimeout(function () { $("#ContentPlaceHolder1_dvPanels").hide(); }, 3000);

        });

        $('.dinheiro').mask('#.##0,00', { reverse: true });

    </script>
</asp:Content>