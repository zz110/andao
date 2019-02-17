var app = angular.module('myApp', []);
app.controller('appController', function ($scope) {

    var momentObj = new moment(new Date());

    $scope.model = $scope.model || {};
    $scope.ErrList = [];
    $scope.monthList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.UserList = [];
    $scope.DeptList = [];
    $scope.model.id = "";

    /**
    * 时间控件回调函数
    * @param {any} ev
    */
    $scope.DatepickerCallback = function (ev) {

        var result = new moment(ev.date).format('YYYY-MM-DD HH:mm:ss');
        var clsName = ev.target.className;
        if (clsName.indexOf('created') != -1) {
            $scope.model.Created = result;
        }

    }

    //初始化时间控件
    adminLteDatepicker.init({
        callback: $scope.DatepickerCallback
    });



    /**
     * 初始化表单
     * */
    $scope.init = function (id) {

        $.get('/EvaluateAverageScores/get/' + id,
            function (resp) {
                if (resp.Code == 200) {
                    $scope.model = resp.Result;
                    $scope.model.Created = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.model.Updated = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.$apply();//通知更新，否则表单数据无法显示
                    $scope.GetUserList();
                    $scope.GetOrgList($scope.model.UserId);
                }
            });
    };

    $scope.show_box_warning = function (flag) {
        if (flag) {
            $("#warningbox").show("fast");
        }
        else {
            $("#warningbox").hide("fast");
        }
    }

    /**
     * 获取用户列表
     * */
    $scope.GetUserList = function () {

        $.get('/Authorise/GetUserListByCurrentUserOrgIds',
            function (resp) {
                if (resp.Code === 200) {

                    $scope.UserList = resp.Result;
                    $scope.$apply();//通知更新，否则表单数据无法显示
                }
            });
    }

    /**
     * 用户选择事件
     * @param {any} value
     */
    $scope.UserSelected = function (value) {
        $scope.DeptList = [];
        $scope.model.OrgId = '';
        if (value == "") return;
        $.get('/Authorise/GetOrgByUserId/' + value,
            function (resp) {
                if (resp.Code === 200) {

                    $scope.DeptList = resp.Result;
                    if ($scope.DeptList.length == 1) {
                        $scope.model.OrgId = $scope.DeptList[0].Id;
                    }
                    $scope.$apply();//通知更新，否则表单数据无法显示
                }
            });
    }


    /**
     * 获取组织结构列表
     * */
    //$scope.GetOrgList = function (id) {

    //    $.get('/Authorise/GetOrgByUserId/' + id,
    //        function (resp) {
    //            if (resp.Code === 200) {

    //                $scope.DeptList = resp.Result;
    //                if ($scope.DeptList.length == 1) {
    //                    $scope.model.OrgId = $scope.DeptList[0].Id;
    //                }
    //                $scope.$apply();//通知更新，否则表单数据无法显示
    //            }
    //        });

    //}

    /**
     * 通过角色名称获取部门列表
     * @param {any} orgid
     */
    $scope.GetUserByOrgId = function (orgid) {
        $scope.UserList = [];
        $.get('/Authorise/GetUserListByOrgId?orgid=' + orgid,
            function (resp) {
                if (resp.Code === 200) {
                    $scope.UserList = resp.Result;
                    $scope.$apply();//通知更新，否则表单数据无法显示
                }
            });
    }

    /**
         * zTree节点点击事件
         * @param {any} event
         * @param {any} treeId
         * @param {any} treeNode
         */
    $scope.zTreeOnClick = function (event, treeId, treeNode) {

        $scope.OrgId = treeNode.Id;
        $scope.OrgName = treeNode.Name;
    }

    /**
         * 部门选择事件
         * @param {any} value
         */
    $scope.OrgSelected = function () {

        if ($scope.OrgId != '') {

            $scope.model.OrgId = $scope.OrgId;
            $scope.model.OrgName = $scope.OrgName;
            $scope.$apply();

            $scope.GetUserByOrgId($scope.model.OrgId);
        }
        $("#modal-default").modal("hide");
    }

    /**
         * 部门点击事件
         * */
    $scope.OrgClick = function () {

        $scope.OrgId = '';
        $scope.OrgName = '';
        App.initZTree({
            onClick: $scope.zTreeOnClick
        });
    }


    /**
     * 关闭 
     * */
    $scope.Close = function () {

        if (window.parent._dataTable) {
            window.parent._dataTable.Refresh();
        }

        if (window.parent.layer) {
            window.parent.layer.closeAll();
        }

    }

    /**
     * 表单提交处理
     * */
    $scope.Submit = function () {

        var url = "/EvaluateAverageScores/save";
        $.post(url, { input: $scope.model }, function (resp) {

            if (resp.Code === 200) {
                $scope.show_box_warning(0);
                $scope.model.Id = resp.Result;
                $scope.$apply();
                $scope.Close();
            }
            else {
                $scope.show_box_warning(1);
                $scope.ErrList = [];
                var errList = resp.Message.split(',');
                $.each(errList, function (idx, item) {
                    if (item != "") {
                        $scope.ErrList.push(item);
                    }
                });
                $scope.$apply();
            }
        });
    }


    $scope.init(Id);

});