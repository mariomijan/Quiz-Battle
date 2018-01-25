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
    public class LoginService : ILoginService
    {
        private LoginCtrl loginCtrl = new LoginCtrl();

        /// <summary>
        /// Calls the method AddGuestAndLoginEncryptPw from the login controller
        /// </summary>
        /// <param name="login">the login object</param>
        /// <param name="user">the user object</param>
        public void AddGuestAndLoginEncryptPw(Login login, User user)
        {
            loginCtrl.AddGuestAndLoginEncryptPw(login, user);
        }

        /// <summary>
        /// Calls the method EncryptAdminPassword from the login controller
        /// </summary>
        /// <param name="login">the login object</param>
        public void EncryptAdminPassword(Login login)
        {
            loginCtrl.EncryptAdminPassword(login);
        }


        /// <summary>
        /// Calls the method DecryptPassword from the login controller
        /// </summary>
        /// <param name="login">the login object</param>
        /// <returns>returns the method with a decryptet login
        /// from login controller</returns>
        public Login DecryptPassword(Login login)
        {
            return loginCtrl.DecryptPassword(login);
        }

        /// <summary>
        /// Calls the method DoesUserExist from the login controller
        /// </summary>
        /// <param name="login">the login object</param>
        /// <returns>returns the method that checks if user exists or not 
        /// from login controller</returns>
        public bool DoesUserExist(Login login)
        {
            return loginCtrl.DoesUserExist(login);
        }

        /// <summary>
        /// Calls the method GetLogin from the login controller
        /// </summary>
        /// <param name="username">the username</param>
        /// <returns>returns the method with a login 
        /// from login controller</returns>
        public Login GetLogin(string username)
        {
            return loginCtrl.GetLogin(username);
        }
    }
}
