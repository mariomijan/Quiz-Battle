using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    [ServiceContract]
    public interface IAnswerService
    {
        [OperationContract]
        void AddAnswer(Answer answer);
        [OperationContract]
        void UpdateAnswer(Answer answer);
        [OperationContract]
        void RemoveAnswer(int id);
        [OperationContract]
        Answer GetAnswer(int id);
        [OperationContract]
        AnswerQuestionDTO AnswerQuestion(int id, int lobbyId, int userId, int categoryId);
        [OperationContract]
        List<Answer> GetAllAnswer();
        [OperationContract]
        bool IsCorrect(int id);
        
    }
}
