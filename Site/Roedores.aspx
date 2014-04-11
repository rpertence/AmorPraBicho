<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Roedores.aspx.cs" Inherits="Site.Roedores" %>
<%@ Register Src="~/Controles/Banners.ascx" TagPrefix="uc1" TagName="Banners" %>
<%@ Register Src="~/Controles/Busca.ascx" TagPrefix="uc1" TagName="Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Banners runat="server" ID="Banners" Bicho="Roedor" Tipo="PaginaDoBicho" />
    <div id="divQualidades">
        <img src="App_Themes/Padrao/Imagens/agilidade.png" />
        <img src="App_Themes/Padrao/Imagens/qualidade.png" />
        <img src="App_Themes/Padrao/Imagens/facilidade.png" />
        <img src="App_Themes/Padrao/Imagens/5_desconto.png" />
    </div>
    <uc1:Busca runat="server" ID="Busca" Bicho="Roedor" Tipo="PaginaDoBicho" />
</asp:Content>
