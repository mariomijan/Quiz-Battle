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
using WPF.QuestionReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for QuestionsWindow.xaml
    /// </summary>
    public partial class QuestionsWindow : Window
    {
        private QuestionServiceClient questionClient = new QuestionServiceClient();
        private IList<Question> questionList;

        public QuestionsWindow()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            questionList = new ObservableCollection<Question>(questionClient.GetAllQuestions());
            try
            {
                questionDatagrid.ItemsSource = questionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;

            }
        }

        public void RefreshDataGrid()
        {
            questionList.Clear();
            questionList = new ObservableCollection<Question>(questionClient.GetAllQuestions());
            questionDatagrid.ItemsSource = null;
            questionDatagrid.ItemsSource = questionList;
        }


        private void EditQuestion()
        {
            Question question = (Question)questionDatagrid.SelectedItem;
            var selectedRow = questionDatagrid.SelectedIndex;
            if (question != null)
            {
                try
                {
                    questionList.ElementAt(selectedRow);
                    questionClient.UpdateQuestion(question);
                    questionDatagrid.Items.Refresh();
                    MessageBox.Show("Information updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void createQuestion_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestion();
        }

        private void editQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            EditQuestion();
        }

        private void removeQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveQuestion();
        }

        private void findQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            FindQuestion();
        }

        private void RemoveQuestion()
        {
            // Hvis man sletter tom row får man cast exception
            try
            {
                Question question = (Question)questionDatagrid.SelectedItem;
                var selectedRow = questionDatagrid.SelectedIndex;
                if (question != null)
                {
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete {question.description} ?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        questionList.RemoveAt(selectedRow);
                        questionClient.RemoveQuestion(question.id);
                    }
                }
            }
            catch (InvalidCastException invalidCast)
            {
                MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void FindQuestion()
        {
            ObservableCollection<Question> questionList = (ObservableCollection<Question>)questionDatagrid.ItemsSource;
            ObservableCollection<Question> foundQuestions = new ObservableCollection<Question>();

            foreach (var ans in questionList)
            {
                if (ans.description.ToLower().Contains(searchTextBox.Text))
                {
                    foundQuestions.Add(ans);
                }
            }
            if (foundQuestions.Count > 0)
            {
                questionDatagrid.ItemsSource = foundQuestions;
            }

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void CreateQuestion()
        {
            CreateQuestionWindow createQuestion = new CreateQuestionWindow();
            createQuestion.ShowDialog();
            RefreshDataGrid();
        }

        private void questionDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                try
                {
                    Question question = (Question)questionDatagrid.SelectedItem;
                    var selectedRow = questionDatagrid.SelectedIndex;
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete: {question.description}?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        questionList.RemoveAt(selectedRow);
                        questionClient.RemoveQuestion(question.id);
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
