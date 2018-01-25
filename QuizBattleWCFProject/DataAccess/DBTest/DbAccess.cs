using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.DBTest
{
    public class DbAccess : IDbAccess
    {
        public int AddAnswer()
        {
            Answer answer = new Answer() { id = 100, description = "Omg", isCorrect = true };
            var answerList = new List<Answer>();
            answerList.Add(answer);
            return answer.id;
        }

        public bool GetAllAnswers()
        {

            var listOfAnswers = new List<Answer>();
            listOfAnswers.Add(
                new Answer
                {
                    id = 200,
                    description = "Hello from DB",
                    isCorrect = true
                });
            listOfAnswers.Add(
                 new Answer
                 {
                     id = 400,
                     description = "hai",
                     isCorrect = false
                 });
            listOfAnswers.Add(
                 new Answer
                 {
                     id = 800,
                     description = "Hello",
                     isCorrect = false
                 });

            if (listOfAnswers.Count == 3)
            {
                return true;
            }
            return false;
        }

        public int GetAnswer(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsCorrect(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAnswer(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAnswer(Answer answer)
        {
            answer.id = 60;
            if (answer.id == 60)
            {
                return true;
            }
            return false; 
        }
    }
}
