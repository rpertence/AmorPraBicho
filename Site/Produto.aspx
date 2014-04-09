<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Site.Produto" MasterPageFile="~/Master/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/Controles/Sugestoes.ascx" TagPrefix="uc1" TagName="Sugestoes" %>
<%@ Register Src="~/Controles/AvaliacaoLeitura.ascx" TagPrefix="uc1" TagName="AvaliacaoLeitura" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery.corner.js"></script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                <div class="bordaPontilhada" style="width: 710px;">&nbsp;</div>
            </div>
            <div class="produtoSugestoesContainer">
                <uc1:Sugestoes runat="server" ID="ucSugestoes" QtdeProdutos="5" />
            </div>
        </div>
        <div class="produtoSugerimos">
            <div class="produtoTituloSugerimos">
                <div class="tituloSugerimosMesmaMarca"><span>Outros Produtos da Marca</span></div>
                <div class="bordaPontilhada" style="width: 640px;">&nbsp;</div>
            </div>
            <div class="produtoSugestoesContainer">
                <uc1:Sugestoes runat="server" ID="ucMesmaMarca" QtdeProdutos="5" />
            </div>
        </div>
        <div id="produtoOpinioes" class="produtoSugerimos">
            <div class="produtoTituloSugerimos">
                <div class="tituloSugerimosMesmaMarca"><span>Opiniões dos Clientes</span></div>
                <div class="bordaPontilhada" style="width: 690px;">&nbsp;</div>
            </div>
            <div id="produtoRating">
                <div id="produtoRatingEsquerda" style="float: left;">
                    <div>
                        <asp:Rating ID="rateReadOnly" runat="server" ReadOnly="true" MaxRating="5" CurrentRating="<%# CalculaMediaNotas() %>"
                            CssClass="ratingStar" StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                            EmptyStarCssClass="Empty" AutoPostBack="false">
                        </asp:Rating>
                    </div>
                    <br />
                    <div class="barras">
                        <div class="textoBarra">
                            <div style="float: left;"><span>5 estrelas</span></div>
                            <div class="barraAvaliacao"></div>
                            <div style="float: right;"></div>
                            <asp:Label ID="lblQtde5Estrelas" runat="server"></asp:Label>
                        </div>
                        <div class="textoBarra">
                            <div style="float: left;"><span>4 estrelas</span></div>
                            <div class="barraAvaliacao"></div>
                            <div style="float: right;"></div>
                            <asp:Label ID="lblQtde4Estrelas" runat="server"></asp:Label>
                        </div>
                        <div class="textoBarra">
                            <div style="float: left;"><span>3 estrelas</span></div>
                            <div class="barraAvaliacao"></div>
                            <div style="float: right;"></div>
                            <asp:Label ID="lblQtde3Estrelas" runat="server"></asp:Label>
                        </div>
                        <div class="textoBarra">
                            <div style="float: left;"><span>2 estrelas</span></div>
                            <div class="barraAvaliacao"></div>
                            <div style="float: right;"></div>
                            <asp:Label ID="lblQtde2Estrelas" runat="server"></asp:Label>
                        </div>
                        <div class="textoBarra">
                            <div style="float: left;"><span>1 estrela </span></div>
                            <div class="barraAvaliacao" style="margin-left: 13px;"></div>
                            <div style="float: right;"></div>
                            <asp:Label ID="lblQtde1Estrela" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="produtoRatingDireita">
                    <div id="deSuaOpiniao">
                        <div id="labelDeSuaOpiniao"><span>Dê sua opinião! O que achou do Produto?</span></div>
                        <div id="imagemAvaliarProduto">
                            <img id="avaliarProduto" src="App_Themes/Padrao/Imagens/botao-avaliar-o-produto.png" class="ImageComOver" /></div>
                    </div>
                    <div id="cadastroOpiniao">
                        <span>Nota: </span>
                        <br />
                        <br />
                        <asp:Rating ID="rateEnabled" runat="server" MaxRating="5" CurrentRating="0"
                            CssClass="ratingStar" StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                            EmptyStarCssClass="Empty" AutoPostBack="true">
                        </asp:Rating>
                        <br />
                        <span>Comente sobre o produto:</span>
                        <br />
                        <br />
                        <div style="float:left;margin-right:10px;"><asp:TextBox ID="txtOpiniaoProduto" runat="server" Width="350px" Height="60px" TextMode="MultiLine" MaxLength="500"></asp:TextBox></div>
                        <div style="margin-top:40px;"><asp:Button ID="btnSalvarAvaliacao" runat="server" Text="Salvar" /></div>
                    </div>
                </div>
            </div>
            <div id="produtoLeituraAvaliacoes">
                <%--<asp:Repeater ID="rptAvaliacoes" runat="server">
                    <ItemTemplate>
                        <uc1:AvaliacaoLeitura runat="server" ID="ucAvaliacao" />
                    </ItemTemplate>
                </asp:Repeater>--%>
            </div>
        </div>
    </div>
</asp:Content>
