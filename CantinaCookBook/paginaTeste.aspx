<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paginaTeste.aspx.cs" Inherits="CantinaTCC.paginaTeste" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Página Teste</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <h1>Teste de criptografia</h1>
            <br/>
            <br/>
            <div>
                <asp:Label ID="lblTexto" runat="server" Text="Texto para criptografar"></asp:Label>
                <input id="txtTexto" runat="server" type="text" style="margin-left:5px" name="txtTexto" />
                <asp:Button ID="btnTestar" runat="server" style="margin-left:5px" Text="Criptografar" OnClick="btnTestar_Click" />
            </div>
            <br />
            <div>
                <asp:Label ID="lblTexto2" runat="server" Text="Texto para comparar"></asp:Label>
                <input id="txtTexto2" runat="server" type="text" style="margin-left:5px" name="txtTexto2" />
                <asp:Button ID="btnComparar" runat="server" style="margin-left:5px" Text="Comparar" OnClick="btnComparar_Click" />
            </div>
            <div>
                <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
            </div>
        </form>
    </body>
</html>
