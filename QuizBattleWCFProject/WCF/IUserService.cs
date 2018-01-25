using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    [ServiceContract]
    public interface IUserService
    {

        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void UpdateUser(User user);
        [OperationContract]
        void RemoveUser(int id);
        [OperationContract]
        List<User> GetAllUsers();
        [OperationContract]
        void AddPointsToUser(User user);
        [OperationContract]
        User GetUserByUsername(string username);
        [OperationContract]
        User GetUser(int id);
        [OperationContract]
        void ClearUsersJoinedLobbyId(User user);
        [OperationContract]
        List<User> GetUsersInLobby(int id);
        [OperationContract]
        void ClearsUsersLobbyOwnedIdAndDeleteLobby(User user);

        
    }
}


