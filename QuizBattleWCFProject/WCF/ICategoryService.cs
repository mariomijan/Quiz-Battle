using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    [ServiceContract]
    public interface ICategoryService
    {
        [OperationContract]
        void AddCategory(Category category);
        [OperationContract]
        void UpdateCategory(Category category);
        [OperationContract]
        void DeleteCategory(int id);
        [OperationContract]
        Category GetCategory(int id);
        [OperationContract]
        List<Category> GetAllCategories();
        [OperationContract]
        List<Category> GetCategoriesQuestionsAnswers();
        [OperationContract]
        void AddCategoryWithQuestionsAndAnswers(Category category, Question question, Answer answer);

    }
}

