var app = angular.module('padelApp', ['ngGrid']);
app.controller('torneosCtrl', function ($scope, $http) {
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: true
    };
    $scope.totalServerItems = 0;
    $scope.pagingOptions = {
        pageSizes: [5, 10, 20],
        pageSize: 5,
        currentPage: 1
    };
    $scope.setPagingData = function (data, page, pageSize) {
        var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        $scope.myData = pagedData;
        $scope.totalServerItems = data.length;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };
    $scope.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                $http.post('/admin/torneos/_listado').success(function (largeLoad) {
                    data = largeLoad.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    $scope.setPagingData(data, page, pageSize);
                });
            } else {
                $http.post('/admin/torneos/_listado').success(function (largeLoad) {
                    $scope.setPagingData(largeLoad, page, pageSize);
                });
            }
        }, 100);
    };

    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);

    $scope.gridOptions = {
        data: 'myData',
        columnDefs: [{ field: 'Nombre', displayName: 'Nombre' }, { field: 'Tipo', displayName: 'Tipo' }, { field: 'Categoria', displayName: 'Categoria' } ],
        enablePaging: true,
        showFooter: true,
        footerTemplate: '<div class="widget-foot">' +
                '<ul class="pagination pull-left">' +
                    '<li><span>Número total de torneos {{totalServerItems}}</span></li>' +
                '</ul>' +
                '<ul class="pagination pull-right">' +
                    '<li><a href="#" ng-click="pageBackward()" ng-disabled="cantPageBackward()">Prev</a></li>' +
                    '<li><a href="#" ng-click="pageForward()" ng-disabled="cantPageForward()">Next</a></li>' +
                '</ul>' +
                '<div class="clearfix">' +
                '</div>' +
            '</div>',
        enableRowSelection: false,
        enableColumnResize: true,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };
});