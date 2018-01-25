using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ControlLayer;

namespace WCF
{
    public class QuizService : IQuizService
    {
        private QuizCtrl qCtrl;

        public QuizService()
        {
            qCtrl = new QuizCtrl();
        }


        public void ClearTableAfterFinish(int lobbyId)
        {
            qCtrl.ClearTableAfterFinish(lobbyId);
        }

        /// <summary>
        /// Calls the method GetAllCategories from the quiz controller
        /// </summary>
        /// <returns>returns the method with the list of categories 
        /// from quiz controller</returns>
        public List<Category> GetAllCategories()
        {
            return qCtrl.GetAllCategories();
        }

        /// <summary>
        /// Calls the method GetAllQuestionsAndAnswersByCategoryId from the quiz controller
        /// </summary>
        /// <param name="categoryId">the category id</param>
        /// <returns>returns the method with a list of questions along with their answers 
        /// by given category id, from quiz controller</returns>    
        public List<Question> GetAllQuestionsAndAnswersByCategoryId(int categoryId)
        {
            return qCtrl.GetAllQuestionsAndAnswersByCategoryId(categoryId);
        }


        /// <summary>
        /// Calls the method GetAnswer from the quiz controller
        /// </summary>
        /// <param name="Id">the answer id</param>
        /// <returns>returns the method with an answer 
        /// from quiz controller</returns>
        public Answer GetAnswer(int id)
        {
            return qCtrl.GetAnswer(id);
        }

        


        /// <summary>
        /// Calls the method GetQuestion from the quiz controller
        /// </summary>
        /// <param name="Id">the question id</param>
        /// <returns>returns the method with a question 
        /// from quiz controller</returns>
        public Question GetQuestion(int id)
        {
            return qCtrl.GetQuestion(id);
        }

        //public bool HasQuestionBeenAnswered(int questionId, int lobbyId)
        //{
        //    return qCtrl.HasQuestionBeenAnswered(questionId, lobbyId);
        //}

        /// <summary>
        /// Calls the method IsCorrect from the quiz controller
        /// </summary>
        /// <param name="Id">the answer id</param>
        /// <returns>returns the method that checks if answer is correct or not  
        /// from quiz controller</returns>
        public bool IsCorrect(int id)
        {
            return qCtrl.IsCorrect(id);
        }
    }
}
