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
using WPF.CategoryReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CreateQuizWindow.xaml
    /// </summary>
    public partial class CreateQuizWindow : Window
    {
        private CategoryServiceClient quizClient = new CategoryServiceClient();

        public CreateQuizWindow()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            CreateQuiz();
        }

        public void CreateQuiz()
        {
            CategoryReference.Category category = new CategoryReference.Category();
            CategoryReference.Question question = new CategoryReference.Question();
            CategoryReference.Answer answer = new CategoryReference.Answer();

            category.name = txtCategoryName.Text;
            question.description = txtQuestionDescription.Text;
            answer.description = txtAnswerDescription.Text;
            answer.isCorrect = Convert.ToBoolean(txtIsAnswerCorrect.Text);
            answer.pointAmount = Convert.ToInt32(txtAnswerPointAmount.Text);

            quizClient.AddCategoryWithQuestionsAndAnswers(category, question, answer);
            ClearTextboxes(this);
            MessageBox.Show("The quiz has been created !");

        }
        public static void ClearTextboxes(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox)
                {
                    TextBox tb = (TextBox)visualChild;
                    tb.Clear();
                }
                ClearTextboxes(visualChild);
            }
        }
    }
}
