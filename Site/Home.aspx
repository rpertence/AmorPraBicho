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
    <div style="min-height: 1000px; margin-top: 30px; overflow: hidden;">
        <div style="float: left;">
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
        </div>
        <div style="float: left; text-align: right;">
            <ul>
                <asp:Repeater ID="rptBannerLateral" runat="server" DataSourceID="odsBannerLateral">
                    <ItemTemplate>
                        <li style="padding-bottom: 10px;">
                            <a href='<%# DataBinder.Eval(Container.DataItem, "banner_url") %>'>
                                <img id="Img1" runat="server" src='<%# ((Site.BasePage)this).CaminhoADMS + "App_Themes/ActioAdms/hd/banner_loja/" + DataBinder.Eval(Container.DataItem, "banner_arquivo") %>' />
                            </a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:ObjectDataSource ID="odsBannerLateral" runat="server" SelectMethod="SelectAllActive" TypeName="Actio.Negocio.Banner_loja">
                <SelectParameters>
                    <asp:Parameter Name="tipo" DefaultValue="1" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
