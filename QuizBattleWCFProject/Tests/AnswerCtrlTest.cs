using Controllers.ControllerTest;
using DataAccess;
using DataAccess.DBTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests
{
    [TestClass]
    public class AnswerCtrlTest
    {
        /// <summary>
        /// Checks if the answer gets updated from a mocked object
        /// </summary>
        [TestMethod]
        public void UpdateAnswerFromDb()
        {
            Answer answer = new Answer();
            answer.id = 50;

            Mock<IDbAccess> dbAccess = new Mock<IDbAccess>();
            dbAccess.Setup(x => x.UpdateAnswer(answer)).Returns(true);
            var testCtrl = new TestController(dbAccess.Object);
            testCtrl.UpdateAnswer(answer);
            Assert.AreEqual(testCtrl.UpdateAnswer(answer), true);
        }

        /// <summary>
        /// Checks if the answer gets added to the list by using a mocked up object of the database
        /// </summary>
        [TestMethod]
        public void AddAnswerFromDb()
        {
            var answerList = new List<Answer>();
            answerList.Add(new Answer
            {
                id = 1337,
                description = "TestAnswer",
                isCorrect = true
            });
            Mock<IDbAccess> dbAccess = new Mock<IDbAccess>();
            dbAccess.Setup(x => x.AddAnswer()).Returns(1337);
            var testCtrl = new TestController(dbAccess.Object);
            Assert.AreEqual(testCtrl.AddAnswer(), 1337);
        }

        [TestMethod]
        public void GetAnswerFromDb()
        {
            Mock<IDbAccess> dbAccess = new Mock<IDbAccess>();
            dbAccess.Setup(x => x.GetAllAnswers()).Returns(true);
            var testCtrl = new TestController(dbAccess.Object);
            testCtrl.GetAllAnswers();
            Assert.AreEqual(testCtrl.GetAllAnswers(), true);
        }

    }
}
