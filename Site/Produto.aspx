﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Site.Produto" MasterPageFile="~/Master/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/Controles/Sugestoes.ascx" TagPrefix="uc1" TagName="Sugestoes" %>
<%@ Register Src="~/Controles/LeituraAvaliacao.ascx" TagPrefix="uc1" TagName="LeituraAvaliacao" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <title>PRODUTO TESTE</title>
    <%--<link rel="canonical" />--%>
    <script src="Scripts/jquery.corner.js"></script>
    <script src="Scripts/jquery.als-1.4.min.js"></script>
    <script type="text/javascript" src="http://www.youtube.com/player_api"></script>

    <%--Facebook--%>
    <script src="http://static.ak.fbcdn.net/connect.php/js/FB.Share" type="text/javascript"></script>
    <%--Twitter--%>
    <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>

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

            //Esconde a div de miniaturas se não houver fotos cadastradas para o produto.
            if ($("#my-als-list-fotos ul li").length == 0)
                $("#my-als-list-fotos").hide();

            $(".iconeVideo").click(function () {
                //Esconde a div de exibição de fotos.
                $("#produtoFotoAmpliada").hide();
                //Exibe a div do vídeo.
                $("#produtoVideo").show();
            });

            //Selecionando cor default
            var hiddenCor = $("[id$='hdfCor']");
            if (hiddenCor != null && hiddenCor.val() == "" && $(".divCorParent") != null)
                selecionarCor($(".divCorParent").first());
            else if (hiddenCor != null && hiddenCor.val != "")
                selecionarCorByHex(hiddenCor.val());

            //Evento de seleção de cor
            $(".divCorParent").click(function () {
                selecionarCor($(this));
            });

            //Montando link para compartilhar nas redes sociais


            //Salvar Avaliação do Produto
            $("[id$='btnSalvarAvaliacao']").click(function () {
                //Validações
                var nota = $("[id$='rateEnabled']").find("span.ratingItem.Filled").length;
                if (nota == 0) {
                    alert('Preencha uma nota para o produto.');
                    return false;
                }
            });
        });

        function pauseVideo() {
            $("object").each(function (index) {
                obj = $(this).get(0);
                if (obj.pauseVideo) obj.pauseVideo();
            });
        }

        function selecionarCor(divCor) {
            //Seta a borda da cor selecionada e volta a borda das outras cores.
            divCor.css('border', 'solid 1px #000');
            var id = divCor.attr('id');
            $(".divCorParent").each(function (i) {
                if ($(this).attr('id') != id)
                    $(this).css('border', 'solid 1px #FFF');
            });
            //Recupera o código hexadecimal da cor e seta valor do hidden field.
            var hexCor = colorToHex(divCor.find("div")[0].style['backgroundColor']);
            var hiddenCor = $("[id$='hdfCor']");
            if (hiddenCor != null) {
                hiddenCor.val(hexCor);
            }
        }

        function selecionarCorByHex(hexCor) {
            $(".divCorParent").each(function (i) {
                if (colorToHex($(this).find("div")[0].style['backgroundColor']) == hexCor) {
                    selecionarCor($(this));
                    return false;
                }
            });
        }

        //converte cor no formato rgb(r,g,b) para hexadecimal (#rrggbb)
        function colorToHex(color) {
            var parts = color.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
            delete (parts[0]);
            for (var i = 1; i <= 3; ++i) {
                parts[i] = parseInt(parts[i]).toString(16);
                if (parts[i].length == 1) parts[i] = '0' + parts[i];
            }
            return '#' + parts.join('');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <meta property="og:title" content="This is the Title of the Page" />
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
                        <div style="float: left; width: 80px; margin-top: 2px;">
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
                            <asp:ImageButton ID="imbComprar" runat="server" ImageUrl="~/App_Themes/Padrao/Imagens/botao-comprar.png" OnClick="imbComprar_Click" />
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
                            <asp:Label ID="lblEscolhaCor" runat="server" Text="escolha a cor"></asp:Label>
                        </div>
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
                    <br />
                    <div id="produtoRedesSociais">
                        <%--<div class="fb-like" data-href="https://www.facebook.com/amorprabicho" data-width="100" data-layout="button_count" data-action="like" data-show-faces="true" data-share="false"
                            data-colorscheme="light" data-header="true" data-stream="false" data-show-border="true">
                        </div>--%>
                        <%--<div class="fb-share-button" data-width="100" data-type="button_count"></div>--%>
                        <div class="linkRedeSocial" style="width:65px;">
                            <a id="link_share_fb" href="javascript: void(0);" style="width:125px;"
                                onclick="window.open('https://www.facebook.com/sharer/sharer.php?u='+encodeURIComponent(location.href),'facebook-share-dialog', 'toolbar=0, status=0, width=650, height=450');">
                                <img src="App_Themes/Padrao/Imagens/facebook.png" class="ImageComOver" /></a>
                        </div>
                        <div class="linkRedeSocial">
                            <a href="https://twitter.com/share" class="twitter-share-button" data-lang="pt" data-dnt="true">
                                <img src="App_Themes/Padrao/Imagens/twitter.png" class="ImageComOver" /></a>
                        </div>
                        <div class="linkRedeSocial">
                            <g:plusone href=""></g:plusone>
                        </div>
                        <div class="linkRedeSocial">
                            <img src="App_Themes/Padrao/Imagens/email.png" class="ImageComOver" />
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
                    <div style="border-bottom: solid 2px #ECECEC;">
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
                    </div>
                    <div id="produtoLeituraAvaliacoes">
                        <asp:Repeater ID="rptAvaliacoes" runat="server">
                            <ItemTemplate>
                                <div>
                                    <uc1:LeituraAvaliacao runat="server" ID="LeituraAvaliacao" Nota='<%# Bind("nota") %>' TituloAvaliacao='<%# Bind("titulo") %>'
                                        NomeUsuario='<%# Bind("nomeUsuario") %>' DataAvaliacao='<%# Bind("data") %>' Depoimento='<%# Bind("depoimento") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
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
    <%--Google Plus--%>
    <script type="text/javascript">
        window.___gcfg = {
            lang: 'pt-BR',
            parsetags: 'onload'
        };
        (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/plusone.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();
    </script>
</asp:Content>
