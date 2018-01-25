using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;
using ControlLayer;

namespace WCF
{
    public class AnswerService : IAnswerService 
    {
        private AnswerCtrl answerCtrl = new AnswerCtrl();

        /// <summary>
        /// Calls the method AddAnswer from the answer controller
        /// </summary>
        /// <param name="answer">the answer object</param>
        public void AddAnswer(Answer answer)
        {
            answerCtrl.AddAnswer(answer);
        }

        /// <summary>
        /// Calls the method UpdateAnswer from the answer controller
        /// </summary>
        /// <param name="answer">the answer object</param>
        public void UpdateAnswer(Answer answer)
        {
            answerCtrl.UpdateAnswer(answer);
        }

        /// <summary>
        /// Calls the method RemoveAnswer from the answer controller
        /// </summary>
        /// <param name="Id">the answer id</param>
        public void RemoveAnswer(int Id)
        {
            answerCtrl.RemoveAnswer(Id);
        }

        /// <summary>
        /// Calls the method GetAnswer from the answer controller
        /// </summary>
        /// <param name="Id">the answer id</param>
        /// <returns>returns the method with an answer 
        /// from answer controller</returns>
        public Answer GetAnswer(int id)
        {
           return answerCtrl.GetAnswer(id);
        }

        /// <summary>
        /// Calls the method GetAllAnswer from the answer controller
        /// </summary>
        /// <returns>returns the method with a list of answers 
        /// from answer controller</returns>
        public List<Answer> GetAllAnswer()
        {
            return answerCtrl.GetAllAnswer();
        }


        /// <summary>
        /// Calls the method IsCorrect from the answer controller
        /// </summary>
        /// <param name="Id">the answer id</param>
        /// <returns>returns the method that checks if answer is correct or not  
        /// from answer controller</returns>
        public bool IsCorrect(int id)
        {
            return answerCtrl.IsCorrect(id);
        }

        public AnswerQuestionDTO AnswerQuestion(int id, int lobbyId, int userId, int categoryId)
        {
            return answerCtrl.AnswerQuestion(id, lobbyId,userId, categoryId);
        }
    }
}
