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

let options = {
    accessToken: function () {
        return mgr.getUser().access_token;
    },
    accessTokenFactory: () => mgr.getUser().access_token,
};

let accessToken;
let connection;

mgr.getUser().then(async function (user) {
    if (user) {
        document.getElementById("userIdInput").value = user.profile.sub;
        document.getElementById("userIdInput").disabled = true;

        await initGame(user);
    } else {
        await LogWarning("User not connected");
    }
});

async function initGame(user) {
    accessToken = user.access_token;

    connection = new signalR.HubConnectionBuilder().withUrl(`/lobbyHub?token=${accessToken}`).build();
    connection.onclose(async () => {
        await LogWarning("Connection closing")
        await start(connection);
        await LogWarning("Connection closed")
    });

    connection.on("LogWarning", LogWarning);
    connection.on("LogMessage", LogMessage);
    connection.on("LogError", LogError);
    connection.on("LobbyCreated", async function (lobbyInfo) {
        await LogWarning(lobbyInfo.key);
    });

    // Start the connection.
    await start(connection);

    async function start(connection) {
        try {
            await connection.start().catch(async err => await LogError(err.toString()));
            await LogMessage("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }
}

async function createLobby() {
    try {
        let userId = document.getElementById("userIdInput").value;
        await connection.invoke("CreateLobby", userId).catch(function (err) {
            return console.error(err.toString());
        });
    } catch (err) {
        console.log(err);
    }
}

async function join() {
    try {
        let userId = document.getElementById("userIdInput").value;
        let key = document.getElementById("lobbyKeyInput").value;
        await connection.invoke("Join", userId, key).catch(async function (err) {
            return await LogError(err.toString());
        });

        document.getElementById("createButton").disabled = true;
        document.getElementById("join").disabled = true;
        document.getElementById("lobbyKeyInput").disabled = true;
    } catch (err) {
        console.log(err);
    }
}

document.getElementById("join").addEventListener("click", join, false);
document.getElementById("createButton").addEventListener("click", createLobby, false);
