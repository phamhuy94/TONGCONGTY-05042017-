app.service('ChecktonService', function ($http) {
    this.get_dataton = function () {
        return $http.get("/api/Api_Checktonkho/").then(function (response) {
            return response.data;
        });
    }
});
