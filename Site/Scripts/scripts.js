/// <reference path="jquery-1.11.0.min.js" />
$(document).ready(function () {
    $('.ImageComOver').mouseover(function () {
        var src = $(this).attr('src');

        if (src.indexOf('-OVER') == -1) {
            src = src.replace('.png', '-OVER.png');
            $(this).attr('src', src);
        }
    }).mouseout(function () {
        var src = $(this).attr('src');

        if (src.indexOf('-OVER') != -1) {
            src = src.replace('-OVER.png', '.png');
            $(this).attr('src', src);
        }
    });

    //âncoras
    $('a[href^=#]').click(function () {
        $($(this).attr('href')).ancora();
        return false;
    });

    //Cantos arredondados das caixas de texto.
    $("[id$='txtCEP']").corner("35px");
    $("[id$='txtNomeAmigo']").corner("10px");
    $("[id$='txtEmailAmigo']").corner("10px");
    $("[id$='txtMensagem']").corner("10px");
    $("[id$='txtNome']").corner("10px");
    $("[id$='txtEmail']").corner("10px");

    //Escondendo/exibindo resultado de frete e prazo de entrega.
    $("#produtoFretePrazoResultado").hide();

    $("[id$='imbOK']").click(function () {
        $("[id$='lblCEPDigitado']").text($("[id$='txtCEP']").val());
        $("#produtoFretePrazoResultado").show();
        return false;
    });

    $("#linkPesquisarOutroCEP").click(function () {
        $("[id$='txtCEP']").focus();
        $("#produtoFretePrazoResultado").hide();
    });

    //Escondendo/exibindo cadastro de opiniões do produto
    $("#cadastroOpiniao").hide();

    $("#avaliarProduto").click(function () {
        $("#deSuaOpiniao").hide();
        $("#cadastroOpiniao").show();
    });

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

    //Evento de clique no envelope
    $("#imgEmail").click(function () {
        $("#produtoFormulario").slideToggle();
    });

    //Salvar Avaliação do Produto
    $("[id$='btnSalvarAvaliacao']").click(function () {
        //Validações
        var nota = $("[id$='rateEnabled']").find("span.ratingItem.Filled").length;
        if (nota == 0) {
            alert('Preencha uma nota para o produto.');
            return false;
        }
    });

    //Texto default das caixas de texto do formulário de e-mail
    $(".textBoxFormulario").placeholder();

    //Validação e envio de e-mail.
    $(".btnEnviarEmail").click(function () {
        var address1 = $("[id$='txtEmailAmigo']").val();
        var address2 = $("[id$='txtEmail']").val();
        var nome1 = $("[id$='txtNomeAmigo']").val();
        var nome2 = $("[id$='txtNome']").val();
        var erro = false;

        if (!isEmailAddress(address1)) {
            $("#vldEmailAmigo").show();
            erro = true;
        }
        else
            $("#vldEmailAmigo").hide();

        if (!isEmailAddress(address2)) {
            $("#vldEmail").show();
            erro = true;
        }
        else
            $("#vldEmail").hide();

        if (nome1 == "") {
            $("#vldNomeAmigo").show();
            erro = true;
        }
        else
            $("#vldNomeAmigo").hide();

        if (nome2 == "") {
            $("#vldNome").show();
            erro = true;
        }
        else
            $("#vldNome").hide();

        if (!erro) {
            var mensagem = $("[id$='txtMensagem']").val();
            var nomeProduto = $("[id$='lblNomeProduto']").text();
            var descricaoProduto = $("[id$='lblResumoProduto']").text();
            var linkProduto = window.location.href;

            $.ajax({
                type: "POST",
                url: "Produto.aspx/EnviarEmail",
                data: "{nomeRemetente: '" + nome2 + "', emailRemetente: '" + address2 + "', nomeDestinatario: '" + nome1 + "', emailDestinatario: '" + address1 +
                    "', mensagem: '" + mensagem + "', nomeProduto: '" + nomeProduto + "', descricaoProduto: '" + descricaoProduto + "', linkProduto: '" + linkProduto + "'}",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $("#carregando").show();
                    $("#respostaEmail").hide();
                },
                success: function (data) {
                    if (data.d == "sucesso") {
                        $("#carregando").hide();
                        $("#respostaEmail").show();
                        $("#spanRespostaEmail").text("Sua mensagem foi enviada com sucesso!");
                        $("#imgRespostaEmail").attr('src', 'App_Themes/Padrao/Imagens/email_success.png');
                    }
                },
                error: function (data) {
                    $("#carregando").hide();
                    $("#respostaEmail").show();
                    $("#spanRespostaEmail").text("Ocorreu um erro ao enviar sua mensagem. Tente novamente mais tarde.");
                    $("#imgRespostaEmail").attr('src', 'App_Themes/Padrao/Imagens/email_error.png');
                }
            });
        }

        return false;
    });
});

$.fn.ancora = function () {
    $('html,body').animate({ scrollTop: $(this).offset().top });
}

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

function isEmailAddress(str) {
    var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return filtro.test(str);
}