(function () {
    'use strict';

    angular
        .module('app')
        .controller('controller', controller);

    controller.$inject = ['$scope'];

    function controller($scope, $http) {

        activate();

        function activate() {
            $scope.keyWord = null;
            $scope.pageIndex = 1;
            $scope.pageSize = 5;

            var data = {
                keyWord: keyWord,
                pageIndex: pageIndex,
                pageSize: pageSize
            }

            $http.get("/Admin/DanhMuc/GetPagingChuyenNganh", data)
                .then(function (response) {
                    console.log(response)
                })
        }
    }
})();
