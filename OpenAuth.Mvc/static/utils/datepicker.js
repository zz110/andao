/**
 * 初始化 datepicker 控件
 * 
 * */
var adminLteDatepicker = function () {

    var initDatepicker = function (opts) {

        opts = opts || {};
        opts.class = opts.class || "adminlte_datepicker";
        opts.autoclose = opts.autoclose || true;
        opts.language = opts.language || "zh-CN";
        opts.format = opts.format || "yyyy-mm-dd";
        opts.callback = opts.callback || function (ev) { }

        $('.' + opts.class).datepicker({
            language: opts.language,
            autoclose: opts.autoclose,
            format: opts.format
        }).on('changeDate', function (ev) {
            opts.callback.call(this, ev);
        });
    }

    return {

        init: function (opts) {
            initDatepicker(opts);
        }
    }
}();
