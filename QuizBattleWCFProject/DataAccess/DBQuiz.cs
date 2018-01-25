using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Configuration;

namespace DatabaseaccesLayer
{
    public class DBQuiz
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;


        #region GetQuizWithCategoriesWithQuestionsWithAnswers ( NOT USED )
        //VI FÅR MÅSKE BRUG FOR NOGET AF DENNE METODE TIL NOGET ANDET, MY NIGGAS
        //public Quiz GetQuizWithCategoriesWithQuestionsWithAnswers(int id)
        //{
        //    Quiz quiz = null;
        //    Category category = null;
        //    Question question = null;
        //    Answer answer = null;
        //    try
        //    {
        //        dbConnection.connection.Open();
        //        using (SqlCommand command = dbConnection.connection.CreateCommand())
        //        {
        //            command.CommandText = "SELECT Category.quizId, Category.id as CatId, Question.id as questId, Answer.[description] as ansDesc, Quiz.name, Category.name AS catName, Question.[description] as questDesc FROM Quiz JOIN Category ON Category.quizId = Quiz.id JOIN Question ON Question.categoryId = Category.id JOIN Answer ON Answer.questionId = Question.id WHERE Quiz.id = @id ORDER BY category.id";
        //            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
        //            var reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                if (quiz == null)
        //                {
        //                    quiz = new Quiz();
        //                    quiz.name = reader.GetString(reader.GetOrdinal("name"));
        //                    quiz.id = reader.GetInt32(reader.GetOrdinal("quizId"));
        //                }

        //                if (question != null)
        //                {
        //                    if (question.description != reader.GetString(reader.GetOrdinal("questDesc")))
        //                    {
        //                        category.question.Add(question);
        //                        question = new Question();
        //                        question.description = reader.GetString(reader.GetOrdinal("questDesc"));
        //                    }
        //                }
        //                else
        //                {
        //                    question = new Question();
        //                    question.description = reader.GetString(reader.GetOrdinal("questDesc"));
        //                }

        //                if (category != null)
        //                {
        //                    if (category.id != reader.GetInt32(reader.GetOrdinal("catId")))
        //                    {
        //                        quiz.categories.Add(category);
        //                        category = new Category();
        //                        category.id = reader.GetInt32(reader.GetOrdinal("catId"));
        //                        category.name = reader.GetString(reader.GetOrdinal("catName"));
        //                    }
        //                }
        //                else
        //                {
        //                    category = new Category();
        //                    category.id = reader.GetInt32(reader.GetOrdinal("catId"));
        //                    category.name = reader.GetString(reader.GetOrdinal("catName"));
        //                }

        //                answer = new Answer();
        //                answer.description = reader.GetString(reader.GetOrdinal("ansDesc"));
        //                question.Answers.Add(answer);

        //            }
        //        }
        //        if (question != null)
        //        {
        //            category.question.Add(question);
        //        }
        //        if (category != null)
        //        {
        //            quiz.categories.Add(category);
        //        }
        //        dbConnection.connection.Close();
        //        return quiz;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        // }
        #endregion
    }

}


