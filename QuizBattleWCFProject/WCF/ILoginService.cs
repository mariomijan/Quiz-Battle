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
    public interface ILoginService
    {
        [OperationContract]
        void AddGuestAndLoginEncryptPw(Login login, User user);
        [OperationContract]
        void EncryptAdminPassword(Login login);
        [OperationContract]
        Login DecryptPassword(Login login);
        [OperationContract]
        bool DoesUserExist(Login login);
        [OperationContract]
        Login GetLogin(string username);

    }
}
