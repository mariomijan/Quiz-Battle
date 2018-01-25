$(function () {
    // Declare a proxy to reference the hub. 
    var lobbyHub = $.connection.lobbyHub;
    InitializeServerCallableFunctions();
    //start up the hub
    $.connection.hub.start().done(function () {

        lobbyHub.server.join("XXl", "testhandle");

        //lobbyHub.server.getLobbies();
        //$('#sendmessage').click(function () {
        //    // Call the Send method on the hub. 
        //    chat.server.join("testLobby");
        //    // Clear text box and reset focus for next comment. 
        //    $('#message').val('').focus();
        //});
    });

    function InitializeServerCallableFunctions() {

        lobbyHub.client.joinLobby = function (lobbyName, handle) {
            // Html encode display name and message. 
            var encodedName = $('<div />').text(lobbyName).html();
            var encodedMsg = $('<div />').text(lobbyName).html();
            // Add the message to the page. 
            $('#discussion').append('<li><strong>' + encodedName
                + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        };
        lobbyHub.client.leaveLobby = function (lobbyName) {
            // Html encode display name and message. 
            var encodedName = $('<div />').text(lobbyName).html();
            var encodedMsg = $('<div />').text(lobbyName).html();
            // Add the message to the page. 
            $('#discussion').append('<li><strong>' + encodedName
                + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        };
        lobbyHub.client.logToConsole = function (msg) {
            console.log(msg, "testLobby");
        };
        lobbyHub.client.displayLobbies = function (lobbies) {
            console.log(lobbies);
            for (var i = 0; i < lobbies.length; i++) {
                console.log(lobbies[i]);
                $('#lobbies-container').append(lobbies[i].Name + "<br/>");
            }
        };


    }
});
