using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class CategoryTests
    {
        /// <summary>
        /// Checks if the Category is added to the list by checking the id
        /// </summary>
        [TestMethod]
        public void AddCategory()
        {
            List<Category> categoryList = new List<Category>();
            Category category = new Category() { id = 1 };
            categoryList.Add(category);
            Assert.AreEqual(category.id, 1);
        }

        /// <summary>
        /// Gets the Category by its id
        /// </summary>
        [TestMethod]
        public void GetCategoryTest()
        {
            List<Category> categoryList = new List<Category>();
            int id = 1;
            Category category = new Category() { id = 1 };
            categoryList.Add(category);
            Assert.AreEqual(category.id, id);
        }

        /// <summary>
        /// Tests if all the elements in the list exist, by checking their ID
        /// </summary>
        [TestMethod]
        public void GetAllCategoryTest()
        {
            List<Category> categoryList = new List<Category>();
            Category category = new Category { id = 1 };
            Category question1 = new Category { id = 2 };
            categoryList.Add(category);
            categoryList.Add(question1);
            Assert.AreEqual(1, category.id);
            Assert.AreEqual(2, question1.id);
        }

        [TestMethod]
        public void RemoveCategoryTest()
        {
            List<Category> categoryList = new List<Category>();
            Category category = new Category() { id = 0, name = "Category" };
            categoryList.Add(category);
            bool isRemoved = categoryList.Remove(category);
            Assert.AreEqual(isRemoved, true);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            Category category = new Category() { id = 0, name = "hello" };
            Assert.AreEqual(category.id, 0);
            category.id = 2;
            Assert.AreEqual(category.id, 2);
        }
    }
}
