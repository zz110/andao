/**
 * 函数对象
 */
var Validate = function () {


    var formObj = {};

    /**
     * 初始化 jquery validation
     */
    var handlerInitValidate = function () {
        formObj =  $("#inputForm").validate({
            errorElement: 'span',
            errorClass: 'help-block',

            errorPlacement: function (error, element) {
                element.parent().parent().attr("class", "form-group has-error");
                error.insertAfter(element);
            }
        });
    };

    /**
     * 初始化 jquery validation 指定表单提交回调函数
     * @param {any} submitHandler
     */
    var handlerInitValidate = function (submitHandler) {

        formObj =  $("#inputForm").validate({
            errorElement: 'span',
            errorClass: 'help-block',
            errorPlacement: function (error, element) {
                element.parent().parent().attr("class", "form-group has-error");
                error.insertAfter(element);
            }, 
            /**
             * 验证成功
             * @param {any} error
             * @param {any} element
             */
            success: function (error, element) {
     
                $(element).parent().parent().attr("class", "form-group has-success");                
            },
            /**
             * 显示错误
             * @param {any} error
             * @param {any} errorList
             */
            showErrors: function (error, errorList) {
                $.each(errorList, function (idx, item) {
                    $(item.element).parent().parent().attr("class", "form-group has-error");
                });
                formObj.defaultShowErrors();
            },
            submitHandler: function (form) {
                if (formObj.form()) {
                    submitHandler.call(this);
                }
            }
        });

    }

    /**
     * 增加自定义验证规则
     */
    var handlerInitCustomValidate = function () {
        $.validator.addMethod("mobile", function (value, element) {
            var length = value.length;
            var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
            return this.optional(element) || (length == 11 && mobile.test(value));
        }, "手机号码格式错误");
    };

    return {
        /**
         * 初始化
         */
        init: function () {
            handlerInitCustomValidate();
            handlerInitValidate();
        },
        /**
         * 初始化 指定表单提交回调函数
         * @param {any} submitHandler
         */
        init: function (submitHandler) {
            handlerInitCustomValidate();
            handlerInitValidate(submitHandler);
        },
        /**
         *  验证 
         * */
        valid: function () {
            if (!$.isEmptyObject(formObj)) {
                $("#inputForm").submit();
                return formObj.form();
            }
            return false;
        }
    }

}();
