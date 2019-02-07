
(function ($) {

    $.fn.extend({
        loadMenus: function (modulecode) {
            var dom = $(this);
            var flag = false;
            $.ajax("/ModuleManager/LoadAuthorizedMenus?modulecode=" + modulecode,
                {
                    async: false
                    , success: function (data) {
                        
                        if ('' !== data && '[]' !== data) {
                            var obj = JSON.parse(data);
                            var sb = '';
                            $.each(obj,
                                function () {
                                    var element = this;
                                    sb += "<button class='btn " + element.Class + "' id='" + element.DomId + "'>" + element.Name + "</button> ";
                                });
                            dom.html(sb);
                            flag = true;
                        }
                    }
                });

            if (!flag) {
                $(this).css('display', 'none');
            }
        }
    });

})(jQuery);
