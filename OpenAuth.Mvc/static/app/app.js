var App = function () {

    var defautAppOptions = {};

    var dataTable = {};

    /**
     * 处理按钮事件绑定
     * */
    var handlerButtonClickBind = function () {

        $("#btnAdd").bind('click', handlerAddClick);
        $("#btnEdit").bind('click', handlerEditClick);
        $("#btnDel").bind('click', handlerDeleteMulti);
    }

    var handlerAddClick = function () {

        layer.open({
            type: 2,
            title: defautAppOptions.title,
            skin: 'layui-layer-lan',
            maxmin: true,
            shade: 0.1,
            area: [defautAppOptions.width, defautAppOptions.height], //宽高
            content: defautAppOptions.form_url
        });
    }

    var handlerEditClick = function () {

        var rows = dataTable.GetSelections();
        if (rows.length > 1) {
            alert('请选择要编辑的数据');
            return;
        }
        layer.open({
            type: 2,
            title: defautAppOptions.title,
            skin: 'layui-layer-lan',
            maxmin: true,
            shade: 0.1,
            area: [defautAppOptions.width, defautAppOptions.height], //宽高
            content: defautAppOptions.form_url + rows[0].Id
        });

    }

    var handlerDeleteMulti = function () {

        var rows = dataTable.GetSelections();
        // 判断用户是否选择了数据项
        if (rows.length === 0) {
            $("#modal-message").html("您还没有选择任何数据项，请至少选择一项");
        }
        else {
            $("#modal-message").html("您确定删除数据项吗？");
        }

        // 点击删除按钮时弹出模态框
        $("#modal-default").modal("show");

        // 如果用户选择了数据项则调用删除方法
        $("#btnModalOk").bind("click", function () {
            handlerDeleteData(defautAppOptions.delete_url);
        });

    }

    var handlerDeleteData = function (url) {

        $("#modal-default").modal("hide");
        $('.modal-backdrop.fade.in').remove();
        var _idArray = [];
        var rows = dataTable.GetSelections();
        $.each(rows, function (idx, item) {
            _idArray.push(item.Id);
        });

        if (_idArray.length > 0) {
            // AJAX 异步删除操作
            $.ajax({
                "url": url,
                "type": "POST",
                "data": { "ids": _idArray },
                "dataType": "JSON",
                "success": function (data) {
                    // 请求成功后，无论是成功或是失败都需要弹出模态框进行提示，所以这里需要先解绑原来的 click 事件
                    $("#btnModalOk").unbind("click");

                    // 请求成功
                    if (data.Code === 200) {
                        // 刷新页面
                        $("#btnModalOk").bind("click", function () {
                            $("#modal-default").modal("hide");
                            $('.modal-backdrop.fade.in').remove();
                            dataTable.Refresh();
                        });
                    }

                    // 请求失败
                    else {
                        // 确定按钮的事件改为隐藏模态框
                        $("#btnModalOk").bind("click", function () {
                            $("#modal-default").modal("hide");
                            $('.modal-backdrop.fade.in').remove();
                        });
                    }

                    // 因为无论如何都需要提示信息，所以这里的模态框是必须调用的
                    $("#modal-message").html(data.Message);
                    $("#modal-default").modal("show");
                }
            });
        }
    }


    /**
     * 初始化列表
     * */
    var handlerTableInit = function (url, columns) {
        var oTableInit = new Object();

        /**
         * 查询参数
         * */
        var QueryParameters = {};

        /**
         * 设置查询参数
         * @param {any} params
         */
        oTableInit.SetParameters = function (params) {
            var temp = params || {};
            $.extend(QueryParameters, temp);
        }

        //初始化Table
        oTableInit.Init = function () {
            $('#tbList').bootstrapTable({
                url: url,         //请求后台的URL（*）
                method: 'get',                      //请求方式（*）
                striped: true,                      //是否显示行间隔色
                cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,                   //是否显示分页（*）
                sortable: false,                     //是否启用排序
                sortOrder: "asc",                   //排序方式
                queryParams: oTableInit.queryParams,//传递参数（*）
                sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                pageNumber: 1,                       //初始化加载第一页，默认第一页
                pageSize: 10,                         //每页的记录行数（*）
                pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                minimumCountColumns: 2,             //最少允许的列数
                clickToSelect: true,                //是否启用点击选中行
                height: 670,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                cardView: false,                    //是否显示详细视图
                detailView: false,                   //是否显示父子表
                columns: columns
            });
        };

        //得到查询的参数
        oTableInit.queryParams = function (params) {
            
            var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                limit: params.limit,   //页面大小
                offset: params.offset  //页码
            };
            $.extend(temp, QueryParameters);
            return temp;
        };

        //获取选中的行
        oTableInit.GetSelections = function () {
            return $("#tbList").bootstrapTable('getSelections');
        };

        //获取选中的行
        oTableInit.Refresh = function () {
            $('#tbList').bootstrapTable('refresh');
        };

        return oTableInit;
    };


    return {

        init: function (options) {
            
            $.extend(defautAppOptions, options);
            handlerButtonClickBind();
        },
        initTable: function (url, columns) {
            dataTable = new handlerTableInit(url, columns);
            return dataTable;
        }
    }

}();