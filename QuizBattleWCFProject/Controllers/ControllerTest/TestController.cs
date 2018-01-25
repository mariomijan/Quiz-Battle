using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.ControllerTest
{
    public class TestController
    {
        private IDbAccess dbAccess;

        public TestController(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        public int AddAnswer()
        {
            return dbAccess.AddAnswer();

        }

        public bool UpdateAnswer(Answer answer)
        {
            var updateAnswer = dbAccess.UpdateAnswer(answer);

            if (updateAnswer)
            {
                return true;
            }
            return false;
        }

        //public void RemoveAnswer(int id)
        //{
        //    dbAnswer.RemoveAnswer(id);
        //}

        public bool GetAllAnswers()
        {
            var AllAnswers = dbAccess.GetAllAnswers();
            if (AllAnswers)
            {
                return true;
            }
            return false;

        }

        //public Answer GetAnswer(int id)
        //{
        //    return dbAnswer.GetAnswer(id);
        //}

        //public bool IsCorrect(int id)
        //{
        //    if (GetAnswer(id).isCorrect == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}
