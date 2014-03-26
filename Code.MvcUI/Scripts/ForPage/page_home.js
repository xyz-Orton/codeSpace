$(function () {
    $("#side_switch").click(function () {
        $(".left").hide();
        $("#main").contents().find(".right_body").css('margin-left', 0);
        $(this).hide();
        $("#side_switchl").show();
    })

    $("#side_switchl").click(function () {
        $(".left").show();
        $("#main").contents().find(".right_body").css('margin-left', 200);
        $(this).hide();
        $("#side_switch").show();
    })

    var main = $("#main");
    var mainLeft = $(".left");
    var thisheight = window.screen.height; //页面高度
    if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
        $("body").height(thisheight - 200);
    } else {
        var setHeight = (thisheight - 200);
        mainLeft.height(setHeight);
        main.height(setHeight);
    }

    $("#LogOff").on("click", function () {
        var hnLogOff = $("#hnLogOff").val();
        seaDialog.confirm("是否确定退出?", 150, function () {
            window.location = hnLogOff;
        });
    });

    $(".top_nav li>a").on("click", function () {
        var _this = $(this);
        $(".top_nav li>a").removeClass("selected");
        _this.addClass("selected");
    });
});

function openMsgWindow() {
    art.dialog({
        id: 'msgDialog',
        title: '提示信息',
        lock: true,
        fixed: true,
        width: 300,
        height: 100,
        ok: function () {
            this.title('成功提示！').content('操作成功！').time(2);
            return false;
        },
        cancel: function () {

            return true;
        },
        content: "<div style='line-height:25px'>您有新的站内信<Br><a href=\"http://www.uimaker.com\">点此阅读</a></div>"
    });
}