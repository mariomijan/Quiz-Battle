using DatabaseaccesLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLayer
{
    public class UserCtrl
    {
        private DBUser dbUser;

        public UserCtrl()
        {
            dbUser = new DBUser();
        }
        /// <summary>
        /// Calls the method AddUser from the dbUser
        /// </summary>
        /// <param name="user">the user object</param>
        public void AddUser(User user)
        {
            dbUser.AddUser(user);
        }

        /// <summary>
        /// Calls the method UpdateUser from the dbUser
        /// </summary>
        /// <param name="user">the user object</param>
        public void UpdateUser(User user)
        {
            dbUser.UpdateUser(user);
        }

        /// <summary>
        /// Calls the method RemoveUser from the dbUser
        /// </summary>
        /// <param name="Id">the user id</param>
        public void RemoveUser(int id)
        {
            dbUser.RemoveUser(id);
        }

        /// <summary>
        /// Calls the method GetAllUsers from the dbUser
        /// </summary>
        /// <returns>returns the method with a list of users 
        /// from dbUser</returns>
        public List<User> GetAllUsers()
        {
            return dbUser.GetAllUsers();
        }
        
        /// <summary>
        /// Calls the method AddPointsToUser from the dbUser
        /// </summary>
        /// <param name="user">the user object</param>
        public void AddPointsToUser(User user)
        {
            dbUser.AddPointsToUser(user);
        }

        /// <summary>
        /// Calls the method GetUserByUsername from dbUser
        /// </summary>
        /// <param name="username">the username for the user</param>
        /// <returns>returns the method with a user from the specified username 
        /// from dbUser</returns>
        public User GetUserByUsername(string username)
        {
            return dbUser.GetUserByUsername(username);
        }

        /// <summary>
        /// Calls the method GetUser from dbUser
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>returns the method with a user from the specified id 
        /// from dbUser</returns>
        public User GetUser(int id)
        {
            return dbUser.GetUser(id);
        }

        /// <summary>
        /// Checks if user is in a lobby
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <returns>returns whether the user is in a lobby or not</returns>
        public bool IsUserInLobby(string username)
        {
            if (dbUser.GetUserByUsername(username).joinedLobbyId != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether a user already owns a lobby
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <returns>returns whether the user owns a lobby or not</returns>
        public bool DoesUserAlreadyOwnALobby(string username)
        {
            if (dbUser.GetUserByUsername(username).lobbyOwnedId != 0)
            {
                return true;
            }
            return false;
        }


        public List<User> GetUsersInLobby(int id)
        {
            return dbUser.GetUsersInLobby(id);
        }


        /// <summary>
        /// Calls the method ClearsUsersLobbyOwnedIdAndDeleteLobby from the dbUser
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearsUsersLobbyOwnedIdAndDeleteLobby(User user)
        {
            dbUser.ClearsUsersLobbyOwnedIdAndDeleteLobby(user);
        }

        /// <summary>
        /// Calls the method ClearUsersJoinedLobbyId from the dbUser
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearUsersJoinedLobbyId(User user)
        {
            dbUser.ClearUsersJoinedLobbyId(user);
        }
    }
}
