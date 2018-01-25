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
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private CategoryServiceClient quizClient = new CategoryServiceClient();
        private IList<Category> quizList;

        public QuizWindow()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            quizList = new ObservableCollection<Category>(quizClient.GetCategoriesQuestionsAnswers());
            try
            {
                quizDatagrid.ItemsSource = quizList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;

            }
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

        public void RefreshDataGrid()
        {
            quizList.Clear();
            quizList = new ObservableCollection<Category>(quizClient.GetCategoriesQuestionsAnswers());
            quizDatagrid.ItemsSource = null;
            quizDatagrid.ItemsSource = quizList;
        }


        //private void EditCategory()
        //{
        //    Category category = (Category)quizDatagrid.SelectedItem;
        //    var selectedRow = quizDatagrid.SelectedIndex;
        //    if (category != null)
        //    {
        //        try
        //        {
        //            quizList.ElementAt(selectedRow);
        //            quiz.categoryClient.UpdateCategory(category);
        //            quizDatagrid.Items.Refresh();
        //            MessageBox.Show("Information updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //    }
        //}

        private void createQuizButton_Click(object sender, RoutedEventArgs e)
        {
            CreateQuiz();
        }

        private void CreateQuiz()
        {
            CreateQuizWindow createQuizWindow = new CreateQuizWindow();
            createQuizWindow.ShowDialog();
            RefreshDataGrid();
        }

        //private void editQuizButton_Click(object sender, RoutedEventArgs e)
        //{
        //    EditCategory();
        //}

        //private void removeQuizButton_Click(object sender, RoutedEventArgs e)
        //{
        //    RemoveCategory();
        //}

        private void findQuizButton_Click(object sender, RoutedEventArgs e)
        {
            FindCategory();
        }

        //private void RemoveCategory()
        //{
        //    // Hvis man sletter tom row får man cast exception
        //    try
        //    {
        //        Category category = (Category)quizDatagrid.SelectedItem;
        //        var selectedRow = quizDatagrid.SelectedIndex;
        //        if (category != null)
        //        {
        //            MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete {category} ?", "Are you sure?", MessageBoxButton.YesNo);
        //            if (messageboxResult == MessageBoxResult.Yes)
        //            {
        //                quizList.RemoveAt(selectedRow);
        //                quiz.categoryClient.DeleteCategory(category.id);
        //            }
        //        }
        //    }
        //    catch (InvalidCastException invalidCast)
        //    {
        //        MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        throw;
        //    }
        //}

        private void FindCategory()
        {
            ObservableCollection<Category> quizList = (ObservableCollection<Category>)quizDatagrid.ItemsSource;
            ObservableCollection<Category> foundCategorys = new ObservableCollection<Category>();

            foreach (var ans in quizList)
            {
                if (ans.name.ToLower().Contains(searchTextBox.Text))
                {
                    foundCategorys.Add(ans);
                }
            }
            if (foundCategorys.Count > 0)
            {
                quizDatagrid.ItemsSource = foundCategorys;
            }

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }



        private void CreateCategory()
        {
            CreateCategoryWindow createCategory = new CreateCategoryWindow();
            createCategory.ShowDialog();
            RefreshDataGrid();
        }

        //private void quizDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Delete)
        //    {
        //        try
        //        {
        //            Category category = (Category)quizDatagrid.SelectedItem;
        //            var selectedRow = quizDatagrid.SelectedIndex;
        //            MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete: {category}?", "Are you sure?", MessageBoxButton.YesNo);
        //            if (messageboxResult == MessageBoxResult.Yes)
        //            {
        //                quizList.RemoveAt(selectedRow);
        //                quiz.categoryClient.DeleteCategory(category.id);
        //            }
        //            else
        //            {
        //                RefreshDataGrid();
        //            }
        //        }
        //        catch (InvalidCastException invalidCast)
        //        {
        //            MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            throw;
        //        }

        //    }
        //}

    }
}
