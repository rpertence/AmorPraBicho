<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Busca.ascx.cs" Inherits="Site.Controles.Busca" %>
<%@ Register Src="~/Controles/Produto.ascx" TagPrefix="uc1" TagName="Produto" %>

<style type="text/css">
    #divBuscaAlternativa {
        float: left;
    }

    #divBuscaAlternativaInterna {
        border: solid 2px rgb(231, 231, 231);
        min-height: 200px;
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
            padding: 10px 0px 5px 0px;
            border-bottom: solid 1px rgb(231, 231, 231);
            font-weight: bold;
        }

        #divBuscaAlternativaInterna a {
            color: rgb(116, 116, 116);
            text-decoration: none;
        }

    #divResultadoBusca {
        float: left;
        width: 720px;
    }

    #divOrdenacao {
        float: left;
        -webkit-border-radius: 10px;
        -moz-border-radius: 10px;
        border-radius: 10px;
        border: solid 2px rgb(172, 172, 172);
        padding: 5px 20px 5px 20px;
    }
</style>
<div style="height: 600px; padding-top: 30px;">
    <div id="divBuscaAlternativa">
        <img src="../App_Themes/Padrao/Imagens/busque-por-aqui.png" />
        <div id="divBuscaAlternativaInterna">
            <ul>
                <asp:Repeater runat="server" ID="rptCategorias" DataSourceID="odsCategoria" OnItemCommand="rptCategorias_ItemCommand">
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton runat="server" ID="Categoria" Text='<%# Bind("titulo") %>' CommandArgument='<%# Bind("id") %>' />
                        </li>
                        <ul style="margin-left: 30px;">
                            <asp:Repeater runat="server" ID="rptSubCategorias" OnItemCommand="rptSubCategorias_ItemCommand">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton runat="server" ID="SubCategoria" Text='<%# Bind("titulo") %>' CommandArgument='<%# Bind("id") %>' />
                                    </li>
                                    <ul style="margin-left: 30px;">
                                        <asp:Repeater runat="server" ID="rptMarcas" OnItemCommand="rptMarcas_ItemCommand">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:LinkButton runat="server" ID="Marca" Text='<%# Bind("descricao") %>' CommandArgument='<%# Bind("id") %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:ObjectDataSource ID="odsCategoria" runat="server" SelectMethod="SelectAll" TypeName="Actio.Negocio.Produtos_Categoria"></asp:ObjectDataSource>
        </div>
    </div>
    <div id="divResultadoBusca">
        <div style="height: 65px; padding-left: 35px;">
            <div id="divOrdenacao">Ordenar por:&nbsp;&nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="ddlOrdenacao">
                    <asp:ListItem>menor preço</asp:ListItem>
                    <asp:ListItem>maior preço</asp:ListItem>
                    <asp:ListItem>mais vendidos</asp:ListItem>
                    <asp:ListItem>melhores avaliações</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="float: left; padding: 12px; margin-left: 17px;">
                <asp:Label runat="server" ID="lblResultadoBusca" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="rptResultado">
            <ItemTemplate>
                <div style="float: left; padding-bottom: 40px;">
                    <uc1:Produto runat="server" ID="Produto" ImageURL='<%# Bind("icone") %>' NomeProduto='<%# Bind("ProdDescricao_") %>' ValorProduto='<%# Valor(Container.DataItem) %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
