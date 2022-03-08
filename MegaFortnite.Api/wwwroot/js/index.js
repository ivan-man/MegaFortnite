﻿
document.getElementById("login").addEventListener("click", login, false);

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

var config = {
    authority: "http://localhost:5666",
    client_id: "js",
    redirect_uri: "http://localhost:5123/callback.html",
    response_type: "code",
    post_logout_redirect_uri: "http://localhost:5123/index.html",
    scope: "x_scope"
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}
