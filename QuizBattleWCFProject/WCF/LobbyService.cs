using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Controllers;

namespace WCF
{
    public class LobbyService : ILobbyService
    {
        private LobbyCtrl lobbyCtrl;

        public LobbyService()
        {
            lobbyCtrl = new LobbyCtrl();
        }

        public void CreateLobby(Lobby lobby, User user)
        {
            lobbyCtrl.CreateLobby(lobby, user);
        }


        public List<Lobby> GetAllLobbies()
        {
            return lobbyCtrl.GetAllLobbies();
        }

        public Lobby GetLobby(int id)
        {
            return lobbyCtrl.GetLobby(id);
        }

        public void JoinLobby(User user, Lobby lobby)
        {
            lobbyCtrl.JoinLobby(user, lobby);
        }
       

        public List<Lobby> GetAllLobbiesWithCategoryId(int categoryId)
        {
            return lobbyCtrl.GetAllLobbiesWithCategoryId(categoryId);
        }

        public bool IsUserInLobby(string username)
        {
            return lobbyCtrl.IsUserInLobby(username);
        }

        public bool DoesUserAlreadyOwnALobby(string username)
        {
            return lobbyCtrl.DoesUserAlreadyOwnALobby(username);
        }

        public void UpdateLobbyStatus(Lobby lobby)
        {
            lobbyCtrl.UpdateLobbyStatus(lobby);
        }

        public bool IsLobbyStarted(int id)
        {
            return lobbyCtrl.IsLobbyStarted(id);
        }
    }
}
