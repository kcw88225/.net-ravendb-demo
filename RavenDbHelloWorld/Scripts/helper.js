(function (root, $) {
    var helper = {},
        selector = {
            createBtn: '#create-btn',
            delAllBtn: '#del-all-btn',
            displaySection: '#display'
        },
        url = {
            read: '/Home/Read',
            create: '/Home/Create',
            deleteAll: '/Home/DeleteAll'
        };

    //public
    helper.bindCreateBtnOnClickEvent = function () {
        $(selector.createBtn).click(function () {
            $.ajax({
                url: url.create,
                method: 'POST'
            }).done(function () {
                helper.render();
            });
        });
    }

    helper.bindDeleteAllBtnOnClickEvent = function () {
        $(selector.delAllBtn).click(function () {
            $.ajax({
                url: url.deleteAll,
                method: 'POST'
            }).done(function () {
                helper.render();
            });
        });
    }

    helper.render = function () {
        $.ajax({
            url: url.read,
            method: 'GET'
        }).done(function (result) {
            var html = $('<div />');
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                html.append(row.Message + ' ' + parseAspDate(row.CreatedTime)).append('<br />');
            }

            $(selector.displaySection).html(html);
        });
    }

    //private
    function parseAspDate(aspDate) {
        return new Date(parseInt(aspDate.replace("/Date(", "").replace(")/", ""), 10));
    }

    root.helper = helper;
})(window, jQuery);