"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

document.getElementById("healthBar").value = 0;
document.getElementById("Result").visibility = false;
document.getElementById("Log").visibility = true;

connection.on("InputDamage", function (damage) {
    document.getElementById("healthBar").value -= damage;
});

connection.on("GameFinished", function (winnerId) {
    if (winnerId === document.getElementById("userIdInput").value) {
        document.getElementById("Result").value = "You won!";
    } else {
        document.getElementById("Result").value = "Looser!";
    }
    document.getElementById("Result").visibility = true;

    document.getElementById("GameResult").value -= damage;
});

connection.on("GameStarted", function (initHealth) {
    document.getElementById("Result").visibility = false;
    document.getElementById("healthBar").value += initHealth;
});

connection.on("LogWarning", function (message) {
    var li = document.createElement("li");
    li.class = "warningMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
});

connection.on("LogError", function (message) {
    var li = document.createElement("li");
    li.class = "errorMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
});

connection.on("LogMessage", function (message) {
    var li = document.createElement("li");
    li.class = "normalMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
});

//Disable the send button until connection is established.
document.getElementById("createButton").disabled = true;

connection.start().then(function () {
    document.getElementById("createButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("createButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userIdInput").value;
    connection.invoke("CreateLobby", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});