"use strict";

let config = {
    authority: "https://localhost:5001",
    client_id: "js",
    redirect_uri: "https://localhost:5123/callback.html",
    response_type: "code",
    post_logout_redirect_uri: "https://localhost:5123/lobby.html",
    scope: "x_scope"
};

let mgr = new Oidc.UserManager(config);

function login() {
    mgr.signinRedirect();
}

let options = {
    accessToken: function () {
        return mgr.getUser().access_token;
    }
};

document.getElementById("login").addEventListener("click", login, false);

mgr.getUser().then(function (user) {
    if (user) {
        document.getElementById("userIdInput").value = user.profile.sub;
        document.getElementById("userIdInput").disabled = true;

        this.connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

        connection.onclose(async () => {
            await start();
        });

        async function start() {
            try {
                await connection.start();
                console.log("SignalR Connected.");
            } catch (err) {
                console.log(err);
                setTimeout(start, 5000);
            }
        }

// Start the connection.
        start();
    }
    else {
        LogWarning("User not connected");
    }
});



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
}

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
}

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
connection.on("LogError", LogError);
connection.on("LogMessage", LogMessage);

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
