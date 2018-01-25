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
using WPF.AnswerReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for AnswersWindow.xaml
    /// </summary>
    public partial class AnswersWindow : Window
    {
        private AnswerServiceClient answerClient = new AnswerServiceClient();
        private IList<Answer> answerList;

        public AnswersWindow()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            answerList = new ObservableCollection<Answer>(answerClient.GetAllAnswer());
            try
            {
                answerDatagrid.ItemsSource = answerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;

            }
        }

        public void RefreshDataGrid()
        {
            answerList.Clear();
            answerList = new ObservableCollection<Answer>(answerClient.GetAllAnswer());
            answerDatagrid.ItemsSource = null;
            answerDatagrid.ItemsSource = answerList;
        }


        private void EditAnswer()
        {
            Answer answer = (Answer)answerDatagrid.SelectedItem;
            var selectedRow = answerDatagrid.SelectedIndex;
            if (answer != null)
            {
                try
                {
                    answerList.ElementAt(selectedRow);
                    answerClient.UpdateAnswer(answer);
                    answerDatagrid.Items.Refresh();
                    MessageBox.Show("Information updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void editAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            EditAnswer();
        }

        private void removeAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveAnswer();
        }

        private void findAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            FindAnswer();
        }

        private void RemoveAnswer()
        {
            // Hvis man sletter tom row får man cast exception
            try
            {
                Answer answer = (Answer)answerDatagrid.SelectedItem;
                var selectedRow = answerDatagrid.SelectedIndex;
                if (answer != null)
                {
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete {answer.description} ?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        answerList.RemoveAt(selectedRow);
                        answerClient.RemoveAnswer(answer.id);
                    }
                }
            }
            catch (InvalidCastException invalidCast)
            {
                MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void FindAnswer()
        {
            ObservableCollection<Answer> answerList = (ObservableCollection<Answer>)answerDatagrid.ItemsSource;
            ObservableCollection<Answer> foundAnswers = new ObservableCollection<Answer>();

            foreach (var ans in answerList)
            {
                if (ans.description.ToLower().Contains(searchTextBox.Text))
                {
                    foundAnswers.Add(ans);
                }
            }
            if (foundAnswers.Count > 0)
            {
                answerDatagrid.ItemsSource = foundAnswers;
            }

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void createAnswer_Click(object sender, RoutedEventArgs e)
        {
            CreateAnswer();
        }

        private void CreateAnswer()
        {
            CreateAnswerWindow createAnswer = new CreateAnswerWindow();
            createAnswer.ShowDialog();
            RefreshDataGrid();
        }

        private void answerDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                try
                {
                    Answer answer = (Answer)answerDatagrid.SelectedItem;
                    var selectedRow = answerDatagrid.SelectedIndex;
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete: {answer.description}?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        answerList.RemoveAt(selectedRow);
                        answerClient.RemoveAnswer(answer.id);
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
