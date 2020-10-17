<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastroVendas.aspx.cs" Inherits="CantinaCookBook.View.CadastroVendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col s8 offset-s2" runat="server" id="dvPanels">
                <div class="card-panel #a5d6a7 green lighten-3" style="text-align:center;" id="dvSucesso" runat="server"></div>
                <div class="card-panel red lighten-3" style="text-align:center;" id="dvAlerta" runat="server"></div>
            </div>
        </div>
        <div class="row" id="dvAcoes" runat="server" style="margin-bottom:0;">
            <div class="col s1">
                <asp:ImageButton ID="btnAdicionar" runat="server" ImageUrl="../Img/botoes/adicionar.jpg" OnClick="btnAdicionar_Click" ToolTip="Nova pendência" Height="50px" Width="50px"/>
            </div>
            <div class="col s1">
                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="../Img/botoes/cancelar.jpg" OnClick="btnCancelar_Click" ToolTip="Cancelar pendência" Height="50px" Width="50px" AccessKey=""/>
            </div>
        </div>
        <div class="row" id="dvCliente" runat="server" style="margin-bottom:0;">
            <div class="row">
                <div class="input-field col s6">
                    <i class="material-icons prefix">person</i>
                    <input placeholder="Cliente" id="txtCliente" type="text" class="validate" runat="server">
                    <label for="txtCliente">Cliente</label>
                </div>
                <div class="input-field col s1">
                    <asp:Button ID="btnConfirmarCliente" runat="server" Text="Buscar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;" OnClick="btnConfirmarCliente_Click"/>
                </div>
            </div>
            <div class="row">
                <div class="col s12">
                <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="false" >
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelecionar"  runat="server" Font-Size="18px" OnClick="btnSelecionar_Click" CommandArgument='<%# Eval("IdCliente") %>'><i class="large material-icons" title="Selecionar Usuário" style="font-size:18px; color:green;">check</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome"/>
                        <asp:BoundField DataField="CPF" HeaderText="CPF"/>
                        <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento"/>
                    </Columns>
                </asp:GridView>
            </div>
            </div>
        </div>
        <div class="row" id="dvDetalhamento" runat="server" style="margin-bottom:0;">
            <div class="row">
                <div class="input-field col s1">
                    <i class="material-icons prefix">search</i>
                    <input placeholder="Código" id="txtCodigo" type="text" class="validate" runat="server">
                    <label for="txtCodigo">Código</label>
                </div>
                <div class="input-field col s4">
                    <input placeholder="Produto" id="txtProduto" type="text" class="validate" runat="server">
                    <label for="txtProduto">Produto</label>
                </div>
                <div class="input-field col s1">
                    <asp:Button ID="btnPesquisarProduto" runat="server" Text="Buscar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;" OnClick="btnPesquisarProduto_Click"/>
                </div>
                <div class="input-field col s1" style="margin-left:2.5%;">
                    <asp:Button ID="btnCancelarPesquisa" runat="server" Text="Cancelar" style="border: 0px;background-color: #715348;color: white;height: 3em;width: 8em;" OnClick="btnCancelarPesquisa_Click"/>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s2" style="margin-left:3.2%;">
                    <input placeholder="Quantidade" id="txtQuantidade" type="number" class="validate" runat="server">
                </div>
            </div>
            <div class="row" id="dvProduto">
                <asp:GridView ID="grdProduto" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelecionarProduto"  runat="server" Font-Size="18px" OnClick="btnSelecionarProduto_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="Selecionar produto" style="font-size:18px; color:green;">check</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo" HeaderText="Código"/>
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição"/>
                        <asp:BoundField DataField="Preco" HeaderText="Preço"/>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row" id="dvProdutoVenda">
                <asp:GridView ID="grdProdutoVenda" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnExcluirProduto"  runat="server" Font-Size="18px" OnClick="btnExcluirProduto_Click" CommandArgument='<%# Eval("IdProduto") %>'><i class="large material-icons" title="remover produto" style="font-size:18px; color:red;">clear</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo" HeaderText="Código"/>
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição"/>
                        <asp:BoundField DataField="Quantidade" HeaderText="Quantidade"/>
                        <asp:BoundField DataField="Valor" HeaderText="Valor"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row" id="dvTotalizadores" runat="server">
            <div class="col s12">
                <div class="col s3">
                    <div class="col s12">
                        Limite de gastos:
                    </div>
                    <div class="col s12">
                        <h5 id="titleLimiteDiario" runat="server"></h5>
                    </div>
                </div>
                <div class="col s3 offset-s1">
                    <div class="col s12">
                         Quantidade de itens:
                    </div>
                    <div class="col s12">
                        <h5 id="titleQuantidadeItens" runat="server"></h5>
                    </div>
                </div>
                <div class="col s3 offset-s1">
                    <div class="col s12">
                        Valor total:
                    </div>
                    <div class="col s12">
                        <h5 id="titleValorTotal" runat="server"></h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:ImageButton ID="btnEncerrarVenda" runat="server" ImageUrl="../Img/botoes/botaoEncerrar.jpg" OnClick="btnEncerrarVenda_Click" Height="75" Width="150"/>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script>

        $(document).ready(function () {

            setTimeout(function () { $("#ContentPlaceHolder1_dvPanels").hide(); }, 5000);

        }); 

    </script>
</asp:Content>
