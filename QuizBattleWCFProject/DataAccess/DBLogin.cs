using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Transactions;
using DatabaseaccesLayer;
namespace DataAccess
{
    public class DBLogin
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region AddGuestAndLoginEncryptPw
        /// <summary>
        /// Encrypts the password for a guest
        /// Hardcoded Role Id so it only works for guests.
        /// </summary>
        /// <param name="login"></param>
        public void AddGuestAndLoginEncryptPw(Login login, User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "INSERT INTO Login (username, password, roleId) OUTPUT inserted.id VALUES(@username, @password, @roleId)";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = login.username;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = login.password;
                        command.Parameters.Add("@roleId", SqlDbType.Int).Value = 2;

                        int newLoginId = (int)command.ExecuteScalar();

                        command.CommandText = "INSERT INTO UserTable (name, lastName, country, phone, loginId, point) VALUES (@name, @lastName, @country,@phone, @loginId, @point)";
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = user.name;
                        command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.lastName;
                        command.Parameters.Add("@country", SqlDbType.NVarChar).Value = user.country;
                        command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = user.phone;
                        command.Parameters.Add("@loginId", SqlDbType.Int).Value = newLoginId;
                        command.Parameters.Add("@point", SqlDbType.Int).Value = user.point;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region EncryptAdminPassword
        /// <summary>
        /// Encrypt the password for admins and gives them the admin role ID.
        /// </summary>
        /// <param name="login"></param>
        public void EncryptAdminPassword(Login login)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "IF NOT EXISTS ( SELECT 1 FROM Login WHERE username = @username) BEGIN INSERT INTO Login (username, password, roleId) VALUES(@username, @password, @roleId) END";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = login.username;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = login.password;
                        command.Parameters.Add("@roleId", SqlDbType.Int).Value = 1;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetLogin
        /// <summary>
        /// Gets a login based on the username
        /// </summary>
        /// <param name="username">the username</param>
        /// <returns>returns a login from the specified username</returns>
        public Login GetLogin(string username)
        {
            Login login = new Login();
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Select id, username, password, roleId from login where username = @username";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            login.id = reader.GetInt32(reader.GetOrdinal("id"));
                            login.username = reader.GetString(reader.GetOrdinal("username"));
                            login.password = reader.GetString(reader.GetOrdinal("password"));
                            login.role = new Role() { id = reader.GetInt32(reader.GetOrdinal("roleId")) };
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
                return login;
            }
        }
        #endregion
    }
}

