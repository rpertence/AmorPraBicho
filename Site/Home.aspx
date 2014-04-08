<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Site.Home" %>

<%@ Register Src="~/Controles/Banners.ascx" TagPrefix="uc1" TagName="Banners" %>
<%@ Register Src="~/Controles/Vitrine.ascx" TagPrefix="uc1" TagName="Vitrine" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .DivVitrine {
            height: 325px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Banners runat="server" ID="Banners" />
    <div id="divQualidades">
        <img src="App_Themes/Padrao/Imagens/agilidade.png" />
        <img src="App_Themes/Padrao/Imagens/qualidade.png" />
        <img src="App_Themes/Padrao/Imagens/facilidade.png" />
        <img src="App_Themes/Padrao/Imagens/5_desconto.png" />
    </div>
    <div class="DivVitrine">
        <uc1:Vitrine runat="server" ID="vitrineCachorro" Tipo="Cachorro" QtdeProdutos="4" />
    </div>
    <div class="DivVitrine">
        <uc1:Vitrine runat="server" ID="vitrineGato" Tipo="Gato" QtdeProdutos="4" />
    </div>
    <div class="DivVitrine" style="float: left;">
        <uc1:Vitrine runat="server" ID="vitrineRoedor" Tipo="Roedor" QtdeProdutos="2" />
    </div>
    <div class="DivVitrine">
        <uc1:Vitrine runat="server" ID="vitrinePassaro" Tipo="Passaro" QtdeProdutos="2" />
    </div>
</asp:Content>
