using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess
{
    public class DbLobby
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region CreateLobby
        public void CreateLobby(Lobby lobby, User user)
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
                        command.CommandText = "INSERT INTO Lobby(name, categoryId, isStarted) OUTPUT inserted.id VALUES(@name, @categoryId, @isStarted)";
                        command.Parameters.Add("name", SqlDbType.NVarChar).Value = lobby.name;
                        command.Parameters.Add("categoryId", SqlDbType.Int).Value = lobby.category.id;
                        command.Parameters.Add("isStarted", SqlDbType.Bit).Value = lobby.isStarted;

                        int ownedLobbyId = (int)command.ExecuteScalar();

                        command.CommandText = "Update usertable set lobbyOwnedId = @lobbyOwnedId where usertable.id = @userId";
                        command.Parameters.Add("@userId", SqlDbType.Int).Value = user.id;
                        command.Parameters.Add("@lobbyOwnedId", SqlDbType.Int).Value = ownedLobbyId;

                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetAllLobbies
        public List<Lobby> GetAllLobbies()
        {
            List<Lobby> lobbies = new List<Lobby>();

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, name, categoryId, isStarted FROM Lobby";

                        conn.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Lobby lobby = new Lobby();
                            lobby.id = reader.GetInt32(reader.GetOrdinal("id"));
                            lobby.name = reader.GetString(reader.GetOrdinal("name"));
                            lobby.category = new Category() { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                            lobby.isStarted = reader.GetBoolean(reader.GetOrdinal("isStarted"));
                            lobbies.Add(lobby);
                        }
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return lobbies;
        }

        #endregion

        #region GetAllLobbiesWithCategoryId
        public List<Lobby> GetAllLobbiesWithCategoryId(int categoryId)
        {
            List<Lobby> lobbies = new List<Lobby>();

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, name, categoryId, isStarted FROM Lobby where categoryId = @categoryId";
                        command.Parameters.Add("categoryId", SqlDbType.Int).Value = categoryId;

                        conn.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Lobby lobby = new Lobby();
                            lobby.id = reader.GetInt32(reader.GetOrdinal("id"));
                            lobby.name = reader.GetString(reader.GetOrdinal("name"));
                            lobby.category = new Category() { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                            lobby.isStarted = reader.GetBoolean(reader.GetOrdinal("isStarted"));
                            lobbies.Add(lobby);
                        }
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return lobbies;
        }
        #endregion

        #region GetLobby
        public Lobby GetLobby(int id)
        {
            Lobby lobby = null;

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, name, categoryId, isStarted FROM Lobby WHERE id = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;

                        conn.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            lobby = new Lobby();
                            lobby.id = reader.GetInt32(reader.GetOrdinal("id"));
                            lobby.name = reader.GetString(reader.GetOrdinal("name"));
                            lobby.category = new Category() { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                            lobby.isStarted = reader.GetBoolean(reader.GetOrdinal("isStarted"));
                        }
                        scope.Complete();
                        conn.Close();
                    }
                }
            }
            return lobby;
        }
        #endregion

        #region JoinLobby
        public void JoinLobby(User user, Lobby lobby)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Update usertable set joinedLobbyId = (select id as lobbyId from Lobby where id = @lobbyId) where usertable.id = @userId";
                        command.Parameters.Add("@userId", SqlDbType.Int).Value = user.id;
                        command.Parameters.Add("@lobbyId", SqlDbType.Int).Value = lobby.id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region UpdateLobbyStatus
        public void UpdateLobbyStatus(Lobby lobby)
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
                        command.CommandText = "UPDATE Lobby SET isStarted = 'true' WHERE id = @lobbyId";
                        command.Parameters.Add("lobbyId", SqlDbType.Int).Value = lobby.id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region ClearTableAfterFinish
        public void ClearTableAfterFinish(int lobbyId)
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
                        command.CommandText = "DELETE FROM UserQuestion WHERE lobbyId = @lobbyId";
                        command.Parameters.Add("lobbyId", SqlDbType.Int).Value = lobbyId;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

    }
}
