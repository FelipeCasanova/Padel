/* Jugadores Controller */

function PlayersCtrl($scope, $http, savePlayersService) {

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
                $http.post('jugadores/_listado').success(function (largeLoad) {
                    data = largeLoad.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    $scope.setPagingData(data, page, pageSize);
                });
            } else {
                $http.post('jugadores/_listado').success(function (largeLoad) {
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
        columnDefs: [{ field: 'Nombre', displayName: 'Nombre', enableCellEdit: true }, { field: 'Sexo', displayName: 'Sexo', enableCellEdit: true }
                    , { field: 'TelefonoMovil', displayName: 'Móvil', enableCellEdit: true }, { field: 'Email', displayName: 'Email', enableCellEdit: true }
                    , { field: 'Role', displayName: 'Rol'}],
        enablePaging: true,
        showFooter: true,
        footerTemplate: '<div class="widget-foot">' +
                '<ul class="pagination pull-left">' +
                    '<li><span>Número total de usuarios {{totalServerItems}}</span></li>' +
                '</ul>' +
                '<ul class="pagination pull-right">' +
                    '<li><a href="#" ng-click="pageBackward()" ng-disabled="cantPageBackward()">Prev</a></li>' +
                    '<li><a href="#" ng-click="pageForward()" ng-disabled="cantPageForward()">Next</a></li>' +
                '</ul>' +
                '<div class="clearfix">' +
                '</div>' +
            '</div>',
        enableColumnResize: true,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };

    $scope.save = function () {
        
        savePlayersService.antiForgeryToken = $scope.antiForgeryToken;
        savePlayersService.data = $scope.myData;
        savePlayersService.save(function (data) {
            //$rootScope.$broadcast("getTournamentsByType", data);
        });
    };
};

PlayersCtrl.$inject = ['$scope', '$http', 'savePlayersService'];

/********************************/

/* Sign Up Service */

app.service('savePlayersService', ['$rootScope', '$http', function ($rootScope, $http) {

    var savePlayersService = {};

    savePlayersService.data;
    savePlayersService.antiForgeryToken;

    savePlayersService.save = function (callbackOk) {
        // Create the http post request
        // the data holds the keywords
        // The request is a JSON request.
        var url = 'jugadores/_modificar';

        $http.post(url, savePlayersService.data, 
        {
            headers: { 'RequestVerificationToken': savePlayersService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest" }
        })
        .success(function (data, status) {
            if (data.Success) {
                callbackOk(data);
            }
            else {
                handleError(data.Message);
            }
        })
        .error(function (data, status) {
            handleError("Ha ocurrido un error y no se pudo guardar a los jugadores. Inténtelo más tarde.");
        });
    }

    return savePlayersService;

} ]);

/********************************/