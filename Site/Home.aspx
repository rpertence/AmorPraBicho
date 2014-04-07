<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Site.Home" %>

<%@ Register Src="~/Controles/Banners.ascx" TagPrefix="uc1" TagName="Banners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Banners runat="server" id="Banners" />
    <div id="divQualidades">
        <img src="App_Themes/Padrao/Imagens/agilidade.png" />
        <img src="App_Themes/Padrao/Imagens/qualidade.png" />
        <img src="App_Themes/Padrao/Imagens/facilidade.png" />
        <img src="App_Themes/Padrao/Imagens/5_desconto.png" />
    </div>
</asp:Content>
