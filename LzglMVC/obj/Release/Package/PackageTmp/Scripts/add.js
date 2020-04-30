$(function() {
    $(window).on("load resize",function(){
        var h=window.innerHeight||document.body.clientHeight||document.documentElement.clientHeight;
        if ($(window).width() < 971) {
            $('.col-sm-3').addClass('text-left').removeClass('text-right');
        } else {
            $('.col-sm-3').removeClass('text-left').addClass('text-right');
        }
    });
    //alert($(document).find("div:last-of-type").last().css("height"));
    //$($('object').parent()).css("background-color", "red");
});