using DatabaseaccesLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLayer
{
    public class QuestionCtrl
    {
        private DBQuestion dbQuestion;

        public QuestionCtrl()
        {
            dbQuestion = new DBQuestion();
        }

        /// <summary>
        /// Calls the method AddQuestion from the dbQuestion
        /// </summary>
        /// <param name="question">the question object</param>
        public void AddQuestion(Question question)
        {
            dbQuestion.AddQuestion(question);
        }

        /// <summary>
        /// Calls the method UpdateQuestion from the dbQuestion
        /// </summary>
        /// <param name="question">the question object</param>
        public void UpdateQuestion(Question question)
        {
            dbQuestion.UpdateQuestion(question);
        }


        /// <summary>
        /// Calls the method RemoveQuestion from the dbQuestion
        /// </summary>
        /// <param name="Id">the question id</param>
        public void RemoveQuestion(int id)
        {
            dbQuestion.RemoveQuestion(id);
        }


        /// <summary>
        /// Calls the method GetAllQuestionsAndAnswersByCategoryId from the dbQuestion
        /// </summary>
        /// <param name="categoryId">the category id</param>
        /// <returns>returns the method with a list of questions along with their answers 
        /// by given category id, from dbQuestion</returns>    
        public List<Question> GetAllQuestionsAndAnswersByCategoryId(int categoryId)
        {
            return dbQuestion.GetAllQuestionsAndAnswersByCategoryId(categoryId);
        }


        /// <summary>
        /// Calls the method GetQuestion from the dbQuestion
        /// </summary>
        /// <param name="Id">the question id</param>
        /// <returns>returns the method with a question 
        /// from dbQuestion</returns>
        public Question GetQuestion(int id)
        {
            return dbQuestion.GetQuestion(id);
        }

        /// <summary>
        /// Calls the method GetAllQuestions from the dbQuestion
        /// </summary>
        /// <returns>returns the method with a list of questions 
        /// from dbQuestion</returns>
        public List<Question> GetAllQuestions()
        {
            return dbQuestion.GetAllQuestions();
        }

        
    }
}
