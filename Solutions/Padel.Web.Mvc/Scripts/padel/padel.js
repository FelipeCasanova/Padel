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