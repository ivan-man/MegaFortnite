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
    // let connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub", options).build();
    let connection = new signalR.HubConnectionBuilder().withUrl(`/lobbyHub?token=${user.access_token}`).build();
    // const signalR = new HubConnectionBuilder().withUrl(`/lobbyHub?token=${token}`).build();
    connection.onclose(async () => {
        await start();
    });

    connection.on("LogWarning", LogWarning);
    connection.on("LogMessage", LogMessage);
    connection.on("LogError", LogError);

    // Start the connection.
    await start();

    async function start() {
        try {
            await connection.start().catch(async err => await LogError(err.toString()));
            await LogMessage("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }
}

async function LogWarning(message) {
    let li = document.createElement("li");
    li.class = "warningMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
}

async function LogMessage(message) {
    let li = document.createElement("li");
    li.class = "normalMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = `${message}`;
}

async function LogError(message) {
    let li = document.createElement("li");
    li.class = "errorMessage";
    document.getElementById("Log").appendChild(li);
    li.textContent = typeof message === 'string' || message instanceof String ? `${message}` : `${message.message}`;
}
