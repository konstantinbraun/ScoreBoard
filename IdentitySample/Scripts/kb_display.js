$(function () {
    var url = '/Referees/DisplayFullHD/1';
    $("#PartialViewDivId").load(url);
    setInterval(function () {
        var url = '/Referees/DisplayFullHD/1';
        $("#PartialViewDivId").load(url);
    }, 5000);

    $.ajaxSetup({ cache: false });
});