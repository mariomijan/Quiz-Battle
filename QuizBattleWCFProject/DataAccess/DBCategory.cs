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
    public class DBCategory
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        #region AddCategory
        /// <summary>
        /// Method to add the category in the database table
        /// </summary>
        /// <param name="category">category object</param>
        public void AddCategory(Category category)
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
                        command.CommandText = "INSERT INTO Category(name)VALUES(@name)";
                        command.Parameters.Add("name", SqlDbType.NVarChar).Value = category.name;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion
        
        #region DeleteCategory
        /// <summary>
        /// Method to delete the category from the database table
        /// </summary>
        /// <param name="id">id to delete the category</param>
        public void DeleteCategory(int id)
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
                        command.CommandText = "DELETE FROM Category WHERE id = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetAllCategories
        /// <summary>
        /// Method to get all the categories from the database table
        /// </summary>
        /// <returns>returns a list of all the categories</returns>
        public List<Category> GetAllCategories()
        {
            List<Category> catList = new List<Category>();

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, name FROM Category";

                        conn.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.id = reader.GetInt32(reader.GetOrdinal("id"));
                            category.name = reader.GetString(reader.GetOrdinal("name"));
                            catList.Add(category);
                        }
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
            return catList;
        }
        #endregion

        #region GetCategory
        /// <summary>
        /// Method to get a category from the database table
        /// </summary>
        /// <param name="id">id to get the category</param>
        /// <returns>returns a category</returns>
        public Category GetCategory(int id)
        {
            Category category = null;

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT id, name FROM Category WHERE id = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = id;

                        conn.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            category = new Category();
                            category.id = reader.GetInt32(reader.GetOrdinal("id"));
                            category.name = reader.GetString(reader.GetOrdinal("name"));
                        }
                        scope.Complete();
                        conn.Close();
                    }
                }
            }
            return category;
        }
        #endregion

        #region UpdateCategory
        /// <summary>
        /// Method to update the category in database table
        /// </summary>
        /// <param name="category">category object</param>
        public void UpdateCategory(Category category)
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
                        command.CommandText = "UPDATE Category SET name = @name WHERE id = @id";
                        command.Parameters.Add("id", SqlDbType.Int).Value = category.id;
                        command.Parameters.Add("name", SqlDbType.NVarChar).Value = category.name;
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                    conn.Close();
                }
            }
        }
        #endregion

        #region GetCategoriesQuestionsAnswers
        public List<Category> GetCategoriesQuestionsAnswers()
        {
            var catQuestionAnswersList = new List<Category>();
            Category category = null;
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
                        command.CommandText = "SELECT Category.id as CatId, category.name, Question.id as questId, question.[description] as questDesc, answer.id, Answer.[description] as ansDesc, answer.isCorrect, answer.pointAmount FROM Category JOIN Question ON Question.categoryId = Category.id JOIN Answer ON Answer.questionId = Question.id ORDER BY category.id;";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            category = new Category();
                            category.id = reader.GetInt32(reader.GetOrdinal("CatId"));
                            category.name = reader.GetString(reader.GetOrdinal("name"));

                            category.question.Add(question);
                            question = new Question();
                            question.id = reader.GetInt32(reader.GetOrdinal("questId"));
                            question.description = reader.GetString(reader.GetOrdinal("questDesc"));

                            question = new Question();
                            question.description = reader.GetString(reader.GetOrdinal("questDesc"));

                            answer = new Answer();
                            answer.id = reader.GetInt32(reader.GetOrdinal("id"));
                            answer.description = reader.GetString(reader.GetOrdinal("ansDesc"));
                            answer.isCorrect = reader.GetBoolean(reader.GetOrdinal("isCorrect"));
                            answer.pointAmount = reader.GetInt32(reader.GetOrdinal("pointAmount"));

                            question.Answers.Add(answer);

                            catQuestionAnswersList.Add(category);
                        }
                    }
                    scope.Complete();
                    conn.Close();
                }
                return catQuestionAnswersList;
            }
        }
        #endregion

        #region AddCategoryWithQuestionsAndAnswers
        public void AddCategoryWithQuestionsAndAnswers(Category category, Question question, Answer answer)
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
                        command.CommandText = "INSERT INTO Category(name) OUTPUT inserted.id VALUES(@name)";
                        command.Parameters.Add("name", SqlDbType.NVarChar).Value = category.name;

                        int categoryIdForQuestion = (int)command.ExecuteScalar();

                        command.CommandText = "INSERT INTO Question (Question.description, categoryId) OUTPUT inserted.id VALUES (@questDescription, @categoryId)";
                        command.Parameters.Add("@questDescription", SqlDbType.NVarChar).Value = question.description;
                        command.Parameters.Add("@categoryId", SqlDbType.NVarChar).Value = categoryIdForQuestion;

                        int questionIdForAnswer = (int)command.ExecuteScalar();

                        command.CommandText = "INSERT INTO Answer (Answer.description, questionId, isCorrect, pointAmount) VALUES (@answerDescription, @questionId, @isCorrect, @pointAmount)";
                        command.Parameters.Add("@answerDescription", SqlDbType.NVarChar).Value = answer.description;
                        command.Parameters.Add("@questionId", SqlDbType.NVarChar).Value = questionIdForAnswer;
                        command.Parameters.Add("@isCorrect", SqlDbType.NVarChar).Value = answer.isCorrect;
                        command.Parameters.Add("@pointAmount", SqlDbType.Int).Value = answer.pointAmount;
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
