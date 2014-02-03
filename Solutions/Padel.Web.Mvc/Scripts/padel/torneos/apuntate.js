$(function () {
    $('#apuntateWizard').on("finished", 
        function() {
            alert("hola");
        });
});

function BuscarEquiposCtrl($scope, $http) {
    $scope.url = '/usuarios/_equiposporjugador'; // The url of our search
    $scope.title = "No hay equipos";
    $scope.status = "disabled";
    $scope.teams = [];
    $scope.teamSelected = 0;
    $scope.idJugador = 0;

    $scope.selectTeam = function (teamId) {
        $scope.teamSelected = teamId;
    };

    $scope.$watch('idJugador', function (newValue, oldValue) {
        // Create the http post request
        // The request is a JSON request.
        $http.post($scope.url, { "idJugador": $scope.idJugador }).
            success(function (data, status) {
                $scope.teams = data;
                if ($scope.teams.length > 0) {
                    $scope.title = "Selecciona un equipo";
                    $scope.status = "";
                }
                else {
                    $scope.status = "disabled";
                }
            })
            .
            error(function (data, status) {
            });
    });
}

function BuscarJugadorCtrl($scope, $http) {
    $scope.url = '/usuarios/_jugadorpornombre'; // The url of our search
    $scope.status = "";
    $scope.showDelete = false;
    $scope.players = [];
    $scope.playerSelected = 0;

    // The function that will be executed on button click (ng-click="search()")
    $scope.search = function () {

        // Create the http post request
        // the data holds the keywords
        // The request is a JSON request.
        $http.post($scope.url, { "nombreJugador": $scope.nombreJugador }).
        success(function (data, status) {
            $scope.players = data;
            if ($scope.players.length > 0) {
                $scope.status = "open";
            }
            else {
                $scope.status = "";
            }
        })
        .
        error(function (data, status) {
        });
    };

    $scope.selectPlayer = function (player) {
        $scope.playerSelected = player;
        $scope.nombreJugador = player.Nombre;
        $scope.status = "";
        $scope.showDelete = true;
    };

    $scope.deleteSelectedPlayer = function () {
        $scope.playerSelected = null;
        $scope.showDelete = false;
        $scope.status = "";
        $scope.nombreJugador = "";
    };

}

function AceptarTerminosCtrl($scope, $http) {
    $scope.aceptarTerminos = false;
}

