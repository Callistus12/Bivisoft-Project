// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
setInterval(myTimer);

function myTimer() {
	const d = new Date();
	d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + d.getDay() + "/" + d.getMonth() + "/" + d.getFullYear() + "<br>";

	document.getElementById("schoolClock").innerHTML = d;
};
	////$(document).ready(function () {
	////	$("#flip").click(function () {
	////		$("#panel").slideToggle(4000)
	////	});
	////});
