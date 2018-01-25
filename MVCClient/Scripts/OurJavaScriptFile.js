//Detect browser refresh and call action from controller
var logout = true;
var success = false;

$(document).ready(function () {

    $(".answerbtn").click(function () {
        logout = false;
    });

    var myEvent = window.attachEvent || window.addEventListener;
    var chkevent = window.attachEvent ? 'onbeforeunload' : 'beforeunload';

    myEvent(chkevent, function (e) {

        if (logout === true) {

            $.ajax(
            {
                type: "POST",
                traditional: true,
                dataType: "json",
                async: false,
                url: '/Quiz/RedirectToPage',

                success: function (data) {

                    if (data === "Success") {
                        success = true;
                    }
                }
            });
        }
    });
});



