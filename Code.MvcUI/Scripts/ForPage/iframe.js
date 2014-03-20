$(function () {
    $('.table').each(function () {
        $(this).find('tr:odd').find("td").css("background", "#f1f1f1");
    });

    $(".allCheck").on("click", function () {
        var _this = $(this);
        if (_this.attr("checked")) {
            _this.parent().parent().parent().find("td .singleCheck").attr("checked", 'true');
        }
        else {
            _this.parent().parent().parent().find("td .singleCheck").removeAttr("checked");
        }
    });

    $(".singleCheck").on("click", function () {
        var _this = $(this);
        if (!_this.attr("checked")) {
            _this.parent().parent().parent().find("th .allCheck").removeAttr("checked");
        }
    });
})

function getAllCheckID() {
    var arr = new Array();
    $(".table .singleCheck[checked]").each(function () {
        arr.push($(this).val());
    });
    return arr;
}