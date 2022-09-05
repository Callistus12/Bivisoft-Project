// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
setInterval(myTimer);

function myTimer() {
	
	const d = new Date();
    var time = d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "<br>" + d.getDay() + "/" + d.getMonth() + "/" + d.getFullYear();

    document.getElementById("schoolClock").innerHTML = d;
};
$(window).on("load", function () {
    debugger;

    $("#hideBtnFromEditUser").hide();
});
$(document).ready(function () {
     debugger;
    $('#changeDisableInput').click(function () {
        $("#changeDisableInput").hide();
        $("#hideBtnFromEditUser").show();
        $("#nameForInput :input").prop('disabled', false);
    })
});



function deleteUser(id) {
    $("#link").attr("href", "/Admin/Delete?userId=" + id);

}

$(document).ready(function () {
    $("#AdminBtnForSignInRole").on({
        click: function () {
            $(this).css("background-color", "yellow");
        }
    });
});