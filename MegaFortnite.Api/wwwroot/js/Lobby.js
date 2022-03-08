"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();


async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();


async function createLobby() {
    try {
        var userId = parseInt(document.getElementById("userIdInput").value, 10);
        await connection.invoke("CreateLobby", userId).catch(function (err) {
            return console.error(err.toString());
        });
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function join() {
    try {
        var userId = parseInt(document.getElementById("userIdInput").value, 10);
        var key = document.getElementById("lobbyKeyInput").value;
        await connection.invoke("Join", userId, key).catch(function (err) {
            return console.error(err.toString());
        });

        document.getElementById("createButton").disabled = true;
        document.getElementById("join").disabled = true;
        document.getElementById("lobbyKeyInput").disabled = true;
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

document.getElementById("join").addEventListener("click", join, false);
document.getElementById("createButton").addEventListener("click", createLobby, false);

document.getElementById("healthBar").value = 0;
document.getElementById("healthBar").visibility = false;
document.getElementById("Result").visibility = false;
document.getElementById("Log").visibility = true;

connection.on("InputDamage", function (damage) {
    document.getElementById("healthBar").value -= damage;
});

connection.on("LobbyCreated", function (lobbyInfo) {
    LogWarning(lobbyInfo.key);
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

connection.on("LogWarning", LogWarning);
async function LogWarning(message) {
    var li = document.createElement("li");
    li.class = "warningMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
}

connection.on("LogError", LogError);
async function LogError(message) {
    var li = document.createElement("li");
    li.class = "errorMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = typeof message === 'string' || message instanceof String ? `${message}` : `${message.message}`;
}

connection.on("LogMessage", LogMessage);
async function LogMessage(message) {
    var li = document.createElement("li");
    li.class = "normalMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
}

connection.start().then(function () {
    document.getElementById("createButton").disabled = false;

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("createButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userIdInput").value;
    connection.invoke("CreateLobby", userId, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});