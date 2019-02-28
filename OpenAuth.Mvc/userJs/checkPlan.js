layui.config({
    base: "/js/"
}).use(['form', 'vue', 'ztree', 'layer', 'jquery', 'table', 'droptree', 'openauth', 'utils'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var openauth = layui.openauth;
    var id = $.getUrlParam("id");      //待分配的id
    layui.droptree("/UserSession/GetOrgs", "#Organizations", "#OrganizationIds");

    //主列表加载，可反复调用进行刷新
    var config = {};  //table的参数，如搜索key，点击tree的id
    var mainList = function (options) {
        if (options != undefined) {
            $.extend(config, options);
        }
        $.ajax("/Plans/LoadJudgeAndDept?Id=" + id, {
            async: false
            , dataType: 'json'
            , success: function (json) {
                if (json.Code == 500) return;
                var roles = json.Result;

                for (var i = 0; i < json.length; i++) {
                    var cc = '<button class="layui-btn name" onclick="rem(this)" style="margin:10px" tag="' + json[i].Id + '" >' + json[i].Name + '</button>';
                    $('.layui-card-body').append(cc);
                }

            }
        });
        table.reload('mainList', {
            url: '/UserManager/Load',
            where: config
            , done: function (res, curr, count) {
                //如果是异步请求数据方式，res即为你接口返回的信息。
                //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度


            }
        });
    }
    //左边树状机构列表
    var ztree = function () {
        var url = '/UserSession/GetOrgs';
        var zTreeObj;
        var setting = {
            view: { selectedMulti: false },
            data: {
                key: {
                    name: 'Name',
                    title: 'Name'
                },
                simpleData: {
                    enable: true,
                    idKey: 'Id',
                    pIdKey: 'ParentId',
                    rootPId: ""
                }
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    mainList({ orgId: treeNode.Id });
                }
            }
        };
        var load = function () {
            $.getJSON(url, function (json) {
                zTreeObj = $.fn.zTree.init($("#tree"), setting);
                var newNode = { Name: "根节点", Id: null, ParentId: "" };
                json.push(newNode);
                zTreeObj.addNodes(null, json);
                mainList({ orgId: "" });
                zTreeObj.expandAll(true);
            });
        };
        load();
        return {
            reload: load
        }
    }();
    $("#tree").height($("div.layui-table-view").height());


    //分配及取消分配
    table.on('checkbox(list)', function (obj) {
        console.log(obj.checked); //当前是否选中状态
        console.log(obj.data); //选中行的相关数据
        console.log(obj.type); //如果触发的是全选，则为：all，如果触发的是单选，则为：one


        if (obj.type == "all") {
            var checkStatus = table.checkStatus('mainList');
            $.each(checkStatus.data, function (i, v) {
                var cc = '<button class="layui-btn name" onclick="rem(this)" style="margin:10px" tag="' + v.Id + '" >' + v.Name + '</button>';
                $('.layui-card-body').append(cc);
            })
        } else {
            var cc = '<button class="layui-btn name" onclick="rem(this)" style="margin:10px" tag="' + obj.data.Id + '" >' + obj.data.Name + '</button>';
            $('.layui-card-body').append(cc);
        }
    });
    //监听页面主按钮操作 end

    $('#sub').on('click', function () {
        var ids = $('.ddr button');
        var res = "";
        debugger;
        for (var i = 0; i < ids.length; i++) {
            res += $(ids[i]).attr('tag') + ',';
        }
        $.post("/Plans/CheckUp", { ids: res, id:id }
            , function (data) {
                layer.msg(data.msg);
            }
            , "json");
    })

})
function rem(dd) {
    dd.remove();
}
