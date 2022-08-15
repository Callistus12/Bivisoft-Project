setInterval(myTimer);

function myTimer() {
	const d = new Date();
	d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();

	document.getElementById("schoolClock").innerHTML = d;
  }
