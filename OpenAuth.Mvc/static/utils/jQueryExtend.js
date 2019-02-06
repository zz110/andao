
(function ($) {

    $.fn.extend({
        loadMenus: function (modulecode) {
            var dom = $(this);
            $.ajax("/ModuleManager/LoadAuthorizedMenus?modulecode=" + modulecode,
                {
                    async: false
                    , success: function (data) {
                        if ('' !== data) {
                            var obj = JSON.parse(data);
                            var sb = '';
                            $.each(obj,
                                function () {
                                    var element = this;
                                    sb += "<button class='btn " + element.Class + "' id='" + element.DomId + "'>" + element.Name + "</button> ";
                                });
                            dom.html(sb);
                        }
                        else {
                            $(this).hide();
                        }
                    }
                });
        }
    });

})(jQuery);
