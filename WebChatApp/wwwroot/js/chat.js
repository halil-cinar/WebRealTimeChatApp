"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (item) {
    var messageList = document.getElementById("messagesList")
    
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    messageList.innerHTML += `   <div class="${((item.mySend == true) ? "chat-message-right" : "chat-message-left")} pb-4">
                <div>
                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" class="rounded-circle mr-1" alt="Chris Wood" width="40" height="40">
                    <div class="text-muted small text-nowrap mt-2">${item.sendTime}</div>
                </div>
                <div class="flex-shrink-1 bg-light rounded py-2 px-3 mr-3">
                                    <div class="font-weight-bold mb-1">${item.senderName}</div>
                    ${item.message}
                </div>
            </div>`
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var chatId = document.getElementById("chatIdInput").value;

    var item = {
        SenderName: user,
        Message: message,
        chatId: chatId
    }

    connection.invoke("SendMessage", JSON.stringify(item)
    ).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});