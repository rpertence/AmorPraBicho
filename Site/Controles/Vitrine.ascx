<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Vitrine.ascx.cs" Inherits="Site.Controles.Vitrine" %>
<%@ Register Src="~/Controles/Produto.ascx" TagPrefix="uc1" TagName="Produto" %>

<asp:HyperLink runat="server" ID="imgBicho" /><br /><br />
<asp:Repeater runat="server" ID="rptProdutos">
    <ItemTemplate>
        <div style="float: left;">
            <uc1:Produto runat="server" ID="Produto" ImageURL='<%# Bind("NomeArquivo") %>' NomeProduto='<%# Bind("Nome") %>' ValorProduto='<%# Bind("ValorProduto") %>' />
        </div>
    </ItemTemplate>
</asp:Repeater>
