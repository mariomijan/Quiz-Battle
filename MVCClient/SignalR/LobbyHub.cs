using Microsoft.AspNet.SignalR;
using MVCClient.LobbyReference;
using MVCClient.Models;
using MVCClient.UserReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace MVCClient.SignalR
{
    public class LobbyHub : Hub
    {
        private UserServiceClient userClient = new UserServiceClient();

        public LobbyHub()
        {

        }

        public void Join(int lobbyId)
        {
            Groups.Add(Context.ConnectionId, lobbyId.ToString());

            var users = userClient.GetUsersInLobby(lobbyId);
            Clients.Group(lobbyId.ToString()).getUsersInLobby(users);
        }
    }
}