"use strict";

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
