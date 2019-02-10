var app = angular.module('myApp', []);
app.controller('appController', function ($scope) {

    var momentObj = new moment(new Date());

    $scope.model = $scope.model || {};
    $scope.ErrList = [];
    $scope.monthList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.UserList = [];
    $scope.model.id = "";

    $scope.OrgId = '';
    $scope.OrgName = '';

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

        $.get('/MonthlyAssessments/get/' + id,
            function (resp) {
                if (resp.Code == 200) {
                    $scope.model = resp.Result;
                    $scope.model.Created = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.model.Updated = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.$apply();
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

        var url = "/MonthlyAssessments/save";
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

    /**
     * 安管成绩change事件
     * @param {any} AnntubeScore
     */
    $scope.AnntubeScoreChange = function (AnntubeScore) {
        var _score = parseFloat(AnntubeScore);
        _score = isNaN(_score) ? 0 : _score;

        var _QuantifyScore = parseFloat($scope.model.QuantifyScore);
        _QuantifyScore = isNaN(_QuantifyScore) ? 0 : _QuantifyScore;

        $scope.model.Score = (_score * 0.3 + _QuantifyScore * 0.7).toFixed(2);

        $scope.$apply();
    }

    /**
     * 标准量化成绩change事件
     * @param {any} QuantifyScore
     */
    $scope.QuantifyScore = function (QuantifyScore) {
        var _score = parseFloat(QuantifyScore);
        _score = isNaN(_score) ? 0 : _score;

        var _AnntubeScore = parseFloat($scope.model.AnntubeScore);
        _AnntubeScore = isNaN(_AnntubeScore) ? 0 : _AnntubeScore;

        $scope.model.Score = (_AnntubeScore * 0.3 + _score * 0.7).toFixed(2);

        $scope.$apply();
    }

    $scope.init(Id);
    
});