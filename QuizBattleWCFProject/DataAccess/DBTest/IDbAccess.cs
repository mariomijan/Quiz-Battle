using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDbAccess
    {
        int AddAnswer();
        bool UpdateAnswer(Answer answer);
        bool RemoveAnswer(int id);
        int GetAnswer(int id);
        bool GetAllAnswers();
        bool IsCorrect(int id);
    }
}
