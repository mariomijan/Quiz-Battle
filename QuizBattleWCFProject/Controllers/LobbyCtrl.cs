using ControlLayer;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class LobbyCtrl
    {
        private DbLobby dbLobby;
        private UserCtrl userCtrl;
        

        public LobbyCtrl()
        {
            dbLobby = new DbLobby();
            userCtrl = new UserCtrl();
        }

        public void CreateLobby(Lobby lobby, User user)
        {
            dbLobby.CreateLobby(lobby, user);
        }

        public Lobby GetLobby(int id)
        {
            return dbLobby.GetLobby(id);
        }

        public List<Lobby> GetAllLobbies()
        {
            return dbLobby.GetAllLobbies();
        }

        public List<Lobby> GetAllLobbiesWithCategoryId(int categoryId)
        {
            return dbLobby.GetAllLobbiesWithCategoryId(categoryId);
        }

        public void ClearTableAfterFinish(int lobbyId)
        {
            dbLobby.ClearTableAfterFinish(lobbyId); 
        }

        public bool IsUserInLobby(string username)
        {
            return userCtrl.IsUserInLobby(username);
        }

        public bool DoesUserAlreadyOwnALobby(string username)
        {
            return userCtrl.DoesUserAlreadyOwnALobby(username);
        }

        public void JoinLobby(User user, Lobby lobby)
        {
            dbLobby.JoinLobby(user, lobby);
        }

        public void UpdateLobbyStatus(Lobby lobby)
        {
            dbLobby.UpdateLobbyStatus(lobby);
        }

        public bool IsLobbyStarted(int id)
        {
            if (dbLobby.GetLobby(id).isStarted == true)
            {
                return true;
            }
            return false;
        }
    }
}
