const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    alert(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});