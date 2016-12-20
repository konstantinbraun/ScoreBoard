$(function () {
    var submitAutocompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();
    }

    var createAutocomplete = function () {
        var $input = $(this);
        var option = {
            source: $input.attr("data-autocomplete"),
            select: submitAutocompleteForm
        };

        $input.autocomplete(option);
    };

    $("input[data-autocomplete]").each(createAutocomplete);
});

function OnBegin() {
    $("#loading").text("Loading...").fadeIn("slow", function () { });
};

function OnComplete() {
    $("#loading").fadeOut("slow", function () { });
};

