using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControlLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DatabaseaccesLayer;

namespace ControlLayer.Tests
{
    [TestClass]
    public class AnswerTests
    {
        /// <summary>
        /// Checks if the answer is added to the list by checking the id
        /// </summary>
        [TestMethod]
        public void AddAnswer()
        {
            List<Answer> answerList = new List<Answer>();
            Answer answer = new Answer() { id = 1 };
            answerList.Add(answer);
            Assert.AreEqual(answer.id, 1);
        }

        /// <summary>
        /// Gets the answer by its id
        /// </summary>
        [TestMethod]
        public void GetAnswerTest()
        {
            List<Answer> answerList = new List<Answer>();
            int id = 1;
            Answer answer = new Answer() { id = 1 };
            answerList.Add(answer);
            Assert.AreEqual(answer.id, id);
        }

        /// <summary>
        /// Tests if all the elements in the list exist, by checking their ID
        /// </summary>
        [TestMethod]
        public void GetAllAnswersTest()
        {
            List<Answer> answerList = new List<Answer>();
            Answer answer = new Answer { id = 1 };
            Answer answer1 = new Answer { id = 2 };
            answerList.Add(answer);
            answerList.Add(answer1);
            Assert.AreEqual(1, answer.id);
            Assert.AreEqual(2, answer1.id);
        }

        [TestMethod]
        public void RemoveAnswerTest()
        {
            List<Answer> answerList = new List<Answer>();
            Answer answer = new Answer() { id = 0, description = "answer" };
            answerList.Add(answer);
            bool isRemoved = answerList.Remove(answer);
            Assert.AreEqual(isRemoved, true);
        }

        [TestMethod]
        public void UpdateAnswerTest()
        {
            Answer answer = new Answer() { id = 0, description = "hello" };
            Assert.AreEqual(answer.id, 0);
            answer.id = 2;
            Assert.AreEqual(answer.id, 2);
        }


        /// <summary>
        /// Checks if the isCorrect value returns true
        /// </summary>
        [TestMethod]
        public void IsCorrectTrueTest()
        {
            bool isCorrect = true;
            Answer answer = new Answer() { isCorrect = true };
            Assert.AreEqual(isCorrect, answer.isCorrect);
        }

        /// <summary>
        /// Checks if the isCorrect value is false
        /// </summary>
        [TestMethod]
        public void IsCorrectFalseTest()
        {
            bool isCorrect = false;
            Answer answer = new Answer() { isCorrect = false };
            Assert.AreEqual(isCorrect, answer.isCorrect);
        }

        /// <summary>
        /// Checks if the id matches the expected id
        /// </summary>
        [TestMethod]
        public void IsCorrectIDTest()
        {
            int id = 1;
            Answer answer = new Answer() { id = 1 };
            Assert.AreEqual(id, answer.id);
        }

        /// <summary>
        /// Tests if the id and isCorrect value matches the input
        /// </summary>
        [TestMethod]
        public void IsCorrectFullTest()
        {
            int id = 1;
            bool isCorrect = true;
            Answer answer = new Answer() { id = 1, isCorrect = true };
            Assert.AreEqual(id, answer.id);
            Assert.AreEqual(isCorrect, answer.isCorrect);
        }
    }
}