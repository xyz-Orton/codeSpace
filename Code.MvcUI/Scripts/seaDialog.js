seajs.config({
    alias: {
        "jquery": "jquery-1.10.2.js"
    },
    charset: 'utf-8',
    timeout: 20000,
    debug: false
});
var seaDialog = {
    alert: function (msg, width, funcName) {
        seajs.use(["jquery", "./Content/artDialog/dialog.js"], function ($, dialog) {
            var d = dialog({
                title: '提示',
                content: msg,
                okValue: '确定',
                ok: function () {
                    if (typeof (eval(funcName)) == "function") {
                        funcName();
                    }
                }
            });
            if (width != null && width > 0) {
                d.width(width);
            }
            d.showModal();
        });
    },
    confirm: function (msg, width, funcOk, funcCancel) {
        seajs.use(["jquery", "./Content/artDialog/dialog.js"], function ($, dialog) {
            var d = dialog({
                title: '提示',
                content: msg,
                okValue: '确定',
                ok: function () {
                    if (typeof (eval(funcOk)) == "function") {
                        funcOk();
                    }
                },
                cancelValue: '取消',
                cancel: function () {
                    if (typeof (eval(funcCancel)) == "function") {
                        funcCancel();
                    }
                }
            });
            if (width != null && width > 0) {
                d.width(width);
            }
            d.showModal();
        });
    },
    open: function (title, url, width, height, funcName) {
        seajs.use(["jquery", "./Content/artDialog/dialog-plus.js"], function ($, dialog) {
            window.dialog = dialog;

            dialog({
                url: url,
                title: title,
                width: width,
                height: height,
                onclose: function () {
                    if (typeof (eval(funcName)) == "function") {
                        funcName();
                    }
                },
                oniframeload: function () {

                }
            }).showModal();
        });
    }
}