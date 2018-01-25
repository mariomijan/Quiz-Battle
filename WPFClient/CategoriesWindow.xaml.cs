using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.CategoryReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        private CategoryServiceClient categoryClient = new CategoryServiceClient();
        private IList<Category> categoryList;

        public CategoriesWindow()
        {
            InitializeComponent();
            LoadCategoryData();
        }

        private void createCategory_Click(object sender, RoutedEventArgs e)
        {
            CreateCategory();
        }

        private void editCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            EditCategory();
        }

        private void removeCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveCategory();
        }

        private void findCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            FindCategory();
        }

        private void LoadCategoryData()
        {
            categoryList = new ObservableCollection<Category>(categoryClient.GetAllCategories());
            try
            {
                categoryDatagrid.ItemsSource = categoryList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void CreateCategory()
        {
            CreateCategoryWindow createCategoryWindow = new CreateCategoryWindow();
            createCategoryWindow.ShowDialog();
            RefreshDataGrid();
        }

        /// <summary>
        /// Refreshes the datagrid with data from DB
        /// </summary>
        public void RefreshDataGrid()
        {
            categoryList.Clear();
            categoryList = new ObservableCollection<Category>(categoryClient.GetAllCategories());
            categoryDatagrid.ItemsSource = null;
            categoryDatagrid.ItemsSource = categoryList;
        }

        /// <summary>
        /// Deletes a category on delete keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categoryDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                try
                {
                    Category category = (Category)categoryDatagrid.SelectedItem;
                    var selectedRow = categoryDatagrid.SelectedIndex;
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete: {category.name}?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        categoryList.RemoveAt(selectedRow);
                        categoryClient.DeleteCategory(category.id);
                    }
                    else
                    {
                        RefreshDataGrid();
                    }
                }
                catch (InvalidCastException invalidCast)
                {
                    MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes a category when clicking the remove button
        /// </summary>
        private void RemoveCategory()
        {
            // Hvis man sletter tom row får man cast exception
            try
            {
                Category category = (Category)categoryDatagrid.SelectedItem;
                var selectedRow = categoryDatagrid.SelectedIndex;
                if (category != null)
                {
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete {category.name} ?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        categoryList.RemoveAt(selectedRow);
                        categoryClient.DeleteCategory(category.id);
                    }
                }
            }
            catch (InvalidCastException invalidCast)
            {
                MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void FindCategory()
        {
            // CategoryReference.Category category = new CategoryReference.Category();
            ObservableCollection<Category> categorylist = (ObservableCollection<Category>)categoryDatagrid.ItemsSource;
            ObservableCollection<Category> foundCategorys = new ObservableCollection<Category>();
            foreach (var category in categorylist)
            {
                if (category.name.ToLower().Contains(searchTextBox.Text))
                {
                    foundCategorys.Add(category);
                }
            }
            if (foundCategorys.Count > 0)
            {
                categoryDatagrid.ItemsSource = foundCategorys;
            }

        }

        private void EditCategory()
        {
            Category category = (Category)categoryDatagrid.SelectedItem;
            var selectedRow = categoryDatagrid.SelectedIndex;
            if (category != null)
            {
                try
                {
                    categoryList.ElementAt(selectedRow);
                    categoryClient.UpdateCategory(category);
                    categoryDatagrid.Items.Refresh();
                    MessageBox.Show("Information updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            Close();
            home.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logwin = new LoginWindow();
            Close();
            logwin.Show();
        }

    }
}
