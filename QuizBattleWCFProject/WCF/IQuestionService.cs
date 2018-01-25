using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IQuestionService
    {
        [OperationContract]
        void AddQuestion(Question question);
        [OperationContract]
        void RemoveQuestion(int id);
        [OperationContract]
        void UpdateQuestion(Question question);
        [OperationContract]
        Question GetQuestion(int id);
        [OperationContract]
        List<Question> GetAllQuestions();

    }
}
