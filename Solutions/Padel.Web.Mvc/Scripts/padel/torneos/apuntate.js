$(function () {

    $('#apuntateWizard').wizard();
    var wizard = $('#apuntateWizard').data("wizard");

    $('#apuntateWizard').on("finished", function () {
        $('#signUpActionDiv').click();
    });

});

function SearchTeamsCtrl($scope, $timeout, searchTeams, signUpTournamentService) {

    $scope.title = "No hay equipos";
    $scope.showDelete = false;
    $scope.status = "disabled";
    $scope.teams = [];
    $scope.teamSelected = null;
    $scope.idJugador = 0;

    $scope.searchTeam = function () {
        searchTeams.search($scope.idJugador, function (data) {
            $scope.teams = data;

            if ($scope.teams.length > 0) {
                $scope.title = "Selecciona un equipo";
                $scope.status = "";
            }
            else {
                $scope.status = "disabled";
            }
        })
    };

    $scope.selectTeam = function (team) {
        $scope.teamSelected = team;
        $scope.title = (team.Nombre != null ? team.Nombre + "-" : "") + ($scope.idJugador == team.JugadorBId ? team.JugadorANombre : team.JugadorBNombre);
        $scope.showDelete = true;

        signUpTournamentService.teamIdBroadCast(team.Id);
    };

    $scope.deleteSelectedTeam = function () {
        $scope.teamSelected = null;
        $scope.showDelete = false;
        $scope.title = "Selecciona un equipo";

        signUpTournamentService.teamIdBroadCast(0);
    };

    $timeout($scope.searchTeam, 0);
}
SearchTeamsCtrl.$inject = ['$scope', '$timeout', 'searchTeams', 'signUpTournamentService'];

function SearchPlayerCtrl($scope, searchPlayer, signUpTournamentService) {

    $scope.status = "";
    $scope.showDelete = false;
    $scope.players = [];
    $scope.playerSelected = null;

    // The function that will be executed on button click (ng-click="search()")
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
        $scope.showDelete = true;

        signUpTournamentService.playerIdBroadCast(player.Id);
    };

    $scope.deleteSelectedPlayer = function () {
        $scope.playerSelected = null;
        $scope.showDelete = false;
        $scope.status = "";
        $scope.playerName = "";

        signUpTournamentService.playerIdBroadCast(0);
    };

}
SearchPlayerCtrl.$inject = ['$scope', 'searchPlayer', 'signUpTournamentService'];

function AcceptTermsCtrl($scope, signUpTournamentService) {
    $scope.acceptedTerms = false;
    $scope.$watch('acceptedTerms', function (newValue, oldValue) {
        signUpTournamentService.acceptedTermsBroadCast(newValue);
    });
}
AcceptTermsCtrl.$inject = ['$scope', 'signUpTournamentService'];

function DetailsPaymentCtrl($scope) {
    $scope.$on('torneoTitle', function (event, title) {
        $scope.title = title;
    });

    $scope.$on('price', function (event, price) {
        $scope.price = price;
    });

}
DetailsPaymentCtrl.$inject = ['$scope'];

function SignUpCtrl($scope, signUpTournamentService) {
    $scope.signUp = function () {
        signUpTournamentService.antiForgeryToken = $scope.antiForgeryToken;
        signUpTournamentService.signUp(
            function (data) {
                if ($('#apuntateModal')) {
                    $('#apuntateModal').on('hidden.bs.modal', function (e) {
                        handleOk(data.Message);
                    })
                    $('#apuntateModal').modal('hide');
                }
                else {
                    handleOk(data.Message);
                }
            });
    }
}

SignUpCtrl.$inject = ['$scope', 'signUpTournamentService'];

app.service('signUpTournamentService', ['$rootScope', '$http', function ($rootScope, $http) {
    var signUpService = {};

    signUpService.teamId;
    signUpService.playerId;
    signUpService.tournamentId;
    signUpService.categoryId;
    signUpService.acceptedTerms;
    signUpService.antiForgeryToken;

    signUpService.teamIdBroadCast = function (teamId) {
        signUpService.teamId = teamId;
        $rootScope.$broadcast("handleTeamIdBroadCast")
    }

    signUpService.playerIdBroadCast = function (playerId) {
        signUpService.playerId = playerId;
        $rootScope.$broadcast("handlePlayerIdBroadCast")
    }

    signUpService.acceptedTermsBroadCast = function (acceptedTerms) {
        signUpService.acceptedTerms = acceptedTerms;
        $rootScope.$broadcast("handleAcceptedTermsBroadCast")
    }

    signUpService.signUp = function (callbackOk) {
        // Create the http post request
        // the data holds the keywords
        // The request is a JSON request.
        var url = '/torneos/_crearequipoparatorneo'; // The url of our search
        $http.post(url, { "idEquipo": signUpService.teamId
            , "idJugador": signUpService.playerId
            , "idTorneo": signUpService.tournamentId
            , "idCategoria": signUpService.categoryId
            , "terminosAceptados": signUpService.acceptedTerms
        }
        , {
            headers: { 'RequestVerificationToken': signUpService.antiForgeryToken, "X-Requested-With": "XMLHttpRequest" }
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
                        handleError("Ha ocurrido un error y no se pudo registrar en el torneo. Inténtelo más tarde.");
                    });
    }

    return signUpService;

} ]);
