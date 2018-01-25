using DatabaseaccesLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLayer
{
    public class AnswerCtrl
    {
        private DBAnswer dbAnswer;

        public AnswerCtrl()
        {
            dbAnswer = new DBAnswer();
        }
        
        /// <summary>
        /// Calls the method AddAnswer from the dbAnswer 
        /// </summary>
        /// <param name="answer">the answer object</param>
        public void AddAnswer(Answer answer)
        {
            dbAnswer.AddAnswer(answer);
        }

        /// <summary>
        /// Calls the method UpdateAnswer from the dbAnswer
        /// </summary>
        /// <param name="answer">the answer object</param>
        public void UpdateAnswer(Answer answer)
        {
            dbAnswer.UpdateAnswer(answer);
        }

        /// <summary>
        /// Calls the method RemoveAnswer from the dbAnswer
        /// </summary>
        /// <param name="Id">the answer id</param>
        public void RemoveAnswer(int id)
        {
            dbAnswer.RemoveAnswer(id);
        }

        /// <summary>
        /// Calls the method GetAllAnswer from the dbAnswer
        /// </summary>
        /// <returns>returns the method with a list of answers 
        /// from dbAnswer</returns>
        public List<Answer> GetAllAnswer()
        {
            return dbAnswer.GetAllAnswers();
        }

        /// <summary>
        /// Calls the method GetAnswer from the dbAnswer
        /// </summary>
        /// <param name="Id">the answer id</param>
        /// <returns>returns the method with an answer 
        /// from dbAnswer</returns>
        public Answer GetAnswer(int id)
        {
            return dbAnswer.GetAnswer(id);
        }

        public AnswerQuestionDTO AnswerQuestion(int id, int lobbyId, int userId, int categoryId)
        {
            return dbAnswer.AnswerQuestion(id, lobbyId, userId, categoryId);
        }

        /// <summary>
        /// Method to check if answer is correct by the specified id 
        /// </summary>
        /// <param name="id">the answer id</param>
        /// <returns>returns a boolean of an answer</returns>
        public bool IsCorrect(int id)
        {
            if (GetAnswer(id).isCorrect == true)
            {
                return true;
            }
            return false;
        }
    }
}
