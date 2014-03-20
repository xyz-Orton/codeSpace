var isSubmit = true;
function deleteInfo(ID) {
    var hnDelete = $("#hnDelete").val();
    isSubmit = false;
    top.seaDialog.confirm("是否确认删除？", 150, function () {
        $.post(hnDelete, { ID: ID, randomNum: Math.random() }, function (data) {
            isSubmit = true;
            if (data != null) {
                if (data.Success) {
                    top.seaDialog.alert("删除成功", 150, function () {
                        window.location.reload();
                    });
                }
                else {
                    top.seaDialog.alert(data.Message, 150);
                }
            }
        }, "json");
    });
}