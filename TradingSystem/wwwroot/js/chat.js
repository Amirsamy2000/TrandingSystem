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
    try {
        await connection.invoke("SendMessage", communityId, text);
        document.getElementById('chat-input').value = '';
    } catch (err) {
        // Handle HubException (e.g., not a member)
        let errorMsg = "An error occurred while sending the message.";
        if (err && err.message && err.message.includes("Not a member of this community")) {
            errorMsg = "You are not a member of this community and cannot send messages.";
        }
        alert(errorMsg);
    }
}

document.addEventListener('DOMContentLoaded', start);

document.getElementById('chat-input').addEventListener('keydown', function (e) {
    if (e.key === 'Enter') {
        sendMessage(this.value);
    }
});
