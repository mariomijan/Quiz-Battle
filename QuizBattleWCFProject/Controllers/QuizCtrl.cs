using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DatabaseaccesLayer;
using Controllers;

namespace ControlLayer
{
    public class QuizCtrl
    {
        private DBQuiz dbQuiz;
        private CategoryCtrl catCtrl;
        private QuestionCtrl qCtrl;
        private AnswerCtrl aCtrl;
        private LobbyCtrl lCtrl;

        public QuizCtrl()
        {
            dbQuiz = new DBQuiz();
            catCtrl = new CategoryCtrl();
            qCtrl = new QuestionCtrl();
            aCtrl = new AnswerCtrl();
            lCtrl = new LobbyCtrl();
        }

        public void ClearTableAfterFinish(int lobbyId)
        {
            lCtrl.ClearTableAfterFinish(lobbyId);
        }

        /// <summary>
        /// Calls the method GetAllQuestionsAndAnswersByCategoryId from the question controller
        /// </summary>
        /// <param name="categoryId">the category id</param>
        /// <returns>returns the method with a list of questions along with their answers 
        /// by given category id, from question controller</returns>    
        public List<Question> GetAllQuestionsAndAnswersByCategoryId(int categoryId)
        {
            return qCtrl.GetAllQuestionsAndAnswersByCategoryId(categoryId);
        }


        /// <summary>
        /// Calls the method GetQuestion from the question controller
        /// </summary>
        /// <param name="Id">the question id</param>
        /// <returns>returns the method with a question 
        /// from question controller</returns>
        public Question GetQuestion(int id)
        {
            return qCtrl.GetQuestion(id);
        }

        /// <summary>
        /// Calls the method IsCorrect from answer controller
        /// </summary>
        /// <param name="id">the answer id</param>
        /// <returns>returns an answer which is ether true or false, from the specified ID</returns>
        public bool IsCorrect(int id)
        {
            return aCtrl.IsCorrect(id);
        }

        /// <summary>
        /// Calls the method GetAnswer from answer controller
        /// </summary>
        /// <param name="id">the answer id</param>
        /// <returns>returns answer from the specified ID</returns>
        public Answer GetAnswer(int id)
        {
            return aCtrl.GetAnswer(id);
        }


        /// <summary>
        /// Calls the method GetAllCategories from category controller
        /// </summary>
        /// <returns>returns all the categories</returns>
        public List<Category> GetAllCategories()
        {

            return catCtrl.GetAllCategories();
        }

        public List<Lobby> GetAllLobbies()
        {
            return lCtrl.GetAllLobbies();
        }

        //public bool HasQuestionBeenAnswered(int questionId, int lobbyId)
        //{
        //    if (uqCtrl.GetUserQuestion(lobbyId) != null)
        //    {
        //        if (uqCtrl.GetUserQuestion(lobbyId).question.id == questionId
        //            && uqCtrl.GetUserQuestion(lobbyId).lobby.id == lobbyId)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    return false;
        //}
    }
}

