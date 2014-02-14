function GetAllTeamsCtrl($rootScope, $scope, $timeout, crudTeamService) {

    $scope.refreshTeams = function () {
        crudTeamService.getTeamsByType(null, function (data) {
            $rootScope.$broadcast("getTeamsByType", data);
        })
    };

    $timeout($scope.refreshTeams, 0);

}

function CreateTeamCtrl($rootScope, $scope, searchPlayer, crudTeamService) {

    $scope.status = "";
    $scope.showCreateDelete = false;
    $scope.players = [];
    $scope.playerSelected = null;

    $scope.search = function () {

        searchPlayer.search($scope.playerName, function (data) {
            $scope.players = data;
            if ($scope.players.length > 0) {
                $scope.status = "open";
            }
            else {
                $scope.status = "";
            }
        });
    };

    $scope.selectPlayer = function (player) {
        $scope.playerSelected = player;
        $scope.playerName = player.Nombre;
        $scope.status = "";
        $scope.showCreateDelete = true;
    };

    $scope.addSelectedPlayerToTeam = function () {
        crudTeamService.antiForgeryToken = $scope.antiForgeryToken;
        crudTeamService.addSelectedPlayerToTeam($scope.playerSelected.Id, function (data) {
            $scope.deleteSelectedPlayer();
            crudTeamService.getTeamsByType(null, function (data) {
                $rootScope.$broadcast("getTeamsByType", data);
            })
        });
    }

    $scope.deleteSelectedPlayer = function () {
        $scope.playerSelected = null;
        $scope.showCreateDelete = false;
        $scope.status = "";
        $scope.playerName = "";
    };
}

app.service('crudTeamService', ['$rootScope', '$http', function ($rootScope, $http) {
    var crudTeamService = {};

    crudTeamService.antiForgeryToken;

    crudTeamService.getTeamsByType = function (type, callbackOk) {

        var url = '/equipos/_equiposporjugador';
        $http.post(url, { "tipo": type })
                    .success(function (data, status) {
                        if (data.length > 0) {
                            callbackOk(data);
                        }
                    })
                    .error(function (data, status) {
                        handleError("Ha ocurrido un error y no se pudo obtener los equipos. Inténtelo más tarde.");
                    });
    }


    crudTeamService.addSelectedPlayerToTeam = function (playerId, callbackOk) {

        var url = '/equipos/_asignarJugadorSeleccionadoAlEquipo';
        $http.post(url, { "idJugador": playerId }
        , { headers: { 'RequestVerificationToken': crudTeamService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
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
                        handleError("Ha ocurrido un error y no se pudo crear el equipo. Inténtelo más tarde.");
                    });
    }

    crudTeamService.verifyPlayer = function (teamId, callbackOk) {

        var url = '/equipos/_verificarJugador';
        $http.post(url, { "idEquipo": teamId }
        , { headers: { 'RequestVerificationToken': crudTeamService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
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
                        handleError("Ha ocurrido un error y no se pudo verificar al jugador. Inténtelo más tarde.");
                    });
    }

    crudTeamService.deleteTeam = function (teamId, callbackOk) {

        var url = '/equipos/_eliminarEquipo';
        $http.post(url, { "idEquipo": teamId }
        , { headers: { 'RequestVerificationToken': crudTeamService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest"} })
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
                        handleError("Ha ocurrido un error y no se pudo eliminar el equipo. Inténtelo más tarde.");
                    });
    }

    return crudTeamService;

} ]);
