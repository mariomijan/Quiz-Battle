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
using System.ServiceModel;

namespace DatabaseaccesLayer
{
    public class DBAnswer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region AddAnswer
        /// <summary>
        /// Adds an answer to a table in the database
        /// </summary>
        /// <param name="answer">answer object</param>

        public void AddAnswer(Answer answer)
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
                        command.CommandText = "INSERT INTO Answer (description, questionId, isCorrect) VALUES (@description, @questionId, @isCorrect)";
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = answer.description;
                        command.Parameters.Add("@questionId", SqlDbType.Int).Value = answer.Question.id;
                        command.Parameters.Add("@isCorrect", SqlDbType.Bit).Value = answer.isCorrect;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region UpdateAnswer
        /// <summary>
        /// Updates an answer in the database
        /// </summary>
        /// <param name="answer">answer object</param>

        public void UpdateAnswer(Answer answer)
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
                        command.CommandText = "UPDATE Answer SET description=@description,questionId=@questionId, isCorrect=@isCorrect where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = answer.id;
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = answer.description;
                        command.Parameters.Add("@questionId", SqlDbType.Int).Value = answer.Question.id;
                        command.Parameters.Add("@isCorrect", SqlDbType.Bit).Value = answer.isCorrect;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region RemoveAnswer
        /// <summary>
        ///Method to delete the answer from the database table
        /// </summary>
        /// <param name="id">id to delete the answer</param>
        public void RemoveAnswer(int id)
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
                        command.CommandText = "Delete from Answer where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetAllAnswers
        /// <summary>
        /// Method to get all the answers from the database table
        /// </summary>
        /// <returns>returns a list of all the answers</returns>
        public List<Answer> GetAllAnswers()
        {
            List<Answer> AnswerList = new List<Answer>();
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Select id, description, questionId, isCorrect, pointAmount from Answer";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Answer answer = new Answer();

                            answer.id = reader.GetInt32(reader.GetOrdinal("id"));
                            answer.description = reader.GetString(reader.GetOrdinal("description"));
                            answer.Question = new Question() { id = reader.GetInt32(reader.GetOrdinal("questionId")) };
                            answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("IsCorrect"));
                            answer.pointAmount = reader.GetInt32(reader.GetOrdinal("pointAmount"));
                            AnswerList.Add(answer);
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return AnswerList;
        }
        #endregion

        #region GetAnswer
        /// <summary>
        /// Method to get the answer from the database table
        /// </summary>
        /// <param name="id">id to get the answer</param>

        public Answer GetAnswer(int id)
        {
            Answer answer = null;
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "SELECT id, description, questionId, isCorrect, pointAmount from Answer where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            answer = new Answer();
                            answer.id = reader.GetInt32(reader.GetOrdinal("id"));
                            answer.description = reader.GetString(reader.GetOrdinal("description"));
                            answer.Question = new Question() { id = reader.GetInt32(reader.GetOrdinal("questionId")) };
                            answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("isCorrect"));
                            answer.pointAmount = reader.GetInt32(reader.GetOrdinal("pointAmount"));
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return answer;
        }
        #endregion

        #region AnswerQuestion
        /// <summary>
        /// Indsætter en række i databasen når en bruger har besvaret et spørgsmål, 
        /// og sørger for at sikre samtidighedsproblemer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lobbyId"></param>
        /// <param name="userId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public AnswerQuestionDTO AnswerQuestion(int id, int lobbyId, int userId, int categoryId)
        {
            AnswerQuestionDTO answer = null;
            UserQuestion usrq = null;
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, description, questionId, isCorrect, pointAmount from Answer where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            answer = new AnswerQuestionDTO();
                            answer.Answer = new Answer();
                            answer.Answer.id = reader.GetInt32(reader.GetOrdinal("id"));
                            answer.Answer.description = reader.GetString(reader.GetOrdinal("description"));
                            answer.Answer.Question = new Question() { id = reader.GetInt32(reader.GetOrdinal("questionId")) };
                            answer.Answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("isCorrect"));
                            answer.Answer.pointAmount = reader.GetInt32(reader.GetOrdinal("pointAmount"));
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        usrq = new UserQuestion();
                        cmd.CommandText = "SELECT UserQuestion.userId, UserQuestion.questionId, UserQuestion.categoryId, UserQuestion.lobbyId FROM UserQuestion WHERE UserQuestion.lobbyId = @lobbyId";
                        cmd.Parameters.Add("@lobbyId", SqlDbType.Int).Value = lobbyId;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            usrq.user = new User() { id = reader.GetInt32(reader.GetOrdinal("userId")) };
                            usrq.question = new Question { id = reader.GetInt32(reader.GetOrdinal("questionId")) };
                            usrq.category = new Category { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                            usrq.lobby = new Lobby { id = reader.GetInt32(reader.GetOrdinal("lobbyId")) };
                        }
                        

                        if (usrq.question.id == answer.Answer.Question.id)
                        {
                            answer.IsAnswered = true;
                            scope.Dispose();
                        }
                        else
                        {
                            answer.IsAnswered = false;
                        }
                        reader.Close();
                    }

                    if (answer.Answer.isCorrect && !answer.IsAnswered)
                    {
                        try
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                //cmd.CommandText = "INSERT INTO UserQuestion(userId, questionId, categoryId, lobbyId)VALUES(@userId,@questionId,@categoryId, @lobbyId)";
                                cmd.CommandText = "if not exists(select questionid from userQuestion where questionid = @questionId)INSERT INTO UserQuestion(userId, questionId, categoryId, lobbyId)VALUES(@userId, @questionId, @categoryId, @lobbyId)";
                                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                                cmd.Parameters.Add("@questionId", SqlDbType.Int).Value = answer.Answer.Question.id;
                                cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = categoryId;
                                cmd.Parameters.Add("@lobbyId", SqlDbType.Int).Value = lobbyId;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception)
                        {
                            throw new FaultException("You answered at the same time");
                        }
                        scope.Complete();
                        conn.Close();
                    }
                }
            }
            return answer;
        }
        #endregion
    }
}








