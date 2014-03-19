function InitPage() {
    $("#txtLoginName").on("keypress", function (evt) {
        evt = (evt) ? evt : ((window.event) ? window.event : "") //兼容IE和Firefox获得keyBoardEvent对象  
        var key = evt.keyCode ? evt.keyCode : evt.which; //兼容IE和Firefox获得keyBoardEvent对象的键值  
        if (key == 13) { //判断是否是回车事件。 
            var txtLoginName = $("#txtLoginName");
            var txtLoginPwd = $("#txtLoginPwd");

            if ($.trim(txtLoginName.val()) != "" && txtLoginPwd.val() == "") {
                txtLoginPwd.focus();
            }
            else {
                UserLogin();
            }
        }
    });
    $("#txtLoginPwd").on("keypress", function (evt) {
        evt = (evt) ? evt : ((window.event) ? window.event : "") //兼容IE和Firefox获得keyBoardEvent对象  
        var key = evt.keyCode ? evt.keyCode : evt.which; //兼容IE和Firefox获得keyBoardEvent对象的键值  
        if (key == 13) { //判断是否是回车事件。 
            UserLogin();
        }
    })
}

var isSubmit = true;
function UserLogin() {
    var loginName = $("#txtLoginName").val();
    var loginPwd = $("#txtLoginPwd").val();

    $("#nameError").html("");
    if (loginName == "" || $.trim(loginName) == "" || loginName == "用户名") {
        $("#nameError").html("请填写用户名!");
    }

    $("#pwdError").html("");
    if (loginPwd == "" || $.trim(loginPwd) == "") {
        $("#pwdError").html("请填写用户密码!");
        return;
    }

    if (isSubmit) {
        isSubmit = false;
        $.post("/Account/LogOn", { UserName: encodeURIComponent(loginName), Password: encodeURIComponent(loginPwd), randomNum: Math.random() }, function (data) {
            isSubmit = true;
            if (data != null) {
                var strIndex = $("#hdIndex").val();
                var goHref = getQueryString("ReturnUrl");
                if (goHref == null || $.trim(goHref) == "")
                    goHref = strIndex;

                if (data.Success) {
                    window.location = goHref;
                } else {
                    $("#messError").html(data.Message);
                    return;
                }
            }
        }, "json");
    }
}