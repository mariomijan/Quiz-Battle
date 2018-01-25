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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class QuestionService : IQuestionService
    {
        private QuestionCtrl questionController;

        public QuestionService()
        {
            questionController = new QuestionCtrl();
        }


        /// <summary>
        /// Calls the method GetAllQuestions from the question controller
        /// </summary>
        /// <returns>returns the method with a list of questions 
        /// from question controller</returns>
        public List<Question> GetAllQuestions()
        {
            return questionController.GetAllQuestions();
        }

        /// <summary>
        /// Calls the method GetQuestion from the question controller
        /// </summary>
        /// <param name="Id">the question id</param>
        /// <returns>returns the method with a question 
        /// from question controller</returns>
        public Question GetQuestion(int id)
        {
            return questionController.GetQuestion(id);
        }
        

        /// <summary>
        /// Calls the method AddQuestion from the question controller
        /// </summary>
        /// <param name="question">the question object</param>
        public void AddQuestion(Question question)
        {
            questionController.AddQuestion(question);
        }

        /// <summary>
        /// Calls the method RemoveQuestion from the question controller
        /// </summary>
        /// <param name="Id">the question id</param>
        public void RemoveQuestion(int id)
        {
            questionController.RemoveQuestion(id);
        }

        /// <summary>
        /// Calls the method UpdateQuestion from the question controller
        /// </summary>
        /// <param name="question">the question object</param>
        public void UpdateQuestion(Question question)
        {
            questionController.UpdateQuestion(question);
        }
    }
}
