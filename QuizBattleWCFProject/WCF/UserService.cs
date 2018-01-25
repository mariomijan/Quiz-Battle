using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;
using ControlLayer;

namespace WCF
{
    public class UserService : IUserService
    {
        private UserCtrl userCtrl = new UserCtrl();

        /// <summary>
        /// Calls the method AddUser from the user controller
        /// </summary>
        /// <param name="user">the user object</param>
        public void AddUser(User user)
        {
            userCtrl.AddUser(user);
        }

        /// <summary>
        /// Calls the method UpdateUser from the user controller
        /// </summary>
        /// <param name="user">the user object</param>
        public void UpdateUser(User user)
        {
            userCtrl.UpdateUser(user);
        }

        /// <summary>
        /// Calls the method RemoveUser from the user controller
        /// </summary>
        /// <param name="Id">the user id</param>
        public void RemoveUser(int id)
        {
            userCtrl.RemoveUser(id);
        }

        /// <summary>
        /// Calls the method GetAllUsers from the user controller
        /// </summary>
        /// <returns>returns the method with a list of users 
        /// from user controller</returns>
        public List<User> GetAllUsers()
        {
            return userCtrl.GetAllUsers();
        }

        /// <summary>
        /// Calls the method AddPointsToUser from the user controller
        /// </summary>
        /// <param name="user">the user object</param>
        public void AddPointsToUser(User user)
        {
            userCtrl.AddPointsToUser(user);
        }

        /// <summary>
        /// Calls the method GetUserByUsername from the user controller
        /// </summary>
        /// <param name="username">the username for the user</param>
        /// <returns>returns the method with a user from the specified username 
        /// from user controller</returns>
        public User GetUserByUsername(string username)
        {
            return userCtrl.GetUserByUsername(username);
        }

        /// <summary>
        /// Calls the method GetUser from the user controller
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>returns the method with a user from the specified id 
        /// from user controller</returns>
        public User GetUser(int id)
        {
            return userCtrl.GetUser(id);
        }


        /// <summary>
        /// Calls the method ClearUsersJoinedLobbyId from the user controller
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearUsersJoinedLobbyId(User user)
        {
            userCtrl.ClearUsersJoinedLobbyId(user);
        }

        /// <summary>
        /// Calls the method ClearsUsersLobbyOwnedIdAndDeleteLobby from the user controller
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearsUsersLobbyOwnedIdAndDeleteLobby(User user)
        {
            userCtrl.ClearsUsersLobbyOwnedIdAndDeleteLobby(user);
        }


        /// <summary>
        /// Calls the method GetUsersInLobby from the user controller
        /// </summary>
        /// <param name="id">the id of the joined lobby</param>
        /// <returns>returns the method with a list of users from the specified id 
        /// from user controller</returns>
        public List<User> GetUsersInLobby(int id)
        {
            return userCtrl.GetUsersInLobby(id);
        }
    }
}
