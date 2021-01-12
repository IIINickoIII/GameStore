var request = new XMLHttpRequest();
request.open('GET', '/games/numberofgames', true);

request.onload = function () {
    if (this.status >= 200 && this.status < 400) {
        var data = JSON.parse(this.response);
        document.getElementById("number-of-games").innerText = "Number of games - " + data;
        console.log("Number of games service request is completed");
    }
};

request.onerror = function () {
    console.log("Number of games service request is failed");
};

request.send();