//字符长度计算
function getStrLength(str) {
    var cArr = str.match(/[^\x00-\xff]/ig);
    return str.length + (cArr == null ? 0 : cArr.length);
}

/**************判断数值在数组中出现的位置 ***************************/
function getPositionArray(needValue, array) {
    var pos = -1;

    if (null != array && array.length > 0) {
        for (var arr in array) {
            if (needValue == array[arr]) {
                pos = arr;
            }
        }
    }

    return pos;
}

/**************判断传过来的值是否在数组中出现 ***************************/
function inArray(needValue, array) {
    var IsExist = false;
    if (null != array && array.length > 0) {
        for (var arr in array) {
            if (needValue == array[arr]) {
                IsExist = true;
            }
        }
    }

    return IsExist;
}


//判断两个数组元素是否完全相等
//要求穿过来的array1、array2数组不能有重复的值
//返回true表示两个数组中元素不完全相等
function IsEqualArray(array1, array2) {
    var IsExist = false;

    if ((array1 == null && array2 != null) ||
    (array1 != null && array2 == null) ||
    (array1.length != array2.length)) {
        return true;
    }

    for (var i = 0; i < array1.length; i++) {
        if (!inArray(array1[i], array2)) {
            IsExist = true;
        }
    }

    return IsExist;
}

/**************判断是否包含HTML字符 ***************************/
function ValidateHTML(strText) {
    var pattern = /<[^>]*>/g;
    return strText.match(pattern);
}

/**************判断是否包含特殊字符***************************/
function ValidateSpecialStr(strText) {
    var pattern = /[,.<>{}~!@#$%^&*\\]/;
    return strText.match(pattern);
}

/**************判断是否包含中文***************************/
function ValidateChineseStr(strText) {
    var pattern = /[\u4e00-\u9fa5]/;
    return strText.match(pattern);
}


/**************判断是否字母数字***************************/
function ValidateLetterNumStr(strText) {
    var pattern = /^[A-Za-z0-9]+$/;
    return strText.match(pattern);
}


//返回数组元素的位置
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};

//删除某个元素  array.remove(3);
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
//克隆一个元素
function clone(myObj) {
    if (typeof (myObj) != 'object') return myObj;
    if (myObj == null) return myObj;

    var myNewObj = new Object();

    for (var i in myObj)
        myNewObj[i] = clone(myObj[i]);

    return myNewObj;
}


//获取url参数 alert(GetQueryString("参数名1"));
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return r[2]; return null;
}

//页面添加遮罩效果
function AddShade() {
    var disabledZone = document.createElement('div');
    disabledZone.setAttribute('id', 'disabledZone');
    disabledZone.style.position = "absolute";
    disabledZone.style.zIndex = "10000";
    disabledZone.style.left = "0px";
    disabledZone.style.top = "0px";
    disabledZone.style.backgroundColor = "#000";
    disabledZone.style.opacity = "0.1"; //透明度
    disabledZone.style.filter = "alpha(opacity=10)";

    disabledZone.innerHTML = '<table width="100%" height="100%"><tr><td>&nbsp;</td></tr></table></div>';

    if (window.addEventListener) {
        disabledZone.style.width = ($(window).width() > $("body").width() ? $(window).width() : $("body").width()).toString() + "px";
        disabledZone.style.height = ($(window).height() > $("body").height() ? $(window).height() : $("body").height()).toString() + "px";
    }
    else {
        disabledZone.style.width = $(window).width() > $("body").width() ? $(window).width() : $("body").width();
        disabledZone.style.height = $(window).height() > $("body").height() ? $(window).height() : $("body").height();
    }
    document.body.appendChild(disabledZone);
}

/**************判断是否是正整数***************************/
function ValidateInteger(strText) {
    var pattern = /^\d+$/;
    return strText.match(pattern);
}

/*获得当前系统时间 格式;yyyy-MM-dd*/
function GetCurrentDate() {
    var date = new Date();
    var year = date.getFullYear()
    var month = AppendNum(date.getMonth() + 1);
    var day = AppendNum(date.getDate());
    return year + "-" + month + "-" + day;
}

/*日期或时间不足两位的补0*/
function AppendNum(obj) {
    if (obj.toString().length <= 1) {
        return "0" + obj;
    }
    return obj;
}

/*比较只读文本框在ie下点击backspace返回上一页 */
$(function () {
    $("input[readOnly]").keydown(function (e) {
        e.preventDefault();
    });
    limit_textarea_input();
});
/*设置textarea能输入的最大长度*/
function limit_textarea_input() {
    $("textarea[maxlength]").bind('input propertychange', function () {
        var maxLength = $(this).attr('maxlength');
        if ($(this).val().length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));
        }
    })
}

function OpenPreview(ID, ProjectID) {
    var hnRreview = $("#hnRreview", top.document).val();
    var url = hnRreview + "?projectID=" + ProjectID + "&ID=" + ID;
    window.open(url);
}