app.service('chuyensaleService', function ($http) {
    this.get_listchuyensale = function () {
        return $http.get('/api/Api_ChuyenSale').then(function (response) {
            return response.data;
        });
    };

    this.save_listchuyensale = function (makh,data_save) {
        return $http.put('/api/Api_ChuyenSale/' + makh, data_save);
    };
});