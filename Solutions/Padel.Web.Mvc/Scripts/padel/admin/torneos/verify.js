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
    $scope.sortInfo = { fields: ['Nombre'], directions: ['asc'] };
    $scope.setPagingData = function (data, page, pageSize) {
        $scope.allData = data;
        var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        $scope.myData = pagedData;
        $scope.totalServerItems = data.length;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };
    $scope.getPagedDataAsync = function (sortInfo, pageSize, page, searchText) {
        setTimeout(function () {
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                $http.post('/admin/torneos/_listadopendientes').success(function (largeLoad) {
                    data = largeLoad.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    $scope.setSortPagingData(sortInfo, data, page, pageSize);
                });
            } else {
                $http.post('/admin/torneos/_listadopendientes').success(function (largeLoad) {
                    $scope.setSortPagingData(sortInfo, largeLoad, page, pageSize);
                });
            }
        }, 100);
    };
    $scope.setSortPagingData = function (sortInfo, data, page, pageSize) {
        var fun = function (a, b) {
            if (sortInfo.directions[0] == "asc") {
                return a[sortInfo.fields[0]].localeCompare(b[sortInfo.fields[0]]);
            }
            else {
                return b[sortInfo.fields[0]].localeCompare(a[sortInfo.fields[0]]);
            }
        };
        $scope.setPagingData(data.sort(fun), page, pageSize);
    };

    $scope.verifyTournament = function (idCategory) {
        $http.post('/admin/torneos/_verificar', { "idCategoria": idCategory },
        {
            headers: { 'RequestVerificationToken': $scope.antiForgeryToken, "X-Requested-With": "XMLHttpRequest" }
        }).success(function (data) {
            if (data.Success) {
                handleOk(data.Message);
            }
            else {
                handleError(data.Message);
            }
        });
    };

    $scope.getPagedDataAsync($scope.sortInfo, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
            $scope.getPagedDataAsync($scope.sortInfo, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getPagedDataAsync($scope.sortInfo, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
    $scope.$watch('sortInfo', function (newVal, oldVal) {
        if (newVal !== oldVal && $scope.allData) {
            $scope.setSortPagingData(newVal, $scope.allData, $scope.pagingOptions.currentPage, $scope.pagingOptions.pageSize);
        }
    }, true);

    $scope.gridOptions = {
        data: 'myData',
        columnDefs: [{ field: 'Nombre', displayName: 'Nombre' }, { field: 'Tipo', displayName: 'Tipo' }, { field: 'Categoria', displayName: 'Categoria' }
        , { field: 'Estado', displayName: 'Estado' }
        , { field: 'FechaModificacion', displayName: 'Ultima Verificación', headerClass: 'center', cellClass: 'center', cellTemplate: '<div>{{row.getProperty(col.field) | date:"dd/MM/yyyy @ HH:mma"}}</div>' }
        , { field: 'CategoriaId', displayName: 'Acciones', headerClass: 'center', cellClass: 'center', cellTemplate: '<a class="btn btn-xs btn-primary" href="#" role="button" ng-click="verifyTournament(row.getProperty(col.field))">Verificar</a>'}],
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
        useExternalSorting: true,
        sortInfo: $scope.sortInfo,
        enableRowSelection: false,
        enableColumnResize: true,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };
});