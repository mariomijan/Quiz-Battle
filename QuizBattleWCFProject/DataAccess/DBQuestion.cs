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

namespace DatabaseaccesLayer
{
    public class DBQuestion
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region AddQuestion
        /// <summary>
        /// Check best isolationlevel
        /// </summary>
        /// <param name="question"></param>
        public void AddQuestion(Question question)
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
                        command.CommandText = "INSERT INTO Question (description, categoryId) VALUES (@description, @categoryId)";
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = question.description;
                        command.Parameters.Add("@categoryId", SqlDbType.Int).Value = question.category.id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetAllQuestions
        /// <summary>
        /// Gets all the questions from the database
        /// </summary>
        /// <returns>The question list</returns>
        public List<Question> GetAllQuestions()
        {
            List<Question> questions = new List<Question>();
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "Select id, description, categoryId from Question";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Question question = new Question();
                            question.id = reader.GetInt32(reader.GetOrdinal("id"));
                            question.description = reader.GetString(reader.GetOrdinal("description"));
                            question.category = new Category() { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                            questions.Add(question);
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return questions;
        }
        #endregion

        #region GetQuestion
        /// <summary>
        /// Gets a question based on the id that is inserted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetQuestion(int id)
        {
            Question question = new Question();
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = "SELECT id, description from Question where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;


                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            question.id = reader.GetInt32(reader.GetOrdinal("id"));
                            question.description = reader.GetString(reader.GetOrdinal("description"));
                            //question.category = reader.GetString(reader.GetOrdinal("categoryId"));
                        }
                        reader.Close();
                        command.ExecuteNonQuery();

                    }
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id,description,questionId,isCorrect FROM Answer WHERE questionId=@id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Answer a = new Answer();
                            a.id = reader.GetInt32(reader.GetOrdinal("id"));
                            a.description = reader.GetString(reader.GetOrdinal("description"));
                            a.isCorrect = reader.GetBoolean(reader.GetOrdinal("isCorrect"));

                            question.Answers.Add(a);
                        }
                    }
                    scope.Complete();
                    conn.Close();
                }


            }

            return question;
        }
        #endregion

        #region RemoveQuestion
        /// <summary>
        /// Removes a question from the database
        /// </summary>
        /// <param name="question"></param>
        public void RemoveQuestion(int id)
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
                        command.CommandText = "Delete from Question where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region UpdateQuestion
        /// <summary>
        /// Updates a questions values 
        /// </summary>
        /// <param name="question"></param>
        public void UpdateQuestion(Question question)
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
                        command.CommandText = "UPDATE Question SET description=@description where id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = question.id;
                        command.Parameters.Add("@description", SqlDbType.NVarChar).Value = question.description;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetAllQuestionsAndAnswersByCategoryId
        /// <summary>
        /// Gets all questions with answers from specified category ID
        /// </summary>
        /// <param name="categoryId">the categoryId</param>
        /// <returns>returns a list of questions along with their answers by given category id</returns>
        public List<Question> GetAllQuestionsAndAnswersByCategoryId(int categoryId)
        {
            List<Question> questions = new List<Question>();
            Question question = null;
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
                        command.CommandText = "SELECT Question.id AS QuestId, Question.[description] AS QuestDesc, categoryId, Answer.id AS AnsId, Answer.[description] AS AnswerDesc, Answer.isCorrect AS AnsIsCorrect, Answer.questionId AS QuestionIdFromAnswer FROM Question JOIN Answer ON questionId = Question.id WHERE categoryID = @categoryId";
                        command.Parameters.Add("categoryId", SqlDbType.Int).Value = categoryId;

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (question == null || question.description != reader.GetString(reader.GetOrdinal("QuestDesc")))
                            {
                                question = new Question();
                                question.id = reader.GetInt32(reader.GetOrdinal("QuestId"));
                                question.description = reader.GetString(reader.GetOrdinal("QuestDesc"));
                                question.category = new Category() { id = reader.GetInt32(reader.GetOrdinal("categoryId")) };
                                answer = new Answer();
                                answer.id = reader.GetInt32(reader.GetOrdinal("AnsId"));
                                answer.description = reader.GetString(reader.GetOrdinal("AnswerDesc"));
                                answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("AnsIsCorrect"));
                                answer.Question = new Question() { id = reader.GetInt32(reader.GetOrdinal("QuestionIdFromAnswer")) };
                                question.Answers.Add(answer);
                                questions.Add(question);

                            }
                            else
                            {
                                answer = new Answer();
                                answer.id = reader.GetInt32(reader.GetOrdinal("AnsId"));
                                answer.description = reader.GetString(reader.GetOrdinal("AnswerDesc"));
                                answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("AnsIsCorrect"));
                                answer.Question = new Question() { id = reader.GetInt32(reader.GetOrdinal("QuestionIdFromAnswer")) };
                                question.Answers.Add(answer);

                            }
                        }
                        scope.Complete();
                        conn.Close();
                        return questions;
                    }
                }
            }
        }
        #endregion
    }
}
