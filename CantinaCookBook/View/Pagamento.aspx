<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagamento.aspx.cs" Inherits="CantinaCookBook.View.Pagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvFundo" runat="server" style="position:fixed; width:100%; height:100%; z-index:1001; background-color:black; opacity:40%;">
    </div>
    <div style="background-color:white; margin:auto; width:50%; padding:0px; min-height:30%; margin-top:10%; margin-left:25%; position:fixed; z-index:1100;" id="dvFinalizar" runat="server">
        <div class="row">
            <div class="row">
                <div class="col s4 offset-s4" style="text-align:center; font-size:32px;">
                    PAGAMENTO
                </div>
            </div>
            <div class="row" id="dvPagamentoFinalizar" runat="server">
                <div class="input-field col s3 offset-s1">
                    <i class="material-icons prefix">attach_money</i>
                    <input placeholder="Valor Pendente." id="txtValorPendente" type="text" class="validate" runat="server">
                    <label for="txtValorPendente">Valor Pendente</label>
                </div>
                <div class="input-field col s3 offset-s3">
                    <i class="material-icons prefix">attach_money</i>
                    <input placeholder="Valor Recebido" id="txtValorRecebido" type="text" class="validate dinheiro" runat="server">
                </div>
                <div class="row">
                    <div class="col s11 offset-s1" style="margin-top:2%;">
                        <asp:Button ID="btnConfirmarPagamento" runat="server" Text="Confirmar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em; cursor:pointer;" OnClick="btnConfirmarPagamento_Click"/>
                    </div>
                </div>
            </div>
            <div class="row" id="dvResultadoPagamento" runat="server"> 
                <div class="row" style="font-size:18px; text-align:center; ">
                    Atenção ! O valor informado foi maior que o valor pendente. Oquê deseja fazer ?
                </div>
                <br/>
                <div class="row">
                    <div class="col s4 offset-s4" style="text-align:center; font-size:72px; font-weight:bold;" id="dvValorResultado" runat="server">
                        
                    </div>
                </div>
                <div class="row">
                    <div class="col s2 offset-s1" style="margin-top:2%; margin-left:30%;">
                        <asp:Button ID="btnAdicionarCredito" runat="server" Text="Adicionar Crédito" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 9em;cursor:pointer;" OnClick="btnAdicionarCredito_Click"/>
                    </div>
                    <div class="col s2 offset-s1" style="margin-top:2%;">
                        <asp:Button ID="btnTroco" runat="server" Text="Troco" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;cursor:pointer;" OnClick="btnTroco_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col s8 offset-s2" runat="server" id="dvPanels">
                <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
            </div>
        </div>
        <div class="row" id="dvPesquisa" runat="server">
            <div class="input-field col s6">
                <i class="material-icons prefix">person</i>
                <input placeholder="Cliente" id="txtCliente" type="text" class="validate" runat="server">
                <label for="txtCliente">Cliente</label>
            </div>
            <div>
                <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;cursor:pointer;" OnClick="btnBuscarCliente_Click"/>
            </div>
            <div>
                <asp:Button ID="btnCancelarCliente" runat="server" Text="Cancelar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;cursor:pointer;" OnClick="btnCancelarCliente_Click"/>
            </div>
        </div>
        <div class="row" id="dvUsuarios" runat="server">
            <div class="col s12">
                <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnConfirmarUsuario"  runat="server" Font-Size="18px" OnClick="btnConfirmarUsuario_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Confirmar Usuário" style="font-size:18px; color:green;">check</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                        <asp:BoundField DataField="CPF" HeaderText="CPF"/>
                        <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento"/>
                        <asp:BoundField DataField="Celular" HeaderText="Celular"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row" id="dvValores" runat="server">
            <div class="card col s12" id="dvValorPendencia" runat="server">
                <div class="col s10" style="text-align:center;">
                    <asp:Label ID="lblReais" runat="server" Text="R$" Font-Size="72px" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblValor" runat="server" Text="0.00" Font-Size="72px" Font-Bold="true"></asp:Label>
                </div>
                <div class="col s2" style="margin-top:15px;">
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
                <br/>
                <br/>
                <br/>
                <div class="row">
                    <div class="col s9 offset-s3">
                        <div class="col s3">
                            <asp:Button ID="btnRealizarPagamento" runat="server" Text="Pagamento" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;cursor:pointer;" OnClick="btnRealizarPagamento_Click"/>
                        </div>
                        <div class="col s3">
                            <asp:Button ID="btnImprimirFolhaPagamento" runat="server" Text="Imprimir" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;cursor:pointer;" OnClick="btnImprimirFolhaPagamento_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div id="dvHistóricoPagamento" runat="server" class="card col s12" style="max-height:600px;overflow-x:hidden; overflow-y:auto;">
                <div class="row">
                    <h3 style="text-align:center;">HISTÓRICO</h3>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="txtEhCredito" runat="server" />
    <asp:HiddenField ID="txtIdCliente" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        $('.dinheiro').mask('#.##0,00', { reverse: true });

        $(document).ready(function () {

            setTimeout(function () { $("#ContentPlaceHolder1_dvPanels").hide(); }, 5000);

        }); 

    </script>
</asp:Content>
