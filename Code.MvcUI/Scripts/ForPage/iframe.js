$(function () {
    var main = $(window.parent.document).find("#main");
    var mainLeft = $(window.parent.document).find(".left");
    //var thisheight = $(window.parent.document).height();
    var thisheight = window.parent.window.screen.height;//Ò³Ãæ¸ß¶È
    if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
        $(window.parent.document).find("body").height(thisheight - 200);
    } else {
        var setHeight = (thisheight - 200);
        mainLeft.height(setHeight);
        main.height(setHeight);
    }

    $('.table').each(function () {
        $(this).find('tr:odd').find("td").css("background", "#f1f1f1");
    });

}) 