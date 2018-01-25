using DataAccess;
using DatabaseaccesLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ControlLayer
{
    public class LoginCtrl
    {
        private DBLogin dbLogin;

        public LoginCtrl()
        {
            dbLogin = new DBLogin();
        }


        /// <summary>
        /// Encrypts the given login password, and pass the login and user to the method AddGuestAndLoginEncryptPw 
        /// from the dbLogin
        /// </summary>
        /// <param name="login">the login object</param>
        /// <param name="user">the user object</param>
        public void AddGuestAndLoginEncryptPw(Login login, User user)
        {
            string hashedPassword;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var key = new Rfc2898DeriveBytes(login.password, salt, 10000);
            byte[] hash = key.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            hashedPassword = Convert.ToBase64String(hashBytes);
            login.password = hashedPassword;
            dbLogin.AddGuestAndLoginEncryptPw(login, user);
        }

        /// <summary>
        /// Encrypts the given login password, and pass the login to the method EncryptAdminPassword 
        /// from the dbLogin
        /// </summary>
        /// <param name="login">the login object</param>
        public void EncryptAdminPassword(Login login)
        {
            string hashedPassword;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var key = new Rfc2898DeriveBytes(login.password, salt, 10000);
            byte[] hash = key.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            hashedPassword = Convert.ToBase64String(hashBytes);
            login.password = hashedPassword;
            dbLogin.EncryptAdminPassword(login);
        }

        /// <summary>
        /// Decrypts the already existing password of the user
        /// </summary>
        /// <param name="login">the login object</param>
        /// <returns>returns the login with the decrypted password</returns>
        public Login DecryptPassword(Login login)
        {
            Login hashedPassword = dbLogin.GetLogin(login.username);
            byte[] hashBytes = Convert.FromBase64String(hashedPassword.password);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var key = new Rfc2898DeriveBytes(login.password, salt, 10000);
            byte[] hash = key.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return hashedPassword;

        }

        /// <summary>
        /// Calls the method GetLogin from the dbLogin
        /// </summary>
        /// <param name="username">the username</param>
        /// <returns>returns the method with a login 
        /// from dbLogin</returns>
        public Login GetLogin(string username)
        {
            return dbLogin.GetLogin(username);
        }


        /// <summary>
        /// Checks if user exists or not by the given username
        /// </summary>
        /// <param name="login">the login object</param>
        /// <returns>returns whether the user exists or not</returns>
        public bool DoesUserExist(Login login)
        {

            if (login.username != dbLogin.GetLogin(login.username).username)
            {
                return true;
            }

            return false;
        }
    }
}
