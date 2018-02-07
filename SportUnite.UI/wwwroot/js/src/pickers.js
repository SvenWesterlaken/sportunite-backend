(function ($) {



    $(document).ready(function() {
      var fullmonths = ['januari', 'februari', 'maart', 'april', 'mei', 'juni', 'juli', 'augustus', 'september', 'oktober', 'november', 'december'],
          shortmonths = ['Jan', 'Feb', 'Mar', 'Apr', 'Mei', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
          fullkweekdays = ['Zondag', 'Maandag', 'Dinsdag', 'Woensdag', 'Donderdag', 'Vrijdag', 'Zaterdag'],
          shortweekdays = ['Zo', 'Ma', 'Di', 'Wo', 'Do', 'Vr', 'Za'];

        Materialize.updateTextFields();

        $(".datepicker").pickadate({
            monthsFull: fullmonths,
            monthsShort: shortmonths,
            weekdaysFull: fullkweekdays,
            weekdaysShort: shortweekdays,
            selectMonths: true,
            selectYears: 15,
            today: 'Vandaag',
            clear: 'Leegmaken',
            close: 'Ok',
            format: 'd mmmm yyyy',
            closeOnSelect: false
        });
        $(".timepicker").pickatime({
            default: 'now',
            fromnow: 0,
            twelvehour: false,
            donetext: 'Ok',
            cleartext: "Leegmaken",
            canceltext: "Annuleren",
            autoclose: false
        });
    });

})(jQuery);
