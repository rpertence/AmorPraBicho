<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banners.ascx.cs" Inherits="Site.Controles.Banners" %>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script src="/Controles/banner/jquery.bxslider.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.bxslider').bxSlider();
    });
</script>
<link href="/Controles/banner/jquery.bxslider.css" rel="stylesheet" />
<ul class="bxslider">
    <asp:Repeater ID="rptBanner" runat="server" DataSourceID="odsBanner">
        <ItemTemplate>
            <li><a href='<%# DataBinder.Eval(Container.DataItem, "banner_url") %>'>
                <img runat="server" src='<%# ((Site.BaseUserControl)this).Pagina.CaminhoADMS + "App_Themes/ActioAdms/hd/banner_loja/" + DataBinder.Eval(Container.DataItem, "banner_arquivo") %>' />
            </a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<asp:ObjectDataSource ID="odsBanner" runat="server" SelectMethod="SelectAllActive" TypeName="Actio.Negocio.Banner_loja"></asp:ObjectDataSource>
