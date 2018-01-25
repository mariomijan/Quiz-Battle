using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    [ServiceContract]
    public interface IQuizService
    {
        //Category
        [OperationContract]
        List<Category> GetAllCategories();

        //Question
        [OperationContract]
        List<Question> GetAllQuestionsAndAnswersByCategoryId(int categoryId);
        [OperationContract]
        Question GetQuestion(int id);
        
        //Answer
        [OperationContract]
        Answer GetAnswer(int id);
        [OperationContract]
        bool IsCorrect(int id);

        [OperationContract]
        void ClearTableAfterFinish(int lobbyId);

        //[OperationContract]
        //bool HasQuestionBeenAnswered(int questionId, int lobbyId);

        ////Lobby
        //[OperationContract]
        //void CreateLobby(Lobby lobby);
        //[OperationContract]
        //List<Lobby> GetAllLobbies();
        //[OperationContract]
        //Lobby GetLobby(int id);
    }
}
