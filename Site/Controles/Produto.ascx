<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Produto.ascx.cs" Inherits="Site.Controles.Produto" %>
<div style="width: 180px; text-align: center; line-height: 20px;">
    <img src="<%= string.Format("{0}App_Themes\\ActioAdms\\hd\\produtos\\icones\\{1}", ((Site.BaseUserControl)this).Pagina.CaminhoADMS, this.ImageURL) %>" width="110" height="110" /><br />
    <span style="font-size: 14px;"><%= this.NomeProduto %><br /></span><br />
    <img src="../App_Themes/Padrao/Imagens/adicionar-ao-carrinho.png" class="ImageComOver" style="cursor: pointer;" /><br />
    por&nbsp;&nbsp;&nbsp;<span style="color: green; font-size: 20px;"><%= this.ValorProduto.ToString("C") %></span><br />
    ou 3x de <%= string.Format("{0:C}", this.ValorProduto / 3) %>
</div>
