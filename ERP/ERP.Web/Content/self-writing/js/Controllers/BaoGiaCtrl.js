app.controller('baogiaCtrl', function (baogiaService,$scope, $http, $location) {

});











// Các phần bổ trợ,không sửa xóa,viết code js ở trên đoạn này
function reload() {
    location.reload();
}

function change() {
    $('.listmenu').toggle();
}

function accept() {
    $('.listmenu').css('display', 'none');
    reload();
}
app.directive('checkList', function () {
    return {
        scope: {
            list: '=checkList',
            value: '@'
        },
        link: function (scope, elem, attrs) {
            var handler = function (setup) {
                var checked = elem.prop('checked');
                var index = scope.list.indexOf(scope.value);

                if (checked && index == -1) {
                    if (setup) elem.prop('checked', false);
                    else scope.list.push(scope.value);
                } else if (!checked && index != -1) {
                    if (setup) elem.prop('checked', true);
                    else scope.list.splice(index, 1);
                }
            };

            var setupHandler = handler.bind(null, true);
            var changeHandler = handler.bind(null, false);

            elem.on('change', function () {
                scope.$apply(changeHandler);
            });
            scope.$watch('list', setupHandler, true);
        }
    };
});

app.filter('unsafe', function ($sce) { return $sce.trustAsHtml; });

app.filter('stringToDate', function ($filter) {
    return function (ele, dateFormat) {
        return $filter('date')(new Date(ele), dateFormat);
    }
})

function help_left() {
    $('.help_left').show();
    $('.nohelp_left').hide();
    $('.container_right').hide();
}

function nohelp_left() {
    $('.help_left').hide();
    $('.nohelp_left').show();
    $('.container_right').show();
}

function help_right() {
    $('.help_right').show();
    $('.nohelp_right').hide();
    $('.container_left').hide();
}

function nohelp_right() {
    $('.help_right').hide();
    $('.nohelp_right').show();
    $('.container_left').show();
}

// End bổ trợ