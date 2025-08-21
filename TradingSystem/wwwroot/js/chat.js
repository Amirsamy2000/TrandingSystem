const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/community")
    .build();

connection.on("ReceiveMessage", function (msg) {
    renderMessage(msg);
});

let connectionStarted = false;

async function start() {
    if (connectionStarted || connection.state !== signalR.HubConnectionState.Disconnected) return; // Prevent multiple starts
    try {
        await connection.start();
        connectionStarted = true;
        await connection.invoke("JoinCommunity", communityId);
    } catch (err) {
        console.error(err);
        setTimeout(start, 2000); // Try again after 2 seconds if failed
    }
}

async function sendMessage(text) {
    if (!text.trim()) return;
    await connection.invoke("SendMessage", communityId, text);
    document.getElementById('chat-input').value = '';
}

document.addEventListener('DOMContentLoaded', start);

document.getElementById('chat-input').addEventListener('keydown', function (e) {
    if (e.key === 'Enter') {
        sendMessage(this.value);
    }
});
