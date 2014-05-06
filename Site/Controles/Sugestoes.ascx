<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sugestoes.ascx.cs" Inherits="Site.Controles.Sugestoes" %>
<%@ Register Src="~/Controles/Produto.ascx" TagPrefix="uc1" TagName="Produto" %>

<span class="als-prev">
    <img src="../App_Themes/Padrao/Imagens/seta-produto-esquerda.png" alt="anterior" />
</span>
<div class="als-viewport">
    <ul class="als-wrapper">
        <asp:Repeater runat="server" ID="rptProdutos">
            <ItemTemplate>
                <li class="als-item">
                    <uc1:Produto runat="server" ID="Produto" ImageURL='<%# Bind("NomeArquivo") %>' NomeProduto='<%# Bind("Nome") %>' ValorProduto='<%# Bind("ValorProduto") %>' CodigoProduto='<%# FormataCodigoProduto(Container.DataItem) %>' />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<span class="als-next">
    <img src="../App_Themes/Padrao/Imagens/seta-produto-direita.png" alt="próximo" />
</span>
