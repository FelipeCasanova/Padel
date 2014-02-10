var app = angular.module('padelApp', ['ngRoute'])
    .service('searchTeams', ['$http', function ($http) {
        return {

            search: function (idJugador, callback) {
                /**
                * searchTeams service allows te user to select one of their teams
                */
                var url = '/equipos/_equiposporjugador'; // The url of our search
                $http.post(url, { "idJugador": idJugador }).
                    success(function (data, status) {
                        callback(data);
                    })
                    .error(function (data, status) {});
            }
        }

    } ])
    .service('searchPlayer', ['$http', function ($http) {
        return {
            /**
            * searchPlayer service allows te user to select one of their teams
            */
            search: function (nombreJugador, callback) {
                // Create the http post request
                // the data holds the keywords
                // The request is a JSON request.
                var url = '/usuarios/_jugadorpornombre'; // The url of our search
                $http.post(url, { "nombreJugador": nombreJugador }).
                    success(function (data, status) {
                        callback(data);
                    })
                    .error(function (data, status) { });
            }
        }

    } ]);


$(function () {

    $('span[data-toggle=tooltip]').tooltip({});

    // Start loading on click
    $('button[data-loading-text]').click(function () {
        var btn = $(this);
        $(btn).button('loading');
        setTimeout(function () {
            $(btn).button('reset')
        }, 5000)
    });

});


function ajaxRegistrationComplete() {
    ajaxComplete();

    var $injector = angular.injector(['ng']);
    var $parse = $injector.get('$parse');

    $injector.invoke(function ($rootScope, $compile, $document) {
        $compile($document)($rootScope);
        $rootScope.telefonoMovil = $('#TelefonoMovil').attr('value');
        $rootScope.email = $('#Email').attr('value');
        $rootScope.password = $('#Password').attr('value');
        $rootScope.$apply();
    });
}

function ajaxComplete() {

    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $("form").removeData("unobtrusiveContainerPopover");
    $.validator.unobtrusive.parse("form");

    bootstrapValidation();

    // Stop loading effect on buttons
    $('button[data-loading-text]').each(function () {
        var btn = $(this);
        $(btn).button('reset')
    });
};