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
using DataAccess;

namespace DatabaseaccesLayer
{
    public class DBUser
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        
        #region AddUser
        /// <summary>
        /// Method to add the user in the database table
        /// </summary>
        /// <param name="user">user object</param>
        public void AddUser(User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "INSERT INTO UserTable (name, lastName, country, phone, loginId, point) VALUES (@name, @lastName, @country,@phone,@loginId, @point)";
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = user.name;
                        command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.lastName;
                        command.Parameters.Add("@country", SqlDbType.NVarChar).Value = user.country;
                        command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = user.phone;
                        command.Parameters.Add("@loginId", SqlDbType.Int).Value = user.login.id;
                        command.Parameters.Add("@point", SqlDbType.Int).Value = user.point;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion

        #region GetUser
        /// <summary>
        /// Gets a user based on the id
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>returns a user from the specified id</returns>
        public User GetUser(int id)
        {
            User user = null;
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Select id, name, lastName, country, phone, loginId, point, timeStamp, joinedLobbyId, lobbyOwnedId from UserTable where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user = new User();
                            user.id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.name = reader.GetString(reader.GetOrdinal("name"));
                            user.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            user.country = reader.GetString(reader.GetOrdinal("country"));
                            user.phone = reader.GetString(reader.GetOrdinal("phone"));
                            user.login = new Login() { id = reader.GetInt32(reader.GetOrdinal("loginId")) };
                            user.point = reader.GetInt32(reader.GetOrdinal("point"));
                            byte[] array = new byte[8];
                            reader.GetBytes(reader.GetOrdinal("timeStamp"), 0, array, 0, array.Length);
                            user.timeStamp = array;
                            int joinedLobbyId = (reader["joinedLobbyId"] != DBNull.Value) ? Convert.ToInt32(reader["joinedLobbyId"]) : 0;
                            user.joinedLobbyId = joinedLobbyId;
                            int lobbyLobbyId = (reader["lobbyOwnedId"] != DBNull.Value) ? Convert.ToInt32(reader["lobbyOwnedId"]) : 0;
                            user.lobbyOwnedId = lobbyLobbyId;

                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
                return user;
            }
        }
        #endregion
        
        #region UpdateUser
        /// <summary>
        /// Method to update the user in database table
        /// </summary>
        /// <param name="user">user object</param>
        public void UpdateUser(User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;

            byte[] originalTimeStamp = GetUser(user.id).timeStamp;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "UPDATE UserTable SET name=@name,lastName=@lastName,country=@country, phone=@phone where id = @id AND timeStamp=@originalTimeStamp";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = user.name;
                        command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.lastName;
                        command.Parameters.Add("@country", SqlDbType.NVarChar).Value = user.country;
                        command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = user.phone;
                        command.Parameters.Add("@originalTimeStamp", SqlDbType.Timestamp).Value = originalTimeStamp;
                        int affected = command.ExecuteNonQuery();

                        if (affected == 0)
                        {
                            throw new Exception();
                        }
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion

        #region RemoveUser

        /// <summary>
        /// Method to delete the user from the database table
        /// </summary>
        /// <param name="email">email to delete the user</param>
        public void RemoveUser(int id)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Delete from UserTable where id = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion

        #region GetAllUsers
        /// <summary>
        /// Method to get all the users from the database table
        /// </summary>
        /// <returns>returns a list of users</returns>
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Select id, name, lastName, country, phone, loginId, point from UserTable";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User();
                            user.id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.name = reader.GetString(reader.GetOrdinal("name"));
                            user.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            user.country = reader.GetString(reader.GetOrdinal("country"));
                            user.phone = reader.GetString(reader.GetOrdinal("phone"));
                            user.login = new Login() { id = reader.GetInt32(reader.GetOrdinal("loginId")) };
                            user.point = reader.GetInt32(reader.GetOrdinal("point"));
                            userList.Add(user);
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
                return userList;
            }
        }

        #endregion

        #region AddPointsToUser

        /// <summary>
        /// Adds points to the user at the end of a quiz
        /// </summary>
        /// <param name="user">the user object</param>
        public void AddPointsToUser(User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "update UserTable set point = @point where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                        command.Parameters.Add("@point", SqlDbType.Int).Value = user.point;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion

        #region GetUsersByUsername
        /// <summary>
        /// Method to get a user from the database with the specified username
        /// <param name="username">the username for the user</param>
        /// <returns>returns a user</returns>
        public User GetUserByUsername(string username)
        {
            User user = null;

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "SELECT UserTable.id, name, lastName, country, phone, loginId, point, joinedLobbyId, lobbyOwnedId FROM UserTable join Login ON loginId = Login.id WHERE username = @username";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user = new User();
                            user.id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.name = reader.GetString(reader.GetOrdinal("name"));
                            user.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            user.phone = reader.GetString(reader.GetOrdinal("phone"));
                            user.country = reader.GetString(reader.GetOrdinal("country"));
                            user.login = new Login() { id = reader.GetInt32(reader.GetOrdinal("loginId")) };
                            user.point = reader.GetInt32(reader.GetOrdinal("point"));
                            int joinedLobbyId = (reader["joinedLobbyId"] != DBNull.Value) ? Convert.ToInt32(reader["joinedLobbyId"]) : 0;
                            user.joinedLobbyId = joinedLobbyId;
                            int lobbyLobbyId = (reader["lobbyOwnedId"] != DBNull.Value) ? Convert.ToInt32(reader["lobbyOwnedId"]) : 0;
                            user.lobbyOwnedId = lobbyLobbyId;

                        }
                        reader.Close();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
            return user;
        }
        #endregion
        
        #region GetUsersInLobby
        public List<User> GetUsersInLobby(int id)
        {
            List<User> users = new List<User>();

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "SELECT name FROM UserTable WHERE joinedLobbyId = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User();
                            user.name = reader.GetString(reader.GetOrdinal("name"));
                            users.Add(user);
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return users;
        }
        #endregion

        #region ClearsUsersLobbyOwnedIdAndDeleteLobby
        /// <summary>
        /// Clears the users owned lobby id, and deletes a lobby that match the owned lobby id  
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearsUsersLobbyOwnedIdAndDeleteLobby(User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            var existingUser = GetUser(user.id);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "UPDATE UserTable SET lobbyOwnedId=null where UserTable.id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                        command.ExecuteNonQuery();

                        if (existingUser.joinedLobbyId == user.lobbyOwnedId)
                        {
                            command.CommandText = "UPDATE UserTable SET joinedLobbyId=null where UserTable.id = @userId";
                            command.Parameters.Add("@userId", SqlDbType.Int).Value = user.id;
                            command.ExecuteNonQuery();
                        }

                        command.CommandText = "DELETE FROM Lobby WHERE id = @lobbyId";
                        command.Parameters.Add("@lobbyId", SqlDbType.Int).Value = user.lobbyOwnedId;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion

        #region ClearUsersJoinedLobbyId
        /// <summary>
        /// Clears the users joined lobby id
        /// </summary>
        /// <param name="user">the user object</param>
        public void ClearUsersJoinedLobbyId(User user)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "UPDATE UserTable SET joinedLobbyId=null where UserTable.id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    connection.Close();
                }
            }
        }
        #endregion
    }
}

