function OnBegin() {
    $("#loading").removeClass("hidden");
  //  InitializeMap();
};

function OnComplete() {
    $('[data-toggle="tooltip"]').tooltip();
    $("#loading").addClass("hidden");
    countdownUpdate();
 };

function OnCompleteMap() {
    $('[data-toggle="tooltip"]').tooltip();
    $("#loading").addClass("hidden");
    InitializeMap();
};

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
