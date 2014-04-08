<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Site.Produto" MasterPageFile="~/Master/Site.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery.corner.js"></script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="produtoTopo">
        <div id="produtoImagens"></div>
        <div id="produtoInformacoes">
            <div id="produtoNome">
                <asp:Label ID="lblNomeProduto" runat="server" Text="NOME DO PRODUTO"></asp:Label>
            </div>
            <div id="produtoEstrelas">
            </div>
            <div id="produtoResumo">
                <asp:Label ID="lblResumoProduto" runat="server" Text="A Roda de Exercício Abdominal Pretorian Perfomance garante um ótimo trabalho para os músculos. Com pegada anatômica e rodas duplas, o equipamento é uma excelente escolha para se exercitar."></asp:Label>
            </div>
            <div id="produtoVejaMais">
                <a href="#produtoDescricao">Veja mais características</a>
            </div>
            <div id="produtoPrecoComprarContainer">
                <div id="produtoPreco">
                    <p>Por:
                        <asp:Label ID="lblPreco" CssClass="produtoPreco2" runat="server" Text="R$ 71,90"></asp:Label></p>
                    <p>ou
                        <asp:Label ID="lblCondicoesPagto" runat="server" Text="3x de R$ 23,96"></asp:Label></p>
                </div>
                <div id="produtoComprar">
                    <img src="App_Themes/Padrao/Imagens/botao-comprar.png" />
                </div>
            </div>
            <div id="produtoFretePrazoContainer">
                <div id="produtoPesquisaFrete">
                    <span>Frete e prazo:</span>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFretePrazo" runat="server" MaxLength="8" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imbOK" runat="server" ImageUrl="~/App_Themes/Padrao/Imagens/cep-ok.png" />
                </div>
            </div>
        </div>
    </div>
    <div id="produtoMeio"></div>
    <div id="produtoFooter">
        <div id="produtoSugerimos"></div>
        <div id="produtoMesmaMarca"></div>
        <div id="produtoOpinioes"></div>
    </div>
</asp:Content>
