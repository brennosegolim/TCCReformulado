<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="CantinaCookBook.View.UserHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main runat="server" id="main">
                <div class="container" runat="server">
                    <div id="dvSelectDependente" runat="server">
                        <div class="row">
                            <br/>
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
                                <div id="dvLimiteCredito" class="card col s5 offset-s1" style="min-height:300px;max-height:300px;" runat="server">
                                    <div class="col s12" style="text-align:center;text-transform:uppercase;font-size:xx-large">Limitar Crédito</div>
                                </div>
                                <div id="dvHistórico" class="card col s5  offset-s1" style="min-height:300px;max-height:300px; overflow:auto;" runat="server">
                                    <div class="row">
                                        <div class="col s12" style="text-align:center;text-transform:uppercase;font-size:xx-large">Histórico</div>
                                    </div>
                                    <div class="row">
                                        <div class="col s12" id="tableHistorico" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('select').formSelect();
        });

    </script>
</asp:Content>