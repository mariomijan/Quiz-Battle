using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using DatabaseaccesLayer;
using ControlLayer;

namespace Controllers
{
    public class CategoryCtrl
    {
        //private variable of type DBCategory
        private DBCategory dbCat;
        private QuestionCtrl qCtrl;

        public CategoryCtrl()
        {
            dbCat = new DBCategory();
            qCtrl = new QuestionCtrl();
        }

        /// <summary>
        /// Calls the method AddCategory from DBCategory
        /// </summary>
        /// <param name="category">category object</param>
        public void AddCategory(Category category)
        {
            dbCat.AddCategory(category);

        }
        
        /// <summary>
        /// Calls the method DeleteCategory from DBCategory
        /// </summary>
        /// <param name="id">id to delete the category</param>
        public void DeleteCategory(int id)
        {
            dbCat.DeleteCategory(id);
        }

        /// <summary>
        /// Calls the method GetAllCategories from DBCategory
        /// </summary>
        /// <returns>returns the method with the list of categories 
        /// from DBCategory</returns>
        public List<Category> GetAllCategories()
        {
            return dbCat.GetAllCategories();
        }

        /// <summary>
        /// Calls the method GetCategory from DBCategory
        /// </summary>
        /// <param name="id">id to get the category</param>
        /// <returns>returns the method with a category 
        /// from DBcategory</returns>
        public Category GetCategory(int id)
        {
            return dbCat.GetCategory(id);
        }

        /// <summary>
        /// Calls the method UpdateCategory from DBCategory
        /// </summary>
        /// <param name="category">category object</param>
        public void UpdateCategory(Category category)
        {
            dbCat.UpdateCategory(category);
        }

        public List<Category> GetCategoriesQuestionsAnswers()
        {
            return dbCat.GetCategoriesQuestionsAnswers();
        }

        public void AddCategoryWithQuestionsAndAnswers(Category category, Question question, Answer answer)
        {
            dbCat.AddCategoryWithQuestionsAndAnswers(category, question, answer);
        }
    }
}
