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

    
});

$.fn.ancora = function () {
    $('html,body').animate({ scrollTop: $(this).offset().top });
}
