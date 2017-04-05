app.controller('chuyensaleCtrl', function (chuyensaleService, khachhangService, $scope) {
    $scope.load_listchuyensale = function () { 
        chuyensaleService.get_listchuyensale().then(function (a) {
            $scope.list_chuyensale = a;
        });
        $scope.check = function () {
            return true;
        };
        
    };
    $scope.load_listchuyensale();

    $scope.save = function (entry) {
        $scope.entry = entry;
        var data_save = {
            ID : $scope.entry.ID,
            MA_KHACH_HANG: $scope.entry.MA_KHACH_HANG,
            SALE_HIEN_THOI: $scope.entry.SALE_HIEN_THOI,
            SALE_SAP_CHUYEN: $scope.entry.SALE_SAP_CHUYEN,
            SALE_CU: $scope.entry.SALE_CU,
            SALE_CU_2 : $scope.entry.SALE_CU_2,
        }
        chuyensaleService.save_listchuyensale($scope.entry.MA_KHACH_HANG, data_save).then(function () {
            $scope.load_listchuyensale();
        });
    };

    var tmpDate = new Date();

    $scope.newField = {};

    $scope.editing = false;

    $scope.editAppKey = function (field) {
        $scope.editing = $scope.appkeys.indexOf(field);
        $scope.newField = angular.copy(field);
    }

    $scope.saveField = function (index) {
        if ($scope.editing !== false) {
            $scope.appkeys[$scope.editing] = $scope.newField;
            $scope.editing = false;
        }
    };

    $scope.cancel = function (index) {
        //if ($scope.editing !== false) {
        //    $scope.appkeys[$scope.editing] = $scope.newField;
        //    $scope.editing = false;
        //}
        $scope.load_listchuyensale();
    };

    $scope.load_nhanvienkd = function () {
        khachhangService.get_nhanvienkd().then(function (c) {
            $scope.list_nhanvienkd = c;
        });
    };
    $scope.load_nhanvienkd();
});