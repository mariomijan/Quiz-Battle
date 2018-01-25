using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    [ServiceContract]
    public interface ILobbyService
    {
        [OperationContract]
        void CreateLobby(Lobby lobby, User user);
        [OperationContract]
        void JoinLobby(User user, Lobby lobby);
        [OperationContract]
        Lobby GetLobby(int id);
        [OperationContract]
        List<Lobby> GetAllLobbies();
        [OperationContract]
        List<Lobby> GetAllLobbiesWithCategoryId(int categoryId);
        [OperationContract]
        bool IsUserInLobby(string username);
        [OperationContract]
        bool DoesUserAlreadyOwnALobby(string username);
        [OperationContract]
        void UpdateLobbyStatus(Lobby lobby);
        [OperationContract]
        bool IsLobbyStarted(int id);
    }
}
