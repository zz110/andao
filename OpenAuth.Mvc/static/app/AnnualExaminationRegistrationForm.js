var app = angular.module('myApp', []);
app.controller('appController', function ($scope) {

    var momentObj = new moment(new Date());

    $scope.model = $scope.model || {};
    $scope.ErrList = [];
    $scope.monthList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.SexList = ["男", "女"];
    $scope.NationList = ['汉族', '壮族', '满族', '回族', '苗族', '阿昌族', '白族', '保安族',
        '布朗族', '布依族', '藏族', '朝鲜族', '达斡尔族', '傣族', '德昂族',
        '东乡族', '侗族', '独龙族', '俄罗斯族', '鄂伦春族', '鄂温克族',
        '高山族', '哈尼族', '哈萨克族', '赫哲族', '基诺族', '京族', '景颇族',
        '柯尔克孜族', '拉祜族', '黎族', '傈僳族', '珞巴族', '毛南族', '门巴族',
        '蒙古族', '仫佬族', '纳西族', '怒族', '普米族', '羌族', '撒拉族', '畲族',
        '水族', '塔吉克族', '塔塔尔族', '土家族', '土族', '佤族', '维吾尔族', '未识别民族',
        '乌孜别克族', '锡伯族', '瑶族', '彝族', '仡佬族', '裕固族'];

    $scope.PoliticalaffiliationList = ['中共党员', '中共预备党员', '共青团员', '民革党员',
        '民盟盟员', '民建会员', '民进会员', '农工党党员',
        '致公党党员', '九三学社社员', '台盟盟员', '无党派人士', '群众'];

    $scope.DegreeEduList = ['博士', '硕士', '本科', '大专', '中专', '技工学校',
        '高中', '初中', '小学', '文盲', '半文盲'];

    $scope.DeptList = [];
    $scope.model.id = "";

    /**
    * 时间控件回调函数
    * @param {any} ev
    */
    $scope.DatepickerCallback = function (ev) {
        var momentObj = new moment(ev.date);
        var clsName = ev.target.className;
        if (clsName.indexOf('created') != -1) {
            $scope.model.Created = momentObj.format('YYYY-MM-DD HH:mm:ss');
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

        $.get('/AnnualExaminationRegistrations/get/' + id,
            function (resp) {
                if (resp.Code == 200) {
                    $scope.model = resp.Result;
                    $scope.model.Created = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.model.Updated = momentObj.format('YYYY-MM-DD HH:mm:ss');
                    $scope.$apply();//通知更新，否则表单数据无法显示
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
     * 获取组织结构列表
     * */
    $scope.GetOrgList = function (id) {

        $.get('/Authorise/GetOrgByUserId/' + id,
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

        var url = "/AnnualExaminationRegistrations/save";
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