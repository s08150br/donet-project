window.onload = function ()
{

    //volunteer index sections
    var vol_opening = document.getElementById("vol_opening");
    var vol_terms = document.getElementById("vol_terms");
    var vol_form = document.getElementById("vol_form");

    //buttons
    var vol_opening__btn = document.getElementById("vol_opening__btn");
    var vol_terms__btn = document.getElementById("vol_terms__btn");

    $("#vol_opening").show();
    $("#vol_terms").hide();
    $("#vol_form").hide();


    vol_opening__btn.onclick = function () {
        $("#vol_opening").hide();
        $("#vol_terms").show();
        $("#vol_form").hide();
    }

    vol_terms__btn.onclick = function () {
        $("#vol_opening").hide();
        $("#vol_terms").hide();
        $("#vol_form").show();
    }
}