using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Controllers;

namespace WCF
{
    public class CategoryService : ICategoryService
    {

        //private variable of type CategoryCtrl
        private CategoryCtrl cCtrl;

        public CategoryService()
        {
            cCtrl = new CategoryCtrl();
        }

        /// <summary>
        /// Calls the method AddCategory from the category controller
        /// </summary>
        /// <param name="category">category object</param>
        public void AddCategory(Category category)
        {
            cCtrl.AddCategory(category);

        }

        /// <summary>
        /// Calls the method DeleteCategory from the category controller
        /// </summary>
        /// <param name="id">id to delete the category</param>
        public void DeleteCategory(int id)
        {
            cCtrl.DeleteCategory(id);

        }

        /// <summary>
        /// Calls the method GetAllCategories from the category controller
        /// </summary>
        /// <returns>returns the method with the list of categories 
        /// from category controller</returns>
        public List<Category> GetAllCategories()
        {

            return cCtrl.GetAllCategories();
        }

        /// <summary>
        /// Calls the method GetCategory from the category controller
        /// </summary>
        /// <param name="id">id to get the category</param>
        /// <returns>returns the method with a category 
        /// from category controller</returns>
        public Category GetCategory(int id)
        {
            return cCtrl.GetCategory(id);

        }

        /// <summary>
        /// Calls the method UpdateCategory from the category controller
        /// </summary>
        /// <param name="category">category object</param>
        public void UpdateCategory(Category category)
        {
            cCtrl.UpdateCategory(category);
        }

        public List<Category> GetCategoriesQuestionsAnswers()
        {
            return cCtrl.GetCategoriesQuestionsAnswers();
        }

        public void AddCategoryWithQuestionsAndAnswers(Category category, Question question, Answer answer)
        {
            cCtrl.AddCategoryWithQuestionsAndAnswers(category, question, answer);
        }
    }
}
