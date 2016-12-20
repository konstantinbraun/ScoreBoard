if (!Modernizr.inputtypes.date) {
    $(function () {
        $(".datecontrol").datepicker({language: 'ru'});
    });
}

$(function () {
    //$.culture = jQuery.cultures['de'];
    //$.preferCulture($.culture.name);
    //Globalization.preferCulture($.culture.name);

    $.validator.methods.number = function (value, element) {
        if (Globalize.parseFloat(value, null, "ru")) {
            return true;
        }
        return false;
    }

    $.validator.methods.date = function (value, element) {
        //Globalize.culture("de");
        // you can alternatively pass the culture to parseDate instead of
        // setting the culture above, like so:
        // parseDate(value, null, "en-AU")
        return this.optional(element) || Globalize.parseDate(value, null, "ru") !== null;
    }
});