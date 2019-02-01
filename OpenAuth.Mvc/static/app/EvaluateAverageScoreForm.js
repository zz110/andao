var app = angular.module('myApp', []);
app.controller('appController', function ($scope) {

    var momentObj = new moment(new Date());

    $scope.model = $scope.model || {};
    $scope.ErrList = [];
    $scope.monthList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    
    $scope.currentMonth = momentObj.format('M');
    $scope.model.EvaluateYear = momentObj.year();
    $scope.model.Created = momentObj.format('YYYY-MM-DD');
    $scope.model.id = "";

    /**
    * 时间控件回调函数
    * @param {any} ev
    */
    $scope.DatepickerCallback = function (ev) {

        var result = new moment(ev.date).format('YYYY-MM-DD');
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
    $scope.init = function () {

       
        $.get('/flow/flowdemonstration/get_by_flow_instance_id/' + $scope.model.flow_instance_id,
            function (resp) {

                if (resp.status == "success" && resp.result) {
                    $scope.model = resp.result;
                    $scope.$apply();//通知更新，否则表单数据无法显示

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
     * 关闭 
     * */
    $scope.Close = function () {

        window.parent.layer.closeAll();
    }

    /**
     * 表单提交处理
     * */
    $scope.Submit = function () {

        $scope.model.flow_id = window.parent.flow_params.flow_id;
        $scope.model.flow_instance_id = window.parent.flow_params.flow_instance_id;

        var url = "/flow/flowdemonstration/employeeturnaround";
        $.post(url, { input: $scope.model }, function (resp) {

            if (resp.status === 'success') {
                $scope.show_box_warning(0);
                $scope.model.id = resp.result;
              
                $scope.$apply();
            }
            else {
                $scope.show_box_warning(1);
                $scope.ErrList = [];
                var errList = resp.message.split(',');
                $.each(errList, function (idx, item) {
                    if (item != "") {
                        $scope.ErrList.push(item);
                    }
                });
                $scope.$apply();
            }
        });
    }

});