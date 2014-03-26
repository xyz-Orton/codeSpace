var isSubmit = true;
var hdOrganizationList;
$(function () {
    hdOrganizationList = $("#hdOrganizationList").val();
    LoadTree();
});

//用户删除
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

//初始化zTree
var setting = {
    view: {
        //addHoverDom: addHoverDom,
        //removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    edit: {
        enable: true,
        editNameSelectAll: true
    },
    data: {
        keep: {
            parent: true,
            leaf: true
        },
        key: {
            title: "fullName"
        },
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "pId",
            rootPId: ""
        }
    },
    callback: {
        //beforeDrag: zTreeBeforeDrag,
        //beforeEditName: beforeEditName,
        //beforeRemove: beforeRemove,
        //onClick: zTreeOnClick
    }
};

//初始化组织结构
function LoadTree() {
    $.post(hdOrganizationList, { randomNum: Math.random() }, function (json) {
        $.fn.zTree.init($("#treePosition"), setting, json);
        var zTree = $.fn.zTree.getZTreeObj("treePosition");
        var treeRoot = zTree.getNodeByParam("level", 0, null);
        zTree.expandNode(treeRoot, true);
        zTree.updateNode(treeRoot);
    });
}