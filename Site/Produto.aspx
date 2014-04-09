<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Site.Produto" MasterPageFile="~/Master/Site.Master" %>
<%@ Register Src="~/Controles/Sugestoes.ascx" TagPrefix="uc1" TagName="Sugestoes" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery.corner.js"></script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="produtoTopo">
        <div id="produtoImagens"></div>
        <div id="produtoInformacoes">
            <div id="produtoNome">
                <asp:Label ID="lblNomeProduto" runat="server" Text="Nome do Produto"></asp:Label>
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
                    <p>
                        Por:
                        <asp:Label ID="lblPreco" CssClass="produtoPreco2" runat="server" Text="R$ 71,90"></asp:Label>
                    </p>
                    <p>
                        ou
                        <asp:Label ID="lblCondicoesPagto" runat="server" Text="3x de R$ 23,96"></asp:Label>
                    </p>
                </div>
                <div id="produtoComprar">
                    <img src="App_Themes/Padrao/Imagens/botao-comprar.png" />
                </div>
            </div>
            <div id="produtoFretePrazoContainer">
                <div id="produtoPesquisaFrete">
                    <img src="App_Themes/Padrao/Imagens/frete.png" />
                    <span>Frete e prazo:</span>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCEP" runat="server" CssClass="fretePrazoTxt" MaxLength="8"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imbOK" runat="server" ImageUrl="~/App_Themes/Padrao/Imagens/cep-ok.png" />
                </div>
                <div id="produtoFretePrazoResultado">
                    <div id="produtoPrazoEntregaContainer">
                        <span>Prazo de entrega em até</span>
                        <asp:Label ID="lblPrazoEntrega" runat="server" CssClass="prazoEntregaResultado" Text="10 dias úteis"></asp:Label>
                    </div>
                    <div><a href="#">Entenda o prazo</a></div>
                    <div id="produtoValorFreteContainer">
                        <span>Valor do Frete para o CEP </span>
                        <asp:Label ID="lblCEPDigitado" runat="server" CssClass="cepDigitado"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblValorFrete" runat="server" CssClass="prazoEntregaResultado" Text="Frete Grátis"></asp:Label>
                    </div>
                    <div><a href="#produtoPesquisaFrete" id="linkPesquisarOutroCEP">pesquisar outro CEP</a></div>
                </div>
            </div>
        </div>
    </div>
    <div id="produtoMeio">
        <div id="produtoDescricao">
            <div id="produtoAbaDescricao"><span class="valignCenter">Descrição</span></div>
            <div id="produtoConteudoDescricao">
                <asp:Label ID="lblConteudoDescricao" runat="server" Text="Assento Tubline para cães e gatos e adaptável à maioria dos assentos de automóveis."></asp:Label>
            </div>
        </div>
        <div id="produtoComentariosFacebook">
            AQUI VIRÁ O PLUGIN DO FACEBOOK
        </div>
    </div>
    <div id="produtoFooter">
        <div class="produtoSugerimos">
            <div class="produtoTituloSugerimos">
                <div class="tituloSugerimosMesmaMarca"><span>Sugerimos Também</span></div>
                <div class="bordaPontilhada" style="width:710px;">&nbsp;</div>
            </div>
            <div class="produtoSugestoesContainer">
                <uc1:Sugestoes runat="server" ID="ucSugestoes" QtdeProdutos="5" />
            </div>
        </div>
        <div class="produtoSugerimos">
            <div class="produtoTituloSugerimos">
                <div class="tituloSugerimosMesmaMarca"><span>Outros Produtos da Marca</span></div>
                <div class="bordaPontilhada" style="width:630px;">&nbsp;</div>
            </div>
            <div class="produtoSugestoesContainer">
                <uc1:Sugestoes runat="server" ID="ucMesmaMarca" QtdeProdutos="5" />
            </div>
        </div>
        <div id="produtoOpinioes"></div>
    </div>
</asp:Content>
