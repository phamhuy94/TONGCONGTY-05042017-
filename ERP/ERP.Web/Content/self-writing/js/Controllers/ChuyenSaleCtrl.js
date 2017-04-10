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
        $scope.check2 = function () {
            return false;
        };
        $scope.check1 = function () {
            return false;
        };
    };


    $scope.add = function (entry) {
        $scope.entry = entry;
        var data_add = {
            ID: $scope.entry.ID,
            MA_KHACH_HANG: $scope.entry.MA_KHACH_HANG,
            SALE_HIEN_THOI: $scope.entry.SALE_HIEN_THOI,
            SALE_SAP_CHUYEN: $scope.entry.SALE_SAP_CHUYEN,
            SALE_CU: $scope.entry.SALE_CU,
            SALE_CU_2: $scope.entry.SALE_CU_2,
        }
        chuyensaleService.add_listchuyensale(data_add).then(function () {
            $scope.load_listchuyensale();
        });
        $scope.check2 = function () {
            return false;
        };
        $scope.check1 = function () {
            return false;
        };
    };
    var tmpDate = new Date();

    $scope.newField = {};

    $scope.editing = false;

    $scope.editAppKey = function (field) {
        
        $scope.entry = field;
        if ($scope.entry.SALE_HIEN_THOI != null || $scope.entry.SALE_SAP_CHUYEN != null || $scope.entry.SALE_CU != null || $scope.entry.SALE_CU_2 != null) {
            $scope.check1 = function () {
                return true;
            };
        } else {
            $scope.check2 = function () {
                return true;
            };
        }
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
        $scope.check2 = function () {
            return false;
        };
        $scope.check1 = function () {
            return false;
        };
    };

    $scope.load_nhanvienkd = function () {
        khachhangService.get_nhanvienkd().then(function (c) {
            $scope.list_nhanvienkd = c;
        });
    };
    $scope.load_nhanvienkd();

    
});