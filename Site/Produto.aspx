<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Site.Produto" MasterPageFile="~/Master/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/Controles/Sugestoes.ascx" TagPrefix="uc1" TagName="Sugestoes" %>
<%@ Register Src="~/Controles/AvaliacaoLeitura.ascx" TagPrefix="uc1" TagName="AvaliacaoLeitura" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery.corner.js"></script>
    <script src="Scripts/jquery.als-1.4.min.js"></script>
    <script type="text/javascript" src="http://www.youtube.com/player_api"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Esconde a div de exibição de vídeo por default.
            $("#produtoVideo").hide();

            //Tratamentos para scroll lists
            var numItensSugestoes = $("#my-als-list ul li").length;
            var numItensMesmaMarca = $("#my-als-list-2 ul li").length;
            var numItensFotos = $("#my-als-list-fotos ul li").length;

            $("#my-als-list").als(
            {
                visible_items: numItensSugestoes > 5 ? 5 : numItensSugestoes,
                scrolling_items: 1,
                circular: numItensSugestoes > 5 ? "yes" : "no"
            });

            $("#my-als-list-2").als(
            {
                visible_items: numItensMesmaMarca > 5 ? 5 : numItensMesmaMarca,
                scrolling_items: 1,
                circular: numItensMesmaMarca > 5 ? "yes" : "no"
            });

            $("#my-als-list-fotos").als(
            {
                visible_items: numItensFotos > 4 ? 4 : numItensFotos,
                orientation: "vertical",
                circular: "yes"
            });

            $("#my-als-list-fotos ul li").click(function () {
                //Esconde a div de exibição do vídeo e pausa o mesmo.
                $("#produtoVideo").hide();
                pauseVideo();
                //Exibe a foto ampliada.
                $("#produtoFotoAmpliada").show();
                var img = $(this).find('img');
                if (img != null) {
                    $("[id$='imgFotoAmpliada']").attr('src', img.attr('src'));
                }
            });

            $(".iconeVideo").click(function () {
                //Esconde a div de exibição de fotos.
                $("#produtoFotoAmpliada").hide();
                //Exibe a div do vídeo.
                $("#produtoVideo").show();
            });

            //Selecionando cor
            $(".divCorParent").click(function () {
                //Seta a borda da cor selecionada e volta a borda das outras cores.
                $(this).css('border', 'solid 1px #000');
                var id = $(this).attr('id');
                $(".divCorParent").each(function (i) {
                    if ($(this).attr('id') != id)
                        $(this).css('border', 'solid 1px #FFF');
                });
                //Recupera o código hexadecimal da cor.
                var hexCor = $(this).children("div")[0].currentStyle['backgroundColor'];
                var hiddenCor = $("[id$='hdfCor']");
                if (hiddenCor != null) {
                    hiddenCor.val(hexCor);
                }
            });
        });

        function pauseVideo() {
            $("object").each(function (index) {
                obj = $(this).get(0);
                if (obj.pauseVideo) obj.pauseVideo();
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:MultiView ID="mvwProduto" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewProduto" runat="server">
            <div id="produtoTopo">
                <div id="produtoImagens">
                    <div class="als-container" id="my-als-list-fotos">
                        <span class="als-prev">
                            <img src="App_Themes/Padrao/Imagens/seta-produto-cima.png" /></span>
                        <div class="als-viewport">
                            <ul class="als-wrapper">
                                <asp:Repeater runat="server" ID="rptFotosProduto" OnItemDataBound="rptFotosProduto_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="als-item">
                                            <asp:Image ID="imgFotoProduto" runat="server" Width="48" Height="48" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <span class="als-next">
                            <img src="App_Themes/Padrao/Imagens/seta-produto-baixo.png" /></span>
                    </div>
                    <div id="produtoFotoAmpliada">
                        <asp:Image ID="imgFotoAmpliada" runat="server" Width="330" Height="330" />
                    </div>
                    <div id="produtoVideo">
                        <object id="frameVideo" data="<%= this.EnderecoVideo %>" width="330" height="330" type="application/x-shockwave-flash">
                            <param name="allowscriptaccess" value="always" />
                            <param name="allowFullScreen" value="true" />
                        </object>
                    </div>
                </div>
                <div id="produtoInformacoes">
                    <div id="produtoNome">
                        <asp:Label ID="lblNomeProduto" runat="server"></asp:Label>
                    </div>
                    <div id="produtoEstrelas">
                        <div style="float: left; width: 80px;">
                            <asp:Rating ID="ratingCabecalho" runat="server" ReadOnly="true" MaxRating="5"
                                CssClass="ratingStar" StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                                EmptyStarCssClass="Empty" AutoPostBack="false">
                            </asp:Rating>
                        </div>
                        <div id="produtoNumAvaliacoes">
                            <asp:Label ID="lblNumAvaliacoes" runat="server"></asp:Label><span> avaliações</span>
                        </div>
                    </div>
                    <div id="produtoResumo">
                        <asp:Label ID="lblResumoProduto" runat="server"></asp:Label>
                    </div>
                    <div id="produtoVejaMais">
                        <a href="#produtoDescricao">Veja mais características</a>
                    </div>
                    <div id="produtoPrecoComprarContainer">
                        <div id="produtoPreco">
                            <p>
                                Por:
                        <asp:Label ID="lblPreco" CssClass="produtoPreco2" runat="server"></asp:Label>
                            </p>
                            <p>
                                ou 3x de
                        <asp:Label ID="lblCondicoesPagto" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="produtoComprar">
                            <img src="App_Themes/Padrao/Imagens/botao-comprar.png" />
                        </div>
                    </div>
                    <div id="produtoFretePrazoContainer" style="display: none;">
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
                <div id="videoCoresRedesSociais">
                    <div id="iconeVideo" runat="server" class="iconeVideo">
                        <asp:Image ID="imgVideo" runat="server" ImageUrl="~/App_Themes/Padrao/Imagens/video.png" CssClass="ImageComOver" />
                    </div>
                    <div id="produtoCores">
                        <div style="float: left; margin-right: 20px; margin-top: 2px;">
                            <asp:Label ID="lblEscolhaCor" runat="server" Text="escolha a cor"></asp:Label></div>
                        <div>
                            <asp:HiddenField ID="hdfCor" runat="server" />
                            <asp:Repeater ID="rptCores" runat="server" OnItemDataBound="rptCores_ItemDataBound">
                                <ItemTemplate>
                                    <div id="divCorParent" runat="server" class="divCorParent">
                                        <div id="divCor" runat="server" class="itemCor"></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div id="produtoMeio">
                <div id="produtoDescricao">
                    <div id="produtoAbaDescricao"><span class="valignCenter">Descrição</span></div>
                    <div id="produtoConteudoDescricao">
                        <span id="spanConteudoDescricao" runat="server"></span>
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
                    <div class="als-container" id="my-als-list">
                        <uc1:Sugestoes runat="server" ID="ucSugestoes" Tipo="MesmaCategoria" QtdeProdutos="10" />
                    </div>
                </div>
                <div class="produtoSugerimos">
                    <div class="produtoTituloSugerimos">
                        <div class="tituloSugerimosMesmaMarca"><span>Outros Produtos da Marca</span></div>
                        <div class="bordaPontilhada" style="width: 640px;">&nbsp;</div>
                    </div>
                    <div class="als-container" id="my-als-list-2">
                        <uc1:Sugestoes runat="server" ID="ucMesmaMarca" Tipo="MesmaMarca" QtdeProdutos="10" />
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
                                <asp:Rating ID="rateReadOnly" runat="server" ReadOnly="true" MaxRating="5"
                                    CssClass="ratingStar" StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                                    EmptyStarCssClass="Empty" AutoPostBack="false">
                                </asp:Rating>
                            </div>
                            <br />
                            <div class="barras">
                                <div class="textoBarra">
                                    <div style="float: left;"><span>5 estrelas</span></div>
                                    <div class="barraAvaliacao">
                                        <div class="barraVerde" style="width: <%= RetornaQtdeAvaliacoes(null) == 0 ? 0 : (RetornaQtdeAvaliacoes(5) / RetornaQtdeAvaliacoes(null)) * 100 %>%;"></div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Label ID="lblQtde5Estrelas" runat="server"><%= RetornaQtdeAvaliacoes(5) %></asp:Label>
                                    </div>
                                </div>
                                <div class="textoBarra">
                                    <div style="float: left;"><span>4 estrelas</span></div>
                                    <div class="barraAvaliacao">
                                        <div class="barraVerde" style="width: <%= RetornaQtdeAvaliacoes(null) == 0 ? 0 : (RetornaQtdeAvaliacoes(4) / RetornaQtdeAvaliacoes(null)) * 100 %>%;"></div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Label ID="lblQtde4Estrelas" runat="server"><%= RetornaQtdeAvaliacoes(4) %></asp:Label>
                                    </div>
                                </div>
                                <div class="textoBarra">
                                    <div style="float: left;"><span>3 estrelas</span></div>
                                    <div class="barraAvaliacao">
                                        <div class="barraVerde" style="width: <%= RetornaQtdeAvaliacoes(null) == 0 ? 0 : (RetornaQtdeAvaliacoes(3) / RetornaQtdeAvaliacoes(null)) * 100 %>%;"></div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Label ID="lblQtde3Estrelas" runat="server"><%= RetornaQtdeAvaliacoes(3) %></asp:Label>
                                    </div>
                                </div>
                                <div class="textoBarra">
                                    <div style="float: left;"><span>2 estrelas</span></div>
                                    <div class="barraAvaliacao">
                                        <div class="barraVerde" style="width: <%= RetornaQtdeAvaliacoes(null) == 0 ? 0 : (RetornaQtdeAvaliacoes(2) / RetornaQtdeAvaliacoes(null)) * 100 %>%;"></div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Label ID="lblQtde2Estrelas" runat="server"><%= RetornaQtdeAvaliacoes(2) %></asp:Label>
                                    </div>
                                </div>
                                <div class="textoBarra">
                                    <div style="float: left;"><span>1 estrela </span></div>
                                    <div class="barraAvaliacao" style="margin-left: 18px;">
                                        <div class="barraVerde" style="width: <%= RetornaQtdeAvaliacoes(null) == 0 ? 0 : (RetornaQtdeAvaliacoes(1) / RetornaQtdeAvaliacoes(null)) * 100 %>%;"></div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Label ID="lblQtde1Estrela" runat="server"><%= RetornaQtdeAvaliacoes(1) %></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="produtoRatingDireita">
                            <div id="deSuaOpiniao">
                                <div id="labelDeSuaOpiniao"><span>Dê sua opinião! O que achou do Produto?</span></div>
                                <div id="imagemAvaliarProduto">
                                    <img id="avaliarProduto" src="App_Themes/Padrao/Imagens/botao-avaliar-o-produto.png" class="ImageComOver" />
                                </div>
                            </div>
                            <div id="cadastroOpiniao">
                                <span>Nota: </span>
                                <br />
                                <br />
                                <asp:Rating ID="rateEnabled" runat="server" MaxRating="5" CurrentRating="0"
                                    CssClass="ratingStar" StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                                    EmptyStarCssClass="Empty" AutoPostBack="false">
                                </asp:Rating>
                                <br />
                                <span>Comente sobre o produto:</span>
                                <br />
                                <br />
                                <div style="float: left; margin-right: 10px;">
                                    <asp:TextBox ID="txtOpiniaoProduto" runat="server" Width="350px" Height="60px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                </div>
                                <div style="margin-top: 40px;">
                                    <asp:Button ID="btnSalvarAvaliacao" runat="server" Text="Salvar" OnClick="btnSalvarAvaliacao_Click" />
                                </div>
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
        </asp:View>
        <asp:View ID="viewProdutoInexistente" runat="server">
            <div id="produtoInexistente">
                <span>Ops! Produto não encontrado.</span>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
