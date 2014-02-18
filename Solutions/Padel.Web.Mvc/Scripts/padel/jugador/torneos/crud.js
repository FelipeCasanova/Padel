function GetAllTournamentsCtrl($rootScope, $scope, $timeout, crudTournamentService) {

    $scope.refreshTournaments = function () {
        crudTournamentService.antiForgeryToken = $scope.antiForgeryToken;
        crudTournamentService.getTournamentsByType(null, function (data) {
            $rootScope.$broadcast("getTournamentsByType", data);
        })
    };

    $timeout($scope.refreshTournaments, 0);

}

app.service('crudTournamentService', ['$rootScope', '$http', function ($rootScope, $http) {
    var crudTournamentService = {};

    crudTournamentService.antiForgeryToken;

    crudTournamentService.getTournamentsByType = function (type, callbackOk) {

        var url = '/torneos/_torneosPorJugador';
        $http.post(url, { "tipo": type },
        { headers: { 'RequestVerificationToken': crudTournamentService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
                    .success(function (data, status) {
                        if (data.length > 0) {
                            callbackOk(data);
                        }
                    })
                    .error(function (data, status) {
                        handleError("Ha ocurrido un error y no se pudo obtener los equipos. Inténtelo más tarde.");
                    });
    };

    crudTournamentService.unsignupFromTournament = function (teamCategoryId, callbackOk) {

        var url = '/torneos/_desapuntanteDelTorneo';
        $http.post(url, { "idEquipoCategoria": teamCategoryId }
        , { headers: { 'RequestVerificationToken': crudTournamentService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
                    .success(function (data, status) {
                        if (data.Success) {
                            callbackOk(data.Result);
                            handleOk(data.Message);
                        }
                        else {
                            handleError(data.Message);
                        }
                    })
                    .error(function (data, status) {
                        handleError("Ha ocurrido un error y no se pudo eliminar el equipo. Inténtelo más tarde.");
                    });
    };

    crudTournamentService.payTournamentWithPoints = function (teamCategoryId, type, callbackOk) {

        var url = '/torneos/_pagaElTorneoConPuntos';
        $http.post(url, { "idEquipoCategoria": teamCategoryId, "tipo" : type }
        , { headers: { 'RequestVerificationToken': crudTournamentService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
                    .success(function (data, status) {
                        if (data.Success) {
                            callbackOk(data);
                            handleOk(data.Message);
                        }
                        else {
                            handleError(data.Message);
                        }
                    })
                    .error(function (data, status) {
                        handleError("Ha ocurrido un error y no se pudo pagar el torneo. Inténtelo más tarde.");
                    });
    };


    return crudTournamentService;

} ]);
