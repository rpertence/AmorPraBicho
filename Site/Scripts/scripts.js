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
});