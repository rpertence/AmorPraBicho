<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Busca.ascx.cs" Inherits="Site.Controles.Busca" %>
<style type="text/css">
    #divBuscaAlternativa {
        float: left;
    }

    #divBuscaAlternativaInterna {
        border: solid 2px rgb(231, 231, 231);
        height: 200px;
        margin-top: -3px;
        margin-left: 1px;
        -webkit-border-bottom-right-radius: 10px;
        -webkit-border-bottom-left-radius: 10px;
        -moz-border-radius-bottomright: 10px;
        -moz-border-radius-bottomleft: 10px;
        border-bottom-right-radius: 10px;
        border-bottom-left-radius: 10px;
        padding: 10px 20px 20px 20px;
    }
        #divBuscaAlternativaInterna li {
            color: rgb(116, 116, 116);
            padding: 10px 0px 5px 0px;
            border-bottom: solid 1px rgb(231, 231, 231);
            font-weight: bold;
        }
</style>
<div style="height: 300px; padding-top: 30px;">
    <div id="divBuscaAlternativa">
        <img src="../App_Themes/Padrao/Imagens/busque-por-aqui.png" />
        <div id="divBuscaAlternativaInterna">
            <ul>
                <asp:Repeater runat="server" ID="rptCategorias" DataSourceID="odsCategoria">
                    <ItemTemplate>
                        <li>
                            <asp:Label runat="server" ID="lblCategoria" Text='<%# Bind("titulo") %>' />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:ObjectDataSource ID="odsCategoria" runat="server" SelectMethod="SelectAll" TypeName="Actio.Negocio.Produtos_Categoria"></asp:ObjectDataSource>
        </div>
    </div>
</div>
