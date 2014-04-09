<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sugestoes.ascx.cs" Inherits="Site.Controles.Sugestoes" %>
<%@ Register Src="~/Controles/Produto.ascx" TagPrefix="uc1" TagName="Produto" %>

<div class="seta" style="float: left;">
    <img src="../App_Themes/Padrao/Imagens/seta-produto-esquerda.png" />
</div>
<div id="divSeletorProdutos" style="float: left;">
    <asp:Repeater runat="server" ID="rptProdutos">
        <ItemTemplate>
            <div style="float: left;">
                <uc1:Produto runat="server" ID="Produto" ImageURL='<%# Bind("NomeArquivo") %>' NomeProduto='<%# Bind("Nome") %>' ValorProduto='<%# Bind("ValorProduto") %>' />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="seta" style="float:right">
    <img src="../App_Themes/Padrao/Imagens/seta-produto-direita.png" />
</div>
