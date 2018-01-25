using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class QuestionTests
    {
        /// <summary>
        /// Checks if the Question is added to the list by checking the id
        /// </summary>
        [TestMethod]
        public void AddQuestion()
        {
            List<Question> questionList = new List<Question>();
            Question question = new Question() { id = 1 };
            questionList.Add(question);
            Assert.AreEqual(question.id, 1);
        }

        /// <summary>
        /// Gets the Question by its id
        /// </summary>
        [TestMethod]
        public void GetQuestionTest()
        {
            List<Question> questionList = new List<Question>();
            int id = 1;
            Question question = new Question() { id = 1 };
            questionList.Add(question);
            Assert.AreEqual(question.id, id);
        }

        /// <summary>
        /// Tests if all the elements in the list exist, by checking their ID
        /// </summary>
        [TestMethod]
        public void GetAllQuestionsTest()
        {
            List<Question> questionList = new List<Question>();
            Question question = new Question { id = 1 };
            Question question1 = new Question { id = 2 };
            questionList.Add(question);
            questionList.Add(question1);
            Assert.AreEqual(1, question.id);
            Assert.AreEqual(2, question1.id);
        }

        [TestMethod]
        public void RemoveQuestionTest()
        {
            List<Question> questionList = new List<Question>();
            Question question = new Question() { id = 0, description = "Question" };
            questionList.Add(question);
            bool isRemoved = questionList.Remove(question);
            Assert.AreEqual(isRemoved, true);
        }

        [TestMethod]
        public void UpdateQuestionTest()
        {
            Question question = new Question() { id = 0, description = "hello" };
            Assert.AreEqual(question.id, 0);
            question.id = 2;
            Assert.AreEqual(question.id, 2);
        }
        
    }
}
