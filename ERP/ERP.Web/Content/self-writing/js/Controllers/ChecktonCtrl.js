app.controller('ChecktonCtrl', function (ChecktonService, $scope) {
    $scope.datatonkho = function () {
        ChecktonService.get_dataton().then(function (a) {
            $scope.listtonkho = a;
        });
    };
    $scope.datatonkho();
    
});
