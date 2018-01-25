using System;
using System.Collections.Generic;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void manageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow users = new UsersWindow();
            users.Show();
            Close();
        }

        private void manageCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow categoriesWindow = new CategoriesWindow();
            categoriesWindow.Show();
            Close();
        }

        private void manageQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionsWindow questionsWindow= new QuestionsWindow();
            questionsWindow.Show();
            Close();
        }

        private void manageAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            AnswersWindow answersWindow = new AnswersWindow();
            answersWindow.Show();
            Close();
        }

        private void manageQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.Show();
            Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logWin = new LoginWindow();
            Close();
            logWin.Show();
        }
    }
}
